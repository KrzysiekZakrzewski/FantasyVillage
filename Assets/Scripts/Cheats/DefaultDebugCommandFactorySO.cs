using UnityEngine;

namespace Cheats.Console.Command
{
    [CreateAssetMenu(fileName = nameof(DefaultDebugCommandFactorySO), menuName = nameof(Cheats.Console) + "/" + nameof(Command) + "/" + nameof(DefaultDebugCommandFactorySO))]

    class DefaultDebugCommandFactorySO : DebugCommandFactorySO
    {
        public override IDebugCammand CreateCommand()
        {
            return new DebugCommand(id, description, format, null);
        }
    }
}
