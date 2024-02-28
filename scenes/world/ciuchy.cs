using Godot;
using System;
using Geek4.scripts.quests;

public partial class ciuchy : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public singleton Singleton;
	public override void _Ready()
	{
		Singleton = GetNode<singleton>("/root/Singleton");
		if (!Singleton.QuestHandler.GetQuestInstance(typeof(Wies)).IsStepMilestone(0,1))
		{
			QueueFree();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_node_2d_interact()
	{
		Dialog dialog = new();

		dialog.AddDialog("Maria", "Czy to moje ciuchy?", "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddDialog("Maria", "Chyba tak...", "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddDialog("Maria", "Ciężkie są...", "res://assets/textures/maria/idle/maria-glowa.png", default, 2F);
		dialog.AddDialog("Maria", "Spakowane!", "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddDialog("Maria", "Mogę już wyjść z domu.", "res://assets/textures/maria/idle/maria-glowa.png");

		dialog.FinishCallback = () =>
		{
			Singleton.QuestHandler.GetQuestInstance(typeof(Wies)).CompleteStep(0, 1);
			QueueFree();
		};
		
		Singleton.Dialoghandler.PlayDialog(dialog);
	}
}
