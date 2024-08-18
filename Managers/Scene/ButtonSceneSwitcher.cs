using Godot;
using System;

public partial class ButtonSceneSwitcher : Node
{
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
			SceneSwitcher.instance.SwitchScene(1);
		}
	}
}
