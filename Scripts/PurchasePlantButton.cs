using Godot;
using System;

public partial class PurchasePlantButton : TextureButton
{
    [Export] private PlantInfo plantInfo;
    [Export] Label name;
    [Export] Label price;

    public void SetPlantInfo(PlantInfo plantInfo)
    {
        this.plantInfo= plantInfo;
        name.Text = plantInfo.species.species.ToString();
        price.Text = "$" + plantInfo.price;
    }
}