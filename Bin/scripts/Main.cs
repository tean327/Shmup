using Com.IsartDigital.Utils.Effects;
using Godot;
using System;
using System.Collections.Generic;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName
{

    public partial class Main : Node2D
    {
        static public Node2D GameContainer;

        RandomNumberGenerator random = new RandomNumberGenerator();
        public Area2D lastShot;
        private int shotSpace = 70;
        private const int noise = 20; 
        bool IsFirstShot = true;

        [Export] CanvasLayer Canva;

        [Export] PackedScene PlayerScene;
        Area2D player;

        [Export] PackedScene BulletScene;
        Area2D bulletPlayer;
        float Timer;
        float specialTimer;

        [Export] PackedScene EnemyScene;
        Area2D enemyShoot;

        [Export] PackedScene EnemyNoShootScene;
        Area2D enemyNoShoot;

        [Export] PackedScene Enemy3Scene;
        Area2D enemy3;

        [Export] PackedScene BackgroundScene;
        ParallaxBackground background;
        [Export] PackedScene foreground;

        [Export] PackedScene PowerUpScene;
        Area2D powerUp;

        [Export] PackedScene HealScene;
        Area2D heal;

        [Export] PackedScene SmartBombScene;
        Area2D smartBomb;

        [Export] PackedScene SpecialScene;
        public static Area2D special;

        [Export] PackedScene BreakBlockScene;
        Area2D breakBlock;

        [Export] PackedScene PauseScene;


        Timer lTimer = new Timer();

        Label GodModeTxt;
        Label BombTxt;
        public static Label scoreTxt;

        public static int score = 0;

        public static Shaker shake;
        public static Shaker shakeBoss;
        public static Shaker shakeFinal;
        public override void _Ready()
        {
            GameContainer = (Node2D)GetChild(0);

            GodModeTxt = Canva.GetChild(2)as Label;
            BombTxt = Canva.GetChild(5) as Label;
            scoreTxt = Canva.GetChild(0) as Label;

            player = PlayerScene.Instantiate() as Player;
            GameContainer.AddChild(player);

            background = BackgroundScene.Instantiate() as background;
            GameContainer.AddChild(background);
            GameContainer.AddChild(foreground.Instantiate() as Foreground);

            shake = (Shaker)GameContainer.GetChild(0); 
            shakeBoss = (Shaker)GameContainer.GetChild(1); 
            shakeFinal = (Shaker)GameContainer.GetChild(2); 

            SoundManager.GetInstance().LevelMusic.Play();
        }

        public override void _Process(double pDelta)
        {
            float lDelta = (float)pDelta;
            Timer += lDelta;
            specialTimer += lDelta;
            if (Player.GetInstance().IsInvincible) GodModeTxt.Visible = true;
            else GodModeTxt.Visible = false;
            BombTxt.Text = "X " + Player.GetInstance().BombNumber;
            if (Player.IsGameOver)
            {
                GodModeTxt.Visible = false;
                scoreTxt.Visible = false;
                SoundManager.GetInstance().LevelMusic.Stop();
            }
            scoreTxt.Text = "Score: " + score;


            if (Input.IsActionJustPressed("special") && specialTimer >= 1)
            {
                special = SpecialScene.Instantiate() as Special;
                GameContainer.AddChild(special);
                SoundManager.GetInstance().LaserBegining.Play();
            }
            if (Input.IsActionJustReleased("special"))
            {
                specialTimer = 0;
                SoundManager.GetInstance().LaserBegining.Stop();
                SoundManager.GetInstance().LaserEnd.Stop();
                special.QueueFree();
            }
            SpecialCoolDown.GetInstance().Value = specialTimer;

            if(BossSpawn.GetInstance().Position.X < GetWindow().Size.X)
            {
                Boss.GetInstance().Start = true;
            }
        }

        void CreateEnemy()
        {
            enemyShoot = EnemyScene.Instantiate() as Enemy2;
            GameContainer.AddChild(enemyShoot);
            enemy3 = Enemy3Scene.Instantiate() as Enemy3;
            GameContainer.AddChild(enemy3);
        }

        public override void _Input(InputEvent @event)
        {
            if (Input.IsActionPressed("shoot") && CanFire() && Boss.GetInstance().playerCanShoot)
            {
                int Index = random.RandiRange(0, 3);
                int lNoise = random.RandiRange(-15,15);
                Player.GetInstance().shootsSound[Index].Play();

                    bulletPlayer = BulletScene.Instantiate() as BulletPlayer;
                    bulletPlayer.Position = new Vector2(Player.GetInstance().MarkerMid.GlobalPosition.X,Player.GetInstance().MarkerMid.GlobalPosition.Y + lNoise);
                    GameContainer.AddChild(bulletPlayer);
                    IsFirstShot = false;
                    if (Player.GetInstance().IsState3)
                    {
                        bulletPlayer = BulletScene.Instantiate() as BulletPlayer;
                        bulletPlayer.Position = new Vector2(Player.GetInstance().MarkerDown.GlobalPosition.X, Player.GetInstance().MarkerDown.GlobalPosition.Y + lNoise);
                    GameContainer.AddChild(bulletPlayer);

                        bulletPlayer = BulletScene.Instantiate() as BulletPlayer;
                        bulletPlayer.Position = new Vector2(Player.GetInstance().MarkerUp.GlobalPosition.X, Player.GetInstance().MarkerUp.GlobalPosition.Y + lNoise);
                    GameContainer.AddChild(bulletPlayer);
                    }
            }

            if (Input.IsActionJustPressed("bomb") && Player.GetInstance().BombNumber > 0)
            {
                smartBomb = SmartBombScene.Instantiate() as SmartBomb;
                GameContainer.AddChild(smartBomb);
                Player.GetInstance().BombNumber--;
            }
            if (Input.IsActionJustPressed("Pause"))
            {  
                GameContainer.GetTree().Paused = true;
                Canva.AddChild(PauseScene.Instantiate() as PauseMenu);
            }
        }
        protected bool CanFire()
        {
            float lFrame = Timer;
            if (Timer > 0.1f)Timer =0;
            return (lFrame > 0.1f);
        }
    }
}