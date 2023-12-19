using System;
using UnityEngine;

namespace collegeGame
{
    public class MovementState : IStates
    {
        protected Vector2 movementInput;
        protected MovementStateMachine stateMachine;
        protected float baseSpeed = 5f;
        protected float speedModifier = 1f;
        public MovementState(MovementStateMachine playerMovementStateMachine)
        {
            stateMachine = playerMovementStateMachine;
        }
        #region  IState Methods
        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public void HandleInput()
        {
            ReadMovementInput();
        }



        public void PhysicsUpdate()
        {
            Move();
        }
        #endregion
        public void Update()
        {
        }
        #region Main Methods
        private void ReadMovementInput()
        {
            movementInput = stateMachine.Player.Input.PlayerActions.Move.ReadValue<Vector2>();
        }
        private void Move()
        {
            if (movementInput == Vector2.zero || speedModifier == 0f)
            {
                return;
            }
            Vector3 movementDirection = GetMovementDirection();
            float movementSpeed = GetMovementSpeed();

            Vector3 currentHorizontalVelocity = GetPlayerHorizontalVelocity();

            stateMachine.Player.rb.AddForce(movementDirection * movementSpeed - currentHorizontalVelocity, ForceMode.VelocityChange);
        }


        #endregion

        #region Reusable Method
        private Vector3 GetMovementDirection()
        {
            return new Vector3(movementInput.x, 0f, movementInput.y);
        }
        protected float GetMovementSpeed()
        {
            return baseSpeed * speedModifier;
        }
        protected Vector3 GetPlayerHorizontalVelocity()
        {
            Vector3 playerHorizontalVelocity = stateMachine.Player.rb.velocity;
            playerHorizontalVelocity.y = 0f;
            return playerHorizontalVelocity;
        }
        #endregion
    }
}
