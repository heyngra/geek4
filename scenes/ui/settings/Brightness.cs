using Godot;
using System;

public partial class Brightness : HSlider
{
	private int _brightnessLevel = 0;
	
	public void _on_value_changed(float changedValue)
	{
		_brightnessLevel = (int)changedValue;
		GetNode<singleton>("/root/Singleton").ConfigHandler.Config.SetValue("settings", "brightness", _brightnessLevel);
		GetNode<singleton>("/root/Singleton").ConfigHandler.SaveConfig();
	}
	public void _on_visibility_changed()
	{
		_brightnessLevel = (int)GetNode<singleton>("/root/Singleton").ConfigHandler.Config.GetValue("settings", "brightness", 50);
		Value = _brightnessLevel;
	}
}
