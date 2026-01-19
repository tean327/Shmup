using Godot;
using System;

// Author : 

namespace Com.IsartDigital.ProjectName {
	
	public partial class Camera : Camera2D
	{

		static private Camera instance;

		private Camera() { }
		Vector2 WindowSize;
		static public Camera GetInstance()
		{
			if (instance == null) instance = new Camera();
			return instance;

		}

		public override void _Ready()
		{
			if (instance != null)
			{
				QueueFree();
				GD.Print(nameof(Camera) + " Instance already exist, destroying the last added.");
				return;
			}

			instance = this;
			WindowSize = GetWindow().Size;
			Position = WindowSize / 2;	
		}	

		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
			if (Player.GetInstance().Position.Y <= WindowSize.Y - 250 && Player.GetInstance().Position.Y >= 250)
			{
				Position = new Vector2(Position.X, Player.GetInstance().Position.Y);
			}
		}

		protected override void Dispose(bool pDisposing)
		{
			instance = null;
			base.Dispose(pDisposing);
		}
	}
}
