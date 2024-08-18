using Godot;
using System;

public partial class PurchaseVirusButton : TextureButton
{
    [Export] private VirusItem virus;
    [Export] Label name;
    [Export] Label price;

    public void SetVirusItem(VirusItem virus)
    {
        this.virus = virus;
        name.Text = virus.name;
        price.Text = "$" + virus.price;
    }
}