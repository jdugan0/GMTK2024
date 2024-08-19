using Godot;
using System;

public partial class ButtonSceneSwitcher : Control
{
	public void Switch(int id){
		SceneSwitcher.instance.SwitchScene(id);
	}

	public override void _Process(double delta)
	{
		if (Inventory.instance.GetViruses().Count > 0 && Inventory.instance.GetPlantInfos().Count > 0){
			Visible = true;
		}
		else{
			Visible = false;
		}
	}

	public void BeginMinigame()
	{
		VirusDataTransfer.ClearViruses();
		if (Inventory.GetTableOccuplant() != null)
		{
			foreach (VirusItem virus in Inventory.GetTableOccuplant().GetViruses())
			{
				VirusDataTransfer.AddViruses(virus);
			}
			Inventory.GetTableOccuplant().ClearViruses();
			for (int i = Inventory.GetTableOccuplant().syringe.Count - 1; i >= 0; i--){
				Inventory.GetTableOccuplant().syringe[i].QueueFree();
				Inventory.instance.GetViruses().Remove(Inventory.GetTableOccuplant().syringe[i].virus);
				Inventory.GetTableOccuplant().syringe.RemoveAt(i);
			}
			SceneSwitcher.instance.SwitchScene(1);
			Inventory.TransferPlantInfo();
		}
	}
}
