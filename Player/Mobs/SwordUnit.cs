using Godot;
using System;
using System.Diagnostics;

public partial class SwordUnit : BaseUnit
{
    Timer timer = new Timer();
    public static float _baseDamagePerSecond = 25.0f;
    public static float _baseHealth = 50.0f;
    public SwordUnit()
    {
        Health = _baseHealth;
        DamagePerSecond = _baseDamagePerSecond;
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

    public static void UpgradeBaseHealth(float amount)
    {
        _baseHealth += amount;
        GD.Print("Base Health upgraded to: " + _baseHealth);
    }

    public static void UpgradeBaseDamage(float amount)
    {
        _baseDamagePerSecond += amount;
        GD.Print("Base Damage upgraded to: " + _baseDamagePerSecond);
    }

}
