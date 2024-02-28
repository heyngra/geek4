using Godot;
using System;
using Geek4.scripts.quests;

public partial class principal : Node2D
{
	public singleton Singleton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Singleton = GetNode<singleton>("/root/Singleton");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void _on_node_2d_interact()
	{
		Dialog dialog = new();
		if (Singleton.QuestHandler.GetQuestInstance(typeof(Edukacja)).IsStepMilestone(0, 2))
		{
			dialog.AddDialog("Dyrektor", "Czego tam?", "res://assets/textures/npc/szkola/dyrektor-glowa.png");
			dialog.AddDialog("Maria", "Chciałabym zapisać się do szkoły",
				"res://assets/textures/maria/idle/maria-glowa.png");
			dialog.AddDialog("Dyrektor", "Oczywiście, już zapisuję. Jak się nazywasz?",
				"res://assets/textures/npc/szkola/dyrektor-glowa.png");
			dialog.AddDialog("Maria", "Maria Skłodowska", "res://assets/textures/maria/idle/maria-glowa.png");
			dialog.AddDialog("Dyrektor", "Zapisana. Teraz idź na egzamin.",
				"res://assets/textures/npc/szkola/dyrektor-glowa.png");
			dialog.FinishCallback = () =>
			{
				Singleton.QuestHandler.GetQuestInstance(typeof(Edukacja)).CompleteStep(0,2);
			};
		}
		else
		{
			dialog.AddDialog("Dyrektor","Czego tam? Uczyć się!","res://assets/textures/npc/szkola/dyrektor-glowa.png");
		}
		Singleton.Dialoghandler.PlayDialog(dialog);
	}
}
