using Godot;
using System;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class BulletEnemy : Bullets
	{
		public override void _Ready()
		{
            speed = 1000;
        }

		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
            Position += new Vector2(-1, 0) * speed * lDelta;
            base.DestroyOutWindow();	
        }

		protected override void Dispose(bool pDisposing)
		{

		}
	}
}
