using collegeGame.StateMachine;
using UnityEngine;

namespace collegeGame
{
    public class HealthMediator : MonoBehaviour
    {
        [field: SerializeField] private Character _character;
        [field: SerializeField] private CharacterHealthBar _characterHpBar;

        private void OnEnable() 
        {
            _character.HealthChanged += OnChangeHealth;
        } 
        
        private void OnDisable()
        {
            _character.HealthChanged -= OnChangeHealth;
        }

        private void Awake()
        {
            _characterHpBar.HealthSlider.value = _characterHpBar.HealthSlider.maxValue = _character.GetHealth();
        }

        private void OnChangeHealth()
        {
            _characterHpBar.ChangeValue(_character);
        }
    }
}
