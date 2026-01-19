using Godot;
using System;

// Author : 

namespace Com.IsartDigital.ProjectName {
	
	public partial class Friend2 : Sprite2D
	{

		static private Friend2 instance;
		float timer;
		private Friend2() { }

		static public Friend2 GetInstance()
		{
			if (instance == null) instance = new Friend2();
			return instance;

		}

		public override void _Ready()
		{
			if (instance != null)
			{
				QueueFree();
				GD.Print(nameof(Friend2) + " Instance already exist, destroying the last added.");
				return;
			}

			instance = this;


		}

		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
			if (Boss.GetInstance().Start)
			{ 
				TweenManager.GetInstance().HealthAppear(this);
				timer += lDelta;
			}
            if (timer > 4) QueueFree();
        }

		protected override void Dispose(bool pDisposing)
		{
			instance = null;
			base.Dispose(pDisposing);
		}
	}
}
