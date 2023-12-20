using Cinemachine;
using UnityEngine;

namespace collegeGame
{
    public class CameraZoom : MonoBehaviour
    {
        [field: SerializeField][Range(0f, 10f)] private float defaultDistance = 4f;
        [field: SerializeField][Range(0f, 10f)] private float minimuDistance = 2f;
        [field: SerializeField][Range(0f, 10f)] private float maximumDistance = 6f;

        [field: SerializeField][Range(0f, 10f)] private float smoothing = 4f;
        [field: SerializeField][Range(0f, 10f)] private float zoomSensitivity = 1f;

        private CinemachineFramingTransposer framingTransposer;
        private CinemachineInputProvider inputProvider;
        private float currentTargetDistance;

        private void Awake()
        {
            framingTransposer = GetComponent<CinemachineVirtualCamera>()
                .GetCinemachineComponent<CinemachineFramingTransposer>();
            inputProvider = GetComponent<CinemachineInputProvider>();
            currentTargetDistance = defaultDistance;
        }

        private void Update()
        {
            Zoom();
        }
        private void Zoom()
        {
            float ZoomValue = inputProvider.GetAxisValue(2) * zoomSensitivity;
            currentTargetDistance = Mathf.Clamp(currentTargetDistance + ZoomValue,minimuDistance,maximumDistance);
            float currentDistance = framingTransposer.m_CameraDistance;

            if(currentDistance == currentTargetDistance)
            {
                return;
            }
            float lerpedZoomValue = Mathf.Lerp(currentDistance,currentTargetDistance,smoothing * Time.deltaTime);
            framingTransposer.m_CameraDistance = lerpedZoomValue;
        }
    }
}
