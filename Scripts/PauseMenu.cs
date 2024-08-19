using Godot;
using System;

public partial class PauseMenu : Control
{
    public override void _Ready()
    {
        Visible = false;
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("Pause (test)"))
		{
			Toggle();
		}
    }

    public void Toggle()
	{
		Visible = !Visible;
		if (Visible)
		{
			GetTree().Paused = true;
			return;
		}
		GetTree().Paused = false;
	}
}
