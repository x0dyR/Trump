using UnityEngine;

namespace collegeGame
{
    public class Catalyst : Weapon // все объяснения в Sword/Spear.cs
    {
        Vector3 weaponRadius;
        Vector3 weaponCenter;

        public override void Attack(LayerMask damageableLayer)
        {
            weaponCenter = transform.TransformPoint(boxCollider.center);
            weaponRadius = boxCollider.bounds.size;
            Collider[] colliders = Physics.OverlapBox(weaponCenter, weaponRadius, transform.rotation, damageableLayer);
            if (colliders.Length > 0)
            {
                colliders[0].TryGetComponent(out Damageable damageable);
                damageable.DamageTake(damage);
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            weaponCenter = transform.TransformPoint(boxCollider.center);
            weaponRadius = boxCollider.bounds.size;
            Gizmos.DrawWireCube(weaponCenter, weaponRadius);
        }
    }
}
