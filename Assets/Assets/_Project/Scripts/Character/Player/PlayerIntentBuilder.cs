using District.InputControl;
using System;
using UnityEngine;

namespace District.Character.Player
{
    public class PlayerIntentBuilder
    {
        private PlayerContext m_ctx;
        private PlayerInputData m_inputData;

        public PlayerIntentBuilder(PlayerContext context, PlayerInputData input)
        {
            this.m_ctx = context;
            this.m_inputData = input;
        }

        public void Build(float deltaTime)
        {
            BuildMoveIntent();
            BuildAimIntent();
            BuildSprintIntent();
            BuildJumpIntent(deltaTime);
            BuildInteractionIntent();
        }

        #region Intent Builders
        private void BuildMoveIntent()
        {
            // Build the move intent based on the input and context
            m_ctx.Intent.SetMove(m_inputData.Move);
        }

        private void BuildAimIntent()
        {
            m_ctx.Intent.SetAim(m_inputData.Aim);
        }

        private void BuildSprintIntent()
        {
            m_ctx.Intent.SetSprint(m_inputData.SprintPressed);
        }

        private void BuildJumpIntent(float deltaTime)
        {
            UpdateCoyoteTimer(deltaTime);
            UpdateJumpBufferTimer(deltaTime);

            bool hasJumpBuffer = m_ctx.Jump.JumpBufferTimer > 0f;
            bool canUseCoyote = m_ctx.Jump.CoyoteTimer > 0f;

            if ((hasJumpBuffer && m_ctx.IsGrounded) || (hasJumpBuffer && canUseCoyote && !m_ctx.Jump.IsJumping))
            {
                m_ctx.Jump.JumpBufferTimer = 0f;
                m_ctx.Jump.CoyoteTimer = 0f;
                m_ctx.Intent.SetJumpPressed(true);
            }

            m_ctx.Intent.SetJumpHeld(m_inputData.JumpHeld);
        }

        private void UpdateCoyoteTimer(float deltaTime)
        {
            if (m_ctx.IsGrounded)
            {
                m_ctx.Jump.CoyoteTimer = m_ctx.Stats.Jump.CoyoteTime;
            }
            else
            {
                m_ctx.Jump.CoyoteTimer -= deltaTime;
            }

            m_ctx.Jump.CoyoteTimer = Mathf.Clamp(m_ctx.Jump.CoyoteTimer, 0f, m_ctx.Stats.Jump.CoyoteTime);
        }

        private void UpdateJumpBufferTimer(float deltaTime)
        {
            if (m_inputData.JumpPressed)
            {
                m_ctx.Jump.JumpBufferTimer = m_ctx.Stats.Jump.JumpBufferTime;
            }

            m_ctx.Jump.JumpBufferTimer -= deltaTime;
            m_ctx.Jump.JumpBufferTimer = Mathf.Clamp(m_ctx.Jump.JumpBufferTimer, 0f, m_ctx.Stats.Jump.JumpBufferTime);
        }

        private void BuildInteractionIntent()
        {
            m_ctx.Intent.SetInteractionPressed(m_inputData.InteractPressed);
            m_ctx.Intent.SetInteractionHeld(m_inputData.InteractHeld);
        }
        #endregion
    }
}