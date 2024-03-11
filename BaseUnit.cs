using Godot;
using System;
using System.Collections.Generic;

public partial class BaseUnit : CharacterBody2D
{
    public UnitType UnitType { get; protected set; } = UnitType.None;
     protected AnimatedSprite2D _animatedSprite;
     public float Health { get; set; }
    protected float MoveSpeed = 40.0f;
    protected Vector2 gravity = new Vector2(0, 800.0f);
    private List<Type> validNodeTypes = new List<Type> { typeof(AxeUnit), typeof(SwordUnit) };

    protected bool isEnemy = false;

    public override void _Ready()
    {
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

        if (MoveSpeed <= 0 && !isEnemy)
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

    internal void TakeDamage(float damage)
    {
        if (Health >= 0)
        {
            Health -= damage;
            GD.Print("Get damage");

        }
         
        else QueueFree();
        
    }


    // Общая логика для обработки входа в область
    public virtual void OnAreaEntered(Node2D node)
    {
        if (validNodeTypes.Contains(node.GetType()))
        {
            MoveSpeed = 0.0f;
        }
        else if (node is Node2D)
        {
            isEnemy = true;
            MoveSpeed = 0.0f;
            _animatedSprite.Play("Attack");
            GD.Print($"Entered base area");
        }
    }

    // Общая логика для обработки выхода из области
    public virtual void OnAreaExited(Node2D node)
    {
        MoveSpeed = 40.0f;
    }

    // Ваши другие общие методы или свойства могут быть добавлены здесь
}

