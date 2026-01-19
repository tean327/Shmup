using Godot;
using System;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class crédits : UIUX
	{
		[Export] Button Restart;
		const string START_PATH = "res://Scenes/StartUi.tscn";

        public override void _Ready()
		{
            Restart.GrabFocus();
            Restart.Pressed += Restart_Pressed;
            SoundManager.GetInstance().UiMusic.Play();
        }

        private void Restart_Pressed()
        {
			SoundManager.GetInstance().UiMusic.Stop();
            SoundManager.GetInstance().UiClick.Play();
            GetTree().ChangeSceneToFile(START_PATH);
        }

        public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;

		}

		protected override void Dispose(bool pDisposing)
		{

		}
	}
}
