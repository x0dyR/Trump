using UnityEngine;

namespace collegeGame
{
    [RequireComponent(typeof(PlayerInput))]
    public class Player : MonoBehaviour
    {
        [field: Header("References")]
        [field: SerializeField]public SO playerData { get; private set; }
        public Rigidbody rb { get; private set; }
        public PlayerInput Input { get; private set; }
        public Transform MainCameraTransform { get; private set; }

        private MovementStateMachine movementStateMachine;
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            Input = GetComponent<PlayerInput>();
            MainCameraTransform = Camera.main.transform;
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
