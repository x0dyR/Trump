using System;

namespace collegeGame
{
    public interface IWeapon
    {
        public event Action WeaponAttack;
        void Attack();
    }
}

