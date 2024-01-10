using Godot;
using System;

public partial class ConfigHandler : Control
{
	private singleton Singleton;
	public ConfigFile Config = new ConfigFile();
	public override void _Ready()
	{
		Singleton = GetNode<singleton>("/root/Singleton");
		
		Error err = Config.Load("user://config.cfg");
		if (err != Error.Ok)
		{
						
		}
	}

	public void SaveConfig()
	{
		Config.Save("user://config.cfg");
	}
	
}
