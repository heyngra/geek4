using Godot;
using System;

public partial class player_spawner : Node2D
{
	private singleton Singleton;
	public override void _Ready()
	{
		Singleton = GetNode<singleton>("/root/Singleton");
		if (Singleton.Maria == null) return;
		Singleton.Maria.Connect("SceneChange", new Callable(this, MethodName.OnSceneChange));
	}
	private void OnSceneChange()
	{
		Singleton.Maria.Position = Position;
	}
}
