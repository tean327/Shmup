using Com.IsartDigital.Utils.Tweens;
using Godot;
using System;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class ScorePart : Sprite2D
	{
		public override void _Ready()
		{

		}

		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
            //var lTween = CreateTween();
            //lTween.TweenProperty(this, TweenProp.POSITION, Main.scoreTxt.GlobalPosition, 0.5f);
            //lTween.SetEase(Tween.EaseType.Out);
        }

		protected override void Dispose(bool pDisposing)
		{

		}
	}
}
