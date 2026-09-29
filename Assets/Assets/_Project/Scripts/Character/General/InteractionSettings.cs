using District.UI.Interaction;
using UnityEditor.Search;
using UnityEngine;

namespace District.Character.General
{
    [System.Serializable]
    public class InteractionSettings
    {
        [Header("Interaction Settings")]
        [SerializeField] private float m_interactionOffset = 1.0f;
        [SerializeField] private float m_interactionRadius = 2.0f;
        [SerializeField] private LayerMask m_interactableLayer;

        public float InteractionOffset => m_interactionOffset;
        public float InteractionRadius => m_interactionRadius;
        public LayerMask InteractableLayer => m_interactableLayer;
    }
}
