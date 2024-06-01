using Godot;
using System;

public partial class UpgradeMenu : Control
{
    private static float money = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
		money = BasePlayer._money;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    private void OnAddHelathAxeUnit()
    {
        if (money >= 100)
        {
            AxeUnit.UpgradeBaseHealth(10.0f);
            BasePlayer._money-= 100;
			money = BasePlayer._money;
        }
        else
        {
            GD.Print("Not enough money!");
        }
    }

    private void AddHelathForSword()
    {
        if (money >= 100)
        {
            SwordUnit.UpgradeBaseHealth(10.0f);
            BasePlayer._money-= 100;
			money = BasePlayer._money;
        }
        else
        {
            GD.Print("Not enough money!");
        }
    }

    private void AddDamageForBase()
    {
        if (money >= 100)
        {
            BasePlayer.UpgradeBaseDamage(10.0f);
            BasePlayer._money-= 100;
			money = BasePlayer._money;
        }
        else
        {
            GD.Print("Not enough money!");
        }
    }

    // Метод для установки количества денег
    public static void SetMoney(float amount)
    {
        money = amount;
    }
}


