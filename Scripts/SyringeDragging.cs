using Godot;
using System;

public partial class SyringeDragging : Control
{
	private bool hover;
	[Export] public Node parent;
	[Export] public Node parent1;

	public override void _Ready()
	{

	}

	public override void _Process(double delta)
	{
		if (hover && Input.IsActionJustPressed("Click"))
		{
			Reparent(parent);
		}
		if (Input.IsActionJustReleased("Click"))
		{
			Reparent(parent1);
		}
		if (Input.IsActionPressed("Click"))
		{
			Position = GetViewport().GetCamera2D().GetGlobalMousePosition();
		}
	}

	public void MouseEnteredLogic()
	{
		hover = true;
	}

	public void MouseExitedLogic()
	{
		hover = false;
	}
}
