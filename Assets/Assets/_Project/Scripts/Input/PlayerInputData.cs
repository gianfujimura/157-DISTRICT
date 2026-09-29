using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace District.InputControl
{ 
    public class PlayerInputData : MonoBehaviour, PlayerInputActions.IPlayerActions 
    {
        [Header("Input Actions")]
        PlayerInputActions m_InputActions;

        [Header("Input Values")]
        [SerializeField] Vector2 m_move = Vector2.zero;
        [SerializeField] Vector2 m_aim = Vector2.zero;
        [SerializeField] bool m_sprintPressed = false;
        [SerializeField] bool m_jumpPressed = false;
        [SerializeField] bool m_jumpHeld = false;
        [SerializeField] bool m_interactPressed = false;
        [SerializeField] bool m_interactHeld = false;

        public Vector2 Move => m_move;
        public Vector2 Aim => m_aim;
        public bool SprintPressed => m_sprintPressed;
        public bool JumpPressed => m_jumpPressed;
        public bool JumpHeld => m_jumpHeld;
        public bool InteractPressed => m_interactPressed;
        public bool InteractHeld => m_interactHeld;

        #region Unity Methods
        void Awake() 
        { 
            m_InputActions = new PlayerInputActions(); 
        } 

        void LateUpdate()
        {
            m_jumpPressed = false;
            m_interactPressed = false;
        }

        void OnEnable() 
        { 
            m_InputActions.Player.Enable(); 
            m_InputActions.Player.SetCallbacks(this); 
        } 
        
        void OnDisable() 
        { 
            m_InputActions.Player.Disable(); 
            m_InputActions.Player.RemoveCallbacks(this); 
        }
        #endregion

        #region Player Input Actions Callbacks
        public void OnInteract(InputAction.CallbackContext context) 
        {
            if(context.started)
            {
                m_interactPressed = true;
                m_interactHeld = true;
            }
            else if(context.canceled)
            {
                m_interactHeld = false;
            }
        } 
        
        public void OnJump(InputAction.CallbackContext context) 
        { 
            if(context.started)
            {
                m_jumpPressed = true;
                m_jumpHeld = true;
            }
            else if(context.canceled)
            {
                m_jumpHeld = false;
            }
        }

        public void OnMove(InputAction.CallbackContext context) 
        { 
            m_move = context.ReadValue<Vector2>(); 
        }

        public void OnAim(InputAction.CallbackContext context)
        {
            m_aim = context.ReadValue<Vector2>();
        }

        public void OnSprint(InputAction.CallbackContext context) 
        { 
            m_sprintPressed = context.ReadValueAsButton();
        }
        #endregion
    }
}