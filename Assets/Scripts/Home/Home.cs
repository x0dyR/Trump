using UnityEngine;

namespace collegeGame
{
    public class Home : MonoBehaviour, IInteract
    {
        public void Interact()
        {
            Debug.Log(gameObject.name + "called");
        }
    }
}
