using Godot;
using System;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class Enemy3 : Enemy
	{
        [Export] PackedScene MisileEnemyScene;
        Area2D misileEnemy;
        public Timer lTimer2 = new Timer();
        const string MISILE_PATH = "res://Scenes/misile_enemy.tscn";
        float UpTime;
        float VelocityY = 1;
        float Factor = 1;
        float X;
        public override void _Ready()
		{
            scoreFactor = 1000;
            WindowSize = GetWindow().Size;
            EnemyHealth = 10;
			speed = 250;
            FallSpeed = 750;
            BulletEnemyScene = (PackedScene)GD.Load(MISILE_PATH);
            lTimer2.WaitTime = 1f;
            lTimer2.Timeout += MisileShoot;
            lTimer2.Autostart = true;
            AddChild(lTimer2);  
            base._Ready();
            lTimer.WaitTime = 0.3f;
		}

        public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
            base._Process(lDelta);
        }
        public override void Move(float lDelta)
        {
            Position += direction * speed * lDelta;     
            if (Position.X > WindowSize.X + 100) base.Move(lDelta);
            else
            {
                X += Factor * lDelta; 
                if(state != States.UNGRABBED)direction = new Vector2(-1, -0.5f * Mathf.Log(X));
            }
        }
        
        public virtual void MisileShoot()
        {
            if (Position.Y > 0 && Position.Y < WindowSize.Y && state < States.GRABBED)
            {
                misileEnemy = MisileEnemyScene.Instantiate() as MisileEnemy;
                misileEnemy.Position = new Vector2(Position.X - 50, Position.Y);
                Main.GameContainer.AddChild(misileEnemy);
            }
        }

        protected override void Dispose(bool pDisposing)
		{

		}
	}
}
