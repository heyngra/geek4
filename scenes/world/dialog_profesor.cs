using Godot;
using System;
using System.Collections.Generic;


public partial class dialog_profesor : AnimatedSprite2D
{
	public singleton Singleton;
	public override void _Ready() { 
		Singleton = GetNode<singleton>("/root/Singleton");
	}

	public override void _Process(double delta)
	{
	}
	
	public void _on_node_2d_interact(){
		var dialog = new Dialog();
		dialog.AddChooseDialog("Profesor", "Witaj, Mario. Czy chcesz podejść do egzaminu", new ()
		{
			{"Tak", () => {GD.Print("Tak");Singleton.Dialoghandler._currentDialog.DialogQueue.Insert(0, new DialogSequence("Profesor", "Podjedź teraz do swojej ławki."));}},
			{"Nie", () => {GD.Print("Nie");Singleton.Dialoghandler._currentDialog.DialogQueue.Clear();}}
		});
		Singleton.Dialoghandler.PlayDialog(dialog);
	}
}
