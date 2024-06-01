using Godot;
using System;

public static class UIElementsPlayer
{
    public static float Health
    {
        get; set;
    }

    public static float Money
    {
        get; set;
    }

    public static void GetHealthBase(float helathPointBase)
    {
        Health = helathPointBase;

    }
    

    public static void GetMoneyBase(float helathPointBase)
    {
        Money = helathPointBase;

    }

    public static float SetMoneyBase()
    {
        return Money;
    }


}
