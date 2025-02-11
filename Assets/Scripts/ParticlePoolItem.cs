using UnityEngine;
using Zenject;

namespace BlueRacconGames.Pool
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticlePoolItem : PoolItemBase
    {
        private ParticleSystem system;

        private void Awake()
        {
            system = GetComponent<ParticleSystem>();
            var main = system.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }

        private void OnParticleSystemStopped()
        {
            Expire();
        }
    }
}