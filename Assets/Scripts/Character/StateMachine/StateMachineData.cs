using System;

namespace collegeGame.StateMachine
{
    public class StateMachineData
    {
        public float XVelocity;
        public float YVelocity;
        public float ZVelocity;

        private float _speed;
        private float _xInput;
        private float _yInput;
        private float _zInput;

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
    }
}
