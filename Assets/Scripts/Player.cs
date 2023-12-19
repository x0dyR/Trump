using UnityEngine;

namespace collegeGame
{
    [RequireComponent(typeof(PlayerInput))]
    public class Player : MonoBehaviour
    {
        public Rigidbody rb { get; private set; }
        public PlayerInput Input { get; private set; }

        private MovementStateMachine movementStateMachine;
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            Input = GetComponent<PlayerInput>();
            movementStateMachine = new MovementStateMachine(this);
        }
        private void Start()
        {
            movementStateMachine.ChangeState(movementStateMachine.IdlingState);
        }
        private void Update()
        {
            movementStateMachine.HandleInput();

            movementStateMachine.Update();
        }
        private void FixedUpdate()
        {
            movementStateMachine.PhysicsUpdate();
        }
    }
}
