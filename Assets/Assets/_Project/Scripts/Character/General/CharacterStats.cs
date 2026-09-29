using System;
using UnityEngine;

namespace District.Character.General
{
    [CreateAssetMenu(fileName = "CharacterStats", menuName = "Config/Characters/Character stats")]
    public class CharacterStats : ScriptableObject
    {
        [SerializeField] private GroundCheckSettings m_groundCheck;
        [SerializeField] private MovementSettings m_movement;
        [SerializeField] private JumpSettings m_jump;
        [SerializeField] private InteractionSettings m_interaction;

        public GroundCheckSettings GroundCheck => m_groundCheck;
        public MovementSettings Movement => m_movement;
        public JumpSettings Jump => m_jump;
        public InteractionSettings Interaction => m_interaction;
    }
}