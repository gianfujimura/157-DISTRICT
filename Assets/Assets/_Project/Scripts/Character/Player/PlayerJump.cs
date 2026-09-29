using District.Character.General;
using UnityEngine;

namespace District.Character.Player
{
    public class PlayerJump
    {
        [Header("Unity References")]
        private Rigidbody m_rb;

        [Header("Settings")]
        private PlayerContext m_ctx;

        public PlayerJump(PlayerContext ctx, Rigidbody rb)
        {
            this.m_ctx = ctx;
            this.m_rb = rb;
        }

        public void Tick(float deltaTime)
        {
            Gravity(deltaTime);
        }

        public void Jump()
        {
            float jumpVelocity = Mathf.Sqrt(2f * m_ctx.Jump.Settings.JumpHeigth * Mathf.Abs(m_ctx.Jump.Settings.Gravity));
            m_ctx.Jump.UpdateCurrentVelocity(jumpVelocity);
            m_ctx.Jump.SetIsJumping(true);
            m_ctx.Intent.SetJumpPressed(false); 
        }

        public void Gravity(float deltaTime)
        {
            float velocity = m_ctx.Jump.CurrentVelocity;
            float newVelocity = m_ctx.Jump.CurrentVelocity + m_ctx.Jump.Settings.Gravity * deltaTime;

            if (m_ctx.IsGrounded && newVelocity <= 0f)
            {
                newVelocity = 0f;
            }
            else if (!m_ctx.IsGrounded && newVelocity < m_ctx.Jump.Settings.MaxFallingVelocity)
            {
                newVelocity =  m_ctx.Jump.Settings.MaxFallingVelocity;
            }

            m_ctx.Jump.UpdateCurrentVelocity(newVelocity);
        }

        public void ApplyVerticalVelocity(float deltaTime)
        {
            Vector3 velocity = m_rb.linearVelocity;
            velocity.y = m_ctx.Jump.CurrentVelocity;
            m_rb.linearVelocity = velocity;
        }
    }
}