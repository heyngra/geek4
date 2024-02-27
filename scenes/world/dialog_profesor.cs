using Godot;
using System;
using System.Collections.Generic;
using Geek4.scripts.quests;


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

		if (!Singleton.QuestHandler.GetQuestInstance(typeof(Edukacja)).GetStepFromMilestone(0, 2).StepCompleted)
		{
			dialog.AddDialog("Profesor", "Dziewczynko, ty tu się nie uczysz!", "res://assets/textures/npc/szkola/profesor-glowa.png");
		}
		else if (!Singleton.QuestHandler.GetQuestInstance(typeof(Edukacja)).GetStepFromMilestone(1, 1).StepCompleted &&
		         Singleton.QuestHandler.GetQuestInstance(typeof(Edukacja)).GetStepFromMilestone(1, 0).StepCompleted)
		{
			dialog.AddChooseDialog("Profesor", "Witaj, Mario. Czy chcesz podejść do egzaminu?", new ()
			{
				{"Tak", () => {Singleton.QuestHandler.GetQuestInstance(typeof(Edukacja)).CompleteStep(1,1);Singleton.Dialoghandler._currentDialog.DialogQueue.Insert(0, new DialogSequence("Profesor", "Podjedź teraz do swojej ławki.", "res://assets/textures/npc/szkola/profesor-glowa.png"));}},
				{"Nie", () => {GD.Print("Nie");Singleton.Dialoghandler._currentDialog.DialogQueue.Clear();}}
			}, "res://assets/textures/npc/szkola/profesor-glowa.png");
		}
		else if (Singleton.QuestHandler.GetQuestInstance(typeof(Edukacja)).GetStepFromMilestone(1, 2).StepCompleted)
		{
			dialog.AddDialog("Profesor", "Gratuluję, Mario. Zdałaś egzamin.", "res://assets/textures/npc/szkola/profesor-glowa.png");
		}
		else
		{
			return;
		}
		
		Singleton.Dialoghandler.PlayDialog(dialog);
	}
}
