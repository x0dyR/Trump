using UnityEngine;

namespace collegeGame
{
    public class Home : Interactable
    {
        private void Awake()
        {

        }
        public override void Interact(CharacterController characterController)
        {
            Debug.Log(gameObject + "called");
        }
    }
}
