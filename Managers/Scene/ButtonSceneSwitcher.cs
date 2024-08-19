using Godot;
using System;

public partial class ButtonSceneSwitcher : Control
{
	public void Switch(int id){
		SceneSwitcher.instance.SwitchScene(id);
	}

	public override void _Process(double delta)
	{

	}

	public void BeginMinigame()
	{
		Inventory.instance.quotaCountCurrent++;
		VirusDataTransfer.ClearViruses();
		if (Inventory.instance.GetTableOccuplant() != null)
		{
			foreach (VirusItem virus in Inventory.instance.GetTableOccuplant().GetViruses())
			{
				VirusDataTransfer.AddViruses(virus);
			}
			Inventory.instance.GetTableOccuplant().ClearViruses();
			for (int i = Inventory.instance.GetTableOccuplant().syringe.Count - 1; i >= 0; i--){
				Inventory.instance.GetTableOccuplant().syringe[i].QueueFree();
				Inventory.instance.GetViruses().Remove(Inventory.instance.GetTableOccuplant().syringe[i].virus);
				Inventory.instance.GetTableOccuplant().syringe.RemoveAt(i);
			}
			SceneSwitcher.instance.SwitchScene(1);
		}
	}

	public void Sell(){
		foreach (PlantInfo i in Inventory.instance.plants.Values){
			Inventory.instance.money += i.value;
			Inventory.instance.torwardsQuota += i.value;
		}
		Inventory.instance.SellPlants();
		if (Inventory.instance.torwardsQuota >= Inventory.instance.quotaCap){
			Inventory.instance.torwardsQuota = 0;
			Inventory.instance.quotaCap *= 1.5f;
		}
		else{
			SceneSwitcher.instance.SwitchScene(2);
		}
	}
}
