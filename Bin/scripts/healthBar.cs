using Godot;
using System;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class healthBar : AnimatedSprite2D
	{
		public static int HealthBarState = 0;
		public override void _Ready()
		{

		}

		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
			Frame = HealthBarState;

		}

		protected override void Dispose(bool pDisposing)
		{

		}
	}
}
