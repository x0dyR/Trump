using System;

namespace Trump.StateMachine
{
    public enum Combo
    {
        none = 0,
        first,
        second,
        third = 3,
    }

    public class StateMachineData
    {
        public float XVelocity;
        public float YVelocity;
        public float ZVelocity;
        public float attackThreshold;

        private float _speed;
        private float _xInput;
        private float _zInput;
        private Combo _currentCombo;

        public float XInput
        {
            get => _xInput;
            set
            {
                if (value < -1 || value > 1)
                    throw new ArgumentOutOfRangeException("xInput invalid value");
                _xInput = value;
            }
        }

        public float ZInput
        {
            get => _zInput;
            set
            {
                if (value < -1 || value > 1)
                    throw new ArgumentOutOfRangeException("zInput invalid value");
                _zInput = value;
            }
        }

        public float Speed
        {
            get => _speed;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Speed cant be below 0");

                _speed = value;
            }
        }

        public Combo CurrentCombo
        {
            get => _currentCombo;
            set
            {
                if (!Enum.IsDefined(typeof(Combo), value))
                    throw new ArgumentOutOfRangeException("Invalid combo value");

                _currentCombo = value;
            }
        }
    }
}
