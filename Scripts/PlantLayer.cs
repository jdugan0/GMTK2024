using Godot;
using System;
using System.Diagnostics;

public partial class PlantLayer : CanvasLayer
{
	[Export] public Node2D[] positions = new Node2D[15];
	[Export] public PackedScene plantScene;
	private static bool tableOccupied = false;
	private static Plant tableOccupant;

    public void AddPlant(PlantInfo info)
	{
		// TODO different plants
		Plant plant = (Plant) plantScene.Instantiate();
		plant.SetInfo(info);
		for (int i = 0; i < 15; i++)
		{
			if (positions[i].GetParent().GetType().ToString() != "Plant")
			{
				Vector2 position = new Vector2(positions[i].Position.X - 90.75f, positions[i].Position.Y - 113.5f);
				positions[i].GetParent().RemoveChild(positions[i]);
				plant.AddChild(positions[i]);
				plant.SetPositionPreset(position);
				break;
			}
		}
		info.plant = plant;
		AddChild(plant);
	}

    public override void _Process(double delta)
    {
        // GD.Print("Table occuplant is " + GetTableOccuplant());
    }

    public static Vector2 GetTablePosition()
	{
		return new Vector2(670, 310);
	}

	public static void SetTableOccupied(Plant occuplant)
	{
		tableOccupied = true;
		tableOccupant = occuplant;
	}

	public static void SetTableFree()
	{
		tableOccupied = false;
		tableOccupant = null;
	}

	public static bool GetTableOccupied()
	{
		return tableOccupied;
	}

	public static Plant GetTableOccuplant()
	{
		return tableOccupant;
	}
}
