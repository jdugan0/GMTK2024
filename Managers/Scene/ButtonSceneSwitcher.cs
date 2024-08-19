using Godot;
using System;

public partial class ButtonSceneSwitcher : TextureButton
{
	[Export] public Label errorText;
	[Export] public float errorTime;
	float time;
	public void Switch(int id)
	{
		SceneSwitcher.instance.SwitchScene(id);
	}

	public override void _Process(double delta)
	{
		if (errorText != null)
		{
			if (errorText.Visible && time > 0)
			{
				time -= (float)delta;
			}
			if (time <= 0)
			{
				errorText.Visible = false;
			}
		}
	}

	public void BeginMinigame()
	{
		time = errorTime;

		if (Inventory.instance.GetTableOccuplant() != null)
		{
			if (Inventory.instance.GetTableOccuplant().GetViruses().Count == 0)
			{
				Inventory.instance.quotaCountCurrent++;
				StoreUI.instance.RefreshShop();
				return;
			}
			Inventory.instance.quotaCountCurrent++;
			VirusDataTransfer.ClearViruses();
			foreach (VirusItem virus in Inventory.instance.GetTableOccuplant().GetViruses())
			{
				VirusDataTransfer.AddViruses(virus);
			}
			Inventory.instance.GetTableOccuplant().ClearViruses();
			for (int i = Inventory.instance.GetTableOccuplant().syringe.Count - 1; i >= 0; i--)
			{
				Inventory.instance.GetTableOccuplant().syringe[i].QueueFree();
				Inventory.instance.GetViruses().Remove(Inventory.instance.GetTableOccuplant().syringe[i].virus);
				Inventory.instance.GetTableOccuplant().syringe.RemoveAt(i);
			}
			SceneSwitcher.instance.SwitchScene(1);
		}
		else
		{
			errorText.Visible = true;
		}
	}

	public void Sell()
	{

		
		foreach (PlantInfo i in Inventory.instance.plants.Values)
		{
			Inventory.instance.money += i.value;
			Inventory.instance.torwardsQuota += i.value;
			GD.Print(i.value);
		}
		Inventory.instance.SellPlants();
		if (Inventory.instance.torwardsQuota >= Inventory.instance.quotaCap)
		{

			Inventory.instance.torwardsQuota = 0;
			Inventory.instance.quotaCap *= 1.5f;
			Inventory.instance.quotasReached++;
			AudioManager.instance.PlaySFX(this, "Sell");
		}
		else
		{
			Inventory.instance.Reset();
			SceneSwitcher.instance.SwitchScene(2);
		}
	}
}
