using Godot;
using System;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class Sparkles : GpuParticles2D
	{
		public override void _Ready()
		{

		}

		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
        }

		protected override void Dispose(bool pDisposing)
		{

		}
	}
}
