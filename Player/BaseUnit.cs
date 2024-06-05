using Godot;
using System;
using System.Threading.Tasks;
using System.Timers;

public partial class BaseUnit : CharacterBody2D
{
    System.Timers.Timer timer = new(1000);
    protected AnimatedSprite2D _animatedSprite;
    public float Health { get; set; }
    public float DamagePerSecond {get; set; }
    protected float MoveSpeed = 40.0f;
    protected Vector2 gravity = new Vector2(0, 800.0f);
    private static EnemyBaseUnit enemyBaseUnit;
    private ProgressBar healthBar;

    protected bool isEnemy = false;
    public bool unitIsDead = false;
    private bool unitInBase = false;

    public override void _Ready()
    {
        healthBar = GetNode<ProgressBar>("ProgressBar");
        healthBar.MaxValue = Health;
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

        if (MoveSpeed <= 0 && !isEnemy && !unitIsDead && !unitInBase)
        {
            _animatedSprite.Play("Idle");
        }
        else if (MoveSpeed > 0 && !unitInBase)
        {
            _animatedSprite.Play("Walk");
        }
        healthBar.Value = Health;
        MoveAndSlide();
        MoveRight();
    }

    protected void MoveRight()
    {
        if (MoveSpeed != 0.0f && !unitInBase)
        {
            Vector2 currentPosition = Position;
            Vector2 newPosition = currentPosition + new Vector2(MoveSpeed * (float)GetProcessDeltaTime(), 0);
            Position = newPosition;
        }
    }

    public virtual void OnAreaPlayerEnyered(Area2D area2D)
    {
        if (area2D.Name == "AreaEnemyBase")
        {
            isEnemy = true;
            unitInBase = true;
            MoveSpeed = 0.0f;
            _animatedSprite.Play("Attack");
        }
    }

    // Общая логика для обработки входа в область
    public virtual void OnAreaEntered(Node2D node)
    {

        if (node.Name == "EnemyBase")
        {
            GD.Print("Я атакую базу");
        }


        if (node is EnemyBaseUnit unit)
    {
        enemyBaseUnit = unit;
        isEnemy = true;
        MoveSpeed = 0.0f;
        _animatedSprite.Play("Attack");
        timer.Start();
    }
    else if (node is BaseUnit)
    {
        // Handle the case when a non-enemy BaseUnit is encountered
        MoveSpeed = 0.0f;
    }
    //     GD.Print("Я атакую базу");

        // GD.Print($"{node}");
        // if (node is BaseEnemy)
        // {
        //     GD.Print("this is enemy");
        //     MoveSpeed = 0.0f;
        // }
        // else if (node is EnemyBaseUnit unit)
        // {
        //     GD.Print("this is enemy");
        //     enemyBaseUnit = unit;
        //     isEnemy = true;
        //     MoveSpeed = 0.0f;
        //     _animatedSprite.Play("Attack");
        //     timer.Start();
        // }
        // else if (node is BaseEnemy)
        // {
        //     GD.Print("this is enemy base");
        // }
    }

    // Общая логика для обработки выхода из области
    public virtual void OnAreaExited(Node2D node)
    {
        MoveSpeed = 40.0f;
        timer.Stop();
    }
    public static void GetDamage (EnemyBaseUnit obj)
    {
        // obj.TakeDamage(DamagePerSecnod);

    }
    private void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        if (enemyBaseUnit.unitIsDead == false)
        {
            enemyBaseUnit.TakeDamage(DamagePerSecond);
        }
    }
    internal void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Death();
        }
    }

    internal async void Death()
    {
        if (_animatedSprite != null && !unitIsDead)
            {
                unitIsDead = true;
                _animatedSprite?.Stop();
                _animatedSprite?.Play("Death");
                await Task.Delay(800);
                QueueFree();
            }
    }
}