using Godot;
using System;
using System.Diagnostics;

public partial class BasePlayer : Node2D
{
	private string axeUnit = "res://Persons/Woodcutter/AxeMan.tscn";
	private string swordUnit = "res://Mobs/SwordPerson.tscn";
	[Export] 
	private float helathPointBase = 400.0f;
	[Export] 
	private float money = 500.0f;
	[Export]
	private float costAxeUnit = 100.0f;
	[Export] 
	private float costSwordUnit = 250.0f;
	
	private float cooldown = 2;
	public int Number { get; set; } = 3;

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
