using Godot;
using System;

public partial class ButtonSceneSwitcher : Node
{
	public override void _Ready()
    {
        if (!AudioManager.instance.isPlaying("TitleMusic")){
			AudioManager.instance.PlaySFX(AudioManager.instance, "TitleMusic");
		}
    }
	public void Switch(int id){
		SceneSwitcher.instance.SwitchScene(id);
		if (id == 0){
			AudioManager.instance.CancelSFX("TitleMusic");
		}
	}
}
