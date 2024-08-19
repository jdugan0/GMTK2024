using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class VirusDataTransfer : Node
{
	public static VirusDataTransfer instance;
	private static List<VirusItem> viruses;

	public VirusDataTransfer()
	{
		instance = this;
		viruses = new List<VirusItem>();
	}
    public override void _Ready()
    {
        PlayMusic(2);
    }

    public static void AddViruses(VirusItem virus)
	{
		viruses.Add(virus);
	}

	public static void ClearViruses()
	{
		viruses.Clear();
	}
	public void PlayMusic(int loadOrder){
		if (loadOrder == 0){
			float t = AudioManager.instance.CancelSFX("VirusMusic");
			AudioManager.instance.PlaySFX(this, "WorkspaceMusic", t);
			AudioManager.instance.CancelSFX("Ambient");
			AudioManager.instance.CancelSFX("MainMenu");
		}
		if (loadOrder == 1){
			float t = AudioManager.instance.CancelSFX("WorkspaceMusic");
			AudioManager.instance.PlaySFX(this, "VirusMusic", t);
			AudioManager.instance.PlaySFX(this, "Ambient");
		}
		if (loadOrder == 2 && !AudioManager.instance.isPlaying("MainMenu")){
			AudioManager.instance.CancelSFX("WorkspaceMusic");
			AudioManager.instance.CancelSFX("VirusMusic");
			AudioManager.instance.PlaySFX(this, "MainMenu");
		}
		if (loadOrder == 10){
			AudioManager.instance.CancelSFX("WorkspaceMusic");
			AudioManager.instance.PlaySFX(this, "ShopMusic");
		}
		if (loadOrder == 11){
			AudioManager.instance.CancelSFX("ShopMusic");
			AudioManager.instance.PlaySFX(this, "WorkspaceMusic");
		}
	}

	public static List<VirusItem> GetViruses()
	{
		return viruses;
	}
}
