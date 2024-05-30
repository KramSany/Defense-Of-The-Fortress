using Godot;
using System;
using System.Diagnostics;

public partial class EnemyBaseNew : Node2D
{
	Timer timer = new Timer();
	ProgressBar progressBar = new ProgressBar();

	private string axeUnit = "res://Enemy/EnemyUnits/enemy_axe_player.tscn";
	private string swordUnit = "res://Player/Mobs/SwordPerson.tscn";
	[Export] float xPos = -200;
	[Export] float yPos = 200;
	[Export] int maxUnit = 5;
	public float HealthBaseEnemy = 100;
	public float Damage = 40.0f;
	bool death = false;
	private BaseUnit unitInAreaBase;
	private bool IsOurUnit = false;
	public override void _Ready()
	{
		progressBar = GetNode<ProgressBar>("ProgressBar");
		timer = GetNode<Timer>("DamageTimer");
		HelperUnitCount.CurrentUnitCount = 0;
	}

	public override void _Process(double delta)
	{
		DeathBaseEnemy();
	}

	private void OnTimerTimeout() // Timer??
	{
		progressBar.Value-= unitInAreaBase.DamagePerSecond;
		HealthBaseEnemy -= unitInAreaBase.DamagePerSecond;
		unitInAreaBase.TakeDamage(Damage);

	}

	private void OnSpawnMobs()
	{
		if (HelperUnitCount.CurrentUnitCount < maxUnit && IsOurUnit == false)
        {
            // Создаем нового моба
            EnemyBaseUnit mob = (EnemyBaseUnit)GD.Load<PackedScene>(axeUnit).Instantiate();
            AddChild(mob);
            mob.Position = new Vector2(xPos, yPos);

            // Увеличиваем счетчик количества мобов
            HelperUnitCount.CurrentUnitCount++;
			Debug.Print($"{HelperUnitCount.CurrentUnitCount}");
        }
	}


	private void AreaEnteredToEnemyBase(Area2D area2D)
	{
    // Получаем родительскую ноду (CharacterBody2D) для Area2D
		Node parent = area2D.GetParent();

		// Проверяем, является ли родительская нода экземпляром CharacterBody2D
		if (parent is BaseUnit)
		{
			// Преобразуем родительскую ноду в CharacterBody2D
			CharacterBody2D characterBody = (CharacterBody2D)parent;
			unitInAreaBase = (BaseUnit)characterBody;
            timer.Start();
		}
		else if (parent is EnemyBaseUnit)
		{
			IsOurUnit = true;
		}
	}
	// СДЕЛАТЬ ТАК-ЖЕ ДЛЯ ЮНИТОВ ВРАГОВ. И СДЕЛАТЬ УЖЕ РАНДОМАТИЗАЦИЮ ЮНИТОВ ДЛЯ ЮНИТОВ ВРАГА И ПРОВЕРИТЬ ФИЗИКУ ДЛЯ ЮНИТА ЗА ДОМОМ

	private void AreaExitedToEnemyBase(Area2D area2D)
	{
		Node parent = area2D.GetParent();
		if (parent is BaseUnit)
		{
			timer.Stop();
		}
		if (parent is EnemyBaseUnit)
		{
			IsOurUnit = false;
		}
	}

	private void DeathBaseEnemy() // ??
	{
		if (HealthBaseEnemy <= 0)
		{
			GD.Print("penis");
			GetTree().ChangeSceneToFile("res://leveldesign.tscn");
		}
	}


	private void OnAreaEntered(Node2D node)
	{
	}

	private void OnAreaExited(Node2D node)
	{
	}
}

public static class HelperUnitCount
{
    private static int _currentUnitCount;

    public static int CurrentUnitCount
    {
        get{ return _currentUnitCount;}
        set{ _currentUnitCount = value;}
    }

    public static int DecreaseUnitCount()
    {
        return _currentUnitCount--;
    }

    public static int InCreaseUnitCount()
    {
        return _currentUnitCount++;
    }
}
