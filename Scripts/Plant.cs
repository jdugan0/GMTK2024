using Godot;
using System.Collections.Generic;

public partial class Plant : TextureButton
{
	[Export] private PlantInfo info;

    public override void _Ready()
    {
		if (!info.onTable)
		{
			ToShelf();
		}
		else
		{
			ToTable();
		}
    }

	public void SetInfo(PlantInfo info)
	{
		this.info = info;
	}

    public void SetPositionPreset(Vector2 position)
	{
		info.posSlot = position;
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
		if (!PlantLayer.GetTableOccupied() && PlantLayer.GetTableOccuplant() != this)
		{
			Position = PlantLayer.GetTablePosition();
			info.onTable = true;
			SetScale(new Vector2(1.5f, 1.5f));
			ZIndex = 10;
			PlantLayer.SetTableOccupied(this);
		}
	}

	private void ToShelf()
	{
		Position = info.posSlot;
		info.onTable = false;
		SetScale(new Vector2(0.5f, 0.5f));
		ZIndex = -1;
		PlantLayer.SetTableFree();
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
}
