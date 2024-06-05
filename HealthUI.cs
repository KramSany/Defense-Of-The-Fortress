using Godot;
using System;

public partial class HealthUI : Control
{
    [Export]
    public Texture2D HeartTexture;

    [Export]
    public Texture2D BarTexture;

    private TextureRect _heartRect;
    private TextureRect _barRect;

    [Export] private float _health = 1.0f;

    public override void _Ready()
    {
        _heartRect = GetNode<TextureRect>("HeartRect");
        _barRect = GetNode<TextureRect>("BarRect");

        _heartRect.Texture = HeartTexture;
        _barRect.Texture = BarTexture;

    }

    public void SetHealth(float health)
    {
        _health = Mathf.Clamp(health, 0.0f, 1.0f);
    }


	public override void _Process(double delta)
	{
		if (UIElementsPlayer.Health > 0)
		{
			_barRect.Scale = new Vector2(UIElementsPlayer.Health / 100, 1.0f);
		}
		else
		{
			_barRect.Scale = new Vector2(0, 1.0f);
		}
	}
}
