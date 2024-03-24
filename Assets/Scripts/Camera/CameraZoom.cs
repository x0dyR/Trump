using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace collegeGame
{
    public class CameraZoom : MonoBehaviour
    {
        private PlayerInput _input;
        public CinemachineVirtualCamera cm;
        private float _cameraDistance = 8f;
        private float _minCameraDistance =1f;
        private float _maxCameraDistance =8f;

        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _input.actions["Zoom"].performed += CameraZoom_performed;
        }

        private void CameraZoom_performed(InputAction.CallbackContext obj)
        {
            float scrollValue = obj.ReadValue<float>();
            var ft = cm.GetCinemachineComponent<CinemachineFramingTransposer>();
            _cameraDistance += scrollValue;
            _cameraDistance = Math.Clamp(_cameraDistance, _minCameraDistance, _maxCameraDistance);
            ft.m_CameraDistance = Mathf.Lerp(ft.m_CameraDistance, _cameraDistance,Time.deltaTime*10);
        }

    }
}
