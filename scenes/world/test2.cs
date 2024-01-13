using Godot;
using System;

public partial class test2 : Sprite2D
{
	public void _on_interactable_interact()
	{
		singleton Singleton = GetNode<singleton>("/root/Singleton");

		Dialog dialog = new Dialog();
		
		dialog.AddDialog("Szafka", "Jestem ładna?");
		dialog.AddDialog("Maria", "tak :)");
		dialog.AddDialog("Szafka", "Dziękuję!");
		Singleton.Dialoghandler.PlayDialog(dialog);
	}
}
