using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class StoreUI : Control
{
    [Export] private VirusItem[] virusPresets;
    [Export] private PlantInfo[] plantPresets;
    [Export] private VBoxContainer buttons;
    [Export] private Texture2D[] textures;
    [Export] private PackedScene purchaseVirusButton;
    [Export] private PackedScene purchasePlantButton;
    List<Node> buttons1 = new List<Node>();
    public static StoreUI instance;
    public StoreConfiguration config;

    public override void _Ready()
    {
        instance = this;
        config = RandomizeStore(5, 3);
        int amount = 0;
        foreach (VirusItem virus in config.GetVirusItems())
        {
            PurchaseVirusButton button = (PurchaseVirusButton)(purchaseVirusButton.Instantiate());
            button.TextureNormal = textures[amount % 2];
            button.TextureDisabled = textures[amount % 2];
            button.TextureHover = textures[amount % 2];
            button.TextureFocused = textures[amount % 2];
            button.TexturePressed = textures[amount % 2];
            button.SetVirusItem(virus);
            buttons.AddChild(button);
            buttons1.Add(button);
            amount++;
        }
        foreach (PlantInfo plant in config.GetPlantInfos())
        {
            PurchasePlantButton button = (PurchasePlantButton)(purchasePlantButton.Instantiate());
            button.TextureNormal = textures[amount % 2];
            button.TextureDisabled = textures[amount % 2];
            button.TextureHover = textures[amount % 2];
            button.TextureFocused = textures[amount % 2];
            button.TexturePressed = textures[amount % 2];
            
            button.SetPlantInfo(plant);
            buttons.AddChild(button);
            buttons1.Add(button);
            amount++;
        }
    }

    public void Toggle()
    {
        Visible = !Visible;
        if (Visible){
            VirusDataTransfer.instance.PlayMusic(10);
        }else{
            VirusDataTransfer.instance.PlayMusic(11);
        }
    }

    public void RefreshShop(){
        for (int i = buttons1.Count - 1; i >= 0; i--){
            buttons1[i].QueueFree();
            buttons1.RemoveAt(i);
        }
        config = RandomizeStore(5, 3);
        int amount = 0;
        foreach (VirusItem virus in config.GetVirusItems())
        {
            PurchaseVirusButton button = (PurchaseVirusButton)(purchaseVirusButton.Instantiate());
            button.TextureNormal = textures[amount % 2];
            button.TextureDisabled = textures[amount % 2];
            button.TextureHover = textures[amount % 2];
            button.TextureFocused = textures[amount % 2];
            button.TexturePressed = textures[amount % 2];
            button.SetVirusItem(virus);
            buttons.AddChild(button);
            buttons1.Add(button);
            amount++;
        }
        foreach (PlantInfo plant in config.GetPlantInfos())
        {
            PurchasePlantButton button = (PurchasePlantButton)(purchasePlantButton.Instantiate());
            button.TextureNormal = textures[amount % 2];
            button.TextureDisabled = textures[amount % 2];
            button.TextureHover = textures[amount % 2];
            button.TextureFocused = textures[amount % 2];
            button.TexturePressed = textures[amount % 2];
            
            button.SetPlantInfo(plant);
            buttons.AddChild(button);
            buttons1.Add(button);
            amount++;
        }
    }
    public void RefreshShopWithConfig(){
        for (int i = buttons1.Count - 1; i >= 0; i--){
            buttons1[i].QueueFree();
            buttons1.RemoveAt(i);
        }
        int amount = 0;
        foreach (VirusItem virus in config.GetVirusItems())
        {
            PurchaseVirusButton button = (PurchaseVirusButton)(purchaseVirusButton.Instantiate());
            button.TextureNormal = textures[amount % 2];
            button.TextureDisabled = textures[amount % 2];
            button.TextureHover = textures[amount % 2];
            button.TextureFocused = textures[amount % 2];
            button.TexturePressed = textures[amount % 2];
            button.SetVirusItem(virus);
            buttons.AddChild(button);
            buttons1.Add(button);
            amount++;
        }
        foreach (PlantInfo plant in config.GetPlantInfos())
        {
            PurchasePlantButton button = (PurchasePlantButton)(purchasePlantButton.Instantiate());
            button.TextureNormal = textures[amount % 2];
            button.TextureDisabled = textures[amount % 2];
            button.TextureHover = textures[amount % 2];
            button.TextureFocused = textures[amount % 2];
            button.TexturePressed = textures[amount % 2];
            
            button.SetPlantInfo(plant);
            buttons.AddChild(button);
            buttons1.Add(button);
            amount++;
        }
    }

    private StoreConfiguration RandomizeStore(int numViruses, int numPlants)
    {
        List<VirusItem> viruses = new List<VirusItem>();
        List<PlantInfo> plants = new List<PlantInfo>();
        for (int i = 0; i < numViruses; i++)
        {
            VirusItem v = virusPresets[GD.RandRange(0, virusPresets.Length - 1)];
            viruses.Add(new VirusItem(v));
        }
        for (int i = 0; i < numPlants; i++)
        {
            PlantInfo p = plantPresets[GD.RandRange(0, plantPresets.Length - 1)];
            plants.Add(new PlantInfo(p));
        }
        return new StoreConfiguration(viruses, plants);
    }

    public bool GetShopEmpty()
    {
        return config.GetStoreEmpty();
    }
}
