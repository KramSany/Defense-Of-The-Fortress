using Godot;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

public partial class BasePlayer : Node2D
{
    private string axeUnit = "res://Player/Persons/Woodcutter/AxeMan.tscn";
    private string swordUnit = "res://Player/Mobs/SwordPerson.tscn";

    private float helathPointBase = 1000.0f;
    public static float _money = 500.0f;
    private static float _damagePerSecond = 100.0f;

    private float costAxeUnit = 100.0f;
    private float costSwordUnit = 250.0f;

    private bool axeButtonPressed = false;
    private bool swordButtonPressed = false;

    private float spawnDelay = 2.0f; // Задержка между спаунами в секундах
    private float spawnTimer = 0.0f;

    private Node upgradeMenuContainer;
    private Node upgradeMenuInstance;

    private EnemyBaseUnit unitInAreaBase;

    Timer damageTimer = new Timer();
    Timer moneyTimer = new Timer();

    

    public override void _Process(double delta)
    {
        UIElementsPlayer.GetHealthBase(helathPointBase);
        UIElementsPlayer.GetMoneyBase(_money);
        // Уменьшаем время таймера каждый кадр
        spawnTimer -= (float)delta;

        // установка монет для апгред меню
        UpgradeMenu.SetMoney(_money);

        // Если таймер меньше или равен нулю, кнопки можно активировать снова
        if (spawnTimer <= 0.0f)
        {
            axeButtonPressed = false;
            swordButtonPressed = false;
            DeathBaseEnemy();
        }
    }

    public override void _Ready()
    {
        Engine.TimeScale = 1;
        upgradeMenuContainer = GetNode<Node>("/root/Map/CanvasLayer");
        if (upgradeMenuContainer == null) GD.Print("Меню контейнера нет");

        damageTimer = GetNode<Timer>("DamageTimer");

        // Настройка таймера для добавления денег
        AddChild(moneyTimer);
        moneyTimer.WaitTime = 1.0f;
        moneyTimer.OneShot = false;
    }

    private void SpawnUnit(string unitScenePath, ref bool buttonPressed, float unitCost)
    {
        if (_money >= unitCost && !buttonPressed)
        {
            _money -= unitCost;
            BaseUnit mob = (BaseUnit)GD.Load<PackedScene>(unitScenePath).Instantiate();
            AddChild(mob);
            mob.Position = new Vector2(-30, 40);

            // Запускаем таймер после спауна юнита
            spawnTimer = spawnDelay;

            // Устанавливаем флаг, чтобы не разрешать спаун до завершения таймера
            buttonPressed = true;
        }
    }

    private void AddAxeUnitBtn() // Add Axe unit
    {
        SpawnUnit(axeUnit, ref axeButtonPressed, costAxeUnit);
    }

    private void AddSwordUnitBtn() // Add Sword unit
    {
        SpawnUnit(swordUnit, ref swordButtonPressed, costSwordUnit);
    }

    private void OpenUpgradeMenu()
    {
        if (upgradeMenuInstance == null)
        {
            GD.Print("Кнопка прокачки");
            upgradeMenuInstance = ResourceLoader.Load<PackedScene>("res://UpgradeMenu.tscn").Instantiate();
            upgradeMenuContainer.AddChild(upgradeMenuInstance);
        }
        else if (upgradeMenuInstance != null)
        {
            upgradeMenuInstance.QueueFree();
            upgradeMenuInstance = null;
        }
    }

    private void OnAreaBasePlayerAreaEntered(Area2D area2D)
    {
        Node parent = area2D.GetParent();

        if (parent is EnemyBaseUnit)
        {
            CharacterBody2D unit = (CharacterBody2D)parent;
            unitInAreaBase = (EnemyBaseUnit)unit;
            damageTimer.Start();
        }
    }

    private void OnAreaBasePlayerAreaExited(Area2D area2D)
    {
        Node parent = area2D.GetParent();
        if (parent is EnemyBaseUnit)
        {
            damageTimer.Stop();
        }
    }

    private async void TimeOutDamage()
    {
        if (DeathBaseEnemy())
        {
            Engine.TimeScale = 0;
            await Task.Delay(5000);
            GetTree().ChangeSceneToFile("res://leveldesign.tscn");
            return;
        }
        helathPointBase -= unitInAreaBase.DamagePerSecnod;
        unitInAreaBase.TakeDamage(_damagePerSecond);
    }

    private bool DeathBaseEnemy()
    {
        if (helathPointBase <= 0)
        {
            return true;
        }
        return false;
    }

    private void OnMoneyTimerTimeout()
    {
        _money += 50;
    }

    public static void UpgradeBaseDamage(float amount)
    {
        _damagePerSecond += amount;
        GD.Print("Base Damage upgraded to: " + _damagePerSecond);
    }
    
}
