namespace Trump
{
    public class SceneLoadMediator
    {
        private ISimpleSceneLoader _simpleSceneLoader;

        public SceneLoadMediator(ISimpleSceneLoader sceneLoader)
        {
            _simpleSceneLoader = sceneLoader;
        }

        public void GoToGameplayScene() => _simpleSceneLoader.Load(SceneID.GameScene);

        public void GoToBootScene() => _simpleSceneLoader.Load(SceneID.BootScene);
    }
}