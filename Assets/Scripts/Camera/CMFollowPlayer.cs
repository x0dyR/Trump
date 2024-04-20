using Cinemachine;
using collegeGame.Inputs;
using UnityEngine;
using Zenject;

namespace collegeGame
{
    public class CMFollowPlayer : MonoBehaviour
    {
        [Inject] private CinemachineVirtualCamera cmv;
        public Transform target;
    }
}
