using Godot;
using System;

public partial class drzwi_hospital_room2 : Node2D
{
	public singleton Singleton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Singleton = GetNode<singleton>("/root/Singleton");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_node_2d_interact()
	{
		Singleton.GotoScene("res://scenes/world/hospital_corridor.tscn", default, default, new(118, -25));
	}
}
