using District.InputControl;
using UnityEngine;
using UnityEngine.Animations;

namespace District.Character.Player
{
    public class PlayerMovement
    {
        [Header("Unity References")]
        private Rigidbody m_rb;
        private Camera m_cam;
        private Transform m_tr;

        [Header("Settings")]
        private PlayerContext m_ctx;

        public Vector3 GetMovementVelocity() => m_rb.linearVelocity;

        public PlayerMovement(PlayerContext ctx, Rigidbody rb, Camera cam, Transform tr)
        {
            this.m_ctx = ctx;
            this.m_rb = rb;
            this.m_cam = cam;
            this.m_tr = tr;
        }

        public void UpdateHorizontalVelocity(float deltaTime)
        {
            Vector3 inputDirection = CalculateDirection();

            Vector3 targetVelocity = inputDirection * m_ctx.Movement.TargetSpeed;

            float currentSpeed = m_ctx.Movement.CurrentVelocity.sqrMagnitude;
            float targetSpeed = targetVelocity.sqrMagnitude;
            float rate;

            // If the player is moving and has a target speed, determine if we should accelerate or decelerate
            if (currentSpeed > 0.001f && targetSpeed > 0.001f)
            {
                float dot = Vector3.Dot(m_ctx.Movement.CurrentVelocity.normalized, targetVelocity.normalized);

                // If the dot product is negative, the player is trying to move in the opposite direction, so we should decelerate faster
                if (dot < 0f)
                {
                    rate = m_ctx.Movement.Deceleration * 2f;
                }
                else if (currentSpeed < targetSpeed)
                {
                    rate = m_ctx.Movement.Acceleration;
                }
                else
                {
                    rate = m_ctx.Movement.Deceleration;
                }
            }
            else
            {
                rate = m_ctx.Movement.Deceleration;
            }

            Vector3 newVelocity = Vector3.MoveTowards(m_ctx.Movement.CurrentVelocity,
                targetVelocity,
                rate * deltaTime);

            m_ctx.Movement.UpdateCurrentVelocity(newVelocity);

            if (m_ctx.Intent.Move.sqrMagnitude > 0.01f)
            {
                Vector3 moveDirection =
                    Vector3.ProjectOnPlane(
                        inputDirection,
                        m_tr.up
                    ).normalized;

                m_ctx.Movement.SetMoveDirection(moveDirection);
            }
        }

        public Vector3 CalculateDirection()
        {
            Vector2 moveInput = new Vector3(m_ctx.Intent.Move.x, m_ctx.Intent.Move.y);
            moveInput = Vector3.ClampMagnitude(moveInput, 1f);

            Vector3 cameraFoward = Vector3.ProjectOnPlane(m_cam.transform.forward,
                m_tr.up).normalized;

            Vector3 cameraRight = Vector3.ProjectOnPlane(m_cam.transform.right,
                m_tr.up).normalized;

            Vector3 direction = moveInput.y * cameraFoward +
                moveInput.x * cameraRight;

            return Vector3.ClampMagnitude(direction, 1f);
        }

        public void ApplyHorizontalVelocity(float deltaTime)
        {
            m_rb.linearVelocity = new Vector3(m_ctx.Movement.CurrentVelocity.x,
                m_rb.linearVelocity.y,
                m_ctx.Movement.CurrentVelocity.z);
        }
    }
}
