using Godot;
using System;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class Friend : Sprite2D
	{
        float timer;
        public override void _Ready()
		{

		}

		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
            timer += lDelta;
            if (timer > 4) QueueFree();
        }

        protected override void Dispose(bool pDisposing)
		{

		}
	}
}
