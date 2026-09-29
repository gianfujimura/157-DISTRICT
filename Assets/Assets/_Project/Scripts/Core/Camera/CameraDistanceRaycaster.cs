using UnityEngine;
using UnityEngine.Scripting;

namespace District.Core.CameraSystem
{
    public class CameraDistanceRaycaster : MonoBehaviour
    {
        [SerializeField, RequiredMember] Transform m_cameraTransform;
        [SerializeField, RequiredMember] Transform m_cameraTargetTransform;

        public LayerMask layerMask = Physics.AllLayers;
        public float minimumDistanceFromObstacles = 0.1f;
        public float smoothingFactor = 25f;

        Transform m_tr;
        public float m_currentDistance;

        private void Awake()
        {
            m_tr = transform;
            layerMask &= ~(1 << LayerMask.NameToLayer("Ignore Raycast"));
            layerMask &= ~(1 << LayerMask.NameToLayer("Player"));
            m_currentDistance = (m_cameraTargetTransform.position - m_tr.position).magnitude;
        }

        private void LateUpdate()
        {
            Vector3 castDirection = m_cameraTargetTransform.position - m_tr.position;

            float distance = GetCameraDistance(castDirection);

            m_currentDistance = Mathf.Lerp(m_currentDistance, distance, Time.deltaTime * smoothingFactor);
            m_cameraTransform.position = m_tr.position + castDirection.normalized * m_currentDistance;
        }

        private float GetCameraDistance(Vector3 castDirection)
        {
            float distance = castDirection.magnitude + minimumDistanceFromObstacles;

            //if (Physics.Raycast(new Ray(tr.position, castDirection), out RaycastHit hit, distance, layerMask, QueryTriggerInteraction.Ignore))
            //{
            //    return Mathf.Max(0f, hit.distance - minimumDistanceFromObstacles);
            //}

            float sphereRadius = 0.5f;
            if (Physics.SphereCast(new Ray(m_tr.position, castDirection), sphereRadius, out RaycastHit hit, distance, layerMask, QueryTriggerInteraction.Ignore))
            {
                return Mathf.Max(0f, hit.distance - minimumDistanceFromObstacles);
            }

            return castDirection.magnitude;
        }
    }
}
