using Godot;
using System;

public partial class movement_test : CharacterBody2D
{

	// Called when the node enters the scene tree for the first time.
	/*public override void _Ready()
	{

	}*/
	
	private AnimatedSprite2D _animatedSprite;
	private Vector2 _velocity = Vector2.Zero;
	[Export]
	private float _walkSpeed = 50;
	[Export]
	private float _jumpHeight;
	[Export]
	private float _jumpTimeToPeak;
	[Export]
	private float _jumpTimeToDescent;

	private float _jumpVelocity;
	private float _jumpGravity;
	private float _fallGravity;
	private float _animProgress = 0.0f;
	
	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_jumpVelocity = ((2.0f * _jumpHeight) / _jumpTimeToPeak)*-1.0f;
		_jumpGravity = ((-2.0f * _jumpHeight) / (_jumpTimeToPeak * _jumpTimeToPeak))*-1.0f;
		_fallGravity = ((-2.0f * _jumpHeight) / (_jumpTimeToDescent * _jumpTimeToDescent))*-1.0f;
	}

	private float GetGravity()
	{
		if (_velocity.Y < 0)
		{
			return _jumpGravity;
		}
		return _fallGravity;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		_velocity.Y += GetGravity() * (float)delta;
		_velocity.X = get_input_velocity() * _walkSpeed;
		if (_velocity.X != 0 && IsOnFloor())
		{
			if(!_animatedSprite.IsPlaying()){
				_animatedSprite.SetFrameAndProgress((int)_animProgress,_animProgress);
			}
			_animatedSprite.Play("walk");
		} 
		else
		{
			_animProgress = _animatedSprite.FrameProgress;
			_animatedSprite.Stop();
		}
		if (Input.IsActionPressed("ui_up") && IsOnFloor())
		{
			_velocity.Y = _jumpVelocity;
		}

		Velocity = _velocity;
		MoveAndSlide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	/*public override void _Process(double delta)
	{

	}*/
	public float get_input_velocity()
	{
		var horizontal = 0;
		if (Input.IsActionPressed("ui_left"))
		{
			horizontal -= 1;
		}
		if (Input.IsActionPressed("ui_right"))
		{
			horizontal += 1;
		}

		return horizontal;
	}
}
	
	
