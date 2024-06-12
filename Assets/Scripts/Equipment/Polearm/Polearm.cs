using System;
using UnityEngine;
using Collider = UnityEngine.Collider;

namespace Trump
{
    public class Polearm : MonoBehaviour, IWeapon
    {
        public event Action WeaponAttack;
        [field: SerializeField] private WeaponSO weaponSO;

        public void Attack()
        {
            WeaponAttack?.Invoke();
            Collider[] colls = Physics.OverlapBox(transform.position, weaponSO.BoxCollider.bounds.size, transform.rotation);
            foreach (Collider coll in colls)
            {
                if (coll.TryGetComponent(out ITarget tranform))
                    return;
                Debug.Log("Find enemy!!!");
                if (coll.TryGetComponent(out IHealth target))
                    target.TakeDamage(weaponSO.Damage);
            }
        }
    }
}
