using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;

public partial class EnemyBaseUnit : CharacterBody2D
{
    System.Timers.Timer timer = new(1000);
    private BaseUnit playerUnit;
     protected AnimatedSprite2D _animatedSprite;
     public float Health { get; set; }
     public float DamagePerSecnod {get; set; }
    protected float MoveSpeed = 40.0f;
    protected Vector2 gravity = new Vector2(0, 800.0f);

    protected bool isEnemy = false;
    private bool unitIsDead = false;

    public override void _Ready()
    {
        timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    protected void ApplyGravity()
    {
        Vector2 velocity = Velocity;
        velocity.Y = gravity.Y * (float)GetProcessDeltaTime();
        Velocity = velocity;
    }

    public override void _Process(double delta)
    {
        ApplyGravity();

        if (MoveSpeed <= 0 && !isEnemy && !unitIsDead)
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

    protected void MoveRight()
    {
        if (MoveSpeed != 0.0f)
        {
            Vector2 currentPosition = Position;
            Vector2 newPosition = currentPosition + new Vector2(-MoveSpeed * (float)GetProcessDeltaTime(), 0);
            Position = newPosition;
        }
    }

    internal async void TakeDamage(float damage)
    {
        if (Health > 0)
        {
            Health -= damage;
            GD.Print("Get damage");

        }
        else 
        {
            _animatedSprite.Stop();
            _animatedSprite.Play("Death");
            await Task.Delay(900); // delay for animation "death" playing until end. Without this delay animation is not ended because method QueueFree() will clear object
            QueueFree();
            
        }
        
    }

    public virtual void OnAreaEntered(Node2D node)
        {
            if (node is BaseUnit unit)
        {
            playerUnit = unit;
            isEnemy = true;
            MoveSpeed = 0.0f;
            _animatedSprite.Play("Attack");
            timer.Start();
        }
        else if (node is EnemyBaseUnit)
        {
            // Handle the case when a non-enemy BaseUnit is encountered
            MoveSpeed = 0.0f;
        }
        else
        {
            // Handle other cases (e.g., Node2D instances that are not EnemyBaseUnit or BaseUnit)
            GD.Print($"Entered base area");
            isEnemy = true;
            MoveSpeed = 0.0f;
            _animatedSprite.Play("Attack");
        }

        
    }
    
    public virtual void OnAreaExited(Node2D node)
    {
        isEnemy = true;
        MoveSpeed = 40.0f;
    }

    private void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        if (!playerUnit.unitIsDead)
        {
            playerUnit.TakeDamage(DamagePerSecnod);
        }
        
        
    }

}
