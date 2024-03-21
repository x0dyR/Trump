using UnityEngine;
using UnityEngine.SceneManagement;

namespace collegeGame
{
    public class Home : Interactable
    {
        private void Awake()
        {

        }
        public override void Interact(CharacterController characterController)
        {
            Debug.Log(this + "called");
        }
    }
}
