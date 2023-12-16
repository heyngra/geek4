using Godot;
using System;

public partial class NewGame : Button
{
	private singleton Singleton;

	[Export] public PackedScene Scene;
	
	public override void _Ready()
	{
		Singleton = GetNode<singleton>("/root/Singleton");
	}

	public void _on_pressed()
	{
		Singleton.GotoScene(Scene.ResourcePath);
		CanvasLayer n = null;
		foreach (Node allChild in singleton.GetAllChildren(GetTree().CurrentScene)) if (allChild is CanvasLayer) n = (CanvasLayer) allChild;
		n!.Visible = false;
	}
	
}
