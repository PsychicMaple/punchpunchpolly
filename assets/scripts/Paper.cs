using Godot;
using System;

public partial class Paper : CharacterBody3D
{
	public float Health = 10.0f;
	public const float Speed = 7.0f;
	public const float JumpVelocity = 4.5f;
	public bool handlingInput;
	
	public override void _Ready()
	{
		GlobalTranslate(new Vector3(Globals.initialX, Globals.initialY, Globals.initialZ));

		SignalManager signalManage = GetTree().Root.GetNode<SignalManager>("SignalManager");
		signalManage.PInputOff += PInputOff;
		signalManage.PInputOn += PInputOn;
		handlingInput = true;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;
		
		// Add the gravity.
		if (!IsOnFloor()) { velocity += GetGravity() * (float)delta; }

		// Get the input direction and handle the movement/deceleration, IF input is being handled.
		if (handlingInput) {
			Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_up", "move_down");
			Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
			if (direction != Vector3.Zero)
			{
				velocity.X = direction.X * Speed;
				velocity.Z = direction.Z * Speed;
			}
			else
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
				velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
			}
			
			// play character sprite animations
			if (inputDir.X > 0) {
				GetNode<AnimatedSprite3D>("AnimatedSprite3D").Play("walk_right");
			}
			else if (inputDir.X < 0) {
				GetNode<AnimatedSprite3D>("AnimatedSprite3D").Play("walk_left");
			}
			else if (inputDir.Y > 0) {
				GetNode<AnimatedSprite3D>("AnimatedSprite3D").Play("walk_forward");
			}
			else if (inputDir.Y < 0) {
				GetNode<AnimatedSprite3D>("AnimatedSprite3D").Play("walk_back");
			}
			else {
				GetNode<AnimatedSprite3D>("AnimatedSprite3D").Play("idle");
			}
		}
		
		else { return; }
		
		
		// move character
		//GD.Print(inputDir);
		Velocity = velocity;
		MoveAndSlide();
		
		// Enemy collisions
		for (int i = 0; i < GetSlideCollisionCount(); i++)
		{
			KinematicCollision3D collision = GetSlideCollision(i);
			// fix: https://www.reddit.com/r/godot/comments/o60tav/is_in_group_gdscript_vs_isingroup_c/
			var colliderNode = collision.GetCollider() as Node;
			if (colliderNode.IsInGroup("stuffThatKillsYou")) {
				Globals.pHealth -= 1;
				QueueFree();
				GetTree().ReloadCurrentScene();
			}
		}



		// TESTING
		if (Input.IsActionJustReleased("test")) {
			GD.Print("you get nothing");//Health is " + Globals.pHealth);
			/*
			GD.Print("Transform origin:      " + Transform.Origin);
			GD.Print("Global Transform origin" + GlobalTransform.Origin);

			GD.Print("\n---\n");

			GD.Print("Pos x: " + Position.X);
			GD.Print("Pos y: " + Position.Y);
			GD.Print("Pos z: " + Position.Z);
			*/
		}
	}
	
	private void PInputOff() => handlingInput = false;
	private void PInputOn() => handlingInput = true;
}
