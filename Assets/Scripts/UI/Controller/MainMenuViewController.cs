using ViewSystem.Implementation;
using Game.View;

public class MainMenuViewController : SingleViewTypeStackController
{
    private void Start()
    {
        TryOpenSafe<MainMenuView>();
    }
}
