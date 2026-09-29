using UnityEngine;

namespace District.Core.InteractionSystem
{
    public interface IInteractable
    {
        public InteractableData InteractionData { get; }
        public Transform GetInteractionPoint();
        public Transform GetTransform();
        public Collider GetCollider();
        public void Interact();
        public void Ready();
        public void NotReady();
    }
}