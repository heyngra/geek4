using Godot;
using System;

public partial class Drzwi3Otwarte : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_node_interact()
	{
		singleton Singleton = GetNode<singleton>("/root/Singleton");


		Dialog dialog = new();
	
		dialog.AddDialog("Testowa szafka", "Przenoszę cię do test_env2!", "res://assets/ui/qs.png", () =>
		{
			Singleton.GotoScene("res://scenes/world/room_maria.tscn");
		});
	
		Singleton.Dialoghandler.PlayDialog(dialog);
	}
}
