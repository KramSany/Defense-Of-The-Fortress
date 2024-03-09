using Godot;
using System;
using System.Diagnostics;

public partial class BaseEnemy : Node2D
{
	Timer timer = new Timer();
	ProgressBar progressBar = new ProgressBar();

	public float HealthBaseEnemy = 100;
	public float Damage = 40.0f;
	SwordUnit sword = new();
	bool death = false;
	
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
		if (node is CharacterBody2D)
		{
			if (node is SwordUnit)
			{
				sword = (SwordUnit)node;
				timer.Start();
			}
		}

	}

	private void _on_timer_timeout() // Timer??
	{
		progressBar.Value-= sword.DamageUnit;
		HealthBaseEnemy -= sword.DamageUnit;
		sword.TakeDamage(Damage);
		
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
