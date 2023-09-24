using Godot;
using System;

public partial class MarginContainerScript : MarginContainer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        int marginValue = 100;
        AddThemeConstantOverride("margin_top", marginValue);
        AddThemeConstantOverride("margin_left", marginValue);
        AddThemeConstantOverride("margin_bottom", marginValue);
        AddThemeConstantOverride("margin_right", marginValue);
    }

}
