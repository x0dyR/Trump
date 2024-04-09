using UnityEditor.Animations;
using UnityEngine;

namespace collegeGame
{
    public class Weapon : MonoBehaviour
    {
        [field:SerializeField] protected float damage;
        private float _rare;
        private float _subStat;
        private string _subStatName;
        public string weaponName;
        public BoxCollider boxCollider;
        public BlendTree weaponBlendTree;
        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider>();
        }
        public float GetWeaponDamage()
        {
            return damage;
        }
        public virtual void Attack(LayerMask damageableLayer) { } // делаем virtual чтобы можно было менять под каждый тип оружия
    }
}
