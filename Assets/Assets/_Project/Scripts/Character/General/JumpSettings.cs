using UnityEngine;

namespace District.Character.General
{
    [System.Serializable]
    public class JumpSettings
    {
        [SerializeField] private float jumpHeigth;
        [SerializeField] private float gravity;
        [SerializeField] private float maxFallingVelocity;
        [SerializeField] private float coyoteTime;
        [SerializeField] private float jumpBufferTime;

        public float JumpHeigth => jumpHeigth;
        public float Gravity => gravity;
        public float MaxFallingVelocity => maxFallingVelocity;
        public float CoyoteTime => coyoteTime;
        public float JumpBufferTime => jumpBufferTime;
    }
}