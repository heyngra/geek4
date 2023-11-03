using Godot;
using System;

public partial class AirState : State
{
    private GroundState _groundState;

    public override void _Ready()
    {
        _groundState = GetState<GroundState>();
    }

    public override void OnEnter()
    {
        
    }
    public override void StateProcess(double delta)
    {
        if (Character.IsOnFloor())
        {
            NextState = _groundState;
        }
    }
}
