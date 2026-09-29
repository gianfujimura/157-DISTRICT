using UnityEngine;

namespace District.Character.General
{
    [System.Serializable]
    public class GroundCheckSettings
    {
        [SerializeField] private float radius;
        [SerializeField] private LayerMask groundMask;

        public float Radius => radius;
        public LayerMask GroundMask => groundMask;
    }
}