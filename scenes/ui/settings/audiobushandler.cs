using Godot;
using System;
using System.ComponentModel;

public partial class audiobushandler : Control
{
	
	[Export] public string AudioBusName;
	
	[Export] public VSlider Slider;

	[Export] public Label Label;
	private int _audioVolume;

	public void _on_slider_value_changed(float changedValue)
	{
		_audioVolume = (int)changedValue;
		GetNode<singleton>("/root/Singleton").ConfigHandler.Config.SetValue("settings", "audio_"+AudioBusName, _audioVolume);
		GetNode<singleton>("/root/Singleton").ConfigHandler.SaveConfig();
		Label.Text = AudioBusName+": "+_audioVolume+"%";
		SetAudioLevels();
	}
	public void _on_slider_visibility_changed()
	{
		_audioVolume = (int)GetNode<singleton>("/root/Singleton").ConfigHandler.Config.GetValue("settings", "audio_"+AudioBusName, 50);
		Slider.Value = _audioVolume;
		SetAudioLevels();
	}

	public void _on_visibility_changed()
	{
		_on_slider_visibility_changed(); // so the loop from singleton can call
	}

	private void SetAudioLevels()
	{
		AudioServer.SetBusVolumeDb(
			AudioServer.GetBusIndex(AudioBusName),
			// ReSharper disable once PossibleLossOfFraction
			(float)(10*Math.Log10(_audioVolume / (double)100))
		);
	}
}
