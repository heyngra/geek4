using Godot;
using System;

public partial class doornr2school : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public singleton Singleton;
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
		Dialog dialog = new();
		
		dialog.AddDialog("Maria", "Klamka się nie rusza.", "res://assets/textures/maria/idle/maria-glowa.png");
		
		Singleton.Dialoghandler.PlayDialog(dialog);
	}
	
}
