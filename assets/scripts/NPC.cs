using Godot;
using System;
using Dictionary = Godot.Collections.Dictionary;
using Array = Godot.Collections.Array;

// this code is also atrocious but not as bad i dont think, still, please rewrite it and look over the dictionary stuff cuz i still dont get it lol
// TODO turn off interaction input when it's active so you can't skip through (or maybe allow it?)
// idk i think ultimately it would be best to have a dedicated key to skip through text, but maybe not.
// it would also be good to include a system where maybe we could include a parameter if any other action/event/signal should be triggered when this npc is interacted with. it would be a little complex to do but i think it would work.
// also make it so the program can calculate the total number of advances from the length of the text. idk how possible that would be but if i could do it right i think it would save me a lot of trouble in the future

public partial class NPC : Area3D
{	
	SignalManager signalManage;
	Textbox textbox;
	
	[Export] SpriteFrames NPC_sprite;
	[Export] String text_block;
	[Export] int text_number;
	[Export] int totalAdvances; // number of times it needs to be advanced before all text is shown
	int advancesLeft;
	
	AnimatedSprite3D alert;
	AnimatedSprite3D sprite;
	String message;
	bool active;
	
	public override void _Ready()
	{
		signalManage = GetTree().Root.GetNode<SignalManager>("SignalManager");
		textbox = GetTree().Root.GetNode<Textbox>("UI/Textbox");
		alert = GetNode<AnimatedSprite3D>("Alert");
		sprite = GetNode<AnimatedSprite3D>("MainSprite");
		
		alert.Visible = false;
		sprite.SpriteFrames = NPC_sprite;
		sprite.Play("idle");
		
		advancesLeft = totalAdvances;
		active = false;
		
		
		// help: https://www.youtube.com/watch?v=3FlBnWIjDEY
		// retrieves dialogue from npctext.json. I really dont know how
		Json jsonLoader = new Json();
		
		using var textFile = FileAccess.Open("res://assets/npctext.json", FileAccess.ModeFlags.Read);
		
		var datastring = textFile.GetAsText();
		jsonLoader.Parse(datastring);
		
		Dictionary npcText = (Dictionary) jsonLoader.Data;
		
		message = npcText[text_block].As<Array>()[text_number].As<String>();
	}

	public override void _Process(double delta)
	{
		// check if player is in bounds. CHECK IF OVERLAPPING BODY IS PLAYER ALSO, code that in
		if (HasOverlappingBodies()) {
			if (!active) { alert.Visible = true; }
			else { alert.Visible = false; }
			
			// if textbox is close when you press interact, activate
			if (Input.IsActionJustPressed("interact") && !active) {
				startInteraction();
			}
			
			// if textbox is open when you press interact, advance OR deactivate
			else if (Input.IsActionJustPressed("interact") && active) {
				if (advancesLeft > 0) {
					advanceInteraction();
					advancesLeft--;
				}
				else { stopInteraction(); }
			}
		}
		// make this an else if statement so it doesn't reassign the value every frame. idk if it matters but it feels wrong
		else { alert.Visible = false; }
		
		// fixes a glitch where you could start an interaction while there were no overlapping bodies, so you couldnt stop the interaction, leading to a softlock. this is kinda ugly tho, probably a better way to do it
		if (!HasOverlappingBodies() && active) {
			stopInteraction();
		}
		
		// handle animation
		if (active) sprite.Play("interact");
		else sprite.Play("idle");
	}
	
	public void startInteraction() {
		textbox.Activate(message, totalAdvances);
		signalManage.EmitSignal("PInputOff");
				
		active = true;
	}
	
	public void advanceInteraction() {
		textbox.Advance();
	}
	
	public void stopInteraction() {
		textbox.Deactivate();
		signalManage.EmitSignal("PInputOn");
		
		advancesLeft = totalAdvances;
		active = false;
	}
}
