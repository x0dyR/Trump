namespace Trump
{
    public class SceneLoader : ISimpleSceneLoader
    {
        private ZenjectSceneLoaderWrapper _zenjectSceneLoader;

        public SceneLoader(ZenjectSceneLoaderWrapper zenjectSceneLoader)
        {
            _zenjectSceneLoader = zenjectSceneLoader;
        }


        public void Load(SceneID sceneID)
        {
            _zenjectSceneLoader.Load((int)sceneID);
        }
    }
}