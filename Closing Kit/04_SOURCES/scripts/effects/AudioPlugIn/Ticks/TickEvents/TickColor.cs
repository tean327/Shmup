using Godot;
using System;

// Author : Julien Fournier

namespace Com.IsartDigital.ProjectName
{
	public partial class TickColor : TickEvent
    {
        Color lColor;

        public override void _Ready()
        {
            base._Ready();
            GD.Randomize();
        }

        protected override void OnBeat()
        {
            base.OnBeat();
            //TickEvent.newColor = new Color(GD.Randf(), GD.Randf(), GD.Randf());
            //square.Modulate = newColor;
        }
    }
}
