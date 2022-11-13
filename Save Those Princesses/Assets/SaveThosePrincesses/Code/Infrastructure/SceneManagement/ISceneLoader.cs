using System.Collections;

namespace SaveThosePrincesses.Infrastructure.SceneManagement
{
    public interface ISceneLoader
    {
        bool IsLoaded(SceneType sceneType);
        IEnumerator Load(SceneType sceneType);
        IEnumerator Unload(SceneType sceneType);
        IEnumerator Load(Environment environment);
        IEnumerator Unload(Environment environment);
    }
}