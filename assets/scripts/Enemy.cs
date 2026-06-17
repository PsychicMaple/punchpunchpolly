using Godot;
using System;

public partial class Enemy : CharacterBody3D
{
	[Export] // Speed
	public float _speed { get; set; } 
	
	public override void _Ready()
	{
		Velocity *= _speed;
		Velocity = Velocity.Rotated(Vector3.Up, Rotation.Y);
		GetNode<VisibleOnScreenNotifier3D>("Screenbox").ScreenExited += OnScreenExited;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		MoveAndSlide();
	}
	
	public void OnScreenExited() {
		QueueFree();
	}
}
