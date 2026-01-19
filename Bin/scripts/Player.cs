using Godot;
using System;
using System.Collections.Generic;
using static Godot.TextServer;

// Author : Etan Grimault Martinez 

namespace Com.IsartDigital.ProjectName {
	
    
	public partial class Player : Movables
	{
        public  AnimatedSprite2D PlayerState;
		static private Player instance;
        Vector2 Direction;
        public  bool IsInvincible = false;
        private bool IsImune = false;
        public static bool IsGameOver = false;
        public  int BombNumber = 1;
        int NumberOfBomb;
        bool IsState2 = false;
        public bool IsState3 = false;
        int GodModNum = 1;
        const int MARGIN = 50;
        float timer;

        public  Marker2D MarkerUp;
        public  Marker2D MarkerDown;
        public  Marker2D MarkerMid;

        public  List<AudioStreamPlayer> shootsSound;
		private Player() { }

		static public Player GetInstance()
		{
			if (instance == null) instance = new Player();
			return instance;

		}
        
		public override void _Ready()
		{
			if (instance != null)
			{
				QueueFree();
				GD.Print(nameof(Player) + " Instance already exist, destroying the last added.");
				return;
			}
            speed = 800;
			instance = this;
            Position = new Vector2(60, 291);
            PlayerState = (AnimatedSprite2D)GetChild(0);
            MarkerDown = (Marker2D)GetChild(4);
            MarkerMid = (Marker2D)GetChild(3);
            MarkerUp = (Marker2D)GetChild(2);
            AreaEntered += PlayerCollision;

            shootsSound = new List<AudioStreamPlayer>() {SoundManager.GetInstance().Shoot0, SoundManager.GetInstance().Shoot1, SoundManager.GetInstance().Shoot2, SoundManager.GetInstance().Shoot3 };
        }

        private void PlayerCollision(Area2D area)
        {
            if(area is Grabable || area is BulletEnemy || area is MisileEnemy)
            {
                SoundManager.GetInstance().Loselife.Play();
                if(!IsInvincible && !IsImune)
                {
                    timer = 0;
                    TweenManager.GetInstance().Blink(this);
                    IsImune = true;
                  if (healthBar.HealthBarState < 4 && area is not MisileEnemy)
                  {
                     healthBar.HealthBarState++;
                  }
                  else if (healthBar.HealthBarState < 4 && area is MisileEnemy) healthBar.HealthBarState+= 2;

                }
                if (healthBar.HealthBarState >= 4)
                {
                    SoundManager.GetInstance().PlayerExplosion.Play();
                    IsGameOver = true;
                    IsState2 = false;
                    IsState3 = false;
                    Visible = false;
                    Position = new Vector2(-500, 500); 
                }
            }
        }


        public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
            timer += lDelta;
            Move(lDelta);
            //if the player has pressed the godmode input the number of times it has been pressed increase and if the number is even the player is in godmode
            if (Input.IsActionJustPressed("GodMode"))
            {
                GodModNum++;
            }
            if(GodModNum % 2 == 0)
            {
                GodMode();
            }
            else
            {
                NormalMod();
            }
            //change the state of the player and what it does for the state2 (the state 3 is in main)
            if (PlayerState.Frame == 1)
            {
                IsState2 = true;
            }
            if(PlayerState.Frame == 2)
            {
                IsState3 = true;
            }
            if(IsState2)
            {
                speed = 1200;
            }
            if(timer > 1)IsImune = false;
        }
        // change the alpha to show that the player is in godmode make the player invisible and having infinite number of bomb
        public void GodMode()
        {
            
            if(BombNumber <= 100) NumberOfBomb = BombNumber;
            Modulate = new Color(1, 1, 1, 0.5f);
            IsInvincible = true;
            BombNumber = 10000000;
        }
        public void NormalMod()
        {
            if(BombNumber != NumberOfBomb && NumberOfBomb != 0 && IsInvincible) BombNumber = NumberOfBomb;
            Modulate = new Color(1, 1, 1, 1f);
            IsInvincible = false;
        }

        public override void Move(float lDelta)
        {
            if (Input.GetActionStrength("down") > 0 && Position.Y < WindowSize.Y - MARGIN)
            {
                Direction = new Vector2(0, 1);
                Position += Direction * speed * Input.GetActionStrength("down") *lDelta;
            }
            if (Input.GetActionStrength("up") > 0 && Position.Y > MARGIN)
            {
                Direction = new Vector2(0, -1);
                Position += Direction * speed * Input.GetActionStrength("up") * lDelta;
            }
            if (Input.GetActionStrength("left") > 0 && Position.X > MARGIN)
            {
                Direction = new Vector2(-1, 0);
                Position += Direction * speed * Input.GetActionStrength("left") * lDelta;
            }
            if (Input.GetActionStrength("right") > 0 && Position.X < WindowSize.X - MARGIN)
            {
                Direction = new Vector2(1, 0);
                Position += Direction * speed * Input.GetActionStrength("right") * lDelta;
            }
        }

		protected override void Dispose(bool pDisposing)
		{
			instance = null;
			base.Dispose(pDisposing);
		}
	}
}
