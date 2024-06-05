using Godot;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

public partial class EnemyBaseNew : Node2D
{
    Timer damageTimer = new Timer();
    Timer spawnTimer = new Timer();
    ProgressBar progressBar = new ProgressBar();
    Random random = new Random();

    private string axeUnit = "res://Enemy/EnemyUnits/enemy_axe_player.tscn";
    private string swordUnit = "res://Enemy/EnemyUnits/EnemySwordUnit.tscn";

    [Export] float xPos = -200;
    [Export] float yPos = 200;
    [Export] int maxUnit = 5;

    // Экспортированные поля для интервалов спавна
    [Export] float minSpawnInterval = 5.0f;
    [Export] float maxSpawnInterval = 10.0f;

    public float HealthBaseEnemy = 100;
    public float Damage = 40.0f;
    bool death = false;
    private BaseUnit unitInAreaBase;
    private Node winScene;
    private bool IsOurUnit = false;

    public override void _Ready()
    {
        winScene = ResourceLoader.Load<PackedScene>("res://win_content.tscn").Instantiate();
        progressBar = GetNode<ProgressBar>("ProgressBar");
        progressBar.MaxValue = HealthBaseEnemy;
        damageTimer = GetNode<Timer>("DamageTimer");
        spawnTimer = GetNode<Timer>("SpawnMobs");
        HelperUnitCount.CurrentUnitCount = 0;

        // Настраиваем таймер для спавна юнитов
        spawnTimer.WaitTime = GetRandomSpawnInterval();
        spawnTimer.OneShot = false;  // Повторяющийся таймер
        spawnTimer.Start();
    }

    public override void _Process(double delta)
    {
        if (!death)
    {
        DeathBaseEnemy();
    }
    }

    private void OnDamageTimerTimeout()
    {
        progressBar.Value -= unitInAreaBase.DamagePerSecond;
        HealthBaseEnemy -= unitInAreaBase.DamagePerSecond;
        unitInAreaBase.TakeDamage(Damage);
    }

    private void OnSpawnMobs()
    {
        if (HelperUnitCount.CurrentUnitCount < maxUnit && !IsOurUnit)
        {
            // Выбираем случайный тип юнита
            string unitPath = GetRandomUnit();

            // Создаем нового моба
            EnemyBaseUnit mob = (EnemyBaseUnit)GD.Load<PackedScene>(unitPath).Instantiate();
            AddChild(mob);
            mob.Position = new Vector2(xPos, yPos);

            // Увеличиваем счетчик количества мобов
            HelperUnitCount.CurrentUnitCount++;
            Debug.Print($"{HelperUnitCount.CurrentUnitCount}");

            // Задаем следующий интервал спавна
            spawnTimer.WaitTime = GetRandomSpawnInterval();
            spawnTimer.Start();
        }
    }

    private string GetRandomUnit()
    {
        // Генерируем случайное число для выбора юнита
        int unitType = random.Next(2);
        return unitType == 0 ? axeUnit : swordUnit;
    }

    private float GetRandomSpawnInterval()
    {
        // Генерируем случайный интервал времени от minSpawnInterval до maxSpawnInterval
        return (float)random.NextDouble() * (maxSpawnInterval - minSpawnInterval) + minSpawnInterval;
    }

    private void AreaEnteredToEnemyBase(Area2D area2D)
    {
        Node parent = area2D.GetParent();
        if (parent is BaseUnit)
        {
            CharacterBody2D characterBody = (CharacterBody2D)parent;
            unitInAreaBase = (BaseUnit)characterBody;
            damageTimer.Start();
        }
        else if (parent is EnemyBaseUnit)
        {
            IsOurUnit = true;
        }
    }

    private void AreaExitedToEnemyBase(Area2D area2D)
    {
        Node parent = area2D.GetParent();
        if (parent is BaseUnit)
        {
            damageTimer.Stop();
        }
        if (parent is EnemyBaseUnit)
        {
            IsOurUnit = false;
        }
    }

    private async Task DeathBaseEnemy()
{
    if (HealthBaseEnemy <= 0 && !death)
    {
        death = true; // Помечаем, что объект уже умер, чтобы избежать повторного вызова этого метода

        Engine.TimeScale = 0;
        await Task.Delay(2000);

        GetTree().Root.AddChild(winScene);
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
        get { return _currentUnitCount; }
        set { _currentUnitCount = value; }
    }

    public static int DecreaseUnitCount()
    {
        return _currentUnitCount--;
    }

    public static int IncreaseUnitCount()
    {
        return _currentUnitCount++;
    }
}
