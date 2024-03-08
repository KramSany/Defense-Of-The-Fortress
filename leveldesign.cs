using Godot;
using System;
using System.Diagnostics;

public partial class leveldesign : Node2D
{
	public override void _Ready()
	{
		Debug.Print("wtf?");
	}

	public override void _Process(double delta) 
	{

	}

	private void _on_play_2_pressed() // Button for quike exit
	{
    	GetTree().Quit();
	}

	private void SwitchToLevel() // Buttin for start the game
	{
		GetTree().ChangeSceneToFile("res://level.tscn");
	}



}
