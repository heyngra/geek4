using Godot;
using System;

public partial class interactable : Node2D
{
	[Signal]
	public delegate void InteractEventHandler();
}
