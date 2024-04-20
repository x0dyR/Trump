using UnityEngine;

namespace collegeGame
{
    public class RestState : IEnemyStates
    {

        public void StartMove()
        {
            Debug.Log("Rest");
        }

        public void StopMove()
        {
            Debug.Log("Next state");
        }

        public void Update()
        {
        }
    }
}
