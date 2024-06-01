using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TurnQueue : Node2D
{
    private Dictionary<string, BaseBattlers> currentBattlers;
    public Timer timer;
    [Export]
    public BaseBattlers battler1;
    [Export]
    public BaseBattlers battler2;
    [Export]
    public BaseBattlers battler3;
    [Export] public BaseBattlers enemy;
    public IEnumerable<BaseBattlers> battlers;
    public bool turnOver = false;
    public Vector2 previousPosition;
    private float _t = 0.0f;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var global = GetNode<Global>("/root/GlobalVariables");
        List<BaseBattlers> baseBattlers = new List<BaseBattlers>
        {
            battler1,
            battler2,
            battler3
        };
        global.SetBattlers(baseBattlers);
        currentBattlers = global.GetBattlers();
        timer = new Timer();
        AddChild(timer);
        timer.WaitTime = 2;
        timer.Start();
        timer.Timeout += TurnOver;

        battlers = currentBattlers.Values;
        var currentBattler = GetCurrentBattler();
        currentBattler.SetIsTurn(true);
        PlayTurn(currentBattler, 1);
        previousPosition = battler1.Position;

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        GD.Print(timer.TimeLeft);
        //if (turnOver)
        //{
        //    var currentBattler = GetCurrentBattler();
        //    currentBattler.SetIsTurn(false);
        //    currentBattler.SetTurnOver(true);
        //    currentBattler.Position = currentBattler.Position.Lerp(previousPosition, .4f * (float)delta);
        //    currentBattler.Modulate = Color.Color8(255, 255, 255, 255);
        //    currentBattler = GetCurrentBattler();
        //    if (currentBattler != null)
        //    {
        //        currentBattler.SetIsTurn(true);
        //        PlayTurn(currentBattler, (float)delta);
        //        timer.Stop();
        //        timer.WaitTime = 2;
        //        timer.Start();
        //        turnOver = false;
        //    }
        //    else
        //    {
        //        timer.Stop();
        //        turnOver = false;
        //    }
        //}
        _t += .08f * (float)delta;
        battler1.Position = battler1.Position.Lerp(battler2.Position, _t);
        battler1.Position = battler1.Position.Lerp(previousPosition, _t);

    }

    private void TurnOver()
    {
        turnOver = true;
    }

    private BaseBattlers GetCurrentBattler()
    {
        return battlers.OrderBy(b => b.Speed).FirstOrDefault(x => !x.TurnOver);


    }

    private void PlayTurn(BaseBattlers currentBattler, float delta)
    {
        //determine who goes

        if (currentBattler != null)
        {
            currentBattler.Modulate = Color.Color8(255, 255, 0, 255);
            previousPosition = currentBattler.Position;
            currentBattler.Position = currentBattler.Position.Lerp(enemy.Position, .4f * delta);
        }





        //if(timer.TimeLeft)
    }

    private List<BaseBattlers> GetBattlers()
    {
        return new List<BaseBattlers>();
    }
}
