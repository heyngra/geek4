using Godot;
using System;
using System.Collections.Generic;
using Geek4.scripts.quests;

public partial class rower_script : Node2D
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
		
		Dictionary<string, Action> actions = new();

		actions.Add("Do domu", () => Singleton.GotoScene("res://scenes/world/house_front.tscn", default, default, new(330, -24)));
		
		actions.Add("Do szpitala", () => Singleton.GotoScene("res://scenes/world/hospital.tscn", default, default, new(-160, -24)));

		if (Singleton.QuestHandler.GetQuestInstance(typeof(Edukacja)).IsStarted)
		{
			actions.Add("Do szkoły",
				() => Singleton.GotoScene("res://scenes/world/school_front.tscn", default, default, new(22, -24)));
		}
		
		actions.Add("Nigdzie", () => { });
		dialog.AddChooseDialog("Maria", "Gdzie chciałabym się udać...", actions, "res://assets/textures/maria/idle/maria-glowa.png");
		
		Singleton.Dialoghandler.PlayDialog(dialog);
	}
}
