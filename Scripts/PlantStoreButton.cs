using Godot;
using System;

public partial class PlantStoreButton : TextureButton
{
	[Export] private Label hover;

    public override void _Process(double delta)
	{
		if (hover.Visible)
		{
			hover.Position = GetGlobalMousePosition() + new Vector2(30, 0);
		}
    }

	public void Hover()
	{
		hover.Text = "Buy plants and viruses";
		if (StoreUI.instance.GetShopEmpty())
		{
			hover.Text = "Store is empty";
		}
		hover.Visible = true;
	}

	public void HoverExit()
	{
		hover.Visible = false;
	}
}
