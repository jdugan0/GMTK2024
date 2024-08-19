using Godot;
using System.Collections.Generic;
using System.Security.Cryptography;

public partial class Plant : TextureButton
{
	[Export] public PlantInfo info;
	[Export] public Label hoverText;
	public List<SyringeDragging> syringe = new List<SyringeDragging>();

    public override void _Ready()
    {
		TextureNormal = info.species.texture;
		TextureFocused = info.species.texture;
		TextureHover = info.species.texture;
		TextureDisabled = info.species.texture;
		TexturePressed = info.species.texture;
    }

    public override void _Process(double delta)
    {
        if (hoverText.Visible){
			hoverText.Position = GetLocalMousePosition() + new Vector2(30,0);
		}
    }

	public void Press(){
		GD.Print("ww");
		if (info.onTable){
			ToShelf();
		}
		else{
			ToTable();
		}
	}

    public void AddVirus(VirusItem item)
	{
		info.viruses.Add(item);
	}

	public void RemoveVirus(VirusItem item)
	{
		info.viruses.Remove(item);
	}

	public void ClearViruses(){
		info.viruses.Clear();
	}

	public void ToTable(){
		if (Inventory.instance.GetTableOccuplant() == null){
			info.slot = Position;
			Position = Inventory.instance.GetTablePosition();
			info.onTable = true;
			SetScale(new Vector2(1.5f, 1.5f));
			Inventory.instance.SetTableOccupied(this);
		}
	}
	public void ToShelf(){
		Position = info.slot;
		info.onTable = false;
		SetScale(new Vector2(0.5f, 0.5f));
		Inventory.instance.SetTableOccupied(null);
	}

	// private void ToTable()
	// {
	// 	if (!Inventory.GetTableOccupied() && Inventory.GetTableOccuplant() != this)
	// 	{
	// 		Position = Inventory.GetTablePosition();
	// 		info.onTable = true;
	// 		SetScale(new Vector2(1.5f, 1.5f));
	// 		ZIndex = 10;
	// 		Inventory.SetTableOccupied(this);
	// 	}
	// }

	// private void ToShelf()
	// {
	// 	Position = info.posSlot;
	// 	info.onTable = false;
	// 	SetScale(new Vector2(0.5f, 0.5f));
	// 	ZIndex = -1;
	// 	Inventory.SetTableFree();
	// }


	public List<VirusItem> GetViruses()
	{
		return info.viruses;
	}

	private void SetScale(Vector2 scale)
	{
		Scale = scale;
	}

	public void Hover(){
		hoverText.Text = "Value: " + info.value;
		hoverText.Visible = true;
		
	}
	public void HoverExit(){
		hoverText.Visible = false;
	}
}
