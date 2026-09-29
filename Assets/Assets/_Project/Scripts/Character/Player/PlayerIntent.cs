using System;
using UnityEngine;

namespace District.Character.Player
{
    [Serializable]
    public struct ButtonState
    {
        public bool Pressed;
        public bool Held;
        public bool Released;
    }

    [Serializable]
    public class PlayerIntent
    {
        [Header("Input")]
        [SerializeField] private Vector2 m_move;
        [SerializeField] private Vector2 m_aim;
        [SerializeField] private bool m_wantsToSprint;
        [SerializeField] private ButtonState m_jump;
        [SerializeField] private ButtonState m_interact;

        public Vector2 Move => m_move;
        public Vector2 Aim => m_aim;
        public bool WantsToMove => m_move.sqrMagnitude > 0.01f;
        public bool WantsToSprint => m_wantsToSprint;
        public ButtonState Jump => m_jump;
        public ButtonState Interact => m_interact;

        [Header("Command")]
        [SerializeField] private bool m_requestJump;

        public bool HasJumpRequest => m_requestJump;

        #region Setters for the Intent
        public void SetMove(Vector2 value)
        {
            m_move = value;
        }

        public void SetAim(Vector2 value)
        {
            m_aim = value;
        }

        public void SetSprint(bool value)
        {
            m_wantsToSprint = value;
        }

        public void SetJumpPressed(bool value)
        {
            m_jump.Pressed = value;
        }

        public void SetJumpHeld(bool value)
        {
            m_jump.Held = value;
        }

        public void SetInteractionPressed(bool value)
        {
            m_interact.Pressed = value;
        }

        public void SetInteractionHeld(bool value)
        {
            m_interact.Held = value;
        }
        #endregion
    }
}