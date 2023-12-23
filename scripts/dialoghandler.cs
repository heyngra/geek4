using Godot;
using System;
using System.Collections.Generic;

public class DialogSequence
{
	public String Speaker;
	public String Text;
	public String Art;
	public Action Callback;
	public float TimeToSkip;
	public DialogSequence(String speaker, String text, String art, Action callback, float timeToSkip)
	{
		Speaker = speaker;
		Text = text;
		Art = art;
		Callback = callback;
		TimeToSkip = timeToSkip;
	}
}

public class Dialog
{
	public readonly List<DialogSequence> DialogQueue = new();

	public Dialog(params DialogSequence[] dialogSequences)
	{
		DialogQueue.AddRange(dialogSequences);
	}
	
	/// <summary>
	/// Add a dialog to the queue.
	/// </summary>
	/// <param name="speaker">Speaker, displayed below art.</param>
	/// <param name="text">Text of a dialog</param>
	/// <param name="art">Absolute path to the image. (res://...)</param>
	/// <param name="callback">A function, that is called when the dialog is shown to the user.</param>
	/// <param name="timeToSkip">Time required until you can go to the next dialog.</param>

	public void AddDialog(String speaker, String text, String art, Action callback=null, float timeToSkip=0.5f)
	{
		DialogQueue.Add(new(speaker, text, art, callback, timeToSkip));
	}
}

public partial class dialoghandler : Control
{
	private Dialog _currentDialog = null;
	private singleton Singleton;
	private bool _canProceed = false;

	public void PlayDialog(Dialog dialog)
	{
		if (_currentDialog != null)
		{
			return;
		}
		_currentDialog = dialog;
		PlayNextDialog();
	}
	
	/// <summary>
	/// Use this function to initiate dialog mode.
	/// </summary>
	public async void PlayNextDialog()
	{
		_canProceed = false;

		if ((Singleton.UiLock && !Visible ) || _currentDialog==null ) return; // return if there is another UI element blocking movement, f.e exit menu.
		
		Singleton.UiLock = true;

		if (_currentDialog.DialogQueue.Count == 0)
		{
			GetNode<AnimationPlayer>("AnimationPlayer").PlayBackwards("dialog_appear");
			await ToSignal(GetTree().CreateTimer(GetNode<AnimationPlayer>("AnimationPlayer").GetAnimation("dialog_appear").Length), "timeout");
			GetNode<AnimationPlayer>("AnimationPlayer").Play("RESET"); // really ugly way, but Visible = false doesn't work.
			//Visible = false;
			Singleton.UiLock = false;
			_currentDialog = null;
			return;
		}

		DialogSequence currentDialogSequence = _currentDialog.DialogQueue[0];
		_currentDialog.DialogQueue.Remove(currentDialogSequence);
		GetNode<Label>("Speaker").Text = currentDialogSequence.Speaker;
		GD.Print(currentDialogSequence.Text);
		GetNode<Label>("Text").Text = currentDialogSequence.Text;
		GetNode<TextureRect>("Art").Texture = GD.Load<Texture2D>(currentDialogSequence.Art);
		currentDialogSequence.Callback?.Invoke();

		if (Visible == false)
		{
			GetNode<AnimationPlayer>("AnimationPlayer").Play("dialog_appear");
			await ToSignal(GetTree().CreateTimer(GetNode<AnimationPlayer>("AnimationPlayer").GetAnimation("dialog_appear").Length), "timeout");
		}

		await ToSignal(GetTree().CreateTimer(currentDialogSequence.TimeToSkip), "timeout");
		_canProceed = true;
	}
	
	public override void _Process(double delta)
	{

		GetNode<TextureRect>("arrow_down").Visible = _canProceed;
		
		if ((Input.IsActionJustPressed("ui_accept")) && _canProceed && Visible)
		{
			Singleton.PlayAudio("res://assets/sound/dialog click.mp3", -10, "SFX");
			PlayNextDialog();
		}
		
	}
	
	public override void _Ready()
	{
		Singleton = GetNode<singleton>("/root/Singleton");
	}
	
	
}
