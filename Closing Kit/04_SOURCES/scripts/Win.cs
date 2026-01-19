using Godot;
using System;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class Win : Control
	{
		Vector2 WindowSize;
		[Export] Button Return;
		[Export] AnimatedSprite2D spaceship;
		[Export] Label Score;
		Vector2 direction;
		int speed = 500;
		float X;
		const string START_PATH = "res://Scenes/StartUi.tscn";
        public override void _Ready()
		{
			SoundManager.GetInstance().UiWin.Play();
			WindowSize = GetWindow().Size;
            Return.Pressed += Return_Pressed;
			direction = new Vector2(1,  0.5f *Mathf.Cos(X));
			Score.Text = "Final Score: " + Main.score;
			Return.GrabFocus();
		}

        private void Return_Pressed()
        {
			//SoundManager.GetInstance().UiWin.Stop();
			GetTree().ChangeSceneToFile(START_PATH);
			GetTree().ReloadCurrentScene();
        }

        public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
		    X += lDelta;
			if (spaceship.GlobalPosition.X > WindowSize.X)
			{
				spaceship.GlobalPosition = new Vector2(0, GlobalPosition.Y);
			}
			//         if (spaceship.GlobalPosition.Y > WindowSize.Y || spaceship.Position.Y < 0)
			//         {
			//	direction.Y *= -1;
			//         }
			direction.Y =  0.5f *Mathf.Cos(X);
            spaceship.Position += direction * speed * lDelta;
		}

		protected override void Dispose(bool pDisposing)
		{

		}
	}
}
