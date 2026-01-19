using Godot;
using System;
using System.Collections.Generic;

// Author : 

namespace Com.IsartDigital.ProjectName {
	
	public partial class SoundManager : Node
	{

		static private SoundManager instance;

		private SoundManager() { }

		static public SoundManager GetInstance()
		{
			if (instance == null) instance = new SoundManager();
			return instance;

		}
		[ExportGroup("UI")]
		[Export] public AudioStreamPlayer UiStart;
		[Export] public AudioStreamPlayer UiPause;
		[Export] public AudioStreamPlayer UiClick;
		[Export] public AudioStreamPlayer UiWin;
		[Export] public AudioStreamPlayer UiLose;
		public static AudioStreamPlayer bossMusic;

        [ExportGroup("Ticks")]
        [Export] public AudioStreamPlayer TickMove;
        [Export] public AudioStreamPlayer TickScale;
        [Export] public AudioStreamPlayer TickShoot;


        [ExportGroup("Player")]
        [Export] public AudioStreamPlayer Shoot0;
        [Export] public AudioStreamPlayer Shoot1;
        [Export] public AudioStreamPlayer Shoot2;
        [Export] public AudioStreamPlayer Shoot3;
        [Export] public AudioStreamPlayer PlayerExplosion;
        [Export] public AudioStreamPlayer PowerUpState;
        [Export] public AudioStreamPlayer PowerUpBomb;
        [Export] public AudioStreamPlayer PowerUpHeal;
        [Export] public AudioStreamPlayer BombSound;
        [Export] public AudioStreamPlayer Loselife;
		[Export] public AudioStreamPlayer LaserBegining;
		[Export] public AudioStreamPlayer LaserEnd;

		[ExportGroup("Music")]
		[Export] public AudioStreamPlayer Ambience;
		[Export] public AudioStreamPlayer LevelMusic;
		[Export] public AudioStreamPlayer BossMusic;
		[Export] public AudioStreamPlayer UiMusic;

		[ExportGroup ("Enemy")]
        [Export] public AudioStreamPlayer Explosion0;
        [Export] public AudioStreamPlayer Explosion1;
        [Export] public AudioStreamPlayer Explosion2;
        [Export] public AudioStreamPlayer Explosion3;
        [Export] public AudioStreamPlayer Explosion4;
        [Export] public AudioStreamPlayer ShootEnemy;
        [Export] public AudioStreamPlayer ShootBoss;

		[ExportGroup("Boss")]
		[Export] public AudioStreamPlayer bossPreExplosion;
		[Export] public AudioStreamPlayer bossExplosion;

		public List<AudioStreamPlayer> enemyExplosions;
        public override void _Ready()
		{
			if (instance != null)
			{
				QueueFree();
				GD.Print(nameof(SoundManager) + " Instance already exist, destroying the last added.");
				return;
			}

			instance = this;

			enemyExplosions = new List<AudioStreamPlayer>() {Explosion0,Explosion1,Explosion2, Explosion3,Explosion4 };
		}

		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;

		}

		protected override void Dispose(bool pDisposing)
		{
			instance = null;
			base.Dispose(pDisposing);
		}
	}
}
