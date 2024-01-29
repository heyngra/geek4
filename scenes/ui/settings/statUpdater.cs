using Godot;
using System;
using System.Globalization;

public partial class statUpdater : Label
{
	public void _on_slider_value_changed(float value)
	{
		Text = value.ToString(CultureInfo.InvariantCulture);
	}
}
