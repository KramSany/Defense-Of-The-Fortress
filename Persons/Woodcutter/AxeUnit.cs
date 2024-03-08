using Godot;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

public partial class AxeUnit : CharacterBody2D
{
    public float HelathPoint = 100.0f;
    // Скорость движения юнита
    public float MoveSpeed = 40.0f;
	private Vector2 gravity = new Vector2(0, 800.0f);

    public bool Alive { get; internal set; }

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
		
		ApplyGravity();
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
        // Получаем текущую позицию игрока
        Vector2 currentPosition = Position;

        // Вычисляем новую позицию, смещая ее вправо
        Vector2 newPosition = currentPosition + new Vector2(MoveSpeed * (float)GetProcessDeltaTime(), 0);

        // Устанавливаем новую позицию для игрока
        Position = newPosition;
    }
	
}
