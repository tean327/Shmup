using Godot;
using System;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class Special : Movables
	{
		Sprite2D Bubble;
		public static CollisionShape2D Collision;
		public static bool CoolDown = true;
		[Export] private Marker2D marker;
		Grabable grabbedObject;

		public override void _Ready()
		{
			WindowSize = GetWindow().Size;
			Bubble = GetChild(2) as Sprite2D;
            AreaEntered += OnAreaEntered;
			Collision = GetChild(0) as CollisionShape2D;
        }

        private void OnAreaEntered(Area2D area)
        {
            if(area is Grabable)
			{
				SoundManager.GetInstance().LaserBegining.Stop();
				SoundManager.GetInstance().LaserEnd.Play();
				Bubble.Visible = true;
			}
        }



        public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
            Position = Player.GetInstance().Position;
			//RotationDegrees = Mathf.Atan2(Grabable.GrabPos.X - Position.X, Grabable.GrabPos.Y - Position.Y);
			if(!Bubble.Visible)
			{
				Scale += new Vector2(1f, 0) * lDelta;
			}
		}

		public override void DestroyOutWindow()
		{
			if (Collision.Position.X > WindowSize.X)
			{
				QueueFree();
			}
		}
		protected override void Dispose(bool pDisposing)
		{

		}
	}
}
