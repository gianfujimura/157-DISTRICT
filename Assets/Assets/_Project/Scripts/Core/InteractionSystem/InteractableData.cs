using System;
using UnityEngine;

namespace District.Core.InteractionSystem
{
    [CreateAssetMenu(fileName = "InteractableData", menuName = "Config/Interaction System/Interactable Data")]
    public class InteractableData : ScriptableObject
    {
        [SerializeField] private Sprite m_icon;
        [SerializeField] private string m_name;
        [SerializeField] private string m_description;

        public Sprite Icon => m_icon;
        public string Name => m_name;
        public string Description => m_description;
    }
}