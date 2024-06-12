//using System;
//using UnityEngine;

//namespace Trump
//{
//    public class Sword : MonoBehaviour, IWeapon
//    {
//        public WeaponSO weaponSO;

//        public event Action WeaponAttack;

//        public void Attack()
//        {
//            WeaponAttack?.Invoke();
//            Collider[] colls = Physics.OverlapBox(transform.position, weaponSO.BoxCollider.bounds.size, transform.rotation);
//            foreach (Collider coll in colls)
//            {

//                if (coll.TryGetComponent(out IHealth target))
//                    if (coll.TryGetComponent(out AbsEnemy enemy))
//                    {
//                        Debug.Log("Find enemy!!!");
//                        target.TakeDamage(weaponSO.Damage);
//                    }
//            }
//        }
//    }
//}
using System;
using UnityEngine;

namespace Trump
{
    public class Sword : MonoBehaviour, IWeapon
    {
        public WeaponSO weaponSO;

        public event Action WeaponAttack;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"Collision detected with: {other.name}");
            if (other.TryGetComponent(out IHealth target))
            {
                if (other.TryGetComponent(out AbsEnemy enemy))
                {
                    Debug.Log($"Find enemy: {other.name}");
                    target.TakeDamage(weaponSO.Damage);
                    Debug.Log($"Damage dealt: {weaponSO.Damage} to {other.name}");
                }
            }
        }

        public void Attack()
        {
            WeaponAttack?.Invoke();
        }
    }
}


