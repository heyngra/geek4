using Godot;
using System;

public partial class AirState : State
{
    private GroundState _groundState;
    private bool sceneChanged; // part of changing a scene, debug only

    public override void _Ready()
    {
        _groundState = GetState<GroundState>();
    }

    public override void OnEnter()
    {
        sceneChanged = false; // part of changing a scene, debug only
    }

    public override void StateProcess(double delta)
    {
        if (Character.IsOnFloor())
        {
            NextState = _groundState;
        }

        if (Character.Position.Y > 1000 && !sceneChanged) // part of changing a scene, debug only
        {
            sceneChanged = true;
            if (GD.Randi() % 2 == 1)
            {
                GetNode<singleton>("/root/Singleton").PauseScene("res://scenes/world/test_env3.tscn");
            }
            else
            {
                GetNode<singleton>("/root/Singleton").PauseScene("res://scenes/world/test_env2.tscn");
            }
        }
    }
}
