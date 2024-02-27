using Godot;
using System;

public partial class principalexit : Node2D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void _on_node_2d_interact()
    {
        GetNode<singleton>("/root/Singleton").GotoScene("res://scenes/world/school_corridor.tscn", default, default, new(718, -24));
    }
}