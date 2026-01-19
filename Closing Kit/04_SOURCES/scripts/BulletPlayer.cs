using Godot;
using System;
using System.Collections;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class BulletPlayer : Bullets
	{
		[Export] PackedScene particlesScene;
		GpuParticles2D particles;
		public override void _Ready()
		{
			WindowSize = GetWindow().Size;
			speed = 2000;
            AreaEntered += BulletCollision;
		}

        private void BulletCollision(Area2D area)
        {
            if(area is BulletEnemy || area is MisileEnemy)
			{
				area.QueueFree();
				QueueFree();
			}
			if(area is Enemy)
			{
				QueueFree();
			}
        }

        public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
			Position += new Vector2(1, 0) * speed * lDelta;
			DestroyOutWindow();
        }
        public override void DestroyOutWindow()
        {
			if (Position.X > WindowSize.X - 50)
			{
				QueueFree();
			}
		}
		protected override void Dispose(bool pDisposing)
		{

		}
	}
}
