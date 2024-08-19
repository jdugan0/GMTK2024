using Godot;
using System;

public partial class PurchasePlantButton : TextureButton
{
    [Export] private PlantInfo plantInfo;
    [Export] Label name;
    [Export] Label price;
    public override void _Ready()
    {
        Pressed += Purchase;
    }

    public void SetPlantInfo(PlantInfo plantInfo)
    {
        this.plantInfo= plantInfo;
        name.Text = plantInfo.species.species.ToString();
        price.Text = "$" + plantInfo.price;
    }

    public void Purchase(){
        if (Inventory.instance.money >= plantInfo.price && Inventory.instance.GetPlantInfos().Count < 15){
            Inventory.instance.money -= (int)plantInfo.price;
            Inventory.instance.AddPlant(new PlantInfo(plantInfo));
            // Inventory.instance.RefreshVisuals();
        }
    }
}