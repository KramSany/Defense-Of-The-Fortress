using Godot;
using System;
using System.Diagnostics;

public partial class SwordUnit : BaseUnit
{
    Timer timer = new Timer();
    public float damagePerSecond = 10.0f;
    public float helath = 99.0f;
    public SwordUnit()
    {
        Health = helath;
        DamagePerSecond = damagePerSecond;
    }
    private void _on_area_2d_body_entered(Node2D node)
    {
        OnAreaEntered(node);
    }

    private void _on_area_2d_body_exited(Node2D node)
    {
        OnAreaExited(node);
    }
    private void _on_area_player_area_entered(Area2D area2D)
    {
        OnAreaPlayerEnyered(area2D);
    }

}
