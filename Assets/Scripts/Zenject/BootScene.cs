using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Trump
{
    public class BootScene : MonoBehaviour
    {
        public event System.Action Click;

        [field: SerializeField] private Button _button;
        private SceneLoadMediator _sceneLoader;

        [Inject]
        private void Construct(SceneLoadMediator sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void OnEnable() => _button.onClick.AddListener(OnClick);
        private void OnDisable() => _button.onClick.RemoveListener(OnClick);

        private void OnClick() => _sceneLoader.GoToGameplayScene();

    }
}
