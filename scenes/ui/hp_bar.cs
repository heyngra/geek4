using Godot;
using System;

public partial class hp_bar : ProgressBar
{
    public singleton Singleton;
    public override void _Ready() {
        Singleton = GetNode<singleton>("/root/Singleton"); 
    }
    public override void _Process(double delta)
    {
        Value = Singleton.HP; 
    }
}