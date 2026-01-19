using Godot;
using System;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class SmartBombPowerUp : Collectables
	{
		public override void _Ready()
		{
			base._Ready();
            AreaEntered += SmartBombPowerUpCollision;
		}

        private void SmartBombPowerUpCollision(Area2D area)
        {
            if(area is Player)
			{
				SoundManager.GetInstance().PowerUpBomb.Play();
				Player.GetInstance().BombNumber++;
				TweenManager.GetInstance().Bomb(area);	
			}
        }

        public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
			base._Process(lDelta);
		}

		protected override void Dispose(bool pDisposing)
		{

		}
	}
}
