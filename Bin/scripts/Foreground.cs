using Godot;
using System;
using System.Collections.Generic;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class Foreground : ParallaxBackground
	{
		[Export]ParallaxLayer foreground;
		public override void _Ready()
		{
			foreground.ZIndex += 1;
		}

		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
			foreground.MotionOffset -= new Vector2(1, 0) * lDelta * 1700;
        }

		protected override void Dispose(bool pDisposing)
		{

		}
	}
}
