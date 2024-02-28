using Godot;
using System;
using Geek4.scripts.quests;

public partial class villageroomfirstenter : Node2D
{
	public singleton Singleton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Singleton = GetNode<singleton>("/root/Singleton");
		Singleton.QuestHandler.GetQuestInstance(typeof(Wies)).CompleteStep(0, 2);
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
