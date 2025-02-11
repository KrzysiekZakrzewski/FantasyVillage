using ViewSystem.Implementation;

namespace BlueRacconGames.View
{
    public class GameViewController : SingleViewTypeStackController
    {
        protected override void Awake()
        {
            base.Awake();

            TryOpenSafe<GameHUD>();
        }
    }
}