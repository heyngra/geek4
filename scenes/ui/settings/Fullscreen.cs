using Godot;
using System;

public partial class Fullscreen : CheckBox
{
	private bool _fullscreen = false;
	
	public void _on_pressed()
	{
		_fullscreen = ButtonPressed;
		GetNode<singleton>("/root/Singleton").ConfigHandler.Config.SetValue("settings", "fullscreen", _fullscreen);
		GetNode<singleton>("/root/Singleton").ConfigHandler.SaveConfig();
		ChangeFullscreen();
	}
	
	public void _on_visibility_changed()
	{
		_fullscreen = (bool)GetNode<singleton>("/root/Singleton").ConfigHandler.Config.GetValue("settings", "fullscreen", false);
		ButtonPressed = _fullscreen;
		ChangeFullscreen();
	}

	private void ChangeFullscreen()
	{
		if (_fullscreen)
		{
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
		}
		else
		{
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
		}
	}
}
