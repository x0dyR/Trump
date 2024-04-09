using UnityEngine;

namespace collegeGame
{
    public class Spear : Weapon // ����������� �� Weapon ��� ����� ������ ����� �����
    {
        Vector3 weaponCenter;
        Vector3 weaponRadius;

        public override void Attack(LayerMask damageableLayer)
        {
            weaponCenter = transform.TransformPoint(boxCollider.center);
            weaponRadius = boxCollider.bounds.size;
            Collider[] colliders = Physics.OverlapBox(weaponCenter, weaponRadius, transform.rotation, damageableLayer); // ��������� ������� � ������������ � ������� �������
            foreach (Collider coll in colliders) // ���������� �� ���� �����������
            {
                coll.TryGetComponent(out Damageable damageable); // ��������� ������� ���������� � ���������� � ������� �� �����������
                damageable.DamageTake(damage); // ������� ���� �������� � ����������� Damageable
            }
        }
        private void OnDrawGizmos() // ��� ������������ ������� � ������� ��������� ����(������ � ���� "Scene"!!!)
        {
            weaponCenter = transform.TransformPoint(boxCollider.center);
            weaponRadius = boxCollider.bounds.size;
            Gizmos.DrawWireCube(weaponCenter, weaponRadius);
        }
    }
}
