using Godot;
using System;

public partial class Dzrwirodzice : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void _on_node_2d_interact()
	{
		
		singleton Singleton = GetNode<singleton>("/root/Singleton");
		Singleton.GotoScene("res://scenes/world/room_parents.tscn");
	}
}
