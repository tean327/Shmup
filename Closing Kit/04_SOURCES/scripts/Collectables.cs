using Godot;
using System;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class Collectables : Movables
	{
		const int scoreFactor = 50;
		public override void _Ready()
		{
			speed = 250;
            AreaEntered += CollectablesCollision;
			TweenManager.GetInstance().Collectables(this);
        }

        private  void CollectablesCollision(Area2D pArea)
        {
		   if(pArea is Player)
		   {
				Main.score += scoreFactor;
			  QueueFree();
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
