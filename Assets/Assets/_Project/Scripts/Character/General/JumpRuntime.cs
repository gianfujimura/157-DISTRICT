using System;
using UnityEngine;

namespace District.Character.General
{
    [Serializable]
    public class JumpRuntime
    {
        [Header("Settings")]
        public JumpSettings Settings { get; private set; }

        [Header("Jump Parameters")]
        [SerializeField] private float m_currentVelocity; 
        [SerializeField] private bool m_isJumping;
        [SerializeField] private float jumpBufferTimer;
        [SerializeField] private float coyoteTimer;

        public float CurrentVelocity => m_currentVelocity;
        public bool IsJumping => m_isJumping;
        public float JumpBufferTimer
        {
            get => jumpBufferTimer;
            set => jumpBufferTimer = value;
        }

        public float CoyoteTimer
        {
            get => coyoteTimer;
            set => coyoteTimer = value;
        }

        public JumpRuntime(JumpSettings settings)
        {
            this.Settings = settings;
        }

        #region Setters
        public void UpdateCurrentVelocity(float newVelocity)
        {
            m_currentVelocity = newVelocity;
        }

        public void SetIsJumping(bool value)
        {
            if (m_isJumping == value)
                return;
            m_isJumping = value;
        }
        #endregion
    }
}