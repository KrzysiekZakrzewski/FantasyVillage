using System;
using System.Collections.Generic;

public class SavesEvent
{
    private readonly Dictionary<string, Action> anonymous = new Dictionary<string, Action>();
    private event Action Action = delegate { };

    public void Raise()
    {
        Action?.Invoke();
    }

    public void Add(Action listener)
    {
        Action -= listener;
        Action += listener;
    }
    public void Remove(Action listener)
    {
        Action -= listener;
    }
    public void Clear()
    {
        anonymous.Clear();
        Action = null;
    }
}
