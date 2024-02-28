using Godot;
using System;
using Geek4.scripts.quests;

public partial class SzkolaWejscie : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public singleton Singleton;
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
		Singleton.QuestHandler.GetQuestInstance(typeof(Edukacja)).CompleteStep(0, 0);
		Singleton.GotoScene("res://scenes/world/school_small_corridor.tscn", default, default, new(14, -24));
	}
}
