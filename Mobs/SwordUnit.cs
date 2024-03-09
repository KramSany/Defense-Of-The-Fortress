using Godot;
using System;
using System.Diagnostics;

public partial class SwordUnit : CharacterBody2D
{
    private AnimatedSprite2D _animatedSprite;
    public float HelathPoint = 100.0f;
    public float DamageUnit = 5;
    // Скорость движения юнита
    public float MoveSpeed = 40.0f;
	private Vector2 gravity = new Vector2(0, 800.0f);

    public bool isEnemy = false;

    private void DeathUnit()
    {
        if (HelathPoint <= 0) QueueFree();
        
    }

    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }
	private void ApplyGravity()
    {
        
		Vector2 velocity = Velocity;
        
        velocity.Y += gravity.Y * (float)GetProcessDeltaTime();
        
		Velocity = velocity;
    }
    public override void _Process(double delta)
    {
        DeathUnit();
		Debug.Print($"{HelathPoint}");
		ApplyGravity();
        if (MoveSpeed <= 0 && isEnemy != true)
        {
             _animatedSprite.Play("Idle");
        }
        else if (MoveSpeed > 0)
        {
            _animatedSprite.Play("Walk");
        }
		MoveAndSlide();
        MoveRight();
        
    }

    internal void TakeDamage(float damage)
    {
        HelathPoint-=damage;
    }


    private void MoveRight()
{
    if (MoveSpeed != 0.0f)
    {
        Vector2 currentPosition = Position;
        Vector2 newPosition = currentPosition + new Vector2(MoveSpeed * (float)GetProcessDeltaTime(), 0);
        Position = newPosition;
    }
}

    private void _on_area_2d_body_entered(Node2D node)
    {
        if (node is CharacterBody2D)
        {
            
            if (node is AxeUnit)
            {
                MoveSpeed = 0.0f;
                

            }
            
        }
        else if (node is Node2D)
        {
            isEnemy = true;
            MoveSpeed = 0.0f;
            _animatedSprite.Play("Atack");
            Debug.Print($"penis");
        }
    }

    private void _on_area_2d_body_exited(Node2D node)
    {
        MoveSpeed = 40.0f;

    }
	
}
