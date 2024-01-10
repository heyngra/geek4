using Godot;
using System;

public partial class TitleScreenControl : Control
{
	// Called when the node enters the scene tree for the first time.
	private singleton Singleton;

	[Export] public PackedScene NewGameScene;
	
	public override void _Ready()
	{
		Singleton = GetNode<singleton>("/root/Singleton");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void _on_new_game_pressed()
	{
		Singleton.PlayAudio("res://assets/sound/dialog click.mp3", -10, "SFX");
		Singleton.GotoScene(NewGameScene.ResourcePath);
		CanvasLayer n = null;
		foreach (Node allChild in singleton.GetAllChildren(GetTree().CurrentScene)) if (allChild is CanvasLayer) n = (CanvasLayer) allChild;
		n!.Visible = false;
	}

	public void _on_load_game_pressed()
	{
		Singleton.PlayAudio("res://assets/sound/dialog click.mp3", -10, "SFX");
		Singleton.SaveHandler.LoadGame();
		var loadedScenePath = Singleton.SaveHandler.GetValue(null, "lastScene", "unique").ToString(); 
		Singleton.GotoScene(loadedScenePath);
		CanvasLayer n = null;
		foreach (Node allChild in singleton.GetAllChildren(GetTree().CurrentScene)) if (allChild is CanvasLayer) n = (CanvasLayer) allChild;
		n!.Visible = false;
	}

	public void _on_settings_pressed()
	{
		Singleton.PlayAudio("res://assets/sound/dialog click.mp3", -10, "SFX");
		GetNode<SettingsControl>("/root/Gui/Settings/SettingsControl").OpenMenu();
	}
	public void _on_quit_pressed()
	{
		Singleton.PlayAudio("res://assets/sound/dialog click.mp3", -10, "SFX");
		GetTree().Quit();
	}

	public void _on_legal_pressed()
	{
		Singleton.PlayAudio("res://assets/sound/dialog click.mp3", -10, "SFX");
		OS.ShellOpen("https://github.com/ScrawlingKiddos/geek4/blob/main/legal.md");
	}
	
	
}
