using Godot;
using System;

public partial class SyringeDragging : Control
{
	public bool selected = false;
	[Export] public VirusItem virus;

	public void Hover(){
		WorkspaceInit.hoverText.Text =virus.name;
		WorkspaceInit.hoverText.Visible = true;
		
	}
	public void HoverExit(){
		WorkspaceInit.hoverText.Visible = false;
	}

	public override void _Ready()
	{

	}

	public override void _Process(double delta)
	{
		if (WorkspaceInit.hoverText.Visible){
			WorkspaceInit.hoverText.Position = ((Control)WorkspaceInit.hoverText.GetParent()).GetLocalMousePosition() + new Vector2(30,0) ;
		}
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
