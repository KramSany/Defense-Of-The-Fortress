using Godot;
using System;
using System.Diagnostics;

public partial class SwordUnit : CharacterBody2D
{
    // сделать шлавный класс под всех юнитов
    private AnimatedSprite2D _animatedSprite;
    public float HelathPoint = 100.0f;
    public float DamagePerSecond = 10.0f;
    // Скорость движения игрока
    public float MoveSpeed = 50.0f;
	private Vector2 gravity = new Vector2(0, 800.0f);

    public bool Alive { get; internal set; }

    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    // Вызывается каждый кадр
	private void ApplyGravity()
    {
		Vector2 velocity = Velocity;
        // Применяем гравитацию к вертикальной составляющей скорости
        velocity.Y += gravity.Y * (float)GetProcessDeltaTime();
		Velocity = velocity;
    }
    public override void _Process(double delta)
    {
		
		// ApplyGravity();
        // Движение игрока вправо
		MoveAndSlide();
        MoveRight();
    }

    internal void TakeDamage()
    {
        throw new NotImplementedException();
    }


    private void MoveRight()
    {
        Vector2 currentPosition = Position;
        Vector2 newPosition = currentPosition + new Vector2(MoveSpeed * (float)GetProcessDeltaTime(), 0);
        Position = newPosition;
    }

    public void _on_area_2d_body_entered(Node2D body)
    {

        if (body is CharacterBody2D)
        {
            if (body is AxeUnit)
            {
                MoveSpeed = 40.0f;
                Debug.Print($"{MoveSpeed}");

            }
            
        }
        else if (body is Node2D)
        {
            Debug.Print("lol");
            _animatedSprite.Play("Idle");
            MoveSpeed = 0.0f;
        }
    }

    public void _on_area_2d_body_exited(Node2D body)
    {
        MoveSpeed = 50.0f;
    }
    
	
}
