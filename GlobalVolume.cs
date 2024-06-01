using Godot;
using System.Collections.Generic;

public static class GlobalVolume
{
    private static float _volumeDb = 0f;

    public static float VolumeDb
    {
        get => _volumeDb;
        set
        {
            _volumeDb = value;
            UpdateVolume();
        }
    }

    private static void UpdateVolume()
    {
        foreach (AudioStreamPlayer2D player in GetAudioPlayers())
        {
            player.VolumeDb = _volumeDb;
        }
    }

    private static List<AudioStreamPlayer2D> GetAudioPlayers()
    {
        List<AudioStreamPlayer2D> audioPlayers = new List<AudioStreamPlayer2D>();
        SceneTree tree = Godot.Engine.GetMainLoop() as SceneTree;

        if (tree != null)
        {
            var nodes = tree.GetNodesInGroup("AudioPlayers");

            foreach (Node node in nodes)
            {
                if (node is AudioStreamPlayer2D audioPlayer)
                {
                    audioPlayers.Add(audioPlayer);
                }
            }
        }

        return audioPlayers;
    }
}
