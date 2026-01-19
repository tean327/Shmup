using Com.IsartDigital.Utils.Effects;
using Godot;
using System;
using System.Collections.Generic;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class SmartBomb : BulletPlayer
	{
		public static List<Movables> Destroyables;
		public override void _Ready()
		{
			SoundManager.GetInstance().BombSound.Play();
            speed = 500;
			Position = Player.GetInstance().Position;
            AreaEntered += BombDeath;
		}

        private void BombDeath(Area2D pArea)
        {
			if (pArea is not Player && pArea is not PowerUp && pArea is not SmartBomb && pArea is not BulletPlayer)
			{
				Main.shake.Start();
				//foreach(Movables lDestroyable in Destroyables)
				//{
				//	if(lDestroyable.Position.X < WindowSize.X)
				//	{
				//		lDestroyable.QueueFree();
				//	}
				//}
                QueueFree();
			}
		}
        public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
			Move(lDelta);
			base.DestroyOutWindow();
		}
        public override void Move(float lDelta)
        {
			Position += new Vector2(1, 0) * speed * lDelta;
			RotationDegrees += 70 * lDelta;
        }
		protected override void Dispose(bool pDisposing)
		{

		}
	}
}
