using TMPro;
using UnityEngine;

namespace collegeGame
{
    public class fpsShowController : MonoBehaviour
    {
        // Start is called before the first frame update
        public float fps;
        public float frameTime = 0.2f;
        [field: SerializeField] public TextMeshProUGUI fpsTitle;

        private void UpdateFPS()
        {
            frameTime -= Time.realtimeSinceStartup;
            if (frameTime <= 0f)
            {
                fps = 1f / Time.unscaledDeltaTime;
                fpsTitle.text = "FPS:" + Mathf.Round(fps);
                frameTime = 0.2f;
            }
        }
        void Update()
        {
            UpdateFPS();
        }
    }
}