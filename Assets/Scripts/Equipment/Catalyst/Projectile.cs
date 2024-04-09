using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace collegeGame
{
    public class Projectile : MonoBehaviour
    {
        private ObjectPool<Projectile> _pool;
        public BoxCollider projectileCollider;
        public Rigidbody projectileRigidbody;
        private float damage;
        private void Awake()
        {
            projectileCollider = GetComponent<BoxCollider>();
            projectileRigidbody = GetComponent<Rigidbody>();
        }
        public void SetPool(ObjectPool<Projectile> pool)
        {
            _pool = pool;
        }
        public void CheckColliders(LayerMask damageableLayer, float damage)
        {
            Collider[] colliderArray = Physics.OverlapBox(projectileCollider.center + transform.position, projectileCollider.bounds.extents, Quaternion.identity, damageableLayer);
            foreach (Collider collider in colliderArray)
            {
                collider.TryGetComponent(out Damageable damageable);
                damageable.DamageTake(damage); _pool.Release(this);
            }

        }
        public void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(projectileCollider.center + transform.position, projectileCollider.bounds.size);
        }
    }
}
