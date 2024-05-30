using Godot;
using GodotPlugins.Game;
using System;
using System.Diagnostics;

public partial class leveldesign : Node2D
{
	private AudioStreamPlayer2D _musicPlayer;
    public override void _Ready()
    {
        _musicPlayer = GetNode<AudioStreamPlayer2D>("MusicPlayer");
    }

    public override void _Process(double delta)
    {
    }

    private void _on_play_2_pressed()
    {
        GetTree().Quit();
    }

    private void SwitchToLevel()
    {
        GetTree().ChangeSceneToFile("res://level.tscn");
    }

    private void SwitchToSettingsMenu()
    {
		Node main = ResourceLoader.Load<PackedScene>("res://SettingsScene.tscn").Instantiate();
        GetTree().Root.AddChild(main);
    }
}
