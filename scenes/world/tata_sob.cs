using Godot;
using System;
using Geek4.scripts.quests;

public partial class tata_sob : AnimatedSprite2D
{
	// Called when the node enters the scene tree for the first time.
	public singleton Singleton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Singleton = GetNode<singleton>("/root/Singleton");
		if (Singleton.QuestHandler.GetQuestInstance(typeof(ProblematyczneDziecinstwo)).GetStepFromMilestone(2, 3)
		    .StepCompleted)
		{
			Visible = false;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
