using Godot;
using System;
using System.Diagnostics;

public partial class BasePlayer : Node2D
{
    private string axeUnit = "res://Player/Persons/Woodcutter/AxeMan.tscn";
    private string swordUnit = "res://Player/Mobs/SwordPerson.tscn";

    private float helathPointBase = 100.0f;
    private float money = 10000.0f;

    private float costAxeUnit = 100.0f;
    private float costSwordUnit = 250.0f;

    private bool axeButtonPressed = false;
    private bool swordButtonPressed = false;

    private float spawnDelay = 2.0f; // Задержка между спаунами в секундах
    private float spawnTimer = 0.0f;

    public override void _Process(double delta)
    {
        UIElementsPlayer.GetHealthBase(helathPointBase);
        UIElementsPlayer.GetMoneyBase(money);

        // Уменьшаем время таймера каждый кадр
        spawnTimer -= (float)delta;

        // Если таймер меньше или равен нулю, кнопки можно активировать снова
        if (spawnTimer <= 0.0f)
        {
            axeButtonPressed = false;
            swordButtonPressed = false;
        }
    }

    private void SpawnUnit(string unitScenePath, ref bool buttonPressed, float unitCost)
    {
        if (money >= unitCost && !buttonPressed)
        {
            money -= unitCost;
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
}
