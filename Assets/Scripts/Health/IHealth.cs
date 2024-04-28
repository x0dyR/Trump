using System;

namespace collegeGame
{
    public interface IHealth
    {
        void TakeDamage(float damage);
        float GetHealth();
    }
}
