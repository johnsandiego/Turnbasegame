using Godot;
using System;
using System.Collections.Generic;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
    [Export] public PackedScene ClickableItemScene;
    [Export] public int NumberOfItems = 10;
    [Export] public float CircleRadius = 100.0f;
	Dictionary<int, ClickableItem> clickableItemsDic = new Dictionary<int, ClickableItem>();
    // Get the gravity from the project settings to be synced with RigidBody nodes.
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	private void OnMouseEntered()
	{
		GD.Print("mouse entered");
		GenerateClickableItems();

    }

	private void OnMouseExit()
	{
        foreach(var item in clickableItemsDic.Values)
		{
			item.QueueFree();

        }
		clickableItemsDic.Clear();

    }

    private void GenerateClickableItems()
    {
        for (int i = 1; i < NumberOfItems; i++)
        {
            float angle = (float)(i * Math.PI);

            float x =  CircleRadius * (float)Math.Sin(angle); Mathf.DegToRad
            float y =  CircleRadius * (float)Math.Cos(angle);
            GD.Print("x: ", x);
            GD.Print("y: ", y);
            Vector2 position = new Vector2(x, y) + GlobalPosition;
			GD.Print("position: " + position);
            // Instance the clickable item scene
            ClickableItem clickableItem = ClickableItemScene.Instantiate<ClickableItem>();
            clickableItem.GlobalPosition = position;
			clickableItem.Scale = new Vector2(2, 2);


            AddChild(clickableItem);
            clickableItemsDic.Add(i, clickableItem);
        }
    }
}
