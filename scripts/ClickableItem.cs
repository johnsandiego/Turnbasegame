using Godot;
using System;

public partial class ClickableItem : Node2D
{
	[Export]
	public Sprite2D skillImage;
	[Export]
	public CompressedTexture2D texture;
	[Export]
	public CompressedTexture2D texture2;
	[Export]
	public CompressedTexture2D texture3;
	[Signal]
	public delegate void PerformedActionEventHandler();
	[Export]
	public int skillPosition;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		switch (new Random().Next(1, 3))
		{
			case 1:
				skillImage.Texture = texture;
				break;
			case 2:
				skillImage.Texture = texture2;
				break;
			case 3:
				skillImage.Texture = texture3;
				break;

		}

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnMouseEntered()
	{
		//GD.Print("hovering on skill");
	}

	private void OnButtonPressed()
	{
		GD.Print("pressed on skill");
		EmitSignal(SignalName.PerformedAction);

	}
}
