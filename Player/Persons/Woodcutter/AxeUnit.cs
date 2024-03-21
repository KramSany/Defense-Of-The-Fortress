using Godot;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

public partial class AxeUnit : BaseUnit
{
    Timer timer = new Timer();
    public float damagePerSecond = 25.0f;
    public float helath = 50.0f;
    public AxeUnit()
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