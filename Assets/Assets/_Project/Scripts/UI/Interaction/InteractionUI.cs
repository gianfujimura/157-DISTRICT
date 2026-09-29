using District.Character.Player;
using District.Core.InteractionSystem;
using PlasticGui.WorkspaceWindow.Diff.Type;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;

namespace District.UI.Interaction
{
    public class InteractionUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Camera m_camera;
        [SerializeField] private PlayerInteraction m_interaction;

        [Header("Icons")]
        [SerializeField] private List<InteractionIcon> m_interactionIcons = new List<InteractionIcon>();

        [Header("Sprites")]
        [SerializeField] private Sprite m_interactionSprite;
        [SerializeField] private Sprite m_interactionFocusSprite;

        [Header("Settings")]
        [SerializeField] private int m_maxNearbyInteractables;
        [SerializeField] private InteractionIcon InteractionIconPrefab;

        [Header("Runtime")]
        private IInteractable m_currentInteractable;

        #region Lifecycle Methods
        public void Initialize()
        {
            SpawnInteractionIcons();
        }

        public void Tick(float deltaTime)
        {
            foreach (var icon in m_interactionIcons)
            {
                if (icon == null) 
                    continue;

                icon.Tick();
            }
        }
        #endregion

        #region Icon Management
        private void SpawnInteractionIcons()
        {
            for (int i = 0; i < m_maxNearbyInteractables; i++)
            {
                InteractionIcon icon = Instantiate(InteractionIconPrefab, this.transform, true);
                icon.SetIcon(m_interactionSprite);
                icon.SetCamera(m_camera);
                icon.Initialize();
                m_interactionIcons.Add(icon);
            }
        }
        #endregion

        public void SetPlayerContext(PlayerContext context)
        {
            SpawnInteractionIcons();
        }

        public void SetPlayerInteraction(PlayerInteraction playerInteraction)
        {
            m_interaction = playerInteraction;

            m_interaction.OnNearbyInteractablesChanged += HandleNearbyIntectablesChanged;
            m_interaction.OnCurrentInteractableChanged += HandleCurrentInteractableChanged;
        }

        public void OnDestroy()
        {
            m_interaction.OnNearbyInteractablesChanged -= HandleNearbyIntectablesChanged;
            m_interaction.OnCurrentInteractableChanged -= HandleCurrentInteractableChanged;
        }

        public void HandleNearbyIntectablesChanged(List<IInteractable> interactables)
        {
            int max = Mathf.Min(interactables.Count, m_maxNearbyInteractables);

            if (interactables.Count > max)
            {
                interactables.RemoveRange(max, interactables.Count - max);
            }

            foreach (var icon in m_interactionIcons)
            {
                icon.SetNearby(false);
            } 

            for(int i = 0; i < interactables.Count; i++)
            {
                if (i >= m_interactionIcons.Count)
                    break;

                m_interactionIcons[i].SetInteractionPivot(interactables[i].GetInteractionPoint());
                m_interactionIcons[i].SetNearby(true);
            }
        }

        public void HandleCurrentInteractableChanged(IInteractable newInteractable)
        {
            if (m_currentInteractable == newInteractable)
                return;

            foreach (var icon in m_interactionIcons)
            {
                if(newInteractable == null)
                {
                    icon.SetIcon(m_interactionSprite);
                    continue;
                }

                if (!icon.IsNearby)
                    continue;

                if(icon.InteractionPivot == newInteractable.GetInteractionPoint())
                {
                    icon.SetIcon(m_interactionFocusSprite);
                    continue;
                }

                icon.SetIcon(m_interactionSprite);
            }

            m_currentInteractable = newInteractable;
        }
    }
}
