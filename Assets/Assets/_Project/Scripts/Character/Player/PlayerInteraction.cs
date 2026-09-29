using District.Core.InteractionSystem;
using District.UI;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting.Antlr3.Runtime.Collections;

namespace District.Character.Player
{
    public class PlayerInteraction
    {
        [Header("Referencial Scripts")]
        private readonly PlayerContext m_ctx;
        private readonly UIController m_uiController;

        [Header("Interaction Settings")]
        private readonly Transform m_interactionPivot;
        private readonly Transform m_cameraTransform;

        private IInteractable m_currentInteractable;
        private List<IInteractable> m_previousNearbyInteractables = new List<IInteractable>();
        private List<IInteractable> m_currentNearbyInteractables = new List<IInteractable>();

        public event Action<List<IInteractable>> OnNearbyInteractablesChanged;
        public event Action<IInteractable> OnCurrentInteractableChanged;

        public PlayerInteraction(PlayerContext ctx, Transform interactionPivot, Transform cameraTransform)
        {
            this.m_ctx = ctx;
            this.m_interactionPivot = interactionPivot;
            this.m_cameraTransform = cameraTransform;
        }

        public void Tick(float deltaTime)
        {
            CheckNearbyInteractables();

            if (m_ctx.Intent.Interact.Pressed)
            {
                Interact();
            }
        }

        #region Nearby Interactables
        private void CheckNearbyInteractables()
        {
            Collider[] result = Physics.OverlapSphere(
                m_interactionPivot.position,
                m_ctx.Stats.Interaction.InteractionRadius * 2,
                m_ctx.Stats.Interaction.InteractableLayer);

            m_currentNearbyInteractables.Clear();

            foreach (Collider c in result)
            {
                IInteractable interactable = c.GetComponentInParent<IInteractable>();

                if (interactable == null)
                    continue;

                if(m_currentNearbyInteractables.Contains(interactable))
                    continue;

                m_currentNearbyInteractables.Add(interactable);
            }

            m_currentNearbyInteractables.Sort(CompareInteractableDistances);

            if(HasListChanged(m_currentNearbyInteractables, m_previousNearbyInteractables))
            {
                m_previousNearbyInteractables.Clear();
                m_previousNearbyInteractables.AddRange(m_currentNearbyInteractables);            
                OnNearbyInteractablesChanged?.Invoke(m_previousNearbyInteractables);
            }

            if (m_previousNearbyInteractables.Count == 0)
            {
                UpdateCurrentInteractable(null);
            }
            else
            {
                if (CheckFocusDistance(m_previousNearbyInteractables[0]))
                {
                    UpdateCurrentInteractable(m_previousNearbyInteractables[0]);
                }
                else
                {
                    UpdateCurrentInteractable(null);
                }
            }
        }

        private int CompareInteractableDistances(IInteractable a, IInteractable b)
        {
            Vector3 reference = m_interactionPivot.position + (m_cameraTransform.forward * m_ctx.Stats.Interaction.InteractionOffset);
            float distanceA = (reference - a.GetCollider().ClosestPoint(reference)).sqrMagnitude;   
            float distanceB = (reference - b.GetCollider().ClosestPoint(reference)).sqrMagnitude;
            return distanceA.CompareTo(distanceB);
        }

        private bool HasListChanged(List<IInteractable> listA, List<IInteractable> listB)
        {
            if (listA.Count != listB.Count)
                return true;

            for (int i = 0; i < listA.Count; i++)
            {
                if (listA[i] != listB[i]) return true;
            }

            return false;
        }
        #endregion

        private void Interact()
        {
            if(m_currentInteractable == null)
                return;

            m_currentInteractable?.Interact();
        }

        private bool CheckFocusDistance(IInteractable interactable)
        {
            Vector3 reference = m_interactionPivot.position + (m_cameraTransform.forward * m_ctx.Stats.Interaction.InteractionOffset);
            float distance = (reference - interactable.GetCollider().ClosestPoint(reference)).sqrMagnitude;


            if (distance <= m_ctx.Stats.Interaction.InteractionRadius / 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void UpdateCurrentInteractable(IInteractable newInteractable)
        {
            if (newInteractable == m_currentInteractable)
                return;

            if (!IsObjectNullOrDestroyed(m_currentInteractable)) m_currentInteractable.NotReady();

            m_currentInteractable = newInteractable;

            if (newInteractable != null) m_currentInteractable.Ready();

            OnCurrentInteractableChanged.Invoke(m_currentInteractable);
        }

        private bool IsObjectNullOrDestroyed<T>(T obj)   
        {
            if (obj != null)
            {
                if(obj.Equals(null))
                {
                    return true;
                }

                return false;
            }

            return true;
        }

        #region Gizmos
        public void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(
                m_interactionPivot.position +
                (m_cameraTransform.forward * m_ctx.Stats.Interaction.InteractionOffset),
                m_ctx.Stats.Interaction.InteractionRadius);

            Gizmos.color = new Color(1f, 0f, 0f, 0.1f);
            Gizmos.DrawSphere(
                m_interactionPivot.position,
                m_ctx.Stats.Interaction.InteractionRadius * 2);
        }
        #endregion
    }
}