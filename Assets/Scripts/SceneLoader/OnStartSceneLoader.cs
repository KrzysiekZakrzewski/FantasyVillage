namespace Game.SceneLoader
{
    public class OnStartSceneLoader : BaseSceneLoader
    {
        private void Start()
        {
            LoadScene();

            Destroy(gameObject);
        }
    }
}