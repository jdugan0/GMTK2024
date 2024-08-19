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
		if (Inventory.GetTableOccuplant() != null){
			selected = !selected;
			if (selected){
				Inventory.GetTableOccuplant().AddVirus(virus);
				Inventory.GetTableOccuplant().syringe.Add(this);
			}
			else{
				Inventory.GetTableOccuplant().RemoveVirus(virus);
				Inventory.GetTableOccuplant().syringe.Remove(this);
			}
		}
		else{
			if (selected){
				Inventory.GetTableOccuplant().RemoveVirus(virus);
				Inventory.GetTableOccuplant().syringe.Remove(this);
			}
			selected = false;
		}
	}

}
