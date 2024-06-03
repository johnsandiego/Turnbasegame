using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TurnQueue : Node2D
{
    private Dictionary<string, BaseBattlers> currentBattlers;
    private Timer timer;
    private BaseBattlers currentBattler;
    private Vector2 previousPosition;
    private float _t = 0.0f;

    [Export] public BaseBattlers battler1;
    [Export] public BaseBattlers battler2;
    [Export] public BaseBattlers battler3;
    [Export] public BaseBattlers enemy;
    public bool movingToEnemy;
    public IEnumerable<BaseBattlers> Battlers => currentBattlers.Values;
    public bool TurnOver { get; private set; } = false;
    [Export] public float amplitude = 50.0f;
    [Export] public float frequency = 1.0f;
    public override void _Ready()
    {
        InitializeBattlers();
        SetupTimer();
        currentBattler = GetNextBattler();
        previousPosition = currentBattler.Position;

        currentBattler.Modulate = new Color(1, 1, 0, 1);
        currentBattler.Position = currentBattler.Position.Lerp(previousPosition, 0.4f);
        //StartNextTurn(1);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (movingToEnemy)
        {
            MovePlayerInFrontOfEnemy(currentBattler, enemy, (float)delta);
        } else
        {
            MovePlayerBack(currentBattler, previousPosition, (float)delta);
        }
        //if (TurnOver)
        //{
        //    EndCurrentTurn(delta);
        //    StartNextTurn(delta);
        //}
        //if (TurnOver)
        //{
        //    if (currentBattler != null)
        //    {
        //        currentBattler.Position = currentBattler.Position.Lerp(previousPosition, 0.4f * (float)delta);
        //        currentBattler.SetTurnOver(true);
        //        currentBattler = GetNextBattler();
        //        TurnOver = false;
        //        SetupTimer();
        //    }
        //    else
        //    {
        //        if (timer.TimeLeft > 0)
        //        {
        //            MovePlayerInFrontOfEnemy(currentBattler, enemy, (float)delta);
        //        }
        //    }
        //}
        //else
        //{

        //}
    }

    private void InitializeBattlers()
    {
        var global = GetNode<Global>("/root/GlobalVariables");
        var baseBattlers = new List<BaseBattlers> { battler1, battler2, battler3 };
        global.SetBattlers(baseBattlers);
        currentBattlers = global.GetBattlers();
    }

    private void SetupTimer()
    {
        timer = new Timer();
        AddChild(timer);
        timer.WaitTime = 2;
        timer.Timeout += () => TurnOver = true;
        timer.Start();
    }

    private void StartNextTurn(double delta)
    {
        GD.Print(timer.TimeLeft);
        currentBattler = GetNextBattler();
        if (currentBattler == null)
        {
            GD.Print("All turns are over.");
            return;
        }

        currentBattler.SetIsTurn(true);
        currentBattler.Modulate = new Color(1, 1, 0, 1);
        PlayTurn(currentBattler, (float)delta);
        previousPosition = currentBattler.Position;
        TurnOver = false;
        timer.Start();
    }

    private void EndCurrentTurn(double delta)
    {
        if (currentBattler == null) return;

        currentBattler.SetIsTurn(false);
        currentBattler.SetTurnOver(true);
        currentBattler.Position = currentBattler.Position.Lerp(previousPosition, 0.4f * (float)delta);
        currentBattler.Modulate = new Color(1, 1, 1, 1);
    }

    private BaseBattlers GetNextBattler()
    {
        return Battlers.OrderBy(b => b.Id).FirstOrDefault(b => !b.TurnOver);
    }

    private void PlayTurn(BaseBattlers battler, float delta)
    {
        MovePlayerInFrontOfEnemy(battler, enemy, delta);
    }

    private void MovePlayerInFrontOfEnemy(BaseBattlers playerBattler, BaseBattlers enemyPosition, float delta)
    {
        if (playerBattler != null)
        {
            float sineWave = Mathf.Sin(delta++ * frequency) * amplitude;
            Vector2 sineOffset = new Vector2(0, sineWave);
            Vector2 targetPosition = enemy.Position - new Vector2(150, 150) + sineOffset;
            playerBattler.Position = playerBattler.Position.Lerp(targetPosition, (float).1 * delta);
            //playerBattler.Position = playerBattler.Position.Lerp(enemyPosition.Position - new Vector2(100, 100), 1 * delta);

            // Check if player has reached the enemy position
            if (playerBattler.Position.DistanceTo(targetPosition) < 1.0f)
            {
                movingToEnemy = false;
            }
        }
    }

    private void MovePlayerBack(BaseBattlers playerBattler, Vector2 previousPosition, float delta)
    {
        if (playerBattler != null)
        {
            float sineWave = Mathf.Sin(delta++ * frequency) * amplitude;
            Vector2 sineOffset = new Vector2(0, sineWave);
            Vector2 targetPosition = previousPosition + sineOffset;
            playerBattler.Position = playerBattler.Position.Lerp(targetPosition, (float).1 * delta);

            // Check if player has reached the previous position
            if (playerBattler.Position.DistanceTo(targetPosition) < 1.0f)
            {
                movingToEnemy = true;
                TurnOver = true; // Set the turn over once the player returns
            }
        }
    }
}
