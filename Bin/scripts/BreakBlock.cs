using Godot;
using System;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class BreakBlock : Block
	{
		AnimatedSprite2D State;
		public override void _Ready()
		{
			State = (AnimatedSprite2D)GetChild(0);
            AreaEntered += BreakCollision;
			base._Ready();
		}

        private void BreakCollision(Area2D area)
        {
            if(area is BulletPlayer)
			{
                area.QueueFree();
                State.Frame += 2;
				if(State.Frame == 9)
				{
					QueueFree();
				}
			}
			if(area is SmartBomb)
			{
				area.QueueFree();
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
