using Godot;
using System;
using System.Collections.Generic;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class background : ParallaxBackground
	{
        List<ParallaxLayer> layers = new List<ParallaxLayer>();
		List<float> layerSpeeds = new List<float>() {200, 350, 1700	, 0};
		ParallaxLayer Damier;

        static private background instance;

		private background() { }

		static public background GetInstance()
		{
			if (instance == null) instance = new background();
			return instance;

		}

		public override void _Ready()
		{
			if (instance != null)
			{
				QueueFree();
				GD.Print(nameof(background) + " Instance already exist, destroying the last added.");
				return;
			}

			instance = this;

            foreach (ParallaxLayer layer in GetChildren())
            {
                layers.Add(layer);
            }
			if (GetParent() is Control)
			{
				Damier = (ParallaxLayer)GetChild(3);
				Damier.Visible = false;
			}
		}

		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
            int lLength = layers.Count;	
            for (int i = 0; i < lLength; i++)
            {
                layers[i].MotionOffset -= new Vector2(1, 0) * lDelta * layerSpeeds[i];
            }
        }

		protected override void Dispose(bool pDisposing)
		{
			instance = null;
			base.Dispose(pDisposing);
		}
	}
}
