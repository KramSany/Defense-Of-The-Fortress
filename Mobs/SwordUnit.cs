using Godot;
using System;
using System.Diagnostics;

public partial class SwordUnit : BaseUnit
{
    public SwordUnit()
    {
        Health = 100.0f; // Устанавливаем начальное значение HP для SwordUnit
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
