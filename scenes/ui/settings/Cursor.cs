using Godot;
using System;

public partial class Cursor : CheckBox
{
	private bool _cursorenabled = false;
	
	public void _on_pressed()
	{
		_cursorenabled = ButtonPressed;
		GetNode<singleton>("/root/Singleton").ConfigHandler.Config.SetValue("settings", "customcursor", _cursorenabled);
		GetNode<singleton>("/root/Singleton").ConfigHandler.SaveConfig();
		ChangeCursorMode();
	}
	
	public void _on_visibility_changed()
	{
		_cursorenabled = (bool)GetNode<singleton>("/root/Singleton").ConfigHandler.Config.GetValue("settings", "customcursor", true);
		ButtonPressed = _cursorenabled;
		ChangeCursorMode();
	}

	private void ChangeCursorMode()
	{
		if (_cursorenabled)
		{
			//
		}
		else
		{
			//
		}
	}
}
