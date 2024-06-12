using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trump
{
    public class LookAtPlayer : MonoBehaviour
    {
        void LateUpdate() {
        
        transform.LookAt(Camera.main.transform);
    }
}
}