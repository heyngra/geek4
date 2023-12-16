using Godot;
using System;

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
	
	public void _on_exit_button_pressed()
	{
		Singleton.PlayAudio("res://assets/sound/dialog click.mp3", -10, "SFX");
		GetTree().Quit();
	}

	public void _on_play_button_pressed()
	{
		Singleton.PlayAudio("res://assets/sound/dialog click.mp3", -10, "SFX");
		exitMenu();
	}
	
	public void _on_settings_button_pressed()
	{
		Singleton.PlayAudio("res://assets/sound/dialog click.mp3", -10, "SFX");
		// todo
	}
	
}
