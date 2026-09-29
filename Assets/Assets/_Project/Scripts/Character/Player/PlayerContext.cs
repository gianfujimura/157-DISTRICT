using District.Character.General;
using District.Character.Player;
using System;
using UnityEngine;

namespace District.Character.Player
{
    [Serializable]
    public class PlayerContext
    {
        [Header("Stats")]
        public CharacterStats Stats { get; }

        [Header("Variables")]
        [SerializeField] private bool m_isGrounded;
        [SerializeField] private bool m_wasGrounded; 

        public bool IsGrounded => m_isGrounded;
        public bool WasGrounded => m_wasGrounded;

        [Header("Intents")]
        [SerializeField] private PlayerIntent m_intent;

        public PlayerIntent Intent => m_intent;

        [Header("Runtimes")]
        [SerializeField] private MovementRuntime m_movement;
        [SerializeField] private JumpRuntime m_jump;

        public MovementRuntime Movement => m_movement;
        public JumpRuntime Jump => m_jump;

        public PlayerContext(CharacterStats stats)
        {
            this.Stats = stats;
            Initialize();
        }

        private void Initialize()
        {
            m_intent = new PlayerIntent();
            m_movement = new MovementRuntime(Stats.Movement);
            m_jump = new JumpRuntime(Stats.Jump);
        }

        #region Setters
        public void SetGrounded(bool grounded)
        {
            if (m_isGrounded == grounded)
                return;

            bool justLanded = !m_isGrounded && grounded;

            if (justLanded)
            {
                Jump.SetIsJumping(false);
            }

            m_isGrounded = grounded;
        }
        #endregion
    }
}
