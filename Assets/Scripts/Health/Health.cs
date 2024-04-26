using System;

namespace collegeGame.Health
{
    [Serializable]
    public class Health
    {
        private float _maxHealth;
        private float _health;
        public event Action Die;
        public Health(float health)
        {
            _maxHealth = health;
            _health = _maxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (_health < damage)
            {
                Die?.Invoke();
            }

            _health -= damage;
        }

        public float HealthAmount => _health;
    }
}
