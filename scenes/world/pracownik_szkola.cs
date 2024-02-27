using Godot;
using System;
using Geek4.scripts.quests;

public partial class pracownik_szkola : AnimatedSprite2D
{
	// Called when the node enters the scene tree for the first time.
	public singleton Singleton;
	public override void _Ready()
	{
		Singleton = GetNode<singleton>("/root/Singleton");
		if (Singleton.QuestHandler.GetQuestInstance(typeof(Edukacja)).GetStepFromMilestone(0, 1).StepCompleted)
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
		
		if (Singleton.QuestHandler.GetQuestInstance(typeof(Edukacja)).GetStepFromMilestone(0, 1).StepCompleted)
		{
			return;
		}
		
		Dialog dialog = new();
		
		dialog.AddDialog("Pracownik", "Co tam mała?", "res://assets/textures/npc/szkola/pracownik-glowa.png");
		dialog.AddDialog("Maria", "Dzień dobry!", "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddDialog("Maria", "Gdzie znajdę dyrektora?", "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddDialog("Pracownik", "Na parterze. Poradzisz sobie.", "res://assets/textures/npc/szkola/pracownik-glowa.png");

		dialog.FinishCallback = () =>
		{
			Singleton.QuestHandler.GetQuestInstance(typeof(Edukacja)).CompleteStep(0, 1);
		};
		
		Singleton.Dialoghandler.PlayDialog(dialog);
		
	}
}
