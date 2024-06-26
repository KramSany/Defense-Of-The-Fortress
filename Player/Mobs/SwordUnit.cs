using Godot;
using System;
using System.Diagnostics;

public partial class SwordUnit : BaseUnit
{
    public float damagePerSecond = 10.0f;
    public float helath = 10.0f;
    public SwordUnit()
    {
        Health = helath;
        DamagePerSecnod = damagePerSecond;
    }
   private void _on_area_2d_body_entered(Node2D node)
    {
        OnAreaEntered(node);
    }

    private void _on_area_2d_body_exited(Node2D node)
    {
        OnAreaExited(node);
    }
}
