using Godot;
using System;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class Movables : Area2D
	{
		public static Vector2 WindowSize;
		public int speed;
		const int scrollSpeed = 200;
		public override void _Ready()
		{
			WindowSize = GetWindow().Size;
		}
		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
			Move(lDelta);
			Scroll();
			GameOver();
        }
        public virtual void Move(float lDelta)
		{
			Position += new Vector2(-1, 0) * speed * lDelta;
		}
		public virtual void GameOver()
		{
			if(Player.IsGameOver)QueueFree();
		}
		public virtual void DestroyOutWindow()
		{
			if (GlobalPosition.X < 0)
			{
				QueueFree();
			}
		}

		public void Scroll()
		{
			int lSpeed;
			int i = 0;
			if(i == 0) lSpeed = speed;
            if (GlobalPosition.X > WindowSize.X)
			{
				speed = scrollSpeed;
			}
			i++;
        }

        public virtual void DeathAfterSound()
		{
			QueueFree();
		}

		protected override void Dispose(bool pDisposing)
		{

		}
	}
}
