using UnityEngine;
using UnityEngine.InputSystem;

namespace collegeGame
{
    public class PlayerAttack : MonoBehaviour
    {
        private PlayerInput _input;
        private CharacterController _controller;
        public GameObject debugObject;
        public Vector3 attackHeight;
        public Vector3 attackRadius;

        public LayerMask damageadbleLayer;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<PlayerInput>();
            _input.actions["Attack"].performed += PlayerAttack_performed;
            attackHeight = new(0, _controller.height / 2, 0);
            attackRadius = Vector3.forward * 2 + attackHeight;
            Instantiate(debugObject, transform.position + _controller.center + attackRadius, Quaternion.identity, transform);
        }

        private void PlayerAttack_performed(InputAction.CallbackContext obj)
        {
            Collider[] colliderArray = Physics.OverlapBox(transform.position + _controller.center + attackRadius, attackRadius, Quaternion.identity, damageadbleLayer);
            foreach (Collider collider in colliderArray)
            {
                collider.TryGetComponent(out Damageable damageable);
                Debug.Log(damageable.transform);
                Instantiate(debugObject, transform.position + _controller.center + attackRadius, Quaternion.identity, transform);
            }
        }
    }
}
