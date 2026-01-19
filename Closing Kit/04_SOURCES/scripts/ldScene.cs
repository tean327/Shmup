using Godot;
using System;

// Author : 

namespace Com.IsartDigital.ProjectName {
	
	public partial class ldScene : Node2D
	{
		[Export] Button Restart;
		static private ldScene instance;

		private ldScene() { }

		static public ldScene GetInstance()
		{
			if (instance == null) instance = new ldScene();
			return instance;

		}

		public override void _Ready()
		{
			if (instance != null)
			{
				QueueFree();
				GD.Print(nameof(ldScene) + " Instance already exist, destroying the last added.");
				return;
			}

			instance = this;
            
			Restart.Pressed += Restart_Pressed;

		}

        private void Restart_Pressed()
        {
            SoundManager.GetInstance().UiClick.Play();
            GetTree().Paused = false;
            GetTree().ReloadCurrentScene();
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
