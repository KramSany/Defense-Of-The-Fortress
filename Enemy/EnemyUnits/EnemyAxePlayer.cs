using Godot;
using System;

public partial class EnemyAxePlayer : EnemyBaseUnit
{
	public EnemyAxePlayer()
    {
        Health = 100.0f;
        DamagePerSecnod = 25;
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
        OnAreaToAreaNtered(area);
    }

    private void _on_area_2d_area_exited(Area2D area)
    {
        OnAreaToAreaExited(area);
    }

    

}
