using Cinemachine;
using collegeGame.StateMachine;
using UnityEngine;
using Zenject;

namespace collegeGame.Zenject
{
    public class GameInstaller : MonoInstaller
    {
        public Transform playerSpawnPos;
        public GameObject playerPrefab;

        public CinemachineVirtualCamera cm;

        public override void InstallBindings()
        {
            BindPlayer();
            BindCM(); Container.BindInterfacesAndSelfTo<Troll>().AsSingle();
        }

        private void BindCM()
        {
            Container.Bind<CinemachineVirtualCamera>().FromInstance(cm).AsSingle();
        }

        private void BindPlayer()
        {
            var player = Container.InstantiatePrefabForComponent<Character>(playerPrefab, playerSpawnPos.position, Quaternion.identity, null);
            Container.BindInterfacesAndSelfTo<Character>().FromInstance(player).AsSingle();
        }
    }
}
