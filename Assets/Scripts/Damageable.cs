using UnityEngine;

namespace collegeGame
{
    public class Damageable : MonoBehaviour
    {
        [field: SerializeField] private float _health;
        public void DamageTake(float damage)
        {
            if (_health < 0)
            {
                return;
            }
            _health -= damage;
        }
        public float GetHealth()
        {
            return _health;
        }
        private void Awake()
        {
            Debug.Log(_health);
        }
    }
}
