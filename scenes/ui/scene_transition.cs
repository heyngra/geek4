using Godot;
using System;
using System.ComponentModel.Design.Serialization;

public partial class scene_transition : CanvasLayer
{
	private bool isLoaded = false;
	public float LoadTransition(String name)
	{
		if (isLoaded) return 0;
		GetNode<AnimationPlayer>("AnimationPlayer").Play(name);
		isLoaded = true;
		return GetNode<AnimationPlayer>("AnimationPlayer").GetAnimation(name).Length;
	}
	public float UnloadTransition(String name)
	{
		if (!isLoaded) return 0;
		GetNode<AnimationPlayer>("AnimationPlayer").PlayBackwards(name);
		isLoaded = false;
		return GetNode<AnimationPlayer>("AnimationPlayer").GetAnimation(name).Length;
	}
}
