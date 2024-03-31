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


	public override void _Process(double delta)
    {
		UIElementsPlayer.GetHealthBase(helathPointBase);
		UIElementsPlayer.GetMoneyBase(money);
    }



	private void AddAxeUnitBtn() // Add Axe unit
	{
		if (money >= costAxeUnit)
		{
			money -= costAxeUnit;
			AxeUnit mob = (AxeUnit)GD.Load<PackedScene>(axeUnit).Instantiate();
			AddChild(mob);
			mob.Position = new Vector2(40,40);
		}
		
	} 

	private void AddSwordUnitBtn() // Add Sword unit
	{
		if (money >= costSwordUnit)
		{
			money -= costSwordUnit;
			SwordUnit mob = (SwordUnit)GD.Load<PackedScene>(swordUnit).Instantiate();
			AddChild(mob);
			mob.Position = new Vector2(40,40);
		}
	}

	// I will add more units in the future
}
