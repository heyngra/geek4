using Godot;
using System;

public partial class test3 : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_interactable_interact()
	{
		singleton Singleton = GetNode<singleton>("/root/Singleton");


		Dialog dialog = new();
		
		dialog.AddDialog("Testowa szafka", "Przenoszę cię do szpitala!", "res://assets/ui/qs.png", () =>
		{
			Singleton.GotoScene("res://scenes/world/hospital.tscn");
		});
		
		Singleton.Dialoghandler.PlayDialog(dialog);
		
	}
}
