using Godot;
using System;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class MisileEnemy : Bullets
	{
        public override void _Ready()
		{
	        LookAt(Player.GetInstance().Position);
			TweenManager.GetInstance().MisileSpawn(this);
			speed = 300;
		}

		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
			Move(lDelta);
			base.DestroyOutWindow();
		}

        public override void Move(float lDelta)
        {
            Position += new Vector2(Mathf.Cos(Rotation), Mathf.Sin(Rotation))*speed*lDelta;
        }

		

		protected override void Dispose(bool pDisposing)
		{

		}
	}
}
