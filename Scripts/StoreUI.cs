using Godot;
using System;

public partial class StoreUI : Control
{
    public override void _Ready()
    {
        
    }

	public void Toggle()
	{
		Visible = !Visible;
	}
}
