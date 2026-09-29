using District.InputControl;
using UnityEngine;

namespace District.Core.CameraSystem
{
    public class CameraController : MonoBehaviour
    {
        #region Fields
        [SerializeField] private PlayerInputData m_input;
        public float m_currentXAngle;
        public float m_currentYAngle;

        [Range(0f, 90f)] public float upperVerticalLimit = 35f;
        [Range(0f, 90f)] public float lowerVerticalLimit = 35f;

        public bool smoothCameraRotation;
        public float cameraSpeed = 50f;
        [Range(1f, 500f)] public float cameraSmoothingFactor = 25f;

        Transform tr;
        Camera cam;
        #endregion

        public Vector3 GetUpDirection() => tr.up;
        public Vector3 GetFacingDirection() => tr.forward;

        private void Awake()
        {
            tr = transform;
            cam = GetComponentInChildren<Camera>();

            m_currentXAngle = tr.rotation.eulerAngles.y;
            m_currentYAngle = tr.rotation.eulerAngles.x;
        }

        private void Update()
        {
            RotateCamera(m_input.Aim);
        }

        private void RotateCamera(Vector2 inputDirection)
        {
            float horizontalInput = inputDirection.x;
            float verticalInput = -inputDirection.y;

            if (smoothCameraRotation)
            {
                horizontalInput = Mathf.Lerp(0, inputDirection.x, cameraSmoothingFactor);
                verticalInput = Mathf.Lerp(0, inputDirection.y, cameraSmoothingFactor);
            }

            m_currentXAngle += verticalInput * cameraSpeed * Time.deltaTime;
            m_currentYAngle += horizontalInput * cameraSpeed * Time.deltaTime;

            m_currentXAngle = Mathf.Clamp(m_currentXAngle, -lowerVerticalLimit, upperVerticalLimit);

            tr.rotation = Quaternion.Euler(m_currentXAngle, m_currentYAngle, 0f);
        }
    }
}