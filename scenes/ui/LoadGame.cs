using Godot;
using System;

public partial class LoadGame : Button
{
	private singleton Singleton;

	[Export] public PackedScene Scene;
	
	public override void _Ready()
	{
		Singleton = GetNode<singleton>("/root/Singleton");
		if (FileAccess.FileExists("user://save1.dat"))
		{
			Disabled = false;
		}
	}

	public void _on_pressed()
	{
		Singleton.SaveHandler.LoadGame();
		var loadedScenePath = Singleton.SaveHandler.GetValue(null, "lastScene", "unique").ToString(); 
		Singleton.GotoScene(loadedScenePath);
		CanvasLayer n = null;
		foreach (Node allChild in singleton.GetAllChildren(GetTree().CurrentScene)) if (allChild is CanvasLayer) n = (CanvasLayer) allChild;
		n!.Visible = false;
	}
}
