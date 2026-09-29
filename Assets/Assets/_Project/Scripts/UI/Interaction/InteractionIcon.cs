using District.Core.InteractionSystem;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace District.UI.Interaction
{
    public class InteractionIcon : MonoBehaviour
    {
        [SerializeField] private Camera m_camera;
        [SerializeField] private Image m_iconImage;
        private Transform m_intearctionPivot;
        private bool m_isNearby;

        public Image IconImage => m_iconImage;
        public Transform InteractionPivot => m_intearctionPivot;
        public bool IsNearby => m_isNearby;

        public void Initialize()
        {
            SetNearby(false);
        }

        #region Getters and Setters
        public void SetNearby(bool isNearby)
        {
            m_isNearby = isNearby;

            if (!m_isNearby)
                Hide();
        }

        public void SetIcon(Sprite icon)
        {
            m_iconImage.sprite = icon;
        }

        public void SetInteractionPivot(Transform pivot)
        {
            m_intearctionPivot = pivot;
        }

        public void SetCamera(Camera camera)
        {
            m_camera = camera;
        }
        #endregion

        #region Icon Visibility
        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
           this.gameObject.SetActive(false);
        }
        #endregion

        #region Lifecycle Methods
        public void Tick()
        {
            if (!m_isNearby)
                return;

            UpdatePosition();
        }

        private void UpdatePosition()
        {
            if (m_intearctionPivot == null)
                return;

            Vector3 worldPosition = m_intearctionPivot.position;

            if (!IsVisibleOnScreen(worldPosition))
            {
                Hide();
                return;
            }

            Show();

            Vector3 screenPosition = m_camera.WorldToScreenPoint(worldPosition);

            screenPosition.z = 0f;

            m_iconImage.rectTransform.position = screenPosition;
        }

        private bool IsVisibleOnScreen(Vector3 worldPosition)
        {
            Vector3 viewportPoint = m_camera.WorldToViewportPoint(worldPosition);

            return viewportPoint.z > 0f &&
                   viewportPoint.x >= 0f &&
                   viewportPoint.x <= 1f &&
                   viewportPoint.y >= 0f &&
                   viewportPoint.y <= 1f;
        }
        #endregion
    }
}
