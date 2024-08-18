using Godot;
using System;
using System.Collections.Generic;
[GlobalClass]
public partial class VirusParameter : Resource{
    [Export] public float value {get;set;}
    [Export] public String name{get;set;}
}