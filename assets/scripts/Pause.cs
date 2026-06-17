using Godot;
using System;

public partial class Pause : Control
{
	
	private bool pauseActive = false;
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionJustPressed("pause")) {
			pauseActive = !pauseActive;
			togglePause();
		}
	}
	
	private void togglePause() {
		if (pauseActive == false) {
			GetTree().Paused = false;
		}
		else {
			GetTree().Paused = true;
		}
		Visible = !Visible;
	}
}
