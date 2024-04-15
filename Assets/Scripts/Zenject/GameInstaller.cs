using Cinemachine;
using collegeGame.Inputs;
using UnityEngine;
using Zenject;

namespace collegeGame
{
    public class GameInstaller : MonoInstaller
    {
        public Transform playerSpawnPos;
        public GameObject playerPrefab;

        public CinemachineVirtualCamera cm;

        public override void InstallBindings()
        {
            BindPlayer();
            BindCM();
        }

        private void BindCM()
        {
            Container.Bind<CinemachineVirtualCamera>().FromInstance(cm).AsSingle();
          /*Container.Bind<CMFollowPlayer>().AsSingle();*/
        }

        private void BindPlayer()
        {
            var player = Container.InstantiatePrefabForComponent<ThirdPersonController>(playerPrefab, playerSpawnPos.position, Quaternion.identity, null);
            Container.BindInterfacesAndSelfTo<ThirdPersonController>().FromInstance(player).AsSingle();
/*            Container.BindInterfacesAndSelfTo<Damageable>().FromInstance(player).AsSingle();*/
        }
    }
}
