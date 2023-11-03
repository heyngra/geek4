using Godot;
using System;
using Godot.Collections;

public partial class CharacterStateMachine : Node
{

    [Export] public State CurrentState;
    [Export] public AnimationTree AnimationTree;
    public CharacterBody2D Character;
    
    [Export]
    public Array<State> States = new Array<State>();
    
    public override void _Ready()
    {
        Character = GetParent<CharacterBody2D>();
        foreach (var child in GetChildren())
        {
            if (child is State)
            {
                State stateChild = child as State;
                States.Add(stateChild);

                stateChild.Character = Character;
                stateChild.Playback = AnimationTree.Get("parameters/playback").As<AnimationNodeStateMachinePlayback>();
                GD.Print(stateChild.Playback);


            }
            else
            {
                GD.PushWarning("Child " + child.Name + " is not a State.");
            }
        }
    }
    public override void _PhysicsProcess(double delta)
    {
        if (CurrentState.NextState != null)
        {
            SwitchStates(CurrentState.NextState);
        }
        CurrentState.StateProcess(delta);
    }
    public bool CheckMove()
    {
        return CurrentState.CanMove;
    }
    public void SwitchStates(State state)
    {
        if (CurrentState != null)
        {
            CurrentState.OnExit();
            CurrentState.NextState = null;
        }
        CurrentState =  state;
        CurrentState.OnEnter();
    }
    
    public override void _Input(InputEvent @event)
    {
        CurrentState.StateInput(@event);
    }
}
