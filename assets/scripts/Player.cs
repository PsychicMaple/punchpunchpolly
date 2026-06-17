/*using Godot;
using System;

public partial class Player : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 10.0f;
	public bool cooldownOver = true;
	
	public override void _Ready()
	{
		GetNode<Timer>("CooldownTimer").Timeout += OnCooldownOver;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}
		
		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}
		
		// Handle ataque
		if (Input.IsActionJustPressed("interact") && cooldownOver)
		{
			GetNode<Attack>("Attack").AttackActive();
			GetNode<Timer>("CooldownTimer").Start();
			cooldownOver = false;
		}

		// Get the input direction and handle the movement/deceleration.
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
		Velocity = velocity;
		
		
		// Enemy collisions
		for (int i = 0; i < GetSlideCollisionCount(); i++)
		{
			KinematicCollision3D collision = GetSlideCollision(i);
			
			// if you don't do this cast, then you won't be able to use "IsInGroup" on the collider, because
			// normally it's an Object type variable. it needs to be cast as a Node type for it to work.
			// fix: https://www.reddit.com/r/godot/comments/o60tav/is_in_group_gdscript_vs_isingroup_c/
			var colliderNode = collision.GetCollider() as Node;
			if (colliderNode.IsInGroup("stuffThatKillsYou")) {
				QueueFree();
				GetTree().CallGroup("systemProcesses", "hit");
				GetTree().ReloadCurrentScene();
			}
		}
		
		MoveAndSlide();
	}
	
	private void OnCooldownOver() {
		cooldownOver = true;
	}
}
*/
