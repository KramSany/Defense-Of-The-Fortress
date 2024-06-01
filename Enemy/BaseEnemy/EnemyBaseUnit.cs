using Godot;
using System;
using System.Threading.Tasks;

public partial class EnemyBaseUnit : CharacterBody2D
{
    public EnemyBaseNew enemyBaseNewInstance = new EnemyBaseNew();
    private Timer timer;
    private BaseUnit playerUnit;
    protected AnimatedSprite2D _animatedSprite;
    public float Health { get; set; }
    public float DamagePerSecnod { get; set; }
    protected float MoveSpeed = 40.0f;
    protected Vector2 gravity = new Vector2(0, 800.0f);

    protected bool isEnemy = false;
    public bool unitIsDead = false;
    private bool inBasePlayer = false;
    private ProgressBar healthBar;

    public override void _Ready()
    {
        healthBar = GetNode<ProgressBar>("ProgressBar");
        healthBar.MaxValue = Health;
        timer = new Timer();
        timer.WaitTime = 1.0f; // 1000ms
        Callable onTimedEventCallable = new Callable(this, nameof(OnTimedEvent));
        timer.Connect("timeout", onTimedEventCallable);
        AddChild(timer);
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
            _animatedSprite.CallDeferred("play", "Idle"); // Используем CallDeferred
        }
        else if (MoveSpeed > 0)
        {
            _animatedSprite.CallDeferred("play", "Walk"); // Используем CallDeferred
        }
            healthBar.Value = Health;

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
        HelperUnitCount.CurrentUnitCount--;
        GD.Print("death");
        unitIsDead = true;
        _animatedSprite.CallDeferred("stop"); // Используем CallDeferred
        _animatedSprite.CallDeferred("play", "Death"); // Используем CallDeferred

        await Task.Delay(800); // delay for animation "death" playing until end. Without this delay animation is not ended because method QueueFree() will clear object
        QueueFree();
    }

    public virtual void OnAreaEntered(Node2D node)
        {
            if (node is BaseUnit unit)
            {
            if (!inBasePlayer)
                {
                    GD.Print("В поле зрения игрок");
                    playerUnit = unit;
                    isEnemy = true;
                    MoveSpeed = 0.0f;
                    _animatedSprite.CallDeferred("play", "Attack"); // Используем CallDeferred
                    timer.Start();
                }
            }
            else if (node is EnemyBaseUnit)
            {
                MoveSpeed = 0.0f;
            }
    }

    public virtual void OnAreaExited(Node2D node)
        {
            isEnemy = false;
            MoveSpeed = 40.0f;
            timer.Stop();
        }

    public virtual void OnAreaToAreaEntered(Area2D area)
        {
            if (area.Name == "AreaEnemyBase")
            {
                GD.Print("моя база");
            }
            else if (area.Name == "AreaBasePlayer")
            {
                inBasePlayer = true;
                isEnemy = true;
                MoveSpeed = 0.0f;
                _animatedSprite.CallDeferred("play", "Attack"); // Используем CallDeferred
                timer.Start();
            }
        }

    public virtual void OnAreaToAreaExited(Area2D area)
        {
            timer.Stop();
        }

    private void OnTimedEvent()
    {
        if (playerUnit != null && !playerUnit.unitIsDead)
        {
            GD.Print("Урон!");
            // Используем CallDeferred для вызова TakeDamage в основном потоке
            playerUnit.CallDeferred(nameof(BaseUnit.TakeDamage), DamagePerSecnod);
        }
    }
}