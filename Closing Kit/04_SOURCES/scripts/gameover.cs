using Godot;
using System;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class gameover : UIUX
	{
		Button RestartButton;
		Sprite2D OverImg;
		Label text;
		public override void _Ready()
		{
			RestartButton = GetChild<Button>(1);
			RestartButton.GrabFocus();
            RestartButton.Pressed += RestartGame;
			OverImg = GetChild<Sprite2D>(0);
			text = GetChild<Label>(2);
		}

        private void RestartGame()
        {
			Player.IsGameOver = false;
			healthBar.HealthBarState = 0;
			Main.score = 0;
			Player.GetInstance().IsState3 = false;
			GetTree().ReloadCurrentScene();
        }

        public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
			if(Player.IsGameOver && !SoundManager.GetInstance().UiLose.Playing)
			{
				RestartButton.Visible = true;
				OverImg.Visible = true;
				text.Visible = true;
				text.Text = "Score: " + Main.score;
				if(SoundManager.GetInstance().LevelMusic.Playing)SoundManager.GetInstance().LevelMusic.Stop();
				if(SoundManager.GetInstance().BossMusic.Playing)SoundManager.GetInstance().BossMusic.Stop();
				SoundManager.GetInstance().UiLose.Play();
			}
		}

		protected override void Dispose(bool pDisposing)
		{

		}
	}
}
