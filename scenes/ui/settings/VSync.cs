using Godot;
using System;

public partial class VSync : CheckBox
{
	private bool _vsync = false;
	
	public void _on_pressed()
	{
		_vsync = ButtonPressed;
		GetNode<singleton>("/root/Singleton").ConfigHandler.Config.SetValue("settings", "vsync", _vsync);
		GetNode<singleton>("/root/Singleton").ConfigHandler.SaveConfig();
		ChangeVSync();
	}
	
	public void _on_visibility_changed()
	{
		_vsync = (bool)GetNode<singleton>("/root/Singleton").ConfigHandler.Config.GetValue("settings", "vsync", false);
		ButtonPressed = _vsync;
		ChangeVSync();
	}

	private void ChangeVSync()
	{
		if (_vsync)
		{
			DisplayServer.WindowSetVsyncMode(DisplayServer.VSyncMode.Adaptive);
		}
		else
		{
			DisplayServer.WindowSetVsyncMode(DisplayServer.VSyncMode.Disabled);
		}
	}
}
