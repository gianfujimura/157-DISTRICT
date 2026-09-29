using District.Core.InteractionSystem;
using UnityEngine;

namespace District.Collectables
{
    public class HealthPotion : MonoBehaviour, IInteractable
    {
        [SerializeField] private InteractableData m_interactionData;
        [SerializeField] private Transform m_interationPoint;
        [SerializeField] private Collider m_collider;

        public InteractableData InteractionData => m_interactionData;

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
            return m_interationPoint;
        }

        public void Interact()
        {
            Debug.Log(this.gameObject.name + " interacted.");

            Destroy(gameObject);
        }

        public void NotReady()
        {
            Debug.Log(this.gameObject.name + " cannot be used.");
        }

        public void Ready()
        {
            Debug.Log(this.gameObject.name + " can be used.");
        }
    }
}