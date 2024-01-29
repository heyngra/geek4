using Godot;
using System;

public partial class GroundState : State
{
    // velocity.Y = JumpVelocity;
    public override void StateInput(InputEvent @event)
    {
        if (@event.IsActionPressed("jump"))
        {
            jump();
        }
    }
    
    public override void StateProcess(double delta)
    {
        if (!Character.IsOnFloor() && Character.Velocity.Y > 0)
        {
            NextState = GetState<AirState>();
            Playback.Travel("fall");
        }
    }

    public override void OnEnter()
    {
        Playback.Travel("Move");
    }

    private void jump()
    {
        Vector2 velocity = Character.Velocity;
        velocity.Y = Character.Get("JumpVelocity").AsSingle();
        NextState = GetState<AirState>();
        Playback.Travel("jump_start");
        Character.Velocity = velocity;
    }
    
}
