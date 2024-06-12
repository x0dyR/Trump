using UnityEngine;
using UnityEngine.UI;

namespace Trump
{
    [RequireComponent(typeof(Slider))]
    public class CharacterHealthBar : MonoBehaviour
    {
        [field:SerializeField] private Slider _healthSlider;

        public Slider HealthSlider => _healthSlider;

        private void Awake()
        {
            _healthSlider = GetComponent<Slider>();
        }

        public void ChangeValue(IHealth health)
        {
            _healthSlider.value = health.GetHealth();
        }
    }
}
