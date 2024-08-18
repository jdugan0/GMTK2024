using Godot;
using System;

public partial class PurchaseVirusButton : TextureButton
{
    [Export] private VirusItem virus;
    [Export] Label name;
    [Export] Label price;
    public override void _Ready()
    {
        Pressed += Purchase;
    }

    public void SetVirusItem(VirusItem virus)
    {
        this.virus = virus;
        name.Text = virus.name;
        price.Text = "$" + virus.price;
    }
    public void Purchase(){
        if (Inventory.instance.money >= virus.price){
            Inventory.instance.AddVirus(new VirusItem(virus));
        }
    }
}