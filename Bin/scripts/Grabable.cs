using Godot;
using System;
using System.Collections.Generic;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	public enum States
	{
		NORMAL,
		GRABBED,
		UNGRABBED
	}
	public partial class Grabable : Movables
	{
		Vector2 currentPos;
		Marker2D marker;
		public States state = States.NORMAL;
		public int FallSpeed = 500;
		public Vector2 direction;
		
        public override void _Ready()
        {
			AreaCollisions();
        }
		public void AreaCollisions()
		{
            AreaEntered += Grabable_AreaEntered;
            AreaExited += Grabable_AreaExited;
        }

        private void Grabable_AreaExited(Area2D area)
        {
            if (area is Special && state == States.GRABBED)
			{
				state = States.UNGRABBED;
			}
        }

        private void Grabable_AreaEntered(Area2D area)
        {
            if (area is Special)
            {
				state = States.GRABBED;
            }
            if (state == States.GRABBED)
			{
				if (area is BulletEnemy || area is Enemy || area is Block)
				{
					area.QueueFree();
					QueueFree();
				}
			}
        }

        public override void _Process(double pDelta)
        {
            float lDelta = (float)pDelta;
            base._Process(lDelta);
			Position += direction * speed * lDelta;
			switch (state)
			{
				case States.NORMAL:
					break;
				case States.GRABBED:
					GlobalPosition = Special.Collision.GlobalPosition;
					direction = Vector2.Zero;
					break;
				case States.UNGRABBED:
					direction = new Vector2(0, 1);
					RotationDegrees += 1;
					speed = FallSpeed;
					break;
				default:
					break;
			}
		}
        public override void DestroyOutWindow()
        {
            base.DestroyOutWindow();
			if (Position.Y > WindowSize.Y)
			{
				QueueFree();
			}
        }

        /*public static Vector2 GrabPos;
		public bool Desactivated = false;
		public bool IsGrabed = false;
		public static bool OneIsGrabed = false;
		public int FallSpeed;
		private static List<Grabable> instances = new List<Grabable>();
		private bool IsTarget = false;
		public override void _Ready()
		{
			AreaCollisions();
			instances.Add(this);
		}

        public virtual void AreaCollisions()
		{
            AreaEntered += SpeciallFeature;
            AreaExited += DieSpecial;
        }

        private void DieSpecial(Area2D area)
        {
			//if the special is exited the area the grabable object is now not gradbed an
            if(area is Special)
			{
				IsGrabed = false;
			}
			if(Desactivated && area is BulletEnemy || area is Enemy || area is Block)
			{
				//area.QueueFree();
				//QueueFree();
			}
        }

		public static Grabable GetSpecialTarget()
		{
            for (int i = instances.Count - 1; i >= 0; i--)
            {
                if (!instances[i].IsTarget)
                {
                    instances[i].IsTarget = true;
                    return instances[i];
                }
            }
            return null;
        }

        private void SpeciallFeature(Area2D area)
        {
			// if the area is entered by the special feature and nobody is currently grabed the object is now desactived and grab
            if(area is Special && !OneIsGrabed)
			{
				Desactivated = true;
				IsGrabed = true;
			}
        }
		public virtual void GrabIsGrabed( float lDelta)
		{
            if (Desactivated && IsGrabed)
            {
                Position = Special.Collision.GlobalPosition;
            }
            else if (Desactivated)
            {
                Position += new Vector2(0, 1) * FallSpeed * lDelta;
                Rotation += 1 * lDelta;
            }
        }
        public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
			GrabPos = Position;
			// this ensure that only one object can be grabed by the special
			if (IsGrabed) OneIsGrabed = true;
			else OneIsGrabed = false;
		}

		protected override void Dispose(bool pDisposing)
		{

		}
	*/
    }
}
