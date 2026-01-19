using Godot;
using System;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class Enemy2 : Enemy
	{

        float VelocityY = 90;
        float VelocityFactor = 1f;

        public override void _Ready()
		{
            scoreFactor = 500;
            EnemyHealth = 5;
            FallSpeed = 850;
            speed = 300;
            base._Ready();
        }

		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
            if (VelocityY == 0) VelocityFactor = 1f;
            else if (VelocityY == 180) VelocityFactor = -1f;
            VelocityY += VelocityFactor * lDelta;
            base._Process(lDelta);
        }

        public override void Move(float lDelta)
        {
            if (Position.X > WindowSize.X + 100) base.Move(lDelta);
            else
            {
                Position += direction * speed * lDelta;
                if(state != States.UNGRABBED)direction = new Vector2(-1,  0.25f* Mathf.Cos(VelocityY));
            }
        }

        protected override void Dispose(bool pDisposing)
		{

		}
	}
}
