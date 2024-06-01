using Godot;
using System;
using System.Diagnostics;

public partial class SettingsScene : Control
{
    private HSlider _volumeSlider;
    private Button _backButton;

    public override void _Ready()
    {
        _volumeSlider = GetNode<HSlider>("VBoxContainer/HSlider");
        _backButton = GetNode<Button>("VBoxContainer/Button");

        _volumeSlider.Value = Mathf.DbToLinear(GlobalVolume.VolumeDb);
    }

    private void OnVolumeSliderValueChanged(float value)
    {
        GlobalVolume.VolumeDb = Mathf.LinearToDb(value);
    }

    private void OnBackButtonPressed()
    {
        QueueFree();
    }
}
