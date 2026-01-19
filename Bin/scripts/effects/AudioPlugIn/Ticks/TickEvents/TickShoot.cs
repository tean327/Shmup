using Godot;
using System;

// Author : Julien Fournier

namespace Com.IsartDigital.ProjectName
{
	public partial class TickShoot : TickEvent
    {
        [Export] PackedScene sceneBullet;
        const int BULLET_NUM = 30;
        float BULLET_SPEED = 200f;
        protected override void OnBeat()
        {
            base.OnBeat();
            Boss.GetInstance().Shoot();
            
            
            float lAngleStep = 2 * Mathf.Pi / BULLET_NUM;
            float lAngle;
            BulletEnemy lBullet;
            Vector2 lDirection;

            //for (int i = 0; i < BULLET_NUM; i++)
            //{
            //    lAngle = i * lAngleStep;
            //    lDirection = new Vector2(Mathf.Cos(lAngle), Mathf.Sin(lAngle)).Normalized();

            //    lBullet = sceneBullet.Instantiate() as BulletEnemy;
            //    lBullet.GlobalPosition = .GlobalPosition;
            //    lBullet.Modulate = newColor;
            //    lBullet.velocity = lDirection * BULLET_SPEED;

            //    Main.GameContainer.AddChild(lBullet);
            //}
        }
    }
}

