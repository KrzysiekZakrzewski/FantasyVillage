
using UnityEngine;

namespace Game.Item
{
    public interface IItemRuntimeLogic
    {
        int Id { get; }
        string Name { get; }
        string Description { get; }
        Sprite Icon { get; }

        bool CanUse();
        bool Use();
    }
}