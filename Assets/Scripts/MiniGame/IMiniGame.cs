namespace BlueRacconGames.MiniGame
{
    public interface IMiniGame
    {
        bool MiniGameEnded { get; }
        bool Result {  get; }
        bool IsGameReadyToPlay { get; }
        void SetupGame(MiniGameController miniGameController);
        void StartGame();
        void EndMiniGame(bool result);
    }
}