using UnityEngine;

namespace District.Character.General
{
    public class GroundChecker
    { 
        private Transform m_groundCheckTransform;
        private float m_radius;
        private LayerMask m_groundMask;

        public GroundChecker(Transform groundCheckTransform, float radius, LayerMask groundMask)
        {
            this.m_groundCheckTransform = groundCheckTransform;
            this.m_radius = radius;
            this.m_groundMask = groundMask;
        }

        public bool Check()
        {
            return Physics.CheckSphere(
                m_groundCheckTransform.position,
                m_radius,
                m_groundMask);
        }

        public void OnDrawGizmos(Transform groundCheckTransform, float radius)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(groundCheckTransform.position, radius);
        }
    }
}