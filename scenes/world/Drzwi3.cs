using Godot;
using System;

public partial class Drzwi3 : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_interact_interact()
	{
		singleton Singleton = GetNode<singleton>("/root/Singleton");


		Dialog dialog = new();
	
		dialog.AddDialog("Testowa szafka", "Przenoszę cię do środka szpitala!", "res://assets/ui/qs.png", () =>
		{
			Singleton.GotoScene("res://scenes/world/hospital_corridor.tscn");
		});
	
		Singleton.Dialoghandler.PlayDialog(dialog);
	}
}
