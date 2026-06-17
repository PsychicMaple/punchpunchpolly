using Godot;
using System;

public partial class TransferPoint : Area3D
{
	[Export]
	public float transferX;
	[Export]
	public float transferY;
	[Export]
	public float transferZ;
	[Export]
	public string transferScene;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		if (HasOverlappingBodies()) {
			Globals.initialX = transferX;
			Globals.initialY = transferY;
			Globals.initialZ = transferZ;
			GetTree().ChangeSceneToFile(transferScene);
		}
	}
}
