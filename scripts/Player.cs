using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Player : CharacterBody2D
{
	public const float JumpVelocity = -400.0f;
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	[Export]
	public string Id { get; set; }
	[Export]
	public float Speed { get; set; } = 300.0f;
	[Export]
	public int Strength { get; set; }
	[Export]
	public int Health { get; set; }
	[Export]
	public int Stamina { get; set; }
	[Export]
	public int Energy { get; set; }
	[Export]
	public int Defense { get; set; }
	[Export]
	public bool IsTurn { get; set; } = false;
	[Export]
	public bool TurnOver { get; set; } = false;
	[Export]
	public bool IsPlayer { get; set; }
	public int NumberOfSkillsUnlock = 3;
	[Export]
	public Node2D skillListContainer;

	public Dictionary<int, ClickableItem> clickableItems = new Dictionary<int, ClickableItem>();
	public string GetId()
	{
		return Id;
	}

	public int GetDamage()
	{
		return Strength * Stamina;
	}

	public void SetHealth(int value)
	{
		Health = value;
	}

	public int GetHealth()
	{
		return Health;
	}

	public void SetSpeed(int value)
	{
		Speed = value;
	}

	public float GetSpeed()
	{
		return Speed;
	}

	public void SetEnergy(int value)
	{
		Energy = value;
	}
	public int GetEnergy()
	{
		return Energy;
	}

	public void SetDefense(int value)
	{
		Defense = value;
	}

	public int GetDefense()
	{
		return Defense;
	}

	public void SetIsTurn(bool value)
	{
		IsTurn = value;
	}

	public bool GetTurn()
	{
		return IsTurn;
	}

	public void SetTurnOver(bool value)
	{
		TurnOver = value;
	}

	public bool GetTurnOver()
	{
		return TurnOver;
	}

    public override void _Ready()
    {
		GD.Print("started");
		var test = skillListContainer.GetChildren();
		GD.Print(test[0].Name);
		foreach(var t in test)
		{
			var item = t as ClickableItem;
			clickableItems.Add(item.skillPosition, item);
            GD.Print(item.skillPosition);

        }
		clickableItems.TryGetValue(3, out ClickableItem value);
		value.Visible = true;
    }

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
		//GenerateClickableItems();
		for(var i = 0; i < NumberOfSkillsUnlock; i++)
		{
			clickableItems.TryGetValue(i, out ClickableItem value);
			value.Visible = true;
		}
	}

	private void OnMouseExit()
	{
        for (var i = 0; i < NumberOfSkillsUnlock; i++)
        {
            clickableItems.TryGetValue(i, out ClickableItem value);
            value.Visible = false;
        }
    }

	[Export] public PackedScene ClickableItemScene;
	[Export] public int NumberOfItems = 11;
	[Export] public float CircleRadius = 500.0f;
	public Dictionary<int, ClickableItem> clickableItemsDic = new Dictionary<int, ClickableItem>();

	public void GenerateClickableItems()
	{
		for (int i = 1; i <= NumberOfItems; i++)
		{
			float angle = 12 * i;
			Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * CircleRadius;
			// Instance the clickable item scene
			ClickableItem clickableItem = ClickableItemScene.Instantiate<ClickableItem>();
			clickableItem.Name = "skill_" + 1.ToString();
			clickableItem.Position = -offset;
			clickableItem.Scale = new Vector2(2, 2);

			AddChild(clickableItem);
			clickableItemsDic.Add(i, clickableItem);
		}
	}

	public class SkillItem
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public float Damage { get; set; }
	}

}
