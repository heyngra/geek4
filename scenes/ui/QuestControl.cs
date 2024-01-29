using Godot;
using System;

public partial class QuestControl : Control
{
	// Called when the node enters the scene tree for the first time.
	public singleton Singleton;

	public override void _Ready()
	{
		Singleton = GetNode<singleton>("/root/Singleton");
		Visible = false;
	}

	public override void _Process(double delta)
	{

		if (Input.IsActionJustPressed("quests") && !Singleton.UiLock && !Visible)
		{
			OpenMenu();
		}
	}

	public void OpenMenu()
	{
		Singleton.PlayAudio("res://assets/sound/dialog click.mp3", -10, "SFX");
		Visible = !Visible;
		Singleton.UiLock = Visible;

	}
	public void ExitMenu()
	{
		Visible = false;
		Singleton.UiLock = false;
	}

	public void _on_exit_button_pressed()
	{
		Singleton.PlayAudio("res://assets/sound/dialog click.mp3", -10, "SFX");
		ExitMenu();
	}
}
