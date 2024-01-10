using Godot;
using System;

public partial class SettingsHandler : TabContainer
{
	private singleton Singleton;
    
    // Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Singleton = GetNode<singleton>("/root/Singleton");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_fullscreen_button_pressed()
	{

		bool fullscreen;
		if (GetNode<singleton>("/root/Singleton").SaveHandler == null)
		{
			fullscreen = false;
		}
		else
		{
			fullscreen = (bool)GetNode<singleton>("/root/Singleton").SaveHandler.GetValue(this, "fullscreen", "unique");
		}

		if (fullscreen)
		{
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
		}
		else
		{
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
		}

		Singleton.SaveHandler.SetUniqueValue("fullscreen", fullscreen);
	}

	public void _on_fullscreen_button_ready()
	{
		foreach (var allChild in singleton.GetAllChildren(this))
		{
			if (allChild.Name == "FullscreenButton") { //TODO: make it normal, and not hardcoded (most likely read the signal caller)
				Button button = (Button)allChild;
				bool buttonState;
				if (GetNode<singleton>("/root/Singleton").SaveHandler == null)
				{
					buttonState = false;
				}
				else
				{
					buttonState = (bool)GetNode<singleton>("/root/Singleton").SaveHandler.GetValue(this, "fullscreen", "unique");
				}

				button.ButtonPressed = buttonState;

			}
		}
	}
}
