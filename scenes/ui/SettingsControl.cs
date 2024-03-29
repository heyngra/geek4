using Godot;
using System;

public partial class SettingsControl : Control
{
	public singleton Singleton;
	public override void _Ready()
	{
		Singleton = GetNode<singleton>("/root/Singleton");
		
	}
	public void OpenMenu()
	{
		Visible = true;
		Singleton.UiLock = Visible;
		
	}

	public void ExitMenu()
	{
		Singleton.UiLock = false;
		Visible = false;
	}

	public void _on_exit_button_pressed()
	{
		Singleton.PlayAudio("res://assets/sound/dialog click.mp3", -10, "SFX");
		ExitMenu();
	}
}
