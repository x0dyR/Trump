using System;

namespace Trump
{
    public interface IWeapon
    {
        public event Action WeaponAttack;
        void Attack();
    }
}

