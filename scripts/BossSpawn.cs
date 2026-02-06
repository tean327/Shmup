using Godot;
using System;

// Author : 

namespace Com.IsartDigital.ProjectName {
	
	public partial class BossSpawn : Sprite2D
	{

		static private BossSpawn instance;

		private BossSpawn() { }
		int Speed = 200;
		static public BossSpawn GetInstance()
		{
			if (instance == null) instance = new BossSpawn();
			return instance;

		}

		public override void _Ready()
		{
			if (instance != null)
			{
				QueueFree();
				GD.Print(nameof(BossSpawn) + " Instance already exist, destroying the last added.");
				return;
			}

			instance = this;


		}

		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
			Position += new Vector2(-1, 0) * Speed * lDelta;
		}

		protected override void Dispose(bool pDisposing)
		{
			instance = null;
			base.Dispose(pDisposing);
		}
	}
}
