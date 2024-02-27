using Godot;
using System;
using Geek4.scripts.quests;

public partial class Tata_room : Sprite2D
{

	public singleton Singleton;

	public bool run = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Singleton = GetNode<singleton>("/root/Singleton");
		if (Singleton.QuestHandler.GetQuestInstance(typeof(ProblematyczneDziecinstwo)).GetStepFromMilestone(1, 1)
		    .StepCompleted)
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
		if (Singleton.QuestHandler.GetQuestInstance(typeof(ProblematyczneDziecinstwo)).IsStepMilestone(1, 1))
		{

			Dialog dialog = new();

			dialog.AddDialog("Tata", "Maria! Chodź szybko!", "res://assets/textures/tata/tata-glowa.png");
			dialog.AddDialog("Maria", "Co się dzieje, tato?", "res://assets/textures/maria/idle/maria-glowa.png");
			dialog.AddDialog("Tata", "Mama się źle czuje!", "res://assets/textures/tata/tata-glowa.png");
			dialog.AddDialog("Tata", "Musimy jechać do szpitala!", "res://assets/textures/tata/tata-glowa.png");

			dialog.FinishCallback = () =>
			{
				run = true;
				Singleton.QuestHandler.GetQuestInstance(typeof(ProblematyczneDziecinstwo)).CompleteStep(1, 1);
			};
			
			Singleton.Dialoghandler.PlayDialog(dialog);

		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (run)
		{
			Singleton.UiLock = true;
			GetNode<AnimationPlayer>("AnimationPlayer").Play("run");
			Position = new Vector2((float)(Position.X-(80*delta)), Position.Y);
			if (Position.X <= -60)
			{
				QueueFree();
				Singleton.UiLock = false;
			}
		}
	}
	
	
}
