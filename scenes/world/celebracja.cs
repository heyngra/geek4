using Godot;
using System;
using Geek4.scripts.quests;

public partial class celebracja : Node2D
{
	public singleton Singleton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Singleton = GetNode<singleton>("/root/Singleton");

		if (Singleton.QuestHandler.GetQuestInstance(typeof(Edukacja)).GetStepFromMilestone(1,2).StepCompleted && !Singleton.QuestHandler.GetQuestInstance(typeof(Edukacja)).GetStepFromMilestone(1,3).StepCompleted)
		{
			Singleton.Maria.GetNode<Sprite2D>("Medal").Visible = true;
			Dialog dialog = new();
			
			Singleton.Maria.ZoomCamera(2);
			
			dialog.AddDialog("Maria", "Udało mi się! Zdałam szkołę!", "res://assets/textures/maria/idle/maria-glowa.png");
			dialog.AddDialog("Maria", "Powinnam zobaczyć co u ojca...", "res://assets/textures/maria/idle/maria-glowa.png", default, 1.5F);

			dialog.FinishCallback = () =>
			{
				Singleton.Maria.GetNode<Sprite2D>("Medal").Visible = false;
				Singleton.Maria.ZoomCamera(1);
				Singleton.QuestHandler.GetQuestInstance(typeof(Edukacja)).CompleteStep(1, 3);
				Singleton.QuestHandler.GetQuestInstance(typeof(Wies)).StartQuest();
			};
			
			Singleton.Dialoghandler.PlayDialog(dialog);

		}
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
