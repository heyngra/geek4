using Godot;
using System;
using Geek4.scripts.quests;

public partial class hospital_room1 : Node2D
{
	public singleton Singleton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Singleton = GetNode<singleton>("/root/Singleton");
		if (!Singleton.QuestHandler.GetQuestInstance(typeof(ProblematyczneDziecinstwo)).IsStarted)
		{
			Singleton.QuestHandler.GetQuestInstance(typeof(ProblematyczneDziecinstwo)).StartQuest();
			Dialogue();
		}
		else
		{
			GD.PushWarning("You shouldn't be there!");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void Dialogue()
	{
		Dialog dialog = new();
		
		dialog.AddDialog("Matka", "Jak mogliśmy ją tak szybko stracić....", "res://assets/textures/mama/mama-glowa.png", null, 2F);
		dialog.AddDialog("Tata", "Nie wiem, ale musimy się z tym pogodzić.", "res://assets/textures/tata/tata-glowa.png", null, 2F);
		dialog.AddDialog("Tata", "Niech twoja dusza spoczywa w pokoju...", "res://assets/textures/tata/tata-glowa.png", null, 2F);
		dialog.AddDialog("Maria", "Zofia! Powiesz coś, prawda?", "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddDialog("Zofia", "...", "res://assets/ludziki/zofia lezy.png", null, 1F);
		dialog.AddDialog("Tata", "Wracamy do domu, córciu.", "res://assets/textures/tata/tata-glowa.png", null, 1F);

		dialog.FinishCallback = () =>
		{

			Singleton.QuestHandler.GetQuestInstance(typeof(ProblematyczneDziecinstwo)).CompleteStep(0, 0);
			Singleton.GotoScene("res://scenes/world/room_maria.tscn", "dissolve", false, new(27, -24));
		};
		Singleton.Dialoghandler.PlayDialog(dialog);
		
	}
	
}
