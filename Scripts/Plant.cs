using Godot;
using System.Collections.Generic;
using System.Security.Cryptography;

public partial class Plant : TextureButton
{
	[Export] public PlantInfo info;
	[Export] public Label hoverText;
	public List<SyringeDragging> syringe = new List<SyringeDragging>();

	public Plant FromPlant()
	{
		Plant plant = new Plant();
		plant.info = new PlantInfo(info);
		plant.info.plant = plant;
		return plant;
	}

    public override void _Ready()
    {
		TextureNormal = info.species.texture;
		TextureFocused = info.species.texture;
		TextureHover = info.species.texture;
		TextureDisabled = info.species.texture;
		TexturePressed = info.species.texture;
		if (!info.onTable)
		{
			ToShelfReset();
		}
		else
		{
			ToTableReset();
		}
    }

    public override void _Process(double delta)
    {
        if (hoverText.Visible){
			hoverText.Position = GetLocalMousePosition() + new Vector2(30,0);
		}
    }

    public void SetInfo(PlantInfo info)
	{
		this.info = info;
	}

    public void SetPositionPreset(Node2D position)
	{
		Vector2 coordinates = new Vector2(position.Position.X - 90.75f,
											   position.Position.Y - 113.5f);
		info.posSlot = coordinates;
		if (!info.onTable)
		{
			Position = info.posSlot;
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

	private void ToTable()
	{
		if (!Inventory.GetTableOccupied() && Inventory.GetTableOccuplant() != this)
		{
			Position = Inventory.GetTablePosition();
			info.onTable = true;
			SetScale(new Vector2(1.5f, 1.5f));
			ZIndex = 10;
			Inventory.SetTableOccupied(this);
		}
	}
	private void ToTableReset()
	{
		Position = Inventory.GetTablePosition();
		info.onTable = true;
		SetScale(new Vector2(1.5f, 1.5f));
		ZIndex = 10;
		info.value += VirusDataTransfer.score;
	}

	private void ToShelf()
	{
		GD.Print(info.posSlot);
		Position = info.posSlot;
		info.onTable = false;
		SetScale(new Vector2(0.5f, 0.5f));
		ZIndex = -1;
		Inventory.SetTableFree();
	}

	private void ToShelfReset()
	{
		Position = info.posSlot;
		info.onTable = false;
		SetScale(new Vector2(0.5f, 0.5f));
		ZIndex = -1;
	}

	public void OnClick()
	{
		if (info.onTable)
		{
			ToShelf();
		}
		else
		{
			ToTable();
		}
	}

	public List<VirusItem> GetViruses()
	{
		return info.viruses;
	}

	private void SetScale(Vector2 scale)
	{
		Scale = scale;
	}

	public PlantInfo GetPlantInfo()
	{
		return info;
	}

	public void Hover(){
		hoverText.Text = "Value: " + info.value;
		hoverText.Visible = true;
		
	}
	public void HoverExit(){
		hoverText.Visible = false;
	}
}
