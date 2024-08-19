using Godot;
using System;

public partial class QuitButton : TextureButton
{
    public override void _Ready()
    {
        Pressed += () => Quit();
    }

    private void Quit()
	{
		GetTree().Quit();
	}
}
