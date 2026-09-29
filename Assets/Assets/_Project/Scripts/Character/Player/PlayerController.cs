using District.Character.General;
using District.Character.Player.States;
using District.Core.StateMachine;
using District.InputControl;
using District.UI;
using District.UI.Interaction;
using UnityEngine;

namespace District.Character.Player
{
    public class PlayerController : MonoBehaviour
    {
        public UIController Controller;

        [Header("Unity References")]
        private Rigidbody m_rb;
        private Animator m_animator;

        [Header("Camera")]
        [SerializeField] private Camera m_camera;

        [Header("UI")]
        [SerializeField] private UIController m_uiController;

        [Header("Data")]
        [SerializeField] private CharacterStats m_stats;
        [SerializeField] private PlayerContext m_ctx;

        [Header("Input")]
        [SerializeField] private PlayerInputData m_inputData;
        [SerializeField] private PlayerIntentBuilder m_intentBuilder;

        [Header("State Machine")]
        public StateMachine Machine { get; private set; }
        public StateMachineDebugger MachineDebugger { get; private set; }

        [Header("Behavior")]
        public PlayerSystems Systems { get; private set; }
        private PlayerMovement m_movement;
        private PlayerJump m_jump;
        private PlayerInteraction m_interaction;
        private PlayerAnimationController m_animController;
        private GroundChecker m_groundChecker;

        [Header("Interaction System")]
        [SerializeField] private Transform m_interactionPivot;

        [Header("Check")]
        [SerializeField] private Transform groundCheckTransform;

        [Header("Gizmos")]
        [SerializeField] private bool showGizmos = true;


        void Awake()
        {
            // Unity References
            m_rb = GetComponent<Rigidbody>();
            m_animator = GetComponent<Animator>();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            // Initialize the PlayerContext with the CharacterStats
            m_ctx = new PlayerContext(m_stats);

            // Initialize the IntentBuilder
            m_intentBuilder = new PlayerIntentBuilder(m_ctx, m_inputData);

            // Initialize Systems
            m_movement = new PlayerMovement(m_ctx, m_rb, m_camera, this.transform);
            m_jump = new PlayerJump(m_ctx, m_rb);
            m_interaction = new PlayerInteraction(m_ctx, m_interactionPivot, m_camera.transform);
            m_animController = new PlayerAnimationController(m_animator, m_ctx);

            Systems = new PlayerSystems(m_movement, m_jump, m_interaction,m_animController);

            Controller.InteractionUI.SetPlayerInteraction(m_interaction);

            // Initialize Checkers
            m_groundChecker = new GroundChecker(groundCheckTransform, m_stats.GroundCheck.Radius, m_stats.GroundCheck.GroundMask);

            // Initialize the StateMachine
            State rootState = new RootState(null, m_ctx, Systems);
            Machine = new StateMachineBuilder().Build(rootState);
            MachineDebugger = new StateMachineDebugger(Machine);
            Machine.Start();
        }

        // Update is called once per frame
        void Update()
        {
            // Check if the player is grounded
            m_ctx.SetGrounded(m_groundChecker.Check());

            // Build the player's intent based on the input and context
            m_intentBuilder.Build(Time.deltaTime);

            // Lifecycle the StateMachine and its Debugger
            Machine.Tick(Time.deltaTime);
            MachineDebugger.Tick(Time.deltaTime);

            // Lifecycle behaviors
            Systems.Tick(Time.deltaTime);
        }

        #region Gizmos
        public void OnDrawGizmos()
        {
            if (!showGizmos)
                return;

            if (m_groundChecker != null) m_groundChecker.OnDrawGizmos(groundCheckTransform, m_stats.GroundCheck.Radius);
            if (Systems != null) Systems.OnDrawGizmos();
        }
        #endregion
    }
}

