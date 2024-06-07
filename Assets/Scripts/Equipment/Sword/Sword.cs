using System;
using UnityEngine;

namespace Trump
{
    public class Sword : MonoBehaviour, IWeapon
    {
        public WeaponSO weaponSO;

        public event Action WeaponAttack;

        public void Attack()
        {
            WeaponAttack?.Invoke();
            Collider[] colls = Physics.OverlapBox(transform.position, weaponSO.BoxCollider.bounds.size, transform.rotation);
            foreach (Collider coll in colls)
            {

                if (coll.TryGetComponent(out IHealth target))
                    if (coll.TryGetComponent(out AbsEnemy enemy))
                    {
                        Debug.Log("Find enemy!!!");
                        target.TakeDamage(weaponSO.Damage);
                    }
            }
        }
    }
}
