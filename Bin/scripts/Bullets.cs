using Godot;
using System;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class Bullets : Movables
	{
		
		public override void _Ready()
		{
			
		}

		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;

		}

        public override void DestroyOutWindow()
        {
            if (Position.X < 0 || Position.X > WindowSize.X)
            {
                QueueFree();
            }
        }

		protected override void Dispose(bool pDisposing)
		{
base.Dispose(pDisposing);
		}
	}
}
