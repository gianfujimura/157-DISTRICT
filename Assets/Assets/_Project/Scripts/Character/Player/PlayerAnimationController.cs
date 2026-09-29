using UnityEngine;

namespace District.Character.Player
{
    public class PlayerAnimationController
    {
        private PlayerContext m_ctx;
        private Animator m_animator;

        public PlayerAnimationController(Animator animator, PlayerContext ctx)
        {
            this.m_animator = animator;
            this.m_ctx = ctx;
        }

        public void Tick(float deltaTime)
        {
            if (m_animator == null)
                return;

            m_animator.SetBool("IsGrounded", m_ctx.IsGrounded);

            float normalizedHorizontalSpeed = m_ctx.Movement.CurrentVelocity.magnitude / m_ctx.Stats.Movement.SprintSpeed;
            m_animator.SetFloat("HorizontalSpeed", normalizedHorizontalSpeed);
        }

        public void TriggerJump()
        {
            if (m_animator == null)
                return;

            m_animator.SetTrigger("Jump");
        }
    }
}
