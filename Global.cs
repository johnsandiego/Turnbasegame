using Godot;
using System;
using System.Collections.Generic;

public partial class Global : Node
{
	public Dictionary<string, BaseBattlers> Battlers = new Dictionary<string, BaseBattlers>();
	public void SetBattlers(List<BaseBattlers> list)
	{
		foreach(BaseBattlers b in list)
        {
            if (!Battlers.ContainsKey(b.GetId()))
			{
				Battlers.Add(b.GetId(), b);
			}
        }
    }

	public Dictionary<string, BaseBattlers> GetBattlers()
	{
		return Battlers;
	}

	public void RemoveBattlers()
	{
		Battlers.Clear();
	}
}
