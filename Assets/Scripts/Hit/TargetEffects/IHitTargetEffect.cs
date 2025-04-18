using BlueRacconGames.Hit;

namespace BlueRacconGames.HitEffect
{
    public interface IHitTargetEffect
    {
        void Execute(HitObjectControllerBase source, IHitTarget target);
    }
}