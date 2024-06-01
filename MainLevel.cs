using Godot;
using System;

public partial class MainLevel : Node2D
{
    private Node upgradeMenuContainer;
    private AudioStreamPlayer2D _musicPlayer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        upgradeMenuContainer = GetNode<Node>("/root/Map/CanvasLayer");
        if (upgradeMenuContainer == null) GD.Print("Меню контейнера нет");

        _musicPlayer = GetNode<AudioStreamPlayer2D>("LevelMusic");
        _musicPlayer.AddToGroup("AudioPlayers");
        _musicPlayer.VolumeDb = GlobalVolume.VolumeDb;
		GD.Print($"{_musicPlayer.VolumeDb}");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    private void OpenUpgradeMenu()
    {
        // GD.Print("Кнопка прокачки");
        // Node upgradeMenuInstance = ResourceLoader.Load<PackedScene>("res://UpgradeMenu.tscn").Instantiate();
        // AddChild(upgradeMenuInstance);
        Engine.TimeScale = 0;
    }
}

