using Godot;
using System;
using Geek4.scripts.quests;

public partial class MamaWŁóżku2 : Sprite2D
{
	public singleton Singleton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Singleton = GetNode<singleton>("/root/Singleton");
		if (Singleton.QuestHandler.GetQuestInstance(typeof(ProblematyczneDziecinstwo)).GetStepFromMilestone(2, 3)
		    .StepCompleted)
		{
			Texture = GD.Load<Texture2D>("res://assets/textures/meble/łóżko bokiem.png");
			GetNode<Node2D>("Node2D").QueueFree();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_node_2d_interact()
	{
		Dialog dialog = new();
		
		dialog.AddDialog("Mama", "Mężu! Ja chyba umieram!", "res://assets/textures/mama/mama-glowa.png");
		dialog.AddDialog("Tata", "Nie ma szans... Nie pozwolę na to!", "res://assets/textures/tata/tata-glowa.png");
		dialog.AddDialog("Maria", "Mamo, co się dzieje?", "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddDialog("Mama", "Maria, kochanie... Musisz być silna...", "res://assets/textures/mama/mama-glowa.png");
		dialog.AddDialog("Mama", "...", "res://assets/textures/mama/mama-glowa.png");
		dialog.AddDialog("Tata", "Pójdź do domu sama, Mario. Ja zostanę z mamą.", "res://assets/textures/tata/tata-glowa.png");

		dialog.FinishCallback = () =>
		{
			Singleton.QuestHandler.GetQuestInstance(typeof(ProblematyczneDziecinstwo)).CompleteStep(2, 3);
			Singleton.QuestHandler.GetQuestInstance(typeof(Edukacja)).StartQuest();
		};
		
		Singleton.Dialoghandler.PlayDialog(dialog);
	}
}
