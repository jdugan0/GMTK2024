using Godot;
using System;
using System.Collections.Generic;

public partial class SceneSwitcher : Node
{
	public static SceneSwitcher instance = null;
	[Export] public PackedScene[] scenes;
	// Called when the node enters the scene tree for the first time.
	private int currentScene = 0;

	public override void _Ready()
	{
		instance = this;
	}

	public void SwitchScene(int loadOrder){
		currentScene = loadOrder;
		GetTree().ChangeSceneToPacked(scenes[loadOrder]);
	}

	public int GetCurrentSceneID()
	{
		return currentScene;
	}
}
