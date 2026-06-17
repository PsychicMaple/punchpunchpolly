using Godot;
using System;

public partial class Textbox : Control
{
	Label textLabel;
	AnimatedSprite2D textSprite;
	
	double vRatioStart;
	double vRatioEnd;
	double updatedVisibleChars;
	
	const double wordCutoffCorrection = 0.05;
	const double charsPerFrame = 0.2; // change if you want text displayed faster or slower
	
	bool displayInProgress;
	
	public override void _Ready()
	{
		textLabel = GetNode<Label>("Label");
		textSprite = GetNode<AnimatedSprite2D>("Sprite");
		
		updatedVisibleChars = 0;
		displayInProgress = false;
	}

	public override void _Process(double delta)
	{
		if (displayInProgress) {
			if (vRatioStart <= vRatioEnd) {
				updatedVisibleChars += charsPerFrame;
				textLabel.VisibleCharacters = (int) updatedVisibleChars;
				vRatioStart = textLabel.VisibleRatio;
			}
			else {
				displayInProgress = false;
				vRatioStart = Mathf.Snapped(textLabel.VisibleRatio - wordCutoffCorrection, 0.01);
				textSprite.Play("advance");
			}
		}
		
		
		//GD.Print(textLabel.VisibleRatio);
	}
	
	public void Activate(String message, int numAdvances)
	{
		textSprite.Play("default");
		textLabel.Text = message;
		Visible = true;
		displayInProgress = true;
		
		vRatioStart = 0.0;
		vRatioEnd = (1.0 / (numAdvances + 1.0)) + wordCutoffCorrection;
		/* GD.Print(vRatioStart);
		GD.Print(vRatioEnd); */
	}
	
	public void Advance()
	{
		vRatioEnd += vRatioEnd;
		textLabel.LinesSkipped += 5;
		displayInProgress = true;
		
		GD.Print(vRatioStart);
		GD.Print(vRatioEnd);
	}
	
	public void Deactivate()
	{
		// I probably don't need to include any of these besides Visible because they are reset when Textbox.Activate is called again, still, ill keep them for now
		Visible = false;
		displayInProgress = false;
		textLabel.Text = "";
		
		// use multiple assignment here
		textLabel.VisibleRatio = 0;
		textLabel.LinesSkipped = 0;
		updatedVisibleChars = 0;
		vRatioStart = 0;
		vRatioEnd = 0;
		
	}
	
}
