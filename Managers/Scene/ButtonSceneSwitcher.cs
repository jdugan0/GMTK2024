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
		}
	}
}
