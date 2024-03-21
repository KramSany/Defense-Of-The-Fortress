using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class BaseEnemy : Node2D
{
	private List<Type> validNodeTypes = new List<Type> { typeof(AxeUnit), typeof(SwordUnit) };
	Timer timer = new Timer();
	ProgressBar progressBar = new ProgressBar();

	public float HealthBaseEnemy = 100;
	public float Damage = 40.0f;
	
	bool death = false;
	private BaseUnit unitInAreaBase;
	
	public override void _Ready()
	{
		progressBar = GetNode<ProgressBar>("ProgressBar");
		timer = GetNode<Timer>("Timer");
	}

	public override void _Process(double delta)
	{
		DeathBaseEnemy();
	}

	private void _on_detected_unit_body_entered(Node2D node) // Entered area for player units
	{
		if (node is BaseUnit)
		{
			unitInAreaBase = (BaseUnit)node;
			timer.Start();
		}
	}

	private void _on_detected_unit_body_exited(Node2D node)
	{
		timer.Stop();
	}

	private void _on_timer_timeout() // Timer??
	{
		progressBar.Value-= unitInAreaBase.DamagePerSecnod;
		HealthBaseEnemy -= unitInAreaBase.DamagePerSecnod;
		unitInAreaBase.TakeDamage(Damage);
		
		
	}

	private void DeathBaseEnemy() // ??
	{
		if (HealthBaseEnemy <= 0)
		{
			Debug.Print("penis");
			GetTree().ChangeSceneToFile("res://leveldesign.tscn");
		}
	}
}
