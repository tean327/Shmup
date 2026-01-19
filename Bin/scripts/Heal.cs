using Godot;
using System;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class Heal : Collectables
	{
		public override void _Ready()
		{
			base._Ready();
            AreaEntered += HealPlayer;
		}

        private void HealPlayer(Area2D area)
        {
			if(area is Player)
			{
				SoundManager.GetInstance().PowerUpHeal.Play();
				TweenManager.GetInstance().Heal(area);
				healthBar.HealthBarState--;
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
