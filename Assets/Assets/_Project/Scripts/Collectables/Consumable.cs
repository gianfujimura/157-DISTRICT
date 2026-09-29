using District.Core.InteractionSystem;
using UnityEngine;

namespace District.Collectables
{
    public abstract class Consumable : MonoBehaviour, IInteractable
    {
        [SerializeField] private InteractableData m_data;
        [SerializeField] private Transform m_iconPoint;
        [SerializeField] private Collider m_collider;

        public InteractableData InteractionData => m_data;

        public Transform GetTransform()
        {
            return transform;
        }

        public Collider GetCollider()
        {
            return m_collider;
        }

        public Transform GetInteractionPoint()
        {
            return m_iconPoint;
        }

        public abstract void Interact();
        public abstract void NotReady();
        public abstract void Ready();
    }
}