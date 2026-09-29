using System;
using UnityEngine;

namespace District.Character.General
{
    public enum MovementState
    {
        Idle,
        Walking,
        Running,
        Airborne
    }

    [Serializable]
    public class MovementRuntime
    {
        [Header("Settings")]
        private MovementSettings m_settings;

        [Header("Movement State")]
        [SerializeField] private MovementState m_moveState;

        [Header("Movement Parameters")]
        [SerializeField] private float m_acceleration;
        [SerializeField] private float m_deceleration;
        [SerializeField] private float m_targetSpeed;
        [SerializeField] private Vector3 m_moveDirection;
        [SerializeField] private Vector3 m_currentVelocity;

        public MovementState MoveState => m_moveState;
        public float Acceleration => m_acceleration;
        public float Deceleration => m_deceleration;
        public float TargetSpeed => m_targetSpeed;
        public Vector3 MoveDirection => m_moveDirection;
        public Vector3 CurrentVelocity => m_currentVelocity;

        public MovementRuntime(MovementSettings settings)
        {
            this.m_settings = settings;
        }

        #region State Update Logic
        public void SetMoveState(MovementState newState)
        {
            if (newState == m_moveState) 
                return;

            m_moveState = newState;

            UpdateVariables(m_moveState);
        }

        public void UpdateVariables(MovementState state)
        {
            switch (state)
            {
                case MovementState.Idle:
                    m_targetSpeed = 0f;
                    m_acceleration = m_settings.WalkAcceleration;
                    m_deceleration = m_settings.WalkDeceleration;
                    break;
                case MovementState.Walking:
                    m_targetSpeed = m_settings.WalkSpeed;
                    m_acceleration = m_settings.WalkAcceleration;
                    m_deceleration = m_settings.WalkDeceleration;
                    break;
                case MovementState.Running:
                    m_targetSpeed = m_settings.SprintSpeed;
                    m_acceleration = m_settings.SprintAcceleration;
                    m_deceleration = m_settings.SprintDeceleration;
                    break;
                case MovementState.Airborne:
                    m_targetSpeed = m_settings.AirborneSpeed;
                    m_acceleration = m_settings.AirborneAcceleration;
                    m_deceleration = m_settings.AirborneDeceleration;
                    break;
            }
        }
        #endregion

        #region Seter Methods
        public void SetAcceleration(float newAcceleration)
        {
            m_acceleration = newAcceleration;
        }

        public void SetDeceleration(float newDeceleration)
        {
            m_deceleration = newDeceleration;
        }

        public void SetTargetSpeed(float targetSpeed)
        {
            m_targetSpeed = targetSpeed;
        }

        public void SetMoveDirection(Vector3 moveDirection)
        {
            m_moveDirection = moveDirection;
        }

        public void UpdateCurrentVelocity(Vector3 newVelocity)
        {
            m_currentVelocity = newVelocity;
        }
        #endregion
    }
}