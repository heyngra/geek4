using Godot;
using System;

public partial class Dzrwirodzeństwo : Sprite2D
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
		Dialog dialog = new Dialog();
		
		dialog.AddDialog("Maria", "To jest pokój mojego rodzeństwa.");
		dialog.AddDialog("Maria", "Nie powinnam im zaglądać do pokoju bez pytania.");
		
		Singleton.Dialoghandler.PlayDialog(dialog);
	}
}
