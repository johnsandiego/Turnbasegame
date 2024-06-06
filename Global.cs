using Godot;
using System;
using System.Collections.Generic;

public partial class Global : Node
{
	public Dictionary<string, Player> Battlers = new Dictionary<string, Player>();
	public void SetBattlers(List<Player> list)
	{
		foreach(Player b in list)
        {
            if (!Battlers.ContainsKey(b.GetId()))
			{
				Battlers.Add(b.GetId(), b);
			}
        }
    }

	public Dictionary<string, Player> GetBattlers()
	{
		return Battlers;
	}

	public void RemoveBattlers()
	{
		Battlers.Clear();
	}
}
