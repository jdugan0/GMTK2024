using Godot;
using System;

public partial class SyringeDragging : Control
{
	public bool selected = false;
	[Export] public VirusItem virus;

	public override void _Ready()
	{

	}

	public override void _Process(double delta)
	{
		if (selected){
			Modulate = Colors.White;
		}
		else{
			Modulate = Colors.Gray;
		}
	}

	public void Select(){
		if (PlantLayer.GetTableOccuplant() != null){
			selected = !selected;
			if (selected){
				PlantLayer.GetTableOccuplant().AddVirus(virus);
			}
			else{
				PlantLayer.GetTableOccuplant().RemoveVirus(virus);
			}
		}
		else{
			if (selected){
				PlantLayer.GetTableOccuplant().RemoveVirus(virus);
			}
			selected = false;
		}
	}

}
