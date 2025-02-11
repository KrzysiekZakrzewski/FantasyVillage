using BlueRacconGames.AI.Factory;

namespace BlueRacconGames.AI.Implementation
{
    public class HitStunAIMode : AIModeBase
    {
        public HitStunAIMode(AIControllerBase aiController) 
        {
            //Initialize(aiController);
        }
        public override bool CanChangeMode(out IAIModeFactory modeFactory)
        {
            modeFactory = null;

            return false;
        }

        public override void OnEndWonder()
        {
            throw new System.NotImplementedException();
        }

        public override void OnStartWonder()
        {
            throw new System.NotImplementedException();
        }
    }
}
