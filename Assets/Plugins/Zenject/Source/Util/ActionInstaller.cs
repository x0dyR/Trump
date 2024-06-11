using System;

namespace Zenject
{
    public class Action : Installer<Action>
    {
        readonly Action<DiContainer> _installMethod;

        public Action(Action<DiContainer> installMethod)
        {
            _installMethod = installMethod;
        }

        public override void InstallBindings()
        {
            _installMethod(Container);
        }
    }
}
