using Godot;
using System;
using System.Collections.Generic;

public partial class Enemy : UsesSkills
{
    [Export] public PackedScene ClickableItemScene;
    [Export] public int NumberOfItems = 11;
    [Export] public float CircleRadius = 500.0f;
    public Dictionary<int, ClickableItem> clickableItemsDic = new Dictionary<int, ClickableItem>();
    [Export]
    public Node2D skillListContainer;

    public Dictionary<int, ClickableItem> clickableItems = new Dictionary<int, ClickableItem>();
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
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Print("started");
        var test = skillListContainer.GetChildren();
        GD.Print(test[0].Name);
        foreach (var t in test)
        {
            var item = t as ClickableItem;
            clickableItems.Add(item.skillPosition, item);
            GD.Print(item.skillPosition);

        }
        clickableItems.TryGetValue(3, out ClickableItem value);
        value.Visible = true;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnMouseEntered()
	{
       // GenerateClickableItems();
       
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
