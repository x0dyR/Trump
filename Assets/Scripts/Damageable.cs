using UnityEngine;

namespace collegeGame
{
    public class Damageable : MonoBehaviour
    {
        [field: SerializeField] private float _health;
        public void DamageTake(float damage)
        {
            if (_health < damage)
            {
                Destroy(gameObject);
            }
            _health -= damage;
        }
        public float GetHealth()
        {
            return _health;
        }
    }
}
