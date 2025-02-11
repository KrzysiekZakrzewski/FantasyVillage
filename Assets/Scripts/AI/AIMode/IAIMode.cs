using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Factory;

namespace BlueRacconGames.AI
{
    public interface IAIMode
    {
        AIControllerBase AIController {  get; }
        float MoveSpeed { get; }
        float ReachDestinationDistance { get; }
        bool IsSimulated {  get; }

        void Initialize(AIControllerBase aIController, EnemyAIDataBaseSO enemyAIDataBaseSO);
        void Update();
        void OnDestory();
        bool CanChangeMode(out IAIModeFactory modeFactory);
        void OnStartWonder();
        void OnEndWonder();
        void StartSimulate();
        void StopSimulate();
    }
}