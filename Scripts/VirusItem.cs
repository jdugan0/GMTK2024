using Godot;
using System;

[GlobalClass]
public partial class VirusItem : Resource
{
    [Export] public String name;
    [Export] public VirusBoid.AbilityType ability;
}
