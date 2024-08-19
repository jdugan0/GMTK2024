using Godot;

public partial class VolumeSlider : HSlider
{
	public override void _Ready()
	{
		Value = Mathf.DbToLinear(AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Master")));
	}

	public void OnSliderChanged(float value) {
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), (float) Mathf.LinearToDb(value));
	}
}