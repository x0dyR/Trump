using UnityEngine;

namespace collegeGame
{
    public class Interactable : MonoBehaviour
    {
        public virtual void Interact(CharacterController characterController)
        {
            Debug.Log(this + "should not be called");
        }
    }
}
