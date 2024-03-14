using Godot;
using System;

public partial class EnemyAxePlayer : EnemyBaseUnit
{
	public EnemyAxePlayer()
    {
        Health = 50.0f;
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
