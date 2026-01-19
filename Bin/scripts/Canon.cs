using Godot;
using System;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class Canon : Grabable
	{
		[Export] AnimatedSprite2D anim;
		public int health;
		public bool CanKill = false;
		float timer;
		Arrow arrow;
		public override void _Ready()
		{
			arrow = GetChild(3) as Arrow;
			arrow.Visible = false;
			arrow._Ready();
            AreaEntered += OnAreaEntered;
            AreaExited += Canon_AreaExited;
		}

        private void Canon_AreaExited(Area2D area)
        {
			if (area is Special && Boss.GetInstance().state == BossStates.STATE3)
			{
				state = States.UNGRABBED;
			}
        }

        private void OnAreaEntered(Area2D area)
        {
            if(area is BulletPlayer && Boss.GetInstance().state != BossStates.STATE3 || area is SmartBomb && Boss.GetInstance().state != BossStates.STATE3)
			{
				TweenManager.GetInstance().EnemyTouched(this);
				health--;
			}
			if(area is Special && Boss.GetInstance().state == BossStates.STATE3)
			{
				state = States.GRABBED;
                timer = 0;
            }
        }

        public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
			timer += lDelta;
			if (Boss.GetInstance().state == BossStates.STATE3)
			{
				base._Process(lDelta);
			}
            if (health <= 0)
			{
				Rotation = Mathf.Lerp(Rotation, -Mathf.Pi / 2, 0.01f);
            }
			DestroyOutWindow();
			if(state == States.GRABBED && timer >=3)
			{
				Modulate = new Color(0, 0, 0, 0.75f);
			}
			if (anim.Frame == 2) arrow.Visible = true;
		}

		public void StateChange()
		{
            Rotation =  0;
            anim.Frame = (int)Boss.GetInstance().state;
        }

        public override void DestroyOutWindow()
        {
			if (Position.Y > WindowSize.Y + 150) CanKill = true;
        }

		protected override void Dispose(bool pDisposing)
		{

		}
	}
}
