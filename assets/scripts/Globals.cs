using Godot;
using System;

public partial class Globals : Node
{
	public static int pHealth;

	public static float initialX;
	public static float initialY;
	public static float initialZ;

	public override void _Ready()
	{
		pHealth = 10;
		initialX = 0;
		initialY = 1;
		initialZ = 0;
	}

	public override void _Process(double delta)
	{
	}
}
