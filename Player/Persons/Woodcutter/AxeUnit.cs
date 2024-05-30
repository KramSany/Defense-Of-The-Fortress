using Godot;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

public partial class AxeUnit : BaseUnit
{
    Timer timer = new Timer();
    public static float BaseDamagePerSecond = 25.0f;
    public static float BaseHealth = 50.0f;

    public AxeUnit()
    {
        DamagePerSecond = BaseDamagePerSecond;
        Health = BaseHealth;
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
        BaseHealth += amount;
        GD.Print("Base Health upgraded to: " + BaseHealth);
    }

    public static void UpgradeBaseDamage(float amount)
    {
        BaseDamagePerSecond += amount;
        GD.Print("Base Damage upgraded to: " + BaseDamagePerSecond);
    }
}