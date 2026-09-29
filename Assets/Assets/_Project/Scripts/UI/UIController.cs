using UnityEngine;
using District.UI.Interaction;
using UnityEditor.Search;
using District.Character.Player;

namespace District.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private InteractionUI m_interactionUI;

        public InteractionUI InteractionUI => m_interactionUI;

        private void Start()
        {
            if(m_interactionUI) m_interactionUI.Initialize();
        }

        private void Update()
        {
            if(m_interactionUI) m_interactionUI.Tick(Time.deltaTime);
        }
    }

}
