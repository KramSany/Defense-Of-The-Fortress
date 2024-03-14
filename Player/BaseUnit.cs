using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;

public partial class BaseUnit : CharacterBody2D
{
    System.Timers.Timer timer = new(1000);
     protected AnimatedSprite2D _animatedSprite;
     public float Health { get; set; }
     public static float DamagePerSecnod {get; set; }
    protected float MoveSpeed = 40.0f;
    protected Vector2 gravity = new Vector2(0, 800.0f);
    private static EnemyBaseUnit enemyBaseUnit;



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
        velocity.Y += gravity.Y * (float)GetProcessDeltaTime();
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
            Vector2 newPosition = currentPosition + new Vector2(MoveSpeed * (float)GetProcessDeltaTime(), 0);
            Position = newPosition;
        }
    }

    internal async void TakeDamage(float damage)
    {
        if (Health >= 0)
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

    


    // Общая логика для обработки входа в область
    public virtual void OnAreaEntered(Node2D node)
    {
        if (node is BaseUnit)
        {
            MoveSpeed = 0.0f;
        }
        else if (node is EnemyBaseUnit)
        {
            enemyBaseUnit = (EnemyBaseUnit)node;
            isEnemy = true;
            MoveSpeed = 0.0f;
            _animatedSprite.Play("Attack");
            timer.Start();
            
        }
    }

    // Общая логика для обработки выхода из области
    public virtual void OnAreaExited(Node2D node)
    {
        MoveSpeed = 40.0f;
        timer.Stop();
    }
    
    public static void GetDamage (EnemyBaseUnit obj)
    {
        enemyBaseUnit.TakeDamage(DamagePerSecnod);
        GD.Print("ge");

    }
    private static void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        GetDamage(enemyBaseUnit);
        
    }
    

}

