namespace BlueRacconGames.MiniGame
{
    public abstract class MiniGameBase : IMiniGame
    {
        protected MiniGameController miniGameController;
        protected bool miniGameEnded;
        protected bool result;
        protected bool isGameReadyToPlay;

        public bool MiniGameEnded => miniGameEnded;
        public bool Result => result;
        public bool IsGameReadyToPlay => isGameReadyToPlay;

        public virtual void SetupGame(MiniGameController miniGameController)
        {
            this.miniGameController = miniGameController;
        }

        public abstract void StartGame();

        public abstract void EndMiniGame(bool result);
    }
}
