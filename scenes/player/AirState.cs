using Godot;
using System;
using Geek4.scripts.quests;

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
                Singleton.PauseScene("res://scenes/world/test_env3.tscn", "dissolve", new(-120, 4));
                Singleton.Dialoghandler.PlayNextDialog();
                
            }
            else
            {
                Singleton.GotoScene("res://scenes/world/test_env2.tscn");
                Dialog dialog = new Dialog();
                
                dialog.AddDialog("Kamila", "Ale nie chce mi się rysować!", "res://assets/ui/Haley.png");
                dialog.AddDialog("Kamil", "No ale musisz rysować, bo nie skończymy gry", "res://assets/ui/qs.png");
                dialog.AddDialog("Kamila", "Nie obchodzi mnie to.", "res://assets/ui/Haley.png");
                dialog.AddDialog("Kamil", "No i mamy problem..." );
                if (Singleton.QuestHandler.GetQuestInstance(typeof(ExampleQuest)) != null && !Singleton.QuestHandler.GetQuestInstance(typeof(ExampleQuest)).IsStarted)
                {
                    dialog.AddDialog("Maria", "Powinnam porozmawiać z szafką...", "res://assets/ui/qs.png", () =>
                    {
                        Singleton.QuestHandler.GetQuestInstance(typeof(ExampleQuest)).StartQuest();
                    });
                }
                
                
                Singleton.Dialoghandler.PlayDialog(dialog);
                Singleton.Dialoghandler.PlayNextDialog();
                
            }
        }
    }
}
