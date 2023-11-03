using Godot;
using System;

public partial class State : Node
{
    [Export] public bool CanMove = true;
    public CharacterBody2D Character;
    public State NextState;
    public AnimationNodeStateMachinePlayback Playback;

    public virtual void StateInput(InputEvent @event) {
        
    }
    public virtual void OnExit() {}
    public virtual void OnEnter() {}

    public virtual void StateProcess(double delta)
    {
        
    }
    
    public T GetState<T>() where T : State {
        foreach (var state in GetParent().GetChildren())
        {
            if (state is T)
            {
                return GetNode<T>(state.GetPath());
            }
        }

        return null;
    }
}
