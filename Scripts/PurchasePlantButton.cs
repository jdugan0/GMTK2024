using Godot;
using System;

public partial class PurchasePlantButton : TextureButton
{
    [Export] private PlantInfo plant;

    public void SetPlantInfo(PlantInfo plant)
    {
        this.plant = plant;
    }
}