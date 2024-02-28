using Godot;
using System;
using Geek4.scripts.quests;

public partial class Dzrwiwyjsciowe : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void _on_node_2d_interact()
	{
		
		singleton Singleton = GetNode<singleton>("/root/Singleton");
		if (Singleton.QuestHandler.GetQuestInstance(typeof(ProblematyczneDziecinstwo)).IsStepMilestone(2,0))
		{
			Singleton.QuestHandler.GetQuestInstance(typeof(ProblematyczneDziecinstwo)).CompleteStep(2,0);

		}
		Singleton.GotoScene("res://scenes/world/house_front.tscn");
		
	}
}
