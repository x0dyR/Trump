using UnityEngine;

namespace collegeGame
{
    public class Weapon : MonoBehaviour
    {
        /*public Animator animator;*/
        public float damage;
        public BoxCollider boxCollider;
        /*
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        public virtual void WeaponAttack()
        {
            animator.SetTrigger("Attack");
        }*/
        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider>();
        }
        public void Attack(LayerMask damageableLayer)
        {
            Collider[] colliderArray = Physics.OverlapBox(transform.position, boxCollider.size, Quaternion.identity, damageableLayer);
            foreach (Collider coll in colliderArray)
            {
                coll.TryGetComponent(out Damageable damageable);
                damageable.DamageTake(damage);
            }
        }
    }
}
