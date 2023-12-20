using Godot;
using System;

public partial class HUDControl : Godot.Control
{
	// Called when the node enters the scene tree for the first time.
	private singleton Singleton;
	public override void _Ready()
	{
		Singleton = GetNode<singleton>("/root/Singleton");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Visible = !Singleton.UiLock;
	}
}
