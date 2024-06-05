using Godot;
using System;

public partial class LoseContent : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnRerunPressed()
	{
		GetTree().ReloadCurrentScene();
		QueueFree();
	}

	private void OnBackToMenuPressed()
	{
	GetTree().ChangeSceneToFile("res://leveldesign.tscn");
	QueueFree();
	}
}
