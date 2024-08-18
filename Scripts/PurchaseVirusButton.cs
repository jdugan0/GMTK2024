using Godot;
using System;

public partial class PurchaseVirusButton : TextureButton
{
    [Export] private VirusItem virus;

    public void SetVirusItem(VirusItem virus)
    {
        this.virus = virus;
    }
}