using Godot;
using System;
using System.Collections.Generic;

public partial class InteractPopup : Control
{
	public singleton Singleton;

	public interactable Closest;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Singleton = GetNode<singleton>("/root/Singleton");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Closest == null || !IsInstanceValid(Closest)) return;
		if (Input.IsActionJustPressed("world_interact"))
		{
			if (Closest.HasSignal("Interact")) Closest.EmitSignal("Interact");
		}
		
		GlobalPosition = Closest.GetGlobalTransformWithCanvas().Origin;
	}

	public override void _PhysicsProcess(double delta) 
	{
		maria_2 maria = Singleton.Maria;
		if (maria == null) return;
		
		List<Node> interactables = new List<Node>();
		
		foreach(var obj in singleton.GetAllChildren(Singleton.CurrentScene))
		{
			if (obj is interactable)
			{
				interactables.Add(obj);
			}
		}
		
		interactables.Sort(((Node a, Node b) =>
		{
			return (a.GetParent<Node2D>().GlobalPosition-
			       maria.GlobalPosition).Length() >
			       (b.GetParent<Node2D>().GlobalPosition-
			       maria.GlobalPosition).Length() ? 1 : -1;
		}));
		if (interactables.Count == 0) return;
		interactable closest = interactables[0] as interactable;
		if (closest == null) return;
		if ((closest.GetParent<Node2D>().GlobalPosition - maria.GlobalPosition).Length() < 30 && !Singleton.UiLock)
		{
			Closest = closest;
			Visible = true;
		}
		else
		{
			Closest = null;
			Visible = false;
		}
		
	}
	
}
