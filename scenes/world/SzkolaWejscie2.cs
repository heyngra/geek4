using Godot;
using System;
using Geek4.scripts.quests;

public partial class SzkolaWejscie2 : Node2D
{
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
		if (!Singleton.QuestHandler.GetQuestInstance(typeof(Edukacja)).GetStepFromMilestone(0, 1).StepCompleted) return;
		
		Singleton.GotoScene("res://scenes/world/school_corridor.tscn", default, default, new(-56,-24));
	}
}
