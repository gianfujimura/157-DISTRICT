using UnityEngine;

namespace District.Character.General
{
    [System.Serializable]
    public class MovementSettings
    {
        [Header("Walk Settings")]
        [SerializeField] private float walkSpeed;
        [SerializeField] private float walkAcceleration;
        [SerializeField] private float walkDeceleration;

        public float WalkSpeed => walkSpeed;
        public float WalkAcceleration => walkAcceleration;
        public float WalkDeceleration => walkDeceleration;

        [Header("Sprint Settings")]
        [SerializeField] private float sprintSpeed;
        [SerializeField] private float sprintAcceleration;
        [SerializeField] private float sprintDeceleration;

        public float SprintSpeed => sprintSpeed;
        public float SprintAcceleration => sprintAcceleration;
        public float SprintDeceleration => sprintDeceleration;

        [Header("Airborne Settings")]
        [SerializeField] private float airborneSpeed;
        [SerializeField] private float airborneAcceleration;
        [SerializeField] private float airborneDeceleration;

        public float AirborneSpeed => airborneSpeed;
        public float AirborneAcceleration => airborneAcceleration;
        public float AirborneDeceleration => airborneDeceleration;
    }
}