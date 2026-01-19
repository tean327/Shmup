using Godot;
using System;

// Author : Julien Fournier

namespace Com.IsartDigital.ProjectName
{
	public partial class TickRotation : TickEvent
    {
        protected override void OnBeat()
        {
            base.OnBeat();
            //square.RotationDegrees += 15f;
        }
    }
}
