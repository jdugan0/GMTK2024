using Godot;
using System;

public partial class SyringeDragging : TextureButton
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
		if (Inventory.instance.GetTableOccuplant() != null){
			selected = !selected;
			if (selected){
				Inventory.instance.GetTableOccuplant().AddVirus(virus);
				Inventory.instance.GetTableOccuplant().syringe.Add(this);
			}
			else{
				Inventory.instance.GetTableOccuplant().RemoveVirus(virus);
				Inventory.instance.GetTableOccuplant().syringe.Remove(this);
			}
		}
		else{
			if (selected){
				Inventory.instance.GetTableOccuplant().RemoveVirus(virus);
				Inventory.instance.GetTableOccuplant().syringe.Remove(this);
			}
			selected = false;
		}
	}

	public void SetTextures()
	{
		TextureNormal = virus.syringeTexture;
		TexturePressed = virus.syringeTexture;
		TextureHover = virus.hoverTexture;
		TextureDisabled = virus.hoverTexture;
		TextureFocused = virus.syringeTexture;
	}
}
