using Godot;
using System;

public partial class Platform : StaticBody2D, IColorable
{
	[field: Export]
	public ColorData ColorData { get; private set; } = GD.Load<ColorData>("res://System/Color/ColorData/Data/Black.tres");

	public override void _Ready()
	{
		GetNode<Sprite2D>("Sprite2D").Modulate = ColorData.Color;
		base._Ready();
	}

	public void SendColorData()
	{
		throw new NotImplementedException();
	}

	public void ReceiveColorData(ColorData colorData)
	{
		throw new NotImplementedException();
	}

	public void BlendColorData()
	{
		throw new NotImplementedException();
	}

	public void DropColorData()
	{
		throw new NotImplementedException();
	}
}
