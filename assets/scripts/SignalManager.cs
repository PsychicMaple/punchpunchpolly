using Godot;
using System;

// should this be an object?
public partial class SignalManager : Node
{
	public static SignalManager Instance { get; private set; }

	[Signal]
	public delegate void PInputOffEventHandler();
	
	[Signal]
	public delegate void PInputOnEventHandler();
	
	// MOVE INPUT HANDLING STUFF HERE SO WE CAN PREVENT INTERACT BUTTON FROM BEING USED TOO WITHIN THE NPC AND TEXTBOX SCRIPTS
}
