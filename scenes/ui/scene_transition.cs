using Godot;
using System;

public partial class scene_transition : CanvasLayer
{
	public float LoadTransition(String name)
	{
		GetNode<AnimationPlayer>("AnimationPlayer").Play(name);
		return GetNode<AnimationPlayer>("AnimationPlayer").GetAnimation(name).Length;
	}
	public float UnloadTransition(String name)
	{
		GetNode<AnimationPlayer>("AnimationPlayer").PlayBackwards(name);
		return GetNode<AnimationPlayer>("AnimationPlayer").GetAnimation(name).Length;
	}
}
