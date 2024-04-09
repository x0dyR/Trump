using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace collegeGame
{
    public class LookAtPlayer : MonoBehaviour
    {
        public Transform camera;
        void LateUpdate() {
        
        transform.LookAt(camera);
    }
}
}