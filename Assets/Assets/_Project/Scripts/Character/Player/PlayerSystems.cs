using UnityEngine;

namespace District.Character.Player
{
    public class PlayerSystems
    {
        public PlayerMovement Movement { get; private set; }
        public PlayerJump Jump { get; private set; }
        public PlayerInteraction Interaction { get; private set; }
        public PlayerAnimationController Animation { get; private set; }

        public PlayerSystems(PlayerMovement movement, PlayerJump jump, PlayerInteraction interaction, PlayerAnimationController animation)
        {
            this.Movement = movement;
            this.Jump = jump;
            this.Interaction = interaction;
            this.Animation = animation;
        }

        public void Tick(float deltaTime)
        {
            Movement.UpdateHorizontalVelocity(deltaTime);
            Movement.ApplyHorizontalVelocity(deltaTime);

            Jump.Tick(deltaTime);
            Jump.ApplyVerticalVelocity(deltaTime);

            Interaction.Tick(deltaTime);

            Animation.Tick(deltaTime);
        }

        public void OnDrawGizmos()
        {
            //if (Movement != null) Momement.OnDrawGizmos();
            //if (Jump != null) Jump.OnDrawGizmos();
            if (Interaction != null) Interaction.OnDrawGizmos();
            //if (Animation != null) Animation.OnDrawGizmos();
        }
    }
}
