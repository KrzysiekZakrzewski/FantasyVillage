using UnityEngine;

namespace Cheats.Console.Command
{
    public abstract class DebugCommandFactorySO : ScriptableObject, IDebugCommandFactory
    {
        [SerializeField] protected string id;
        [SerializeField] protected string description;
        [SerializeField] protected string format;

        public abstract IDebugCammand CreateCommand();
    }
}
