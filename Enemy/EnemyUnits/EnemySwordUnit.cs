using Godot;
using System;

public partial class EnemySwordUnit : EnemyBaseUnit
{
	public EnemySwordUnit()
    {
        Health = 25.0f;
        DamagePerSecnod = 40;
    }
	private void _on_area_2d_body_entered(Node2D node)
    {
        OnAreaEntered(node);
    }

    private void _on_area_2d_body_exited(Node2D node)
    {
        OnAreaExited(node);
    }

    private void _on_area_2d_area_entered(Area2D area)
    {
        OnAreaToAreaEntered(area);
    }

    private void _on_area_2d_area_exited(Area2D area)
    {
        OnAreaToAreaExited(area);
    }
}
