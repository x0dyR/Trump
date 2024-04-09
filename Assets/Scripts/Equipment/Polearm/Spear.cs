using UnityEngine;

namespace collegeGame
{
    public class Spear : Weapon // наследуемся от Weapon что после менять метод атаки
    {
        Vector3 weaponCenter;
        Vector3 weaponRadius;

        public override void Attack(LayerMask damageableLayer)
        {
            weaponCenter = transform.TransformPoint(boxCollider.center);
            weaponRadius = boxCollider.bounds.size;
            Collider[] colliders = Physics.OverlapBox(weaponCenter, weaponRadius, transform.rotation, damageableLayer); // проверяем объекты с коллайдерами в заданой области
            foreach (Collider coll in colliders) // проходимся по всем коллайдерам
            {
                coll.TryGetComponent(out Damageable damageable); // проверяем наличие компонента у коллайдера с которым мы столкнулись
                damageable.DamageTake(damage); // наносим урон объектам с компонентом Damageable
            }
        }
        private void OnDrawGizmos() // для визуализации области в которой наносится урон(только в окне "Scene"!!!)
        {
            weaponCenter = transform.TransformPoint(boxCollider.center);
            weaponRadius = boxCollider.bounds.size;
            Gizmos.DrawWireCube(weaponCenter, weaponRadius);
        }
    }
}
