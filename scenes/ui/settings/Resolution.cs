using Godot;
using System;
using System.Linq;
using Godot.Collections;

public partial class Resolution : OptionButton
{
	private Vector2I _resolution = new Vector2I(640, 480);

	private Dictionary<string, Vector2I> _resolutions = new Dictionary<string, Vector2I>
	{
		{"3840x2160", new Vector2I(3840,2160)},
		{"2560x1440", new Vector2I(2560, 1440)},
		{"1920x1080", new Vector2I(1920, 1080)},
		{"1600x900", new Vector2I(1600, 900)},
		{"1536x864", new Vector2I(1536, 864)},
		{"1440x900", new Vector2I(1440,900)},
		{"1366x768", new Vector2I(1366, 768)},
		{"1280x720", new Vector2I(1280, 720)},
		{"800x600", new Vector2I(640, 360)}
	};
	public override void _Ready()
	{
		
		foreach (var (key, value) in _resolutions)
		{
			AddItem(key);
			
			SetItemMetadata(ItemCount-1, value);
		}
	}
	
	
	public void _on_item_selected(int selected)
	{
		_resolution = (Vector2I)GetItemMetadata(selected);		
		GetNode<singleton>("/root/Singleton").ConfigHandler.Config.SetValue("settings", "resolution", _resolution);
		GetNode<singleton>("/root/Singleton").ConfigHandler.SaveConfig();
		ChangeResolution();
	}
	
	public void _on_visibility_changed()
	{
		_resolution = (Vector2I)GetNode<singleton>("/root/Singleton").ConfigHandler.Config.GetValue("settings", "resolution", new Vector2I(640, 360));
		Selected = GetItemIndex(
				_resolutions.Values.Select( (item, index) => new {Item = item, Index = index}).First(i => i.Item == _resolution).Index
			);
		ChangeResolution();
	}

	private void ChangeResolution()
	{
		DisplayServer.WindowSetSize(_resolution);
	}
}
