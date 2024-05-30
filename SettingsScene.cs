using Godot;
using System;
using System.Diagnostics;

public partial class SettingsScene : Control
{
	private HSlider _volumeSlider;
    private Button _backButton;
    private AudioStreamPlayer2D _musicPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_volumeSlider = GetNode<HSlider>("VBoxContainer/HSlider");
        _backButton = GetNode<Button>("VBoxContainer/Button");

		_musicPlayer = GetNode<AudioStreamPlayer2D>("/root/Menu/MusicPlayer");

        if (_musicPlayer == null)
        {
            GD.PrintErr("MusicPlayer not found!");
            return;
        }
		_volumeSlider.Value = Mathf.DbToLinear(_musicPlayer.VolumeDb);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Debug.Print($"{_musicPlayer.VolumeDb}");
	}

	private void OnVolumeSliderValueChanged(float value)
	{
		if (_musicPlayer != null)
        {
            _musicPlayer.VolumeDb = Mathf.LinearToDb(value);
        }
	}

	private void OnBackButtonPressed()
    {
        QueueFree();
    }

}
