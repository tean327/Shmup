using Godot;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

// Author : 

namespace Com.IsartDigital.ProjectName {
	
	public enum BossStates
	{
		STATE1,
		STATE2,
		STATE3,
		STATE4,
	}
	public partial class Boss : Node2D
	{
		Vector2 WindowSize;
		int Speed = 350;
		static private Boss instance;
		public bool Start = false;
		[Export] AudioStreamPlayer TickMove;
		[ExportGroup("Canons")]
		[Export]Canon canonUp;
		[Export]Canon canonDown;
		[Export]Canon canonMid;

		[ExportGroup("Markers")]
		[Export] Marker2D markerMid;
		[Export] Marker2D markerDown;
		[Export] Marker2D markerUp;

		[ExportGroup("Scenes")]
		[Export] PackedScene BulletScene;
        [Export] PackedScene EnemyPopCornScn;
		BulletEnemy bullet;
        List<Canon> canons;
		EnemyNoShoot enemy;
		const uint enemiesNumber = 15;
		bool MusicHasPlay = false;
		[Export] Area2D corpse;
		[Export] Sprite2D flash;
		public BossStates state = BossStates.STATE1;
		List<Marker2D> markers;

		public bool playerCanShoot = true;
		Vector2 direction;
		[Export] public Sprite2D friend;
		public int health = 1;
		bool Win = false;

        float VelocityY = 90;
		const int MARGIN = 100;
        const string WIN_PATH = "res://Scenes/win.tscn";

		int shotNumber = 15;

		RandomNumberGenerator rand = new RandomNumberGenerator();

		[Export] PackedScene rond;
        private Boss() { }

		static public Boss GetInstance()
		{
			if (instance == null) instance = new Boss();
			return instance;

		}

		public override void _Ready()
		{
            if (GetWindow().Size > DisplayServer.ScreenGetSize()) WindowSize = DisplayServer.ScreenGetSize();
            else WindowSize = GetWindow().Size;
            if (instance != null)
			{
				QueueFree();
				GD.Print(nameof(Boss) + " Instance already exist, destroying the last added.");
				return;
			}
            instance = this;
			canons = new List<Canon>() {canonUp, canonMid, canonDown};
			foreach (Canon lCanon in canons) lCanon.health = 6;
			WindowSize = GetWindow().Size;
			friend.Visible = false;
            corpse.AreaEntered += Corpse_AreaEntered;
			
			markers = new List<Marker2D>() { markerDown, markerMid, markerUp };
        }

        private void Corpse_AreaEntered(Area2D area)
        {
            if(friend.Visible)
			{
				GD.Print("boss");
				Main.shakeBoss.Start();
				if (area is SmartBomb) health = 0;
			}
        }

        public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
			if (Start)
			{

				if (GlobalPosition.X > WindowSize.X - 150)
				{
					playerCanShoot = false;
					Position += Vector2.Left * Speed * lDelta;
				}
				else playerCanShoot = true;
			}
			if (GlobalPosition.X <= WindowSize.X && !MusicHasPlay)
			{
				SoundLaunch();
			}
			if (canonUp.health <= 0 && canonMid.health <= 0 && canonDown.health <= 0 && health == 1)
			{
				switch (state)
				{
					case BossStates.STATE1:
						Main.shake.Start();
						state = BossStates.STATE2;
						foreach (Canon lCanon in canons)
						{
							lCanon.health = 12;
							lCanon.StateChange();
						}
						Position = new Vector2(Position.X,WindowSize.Y / 2);
						break;
					case BossStates.STATE2:
                        Main.shake.Start();
                        state = BossStates.STATE3;
						SpawnEnemies();
                        Position = new Vector2(Position.X, WindowSize.Y / 2);
                        foreach (Canon lCanon in canons)
						{
							lCanon.health = 1;
							lCanon.StateChange();
						}
						Text lRond = rond.Instantiate() as Text;
						lRond.Position = GlobalPosition;
						AddChild(lRond);
						break;
					case BossStates.STATE3:
                        Main.shake.Start();
                        state = BossStates.STATE4;
						foreach (Canon lCanon in canons) lCanon.QueueFree();
						friend.Visible = true;
						HealthBoss.GetInstance().Frame = 3;
						SoundManager.GetInstance().bossPreExplosion.Play();
						break;
					default:
						break;
				}
			}
            if (canonUp.CanKill && canonMid.CanKill && canonDown.CanKill)
            {
                foreach (Canon lCanon in canons)
                {
                    lCanon.health = 0;
                }
            }

			if (health == 0)
			{
				friend.Visible = false;
                HealthBoss.GetInstance().Frame = 4;
				TweenManager.GetInstance().Boss(this);
                SoundManager.GetInstance().bossExplosion.Play();
                Main.shakeFinal.Start();
				health = -100;
                if (SoundManager.GetInstance().TickShoot.Playing) SoundManager.GetInstance().TickShoot.Stop();
                if (SoundManager.GetInstance().TickScale.Playing) SoundManager.GetInstance().TickScale.Stop();
                if (TickMove.Playing) TickMove.Stop();
            }

			if (Win)
			{
                Main.score += 5000;
				Player.GetInstance().QueueFree();
				QueueFree();
				GetTree().ChangeSceneToFile(WIN_PATH);
			}
        }

		private void SpawnEnemies()
		{
			float lWidthPart = WindowSize.Y / enemiesNumber;
            for (int i = 0; i < enemiesNumber; i++)
			{
				enemy = (EnemyNoShoot)EnemyPopCornScn.Instantiate();
				enemy.Position = new Vector2(GlobalPosition.X + 100, WindowSize.Y - lWidthPart * (i - 1));
				Main.GameContainer.AddChild(enemy);
			}

        }
		

		private void BossMove(float lDelta)
		{
			Position += direction * Speed * lDelta;
			float VelocityFactor = 1;
            if (VelocityY == 0) VelocityFactor = 1f; 
			else if (VelocityY == 180) VelocityFactor = -1f;
            VelocityY += VelocityFactor * lDelta;
            switch (state)
            {

                case BossStates.STATE1:
					direction = new Vector2(0,  0.25f * Mathf.Cos(VelocityY));
						break;
                case BossStates.STATE2:
					direction = new Vector2( 0.5f*Mathf.Cos(VelocityY), 0.5f*Mathf.Sin(VelocityY));

                    break;
                case BossStates.STATE3:
					direction = Vector2.Zero;
                    break;
            }
        }
		private void SoundLaunch()
		{
            SoundManager.GetInstance().LevelMusic.Stop();
			SoundManager.GetInstance().BossMusic.Play();
			SoundManager.GetInstance().BossMusic.Finished += EndGame;
			TickMove.Play();
			SoundManager.GetInstance().TickScale.Play();
			SoundManager.GetInstance().TickShoot.Play();
			MusicHasPlay = true;
        }
		public void EndGame()
		{
			if (health <= 0) Win = true;	
			else Player.IsGameOver = true;
		}
		public void Shoot()
		{
			if (state < BossStates.STATE3 && Start)
			{
				SoundManager.GetInstance().ShootBoss.Play();
				for (int i = 0; i < shotNumber; i++)
				{
					for (int j = 0; j < markers.Count; j++)
					{
						bullet = BulletScene.Instantiate() as BulletEnemy;
						bullet.Position = new Vector2(markers[j].GlobalPosition.X - i ,markers[j].GlobalPosition.Y);
						Main.GameContainer.AddChild(bullet);
					}
				}
			}
		}
	

        protected override void Dispose(bool pDisposing)
		{
			instance = null;
			base.Dispose(pDisposing);
		}
	}
}
