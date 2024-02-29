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
		if (Singleton.QuestHandler.GetQuestInstance(typeof(ProblematyczneDziecinstwo)).GetStepFromMilestone(1, 1).StepCompleted&&!Singleton.QuestHandler.GetQuestInstance(typeof(Wies)).IsStarted)
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

		else if (Singleton.QuestHandler.GetQuestInstance(typeof(Wies)).IsStepMilestone(0, 0))
		{
			Dialog dialog = new();
			
			dialog.AddDialog("Maria", "Zdałam egzamin!", "res://assets/textures/maria/idle/maria-glowa.png");
			dialog.AddDialog("Tata", "Jestem z ciebie dumny córeczko!", "res://assets/textures/tata/tata-glowa.png");
			dialog.AddDialog("Tata", "W takim razie mam dla ciebie nową informację!", "res://assets/textures/tata/tata-glowa.png");
			dialog.AddDialog("Maria", "Hmm?", "res://assets/textures/maria/idle/maria-glowa.png");
			dialog.AddDialog("Tata", "Jedziemy na wieś! Pakuj się i wyjeżdzamy!", "res://assets/textures/tata/tata-glowa.png");
			dialog.AddDialog("Maria", "Na wieś? Dlaczego?", "res://assets/textures/maria/idle/maria-glowa.png");
			dialog.AddDialog("Tata", "Musimy odpocząć po tym co się stało mamie. To będzie dobre dla naszej całej rodziny.", "res://assets/textures/tata/tata-glowa.png");
			dialog.AddDialog("Tata", "Idź do swojego pokoju i się spakuj!", "res://assets/textures/tata/tata-glowa.png");

			dialog.FinishCallback = () =>
			{
				Singleton.QuestHandler.GetQuestInstance(typeof(Wies)).CompleteStep(0, 0);
			};
			
			Singleton.Dialoghandler.PlayDialog(dialog);
		}
		else if (Singleton.QuestHandler.GetQuestInstance(typeof(Wies)).IsStepMilestone(0, 1))
		{
			Dialog dialog = new();
			
			dialog.AddDialog("Tata", "Nie ma na co czekać! Pakuj się i wyjeżdzamy!", "res://assets/textures/tata/tata-glowa.png");
			
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
