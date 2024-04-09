using collegeGame.Inputs;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace collegeGame
{
    public class Damageable : MonoBehaviour
    {
        [field: SerializeField] private float _health;
        public Slider slider;
        public Transform canvasPrefab;
        public Transform canvasPoint;
        private void Initialize()
        {
            canvasPrefab = Instantiate(canvasPrefab, canvasPoint, false);
            slider = canvasPrefab.GetComponentInChildren<Slider>();
            slider.maxValue = GetHealth();
            slider.value = GetHealth();
        }
        private void Awake()
        {
            if (TryGetComponent(out ThirdPersonController _))
            {
                canvasPrefab = GetComponentInChildren<Canvas>().transform;
                slider = GetComponentInChildren<Slider>();
                slider.maxValue = GetHealth();
                slider.value = GetHealth();
            }
            else
            {
                Initialize();
            }
        }
        public void RotateCanvas()
        {
            if (TryGetComponent(out ThirdPersonController _)) return;
            canvasPrefab.DOLookAt(Camera.main.transform.position, 0); // чтобы полоса здоровья пола воернута к игроку
        }

        private void LateUpdate()
        {
            RotateCanvas();
        }
        public virtual void DamageTake(float damage)
        {
            if (_health <= damage)
            {
                Destroy(gameObject);
                canvasPrefab.DOPause();
            }
            _health -= damage;
            slider.value = GetHealth();
        }
        public float GetHealth()
        {
            return _health;
        }
    }
}
