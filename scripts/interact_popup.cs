using Godot;
using System;

public partial class interact_popup : Node2D
{

	[Export] public Node2D ReferencedObject;
	[Export] public PackedScene Popup = GD.Load<PackedScene>("res://scenes/ui/interact_popup.tscn");
	
	private Node _insitiatedPopup = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)	
	{
		if (ReferencedObject == null) return;
		float dist = ReferencedObject.Position.DistanceTo(this.Position);
		GD.Print(dist);
		if (dist < 30 && _insitiatedPopup == null)
		{
			var popup = Popup.Instantiate();
			AddSibling(popup);
			_insitiatedPopup = popup;
			Control nd = (Control)popup;
			nd.Position = new Vector2(this.Position.X+13, this.Position.Y-18);
			/*var created_node = GetNode("interact_popup");
			var control = popup.GetNodeOrNull<Control>("Control");*/
		}
		else if (dist >= 30 && _insitiatedPopup != null)
		{
			_insitiatedPopup.QueueFree();
			_insitiatedPopup = null;
		}
	}
}
