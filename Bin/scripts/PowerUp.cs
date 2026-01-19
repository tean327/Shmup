using Godot;
using System;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class PowerUp : Collectables
	{
		public override void _Ready()
		{
            //Position = new Vector2 (400, 200);
			base._Ready();
            AreaEntered += PowerUp_Player;
        }

        private void PowerUp_Player(Area2D area)
        {
			if (area is Player)
			{
			  SoundManager.GetInstance().PowerUpState.Play();
			  Player.GetInstance().PlayerState.Frame += 1;
			  healthBar.HealthBarState = 0;
			  TweenManager.GetInstance().PowerUp(area);
			}
        }

        public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
			base._Process(pDelta);
		}

		protected override void Dispose(bool pDisposing)
		{

		}
	}
}
