using Godot;
using System;

// Author : 

namespace Com.IsartDigital.ProjectName {
	
	public partial class SpecialCoolDown : HSlider
	{

		static private SpecialCoolDown instance;
		[Export] Sprite2D touch;
		private SpecialCoolDown() { }

		static public SpecialCoolDown GetInstance()
		{
			if (instance == null) instance = new SpecialCoolDown();
			return instance;

		}

		public override void _Ready()
		{
			if (instance != null)
			{
				QueueFree();
				GD.Print(nameof(SpecialCoolDown) + " Instance already exist, destroying the last added.");
				return;
			}

			instance = this;
			TweenManager.GetInstance().Text(touch);

		}

		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
			if(Value >= 1) touch.Visible = true;
			else touch.Visible = false;
		}

		protected override void Dispose(bool pDisposing)
		{
			instance = null;
			base.Dispose(pDisposing);
		}
	}
}
