using Godot;
using System;
using System.Collections.Generic;

public partial class Plant : Node2D
{
	private List<VirusItem> viruses;
	private static Vector3[] plantCoordSlots = new Vector3[]
	{
		new Vector3((float) 871.5, (float) 159.5, 0),
		new Vector3( 1054, 159, 0),
		new Vector3( 1236, 154, 0),
		new Vector3((float) 1417.5, 154, 0),
		new Vector3((float) 1599.5, 159, 0),
		new Vector3((float) 1781.5, (float) 159.5, 0),
		new Vector3((float) 871.5, (float) 428.5, 0),
		new Vector3(1054, 428, 0),
		new Vector3((float) 1235.5, 423, 0),
		new Vector3((float) 1417.5, (float) 422.5, 0),
		new Vector3((float) 1599.5, (float) 428.5, 0),
		new Vector3((float) 1781.5, (float) 428.5, 0),
		new Vector3((float) 1599.5, (float) 711.5, 0),
		new Vector3((float) 1781.5, (float) 711.5, 0),
		new Vector3((float) 1775.5, (float) 969.5, 0)
	};
	private Vector2 slot;

    public override void _Ready()
    {
		int index = 0;
		for (int i = 0; plantCoordSlots[i].Z == 1 && i < 15; i++) { index = i; }
		slot = new Vector2(plantCoordSlots[index].X, plantCoordSlots[index].Y);
		plantCoordSlots[index].Z = 1;
        Position = slot;
    }

    public void AddVirus(VirusItem item)
	{
		viruses.Add(item);
	}

	public void RemoveLatestVirus()
	{
		viruses.Remove(viruses[viruses.Count - 1]);
	}
}
