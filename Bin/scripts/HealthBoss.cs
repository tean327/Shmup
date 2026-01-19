using Godot;
using System;

// Author : 

namespace Com.IsartDigital.ProjectName {
	
	public partial class HealthBoss : AnimatedSprite2D
	{

		static private HealthBoss instance;
		int currentFrame;
		private HealthBoss() { }

		static public HealthBoss GetInstance()
		{
			if (instance == null) instance = new HealthBoss();
			return instance;

		}

		public override void _Ready()
		{
			if (instance != null)
			{
				QueueFree();
				GD.Print(nameof(HealthBoss) + " Instance already exist, destroying the last added.");
				return;
			}

			instance = this;
			currentFrame = Frame;
            Modulate = Colors.Transparent;
            Visible = false;

		}

		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
			if(Boss.GetInstance().Start && Modulate == new Color(1,1,1,0f)) { TweenManager.GetInstance().HealthAppear(this); }
            if (Boss.GetInstance().state <= BossStates.STATE3)
            {
                Frame = (int)Boss.GetInstance().state;
            }
			while(currentFrame != Frame)
			{
				TweenManager.GetInstance().HealthBoss(this);
				currentFrame = Frame;
			}
        }
		protected override void Dispose(bool pDisposing)
		{
			instance = null;
			base.Dispose(pDisposing);
		}
	}
}
