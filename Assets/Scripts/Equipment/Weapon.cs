using UnityEngine;

namespace collegeGame
{
    public abstract class Weapon : MonoBehaviour
    {
        public float damage;
        public float rare;
        public float subStat;
        public string subStatName;
        public string weaponName;
        public BoxCollider boxCollider;
        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider>();
        }
        public abstract void Attack();// ������ abstract ����� ���� ������������ ��� ������ ������
    }
}

