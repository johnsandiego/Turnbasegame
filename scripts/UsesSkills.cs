using Godot;
using System;
using System.Collections.Generic;

public partial class UsesSkills : CharacterBody2D
{
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
