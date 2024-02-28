using Godot;
using System;
using Geek4.scripts.quests;

public partial class FieldEventHandler : Node2D
{
	public singleton Singleton;
	private bool _isFirstEnter = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Singleton = GetNode<singleton>("/root/Singleton");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Singleton.QuestHandler.GetQuestInstance(typeof(Wies)).IsStepMilestone(0, 3) && !_isFirstEnter && Singleton.Maria.GlobalPosition.X>90)
		{
			_isFirstEnter = true;

			Dialog dialog = new();

			Singleton.CurrentScene.GetNode<AnimatedSprite2D>("Dziadek").Visible = true;
			
			dialog.AddDialog("Dziadek", "Witaj Maria!", "res://assets/textures/npc/dziadek-glowa.png");
			
			dialog.AddDialog("Maria", "Hej dziadek, potrzebujesz w czymś pomocy?", "res://assets/textures/maria/idle/maria-glowa.png");
			
			dialog.AddDialog("Dziadek", "Pomogłabyś mi ogarnąć krowy?", "res://assets/textures/npc/dziadek-glowa.png");
			dialog.AddDialog("Dziadek", "Mam na to za mało sił...", "res://assets/textures/npc/dziadek-glowa.png");
			dialog.AddDialog("Dziadek", "Ja wracam do domu.", "res://assets/textures/npc/dziadek-glowa.png");
			
			dialog.FinishCallback = () =>
			{
				Singleton.CurrentScene.GetNode<AnimatedSprite2D>("Dziadek").Visible = false;
				Singleton.QuestHandler.GetQuestInstance(typeof(Wies)).CompleteStep(0, 3);
			};

			Singleton.Dialoghandler.PlayDialog(dialog);
			
		}
	}
}
