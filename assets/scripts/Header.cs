using Godot;
using System;

public partial class Header : Control
{
	ProgressBar healthbar;

	public override void _Ready()
	{
		healthbar = GetNode<ProgressBar>("Healthbar");
	}

	public override void _Process(double delta)
	{
		healthbar.Value = Globals.pHealth;
	}
}
