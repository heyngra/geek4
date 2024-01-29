using Godot;
using System;

public partial class ConfigHandler : Control
{
	private singleton Singleton;
	public ConfigFile Config = new ConfigFile();
	private bool _queueSave = false;
	public override void _Ready()
	{
		Singleton = GetNode<singleton>("/root/Singleton");
		Error err = Config.Load("user://config.cfg");
		if (err != Error.Ok)
		{
						
		}
	}


	public override void _PhysicsProcess(double d)
	{
		if (_queueSave)
		{
			saveConfig();
		}
	}

	private void saveConfig()
	{
		ToSignal(GetTree().CreateTimer(0.5f), "timeout");
		Config.Save("user://config.cfg");
		_queueSave = false;
	}
	
	public void SaveConfig()
	{
		_queueSave = true;	
	}
	
}
