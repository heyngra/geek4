using Godot;
using System;

public partial class AirState : State
{
    private GroundState _groundState;
    private bool sceneChanged; // part of changing a scene, debug only

    public override void _Ready()
    {
        _groundState = GetState<GroundState>();
    }

    public override void OnEnter()
    {
        sceneChanged = false; // part of changing a scene, debug only
    }

    public override void StateProcess(double delta)
    {
        if (Character.IsOnFloor())
        {
            NextState = _groundState;
        }

        if (Character.Position.Y > 1000 && !sceneChanged) // part of changing a scene, debug only
        {
            sceneChanged = true;
            singleton Singleton = GetNode<singleton>("/root/Singleton");
            if (GD.Randi() % 2 == 1)
            {
                Singleton.PauseScene("res://scenes/world/test_env3.tscn");
                Singleton.Dialoghandler.PlayNextDialog();
                
            }
            else
            {
                Singleton.GotoScene("res://scenes/world/test_env2.tscn");

                Dialog dialog = new Dialog();
                
                dialog.AddDialog("Kamil", "Ale nie chce mi się iść do szkoły!", "res://assets/ui/Haley.png");
                dialog.AddDialog("Kamila", "No mi też, ale ja mieszkam na Bagnie", "res://assets/ui/qs.png");
                dialog.AddDialog("Kamila", "a nie w Elblągu.", "res://assets/ui/qs.png");
                dialog.AddDialog("Kamil", "Aha, no to faktycznie.", "res://assets/ui/Haley.png");
                
                Singleton.Dialoghandler.PlayDialog(dialog);
                Singleton.Dialoghandler.PlayNextDialog();
                
            }
        }
    }
}
