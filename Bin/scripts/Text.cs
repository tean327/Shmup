using Godot;
using System;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class Text : Sprite2D
	{
		int Speed;
		public override void _Ready()
		{
			 Speed = 250;
			TweenManager.GetInstance().Text(this);
		}

		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
			Position += new Vector2(-1, 0) * Speed * lDelta;
		}

		protected override void Dispose(bool pDisposing)
		{

		}
	}
}
