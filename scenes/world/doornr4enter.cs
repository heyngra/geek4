using Godot;
using System;
using Geek4.scripts.quests;

public partial class doornr4enter : Node2D
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
		GetNode<singleton>("/root/Singleton").GotoScene("res://scenes/world/class4.tscn", default, default, new(-50, -24));
		GetNode<singleton>("/root/Singleton").QuestHandler.GetQuestInstance(typeof(Edukacja)).CompleteStep(1,0);
	}
}
