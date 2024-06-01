using Godot;
using System;

public partial class BaseBattlers : Node2D
{
    [Export]
    public string Id { get; set; }
    [Export]
    public int Speed { get; set; }
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

    public int GetSpeed()
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
}
