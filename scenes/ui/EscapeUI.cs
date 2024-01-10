using Godot;
using System;
using System.Collections.Generic;

public partial class EscapeUI : Control
{

	private singleton Singleton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Singleton = GetNode<singleton>("/root/Singleton");
		Visible = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		if (Input.IsActionJustPressed("ui_menu") && !Singleton.UiLock && !Visible)
		{
			Singleton.PlayAudio("res://assets/sound/dialog click.mp3", -10, "SFX");
			Visible = !Visible;
			Singleton.UiLock = Visible;
			GetNode<Button>("PanelContainer/VBoxContainer/PlayButton").GrabFocus();
		}
	}

	public void exitMenu()
	{
		Visible = false;
		Singleton.UiLock = false;
	}
	
	public async void _on_exit_button_pressed()
	{
		Singleton.PlayAudio("res://assets/sound/dialog click.mp3", -10, "SFX");
		exitMenu();
		await ToSignal(GetTree().CreateTimer(Singleton.Transition.LoadTransition("dissolve")), "timeout");
		Singleton.SaveHandler.SaveGame();
		maria_2 maria = Singleton.Maria;
		Singleton.Maria = null;
		maria.Free();
		foreach (KeyValuePair<string, Node> scene in Singleton.PausedScenes)
		{
			scene.Value.Free();
			Singleton.PausedScenes.Remove(scene.Key);
		}
		Singleton.GotoScene("res://scenes/ui/title_screen.tscn");
	}

	public void _on_play_button_pressed()
	{
		Singleton.PlayAudio("res://assets/sound/dialog click.mp3", -10, "SFX");
		exitMenu();
	}
	
	public void _on_settings_button_pressed()
	{
		Singleton.PlayAudio("res://assets/sound/dialog click.mp3", -10, "SFX");
		GetNode<SettingsControl>("/root/Gui/Settings/SettingsControl").OpenMenu();
	}
	
}
