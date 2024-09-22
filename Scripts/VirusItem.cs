using Godot;
using System;

[GlobalClass]
public partial class VirusItem : Resource
{
    [Export] public String name;
    [Export] public VirusBoid.AbilityType ability;
    [Export] public PackedScene scene;
    [Export] public float price;
    [Export] public Texture2D texture;
    [Export] public Texture2D hoverTexture;
    [Export] public Texture2D syringeTexture;
    public VirusItem(){

    }
    public VirusItem(VirusItem item){
        name = item.name;
        ability = item.ability;
        scene = item.scene;
        price = item.price;
        texture = item.texture;
        hoverTexture = item.hoverTexture;
        syringeTexture = item.syringeTexture;
    }

    
}
