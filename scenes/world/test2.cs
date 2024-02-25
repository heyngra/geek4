using Godot;
using System;
using System.Collections.Generic;
using Geek4.scripts.quests;

public partial class test2 : Sprite2D
{
	public void _on_interactable_interact()
	{
		singleton Singleton = GetNode<singleton>("/root/Singleton");

		if (Singleton.QuestHandler.GetQuestInstance(typeof(ExampleQuest)).GetStepName() == "Talk with the Szafka xD")
		{
			Dialog dialogQuest = new Dialog();
			dialogQuest.AddDialog("Szafka", "Porozmawiałaś ze mną?");
			dialogQuest.AddChooseDialog("Maria", "Muszę wybrać mądrze...", new Dictionary<string, Action>
			{
				{"Tak", () => {GD.Print("Tak");Singleton.Dialoghandler._currentDialog.DialogQueue.Insert(0, new DialogSequence("Szafka", "Hmm..."));}},
				{"Nie", () => {GD.Print("Nie");Singleton.Dialoghandler._currentDialog.DialogQueue.Clear();}}
			}, "res://assets/ui/maria lvl.png");
			dialogQuest.AddDialog("Szafka", "Dziękuje <3", "res://assets/ui/qs.png", () =>
			{
				Singleton.QuestHandler.GetQuestInstance(typeof(ExampleQuest)).CompleteStep(0, 0);
			});
			Singleton.Dialoghandler.PlayDialog(dialogQuest);
			return;
		}
		if (Singleton.QuestHandler.GetQuestInstance(typeof(ExampleQuest)).IsStepMilestone(0, 1))
		{
			Dialog dialogQuest = new Dialog();
			dialogQuest.AddDialog("Szafka", "Ale mi miło, że znowu ze mną porozmawiałeś.");
			dialogQuest.AddDialog("Maria", "Tak samo :)", "res://assets/ui/qs.png", () =>
			{
				Singleton.QuestHandler.GetQuestInstance(typeof(ExampleQuest)).CompleteStep(0, 1);
			});
			Singleton.Dialoghandler.PlayDialog(dialogQuest);
			return;
		}
		
		Dialog dialog = new Dialog();
		
		dialog.AddDialog("Szafka", "Jestem ładna?");
		dialog.AddDialog("Maria", "tak :)");
		dialog.AddDialog("Szafka", "Dziękuję!");
		Singleton.Dialoghandler.PlayDialog(dialog);
	}
}
