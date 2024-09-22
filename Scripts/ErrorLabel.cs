using Godot;
using System;
using System.Collections.Generic;

public partial class ErrorLabel : Label
{
	public static ErrorLabel instance;
    private float errorTime = 2;
	private float errorClock;
    public List<SyringeDragging> syringes;

    public override void _Ready()
    {
        instance = this;
    }

    public override void _Process(double delta)
	{
		if (Visible && errorClock > 0)
		{
			errorClock -= (float) delta;
		}
		if (errorClock <= 0)
		{
			Visible = false;
			errorClock = errorTime;
		}
	}

    public void SetErrorTime(float time)
    {
        errorClock = time;
		errorTime = time;
    }

	public void SetErrorMessageNoPlantInjection()
	{
		errorClock = errorTime;
		Visible = true;
        Text = "SELECT A PLANT";
		GD.Print("eghesklg");
	}

    public void UpdateErrorMessage()
    {
		errorClock = errorTime;
        Visible = true;
		Text = "SELECT A PLANT";
		if (Inventory.instance.GetTableOccuplant() != null && Inventory.instance.GetTableOccuplant().info.mutated){
            Text = "PLANT ALREADY MUTATED";
			return;
		}
		if (Inventory.instance.GetTableOccuplant() != null && Inventory.instance.GetTableOccuplant().GetViruses().Count == 0){
            Text = "SELECT A GENOME";
		}
    }
}