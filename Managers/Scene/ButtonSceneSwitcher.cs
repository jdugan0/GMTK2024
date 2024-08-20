using Godot;
using System;

public partial class ButtonSceneSwitcher : TextureButton
{
	[Export] public ErrorLabel errorText;
	[Export] public float errorTime;
	float time;
	public void Switch(int id)
	{
		SceneSwitcher.instance.SwitchScene(id);
		if (id == 2){
			Inventory.instance.Reset();
		}
	}

	public void BeginMinigame()
	{
		errorText.SetErrorTime(errorTime);

		if (Inventory.instance.GetTableOccuplant() != null && !Inventory.instance.GetTableOccuplant().info.mutated && Inventory.instance.GetTableOccuplant().GetViruses().Count != 0)
		{
			Inventory.instance.GetTableOccuplant().info.mutated = true;
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
			errorText.Text = "SELECT A PLANT";
			if (Inventory.instance.GetTableOccuplant() != null && Inventory.instance.GetTableOccuplant().info.mutated){
				errorText.Text = "PLANT ALREADY MUTATED";
				return;
			}
			if (Inventory.instance.GetTableOccuplant() != null && Inventory.instance.GetTableOccuplant().GetViruses().Count == 0){
				errorText.Text = "SELECT A GENOME";
			}
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
