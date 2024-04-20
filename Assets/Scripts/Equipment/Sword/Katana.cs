using UnityEngine;
using Zenject;

namespace collegeGame
{
    public class Katana : Weapon
    {
        private void OnValidate()
        {
            if (damage < 0)
            {
                damage = 1;
            }
        }

        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider>();
        }

        public override void Attack()
        {
            Collider[] colls = Physics.OverlapBox(transform.position, boxCollider.bounds.size, transform.rotation);
            foreach (Collider coll in colls)
            {
                if (coll.TryGetComponent(out ITarget tranform))
                    return;
                Debug.Log("Find enemy!!!");
                if (coll.TryGetComponent(out IHealth target))
                    target.TakeDamage(damage);
            }
        }
    }
}
