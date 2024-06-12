using System;

namespace Trump
{
    public interface IHealth
    {
        void TakeDamage(float damage);

        float GetHealth();

        void Heal(float heal);

        event Action HealthChanged;

        event Action Died;
    }
}
