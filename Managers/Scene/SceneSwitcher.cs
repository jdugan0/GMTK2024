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
		if (loadOrder == 0){
			float t = AudioManager.instance.CancelSFX("VirusMusic");
			AudioManager.instance.PlaySFX(this, "WorkspaceMusic", t);
			AudioManager.instance.CancelSFX("Ambient");
		}
		if (loadOrder == 1){
			float t = AudioManager.instance.CancelSFX("WorkspaceMusic");
			AudioManager.instance.PlaySFX(this, "VirusMusic", t);
			AudioManager.instance.PlaySFX(this, "Ambient");
		}
	}

	public int GetCurrentSceneID()
	{
		return currentScene;
	}
}
