using Godot;
using System;

public partial class maria_2 : CharacterBody2D
{
	[Export]
	public float Speed = 200.0f;
	[Export]
	public float JumpVelocity = -200.0f;
	
	private Sprite2D _sprite;
	private AnimationTree _animationTree;
	private Vector2 _direction = Vector2.Zero;
	private CharacterStateMachine _characterStateMachine;
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
	{
		_animationTree = GetNode<AnimationTree>("AnimationTree");
		_characterStateMachine = GetNode<CharacterStateMachine>("CharacterStateMachine");
		_sprite = GetNode<Sprite2D>("Sprite2D");
		_animationTree.Active = true;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor()) velocity.Y += gravity * (float)delta;



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
		_animationTree.Set("parameters/Move/blend_position", _direction.X);
		_sprite.FlipH = _direction.X < 0 || !(_direction.X > 0) && _sprite.FlipH; // Flip the sprite if the direction is negative, or positive. Don't flip it, if it's 0.
	}	
	
}
