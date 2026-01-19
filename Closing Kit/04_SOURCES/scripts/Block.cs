using Godot;
using System;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class Block : Grabable
	{
		
		public override void _Ready()
		{
            base._Ready();
			speed = 200;
			FallSpeed = 900;
            AreaEntered += Collisions;
        }

        private void Collisions(Area2D area)
        {
            if(area is BulletPlayer) area.QueueFree();
        }

        public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
			base._Process(lDelta);
			if(state == States.NORMAL) direction = new Vector2(-1, 0);
		}

		protected override void Dispose(bool pDisposing)
		{

		}
	}
}
