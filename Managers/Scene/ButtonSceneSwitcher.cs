using Godot;
using System;

public partial class ButtonSceneSwitcher : Node
{
	[Export] ViewVirusButton b;
	public void Switch(int id){
		SceneSwitcher.instance.SwitchScene(id);
	}

	public void BeginMinigame()
	{
		VirusDataTransfer.ClearViruses();
		if (PlantLayer.GetTableOccuplant() != null)
		{
			foreach (VirusItem virus in PlantLayer.GetTableOccuplant().GetViruses())
			{
				VirusDataTransfer.AddViruses(virus);
			}
			PlantLayer.GetTableOccuplant().ClearViruses();
			for (int i = PlantLayer.GetTableOccuplant().syringe.Count - 1; i >= 0; i--){
				PlantLayer.GetTableOccuplant().syringe[i].QueueFree();
				Inventory.instance.GetViruses().Remove(PlantLayer.GetTableOccuplant().syringe[i].virus);
				PlantLayer.GetTableOccuplant().syringe.RemoveAt(i);
			}
			SceneSwitcher.instance.SwitchScene(2);
		}
	}
}
