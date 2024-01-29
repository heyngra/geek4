using Godot;
using System;

public partial class test : Node
{
    //public string OverrideSaveName = "testtest";
    
    public Godot.Collections.Dictionary<string, Variant> SaveData()
    {
        return new Godot.Collections.Dictionary<string, Variant>
        {
            {"lorem", "ipsum"}
        };
    }

    public override void _Ready()
    {
        GD.Print("Wartość to: "+ GetNode<singleton>("/root/Singleton").SaveHandler.GetValue(this, "lorem"));
    }
}
