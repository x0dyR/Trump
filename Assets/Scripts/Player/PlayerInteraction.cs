using UnityEngine;
using UnityEngine.InputSystem;

namespace collegeGame
{ 
    public class PlayerInteraction : MonoBehaviour
    { 
        private PlayerInput _input;
        private CharacterController _controller;

        public LayerMask interactableLayer;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<PlayerInput>();
            _input.actions["Interaction"].performed += Interaction;
        }

        private void Interaction(InputAction.CallbackContext context)
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, 2f, interactableLayer);
            foreach (Collider collider in colliderArray)
            {
                collider.TryGetComponent(out Interactable interactable);
                interactable.Interact(_controller);
            }
        }
    }
}
