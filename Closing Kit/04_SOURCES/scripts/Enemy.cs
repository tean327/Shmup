using Com.IsartDigital.Utils.Tweens;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class Enemy : Grabable
	{
        static RandomNumberGenerator lRand = new RandomNumberGenerator();
        PackedScene PowerUpScene;
        Area2D powerUp;
		const string POWER_PATH = "res://Scenes/power_up.tscn";

		PackedScene explosionScn;
		GpuParticles2D explosion;
		const string EXPLOSION_PATH = "res://Scenes/Explode.tscn";

        PackedScene HealScene;
        Area2D heal;
		const string HEAL_PATH = "res://Scenes/heal.tscn";

        PackedScene SmartBombScene;
        Area2D smartBomb;
		const string BOMB_PATH = "res://Scenes/smart_bomb_power_up.tscn";

        public PackedScene BulletEnemyScene;
        public Area2D BulletEnemy;
        public Timer lTimer = new Timer();
		const string BULLET_PATH = "res://Scenes/BulletEnemy.tscn";
		public int EnemyHealth;
		PackedScene scorePartScn;
		Sprite2D scorePart;
		const string SCORE_PATH = "res://Scenes/ScorePart.tscn";

		const int MARGIN = 150;


		public static bool IsTouched = false;

		int PreviousDrop;

		public bool IsImuneToShoot;

		public int scoreFactor;


		public override void _Ready()
		{
            ScenesLoad();
			BulletEnemyScene = (PackedScene)GD.Load(BULLET_PATH);
			explosionScn = (PackedScene)GD.Load(EXPLOSION_PATH);
			lTimer.WaitTime = 1f;
				lTimer.Timeout += Shoot;
				lTimer.Autostart = true;
				AddChild(lTimer);
            AreaEntered += DamageEnemy;
			base._Ready();
        }
		public  void ScenesLoad()
		{
            BulletEnemyScene = (PackedScene)GD.Load(BULLET_PATH);
            explosionScn = (PackedScene)GD.Load(EXPLOSION_PATH);
            PowerUpScene = (PackedScene)GD.Load(POWER_PATH);
            HealScene = (PackedScene)GD.Load(HEAL_PATH);
            SmartBombScene = (PackedScene)GD.Load(BOMB_PATH);
			scorePartScn = (PackedScene)GD.Load(SCORE_PATH);
        }

        private void DamageEnemy(Area2D pArea)
        {
			if (pArea is BulletPlayer)
			{
				TweenManager.GetInstance().EnemyTouched(this);
				EnemyHealth--;
			}
            if (pArea is SmartBomb) EnemyHealth = 0;
        }


		public override void _Process(double pDelta)
		{
            float lDelta = (float)pDelta;
			base.DestroyOutWindow();
            base.GameOver();
			base._Process(lDelta);
			Dead();	
		}

		private  int DropNumber()
		{
			//return an int which is diferent of the two previous returned int
	       
			int lDrop;
			do
			{
				lDrop = lRand.RandiRange(0, 2);
			} while (lDrop == PreviousDrop);
			GD.Print(lDrop, PreviousDrop);
			PreviousDrop = lDrop;
            return lDrop;
        }

		private void DropPowerUp()
		{
			//based of the int returned by the DropNumber function we Add a powerUp
			switch(DropNumber())
			{
				case 0:
                    powerUp = PowerUpScene.Instantiate() as PowerUp;
					powerUp.Position = GlobalPosition;
                    Main.GameContainer.AddChild(powerUp);
					break;
				case 1:
                    heal = HealScene.Instantiate() as Heal;
					heal.Position = GlobalPosition;
                    Main.GameContainer.AddChild(heal);
					break;
				case 2:
                    smartBomb = SmartBombScene.Instantiate() as SmartBombPowerUp;
					smartBomb.Position = GlobalPosition;
                    Main.GameContainer.AddChild(smartBomb);
					break;
            }
        }
		
		public virtual void Shoot()
		{
			if (state < States.GRABBED && Position.X < WindowSize.X)
			{
				SoundManager.GetInstance().ShootEnemy.Play();
				BulletEnemy = BulletEnemyScene.Instantiate() as BulletEnemy;
				BulletEnemy.Position = new Vector2(Position.X - 50, Position.Y);
				Main.GameContainer.AddChild(BulletEnemy);
			}
		}
		public void Dead()
		{
			if(EnemyHealth <= 0)
			{
                int Index = GD.RandRange(0, 4);
			    SoundManager.GetInstance().enemyExplosions[Index].Play();
                Main.score += scoreFactor;
				explosion = explosionScn.Instantiate() as GpuParticles2D;
				explosion.Emitting = true;
				explosion.Position = GlobalPosition;
				Main.GameContainer.AddChild(explosion);
				DropPowerUp();
				QueueFree();
			}
		}

		protected override void Dispose(bool pDisposing)
		{

		}
	}
}
