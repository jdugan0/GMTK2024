using Godot;
using System;
using System.Collections.Generic;

public partial class ErrorLabel : Label
{
    [Export] private float errorTime;
    public List<SyringeDragging> syringes;
	[Export] private ViewVirusButton view;

    public override void _Process(double delta)
	{
		if (Visible && errorTime > 0)
		{
			errorTime -= (float) delta;
		}
		if (errorTime <= 0)
		{
			Visible = false;
		}
	}

    public void SetErrorTime(float time)
    {
        errorTime = time;
    }

    public void UpdateErrorMessage()
    {
        Visible = true;
		Text = "SELECT A PLANT";
        if (Inventory.instance.GetTableOccuplant() == null && view.GetInjectionAttempted())
        {
            Text = "SELECT A PLANT";
			return;
        }
		if (Inventory.instance.GetTableOccuplant() != null && Inventory.instance.GetTableOccuplant().info.mutated){
            Text = "PLANT ALREADY MUTATED";
			return;
		}
		if (Inventory.instance.GetTableOccuplant() != null && Inventory.instance.GetTableOccuplant().GetViruses().Count == 0){
            Text = "SELECT A GENOME";
		}
    }
}