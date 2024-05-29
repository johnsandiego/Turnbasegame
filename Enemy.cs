using Godot;
using System;

public partial class Enemy : UsesSkills
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnMouseEntered()
	{
        GenerateClickableItems();
    }

    private void OnMouseExit()
    {
        foreach (var item in clickableItemsDic.Values)
        {
            item.QueueFree();

        }
        clickableItemsDic.Clear();

    }
}
