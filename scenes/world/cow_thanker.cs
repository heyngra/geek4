using Godot;
using System;

public partial class cow_thanker : AnimatedSprite2D
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
		Dialog dialog = new();
		
		dialog.AddDialog("Krowa?", "Muuu!", "res://assets/textures/zwierzeta/krowa-head.png");
		dialog.AddDialog("Twórcy", "Dziękujemy za zagranie w grę!", "res://assets/textures/zwierzeta/krowa-head.png");
		dialog.AddDialog("Twórcy", "Staraliśmy się jak mogliśmy!", "res://assets/textures/zwierzeta/krowa-head.png");
		dialog.AddDialog("Twórcy", "Mamy nadzieję, że chociaż trochę poprawiliśmy wam humor!", "res://assets/textures/zwierzeta/krowa-head.png");
		
		// TODO: dodać hinta na secret quest
		
		dialog.AddDialog("Twórcy", "Muuu!", "res://assets/textures/zwierzeta/krowa-head.png");

		Singleton.Dialoghandler.PlayDialog(dialog);
	}
}
