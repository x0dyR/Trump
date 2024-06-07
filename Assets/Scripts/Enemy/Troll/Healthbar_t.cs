using UnityEngine;
using UnityEngine.UI;

namespace Trump
{
    public class Healthbar_t : MonoBehaviour
    {
        [SerializeField] private Image _backSprite;
        [SerializeField] private EnemyConfig _trollConfig; // Изменено на TrollConfig
        [SerializeField] private float _reduceSpeed = 2;
        private float _target = 2;
        private Camera _cam;

        void Start()
        {
            _cam = Camera.main;
        }

        public void UpdateHealthBar(float currentHealth)
        {
            _target = currentHealth / _trollConfig.Health; // Изменено на _trollConfig.Health
        }

        void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
            _backSprite.fillAmount = Mathf.MoveTowards(_backSprite.fillAmount, _target, _reduceSpeed * Time.deltaTime);
        }
    }
}
