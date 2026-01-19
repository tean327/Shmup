using Godot;
using System;
using System.Net.NetworkInformation;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class EnemyNoShoot : Enemy
	{
		public Sprite2D Shield;
		RandomNumberGenerator Rand = new RandomNumberGenerator();
		//int ShieldOrNot;
		[Export] bool ShieldOrNot;
		public override void _Ready()
		{
			base.ScenesLoad();
            scoreFactor = 100;
			EnemyHealth = 1;
			speed = 225;
			FallSpeed = 450;
            Shield = GetNode<Sprite2D>("Shield");
			Shield.Visible = ShieldOrNot;
            AreaEntered += DamageEnemy1;
			base.AreaCollisions();
        }

        private void DamageEnemy1(Area2D area)
        {
			if(!Shield.Visible)
			{
			  if (area is BulletPlayer) EnemyHealth--;
				if (area is SmartBomb) EnemyHealth = 0;
			}
        }

        public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
			base._Process(lDelta);
			Shield.RotationDegrees += 1;
            Move(lDelta);
        }
        public override void Move(float lDelta)	
        {
			if(state == States.NORMAL) base.Move(lDelta);
        }

		protected override void Dispose(bool pDisposing)
		{

		}
	}
}
