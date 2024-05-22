using System;
using UnityEngine;

namespace collegeGame
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
                if (coll.TryGetComponent(out ITarget tranform))
                    return;
                Debug.Log("Find enemy!!!");
                if (coll.TryGetComponent(out IHealth target))
                    target.TakeDamage(weaponSO.Damage);
            }
        }
    }
}
