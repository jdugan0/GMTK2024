using Godot;
using System.Collections.Generic;

public partial class Plant : TextureButton
{
	private List<VirusItem> viruses = new List<VirusItem>();
	private Vector2 posSlot;
	private bool onTable;
	[Export] private Species species;

	public Plant()
	{
		posSlot = new Vector2(0, 0);
		Position = posSlot;
	}

    public void SetPositionPreset(Vector2 position)
	{
		posSlot = position;
		Position = posSlot;
	}

    public void AddVirus(VirusItem item)
	{
		viruses.Add(item);
	}
	public void RemoveVirus(VirusItem item)
	{
		viruses.Remove(item);
	}



	private void ToTable()
	{
		if (!PlantLayer.GetTableOccupied())
		{
			Position = PlantLayer.GetTablePosition();
			onTable = true;
			Scale = new Vector2(1.5f, 1.5f);
			ZIndex = 10;
			PlantLayer.SetTableOccupied(this);
		}
	}

	private void ToShelf()
	{
		Position = posSlot;
		onTable = false;
		Scale = new Vector2(0.5f, 0.5f);
		ZIndex = -1;
		PlantLayer.SetTableFree();
	}

	public void OnClick()
	{
		if (onTable)
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
		return viruses;
	}
}
