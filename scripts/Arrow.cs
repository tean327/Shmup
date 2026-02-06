using Com.IsartDigital.Utils.Tweens;
using Godot;
using System;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class Arrow : Sprite2D
	{
		public override void _Ready()
		{
			var lTween = CreateTween().SetLoops();
			lTween.TweenProperty(this, TweenProp.POSITION, Position += new Vector2(10, 0), 0.5f);
			lTween.TweenProperty(this, TweenProp.POSITION, Position += new Vector2(-10, 0), 0.5f);
			lTween.SetEase(Tween.EaseType.InOut);
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
