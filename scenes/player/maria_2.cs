using Godot;
using System;

public partial class maria_2 : CharacterBody2D
{
	[Export]
	public float Speed = 200.0f;
	[Export]
	public float JumpVelocity = -200.0f;

	private bool _animationLocked = false;
	private bool _wasInAir = false;
	private AnimatedSprite2D _animatedSprite;
	private Vector2 _direction = Vector2.Zero;
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;
		else
		{
			if (_wasInAir)
			{
				_wasInAir = false;
				_animationLocked = false;
			}
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
			velocity = Jump(velocity);

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		
		_direction = Input.GetVector("left", "right", "up", "down");
		if (_direction != Vector2.Zero)
		{
			velocity.X = _direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
		update_animation();
	}
	
	private void update_animation()
	{
		_animatedSprite.FlipH = _direction.X < 0 || !(_direction.X > 0) && _animatedSprite.FlipH; // Flip the sprite if the direction is negative, or positive. Don't flip it, if it's 0.
		if (!_animationLocked)
		{
			if (_direction.X != 0)
			{
				_animatedSprite.Play("run");
				
			}
			else
			{
				_animatedSprite.Play("idle");
			}
		}	
	}

	private Vector2 Jump(Vector2 velocity)
	{
		velocity.Y = JumpVelocity;
		_animatedSprite.Play("jump_start");
		_wasInAir = true;
		_animationLocked = true;
		return velocity;
	}
	
	
}
