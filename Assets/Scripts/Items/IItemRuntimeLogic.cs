
using System;
using UnityEngine;

namespace Game.Item
{
    public interface IItemRuntimeLogic : IDisposable
    {
        int Id { get; }
        string Name { get; }
        string Description { get; }
        Sprite Icon { get; }
    }
}