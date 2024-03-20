using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace collegeGame
{
    public class CameraZoom : MonoBehaviour
    {
        [field: SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [field: SerializeField] private PlayerControler _playerControler;
        private CinemachineFramingTransposer thirdperson;
        [field: SerializeField] float targetDistance;
        [field: SerializeField] float minCameraDistance = 1f;
        [field: SerializeField] float maxCameraDistance = 10f;

/*        private void Start()
        {
            _playerControler.inputActions.Player.Zoom.performed += ZoomCamera;
            thirdperson = _virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }

        private void ZoomCamera(InputAction.CallbackContext context)
        {
            float zoomSpeed = 21f;
            targetDistance = Mathf.Clamp(_playerControler.inputActions.Player.Zoom.ReadValue<float>() / 10, minCameraDistance,
                maxCameraDistance);

            thirdperson.m_CameraDistance = Mathf.Lerp(thirdperson.m_CameraDistance, targetDistance, Time.deltaTime * zoomSpeed);
        }*/
    }
}
