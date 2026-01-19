using Com.IsartDigital.Utils.Tweens;
using Godot;
using System;

// Author : 

namespace Com.IsartDigital.ProjectName {
	
	public partial class TweenManager : Node
	{

		static private TweenManager instance;

		private TweenManager() { }

		static public TweenManager GetInstance()
		{
			if (instance == null) instance = new TweenManager();
			return instance;

		}

		public override void _Ready()
		{
			if (instance != null)
			{
				QueueFree();
				GD.Print(nameof(TweenManager) + " Instance already exist, destroying the last added.");
				return;
			}

			instance = this;

		}
		public void Blink(Node2D pNode)
		{
			var lTween = CreateTween();
			for(int i = 0; i < 2; i++)
			{
                lTween.TweenProperty(pNode, TweenProp.MODULATE, new Color(1, 1, 1, 0.1f), 0.1f);
                lTween.TweenProperty(pNode, TweenProp.MODULATE, new Color(1, 1, 1, 1), 0.1f);
            }
            lTween.SetEase(Tween.EaseType.OutIn);
		}

        public void PowerUp(Node2D pNode)
        {
            var lTween = CreateTween();
            lTween.TweenProperty(pNode, TweenProp.SCALE, new Vector2(2,2), 1f);
            lTween.TweenProperty(pNode, TweenProp.SCALE, new Vector2(1, 1), 0.3f);
            lTween.SetEase(Tween.EaseType.OutIn);
        }
        public void Heal(Node2D pNode)
        {
            var lTween = CreateTween();
            for (int i = 0; i < 2; i++)
            {
                lTween.TweenProperty(pNode, TweenProp.MODULATE, new Color(0, 1, 0, 1f), 0.2f);
                lTween.TweenProperty(pNode, TweenProp.MODULATE, new Color(1, 1, 1, 1), 0.1f);
            }
            lTween.SetEase(Tween.EaseType.OutIn);
        }
        public void Bomb(Node2D pNode)
        {
            var lTween = CreateTween();
            for (int i = 0; i < 2; i++)
            {
                lTween.TweenProperty(pNode, TweenProp.MODULATE, new Color(0.5f, 0, 0, 1f), 0.2f);
                lTween.TweenProperty(pNode, TweenProp.MODULATE, new Color(1, 1, 1, 1), 0.1f);
            }
            lTween.SetEase(Tween.EaseType.OutIn);
        }

        public void EnemyTouched(Grabable pEnemy)
		{
            var lTween = CreateTween();
            lTween.TweenProperty(pEnemy, TweenProp.MODULATE, Colors.DarkGray, 0.1f);
            lTween.TweenProperty(pEnemy, TweenProp.MODULATE, new Color(1, 1, 1, 1), 0.1f);
            lTween.SetEase(Tween.EaseType.OutIn);
        }


		public void Text(Sprite2D pSprite)
		{
			var lTween = CreateTween().SetLoops();
            lTween.TweenProperty(pSprite, TweenProp.MODULATE, new Color(0.2f,0.2f,0.2f,1), 0.2f);
            lTween.TweenProperty(pSprite, TweenProp.MODULATE, new Color(1, 1, 1, 1), 0.2f);
        }
        public void Collectables(Area2D pArea)
        {
            var lTween = CreateTween().SetLoops();
            var lTween2 = CreateTween().SetLoops();
            lTween.TweenProperty(pArea, TweenProp.MODULATE, new Color(0.2f, 0.2f, 0.2f, 1), 0.2f);
            lTween2.TweenProperty(pArea, TweenProp.SCALE, new Vector2(1.5f, 1.5f), 0.5f);
            lTween.TweenProperty(pArea, TweenProp.MODULATE, new Color(1, 1, 1, 1), 0.2f);
            lTween2.TweenProperty(pArea, TweenProp.SCALE, new Vector2(1f, 1f), 0.5f);
            lTween.SetEase(Tween.EaseType.In);
            lTween.SetTrans(Tween.TransitionType.Expo);
        }
		public void MisileSpawn(Area2D pArea)
		{
            var lTween = CreateTween();
			var lTween2 = CreateTween();
            lTween.TweenProperty(pArea, TweenProp.MODULATE, new Color(0.2f, 0.2f, 0.2f, 1), 0.2f);
            lTween2.TweenProperty(pArea, TweenProp.SCALE, new Vector2(2f, 2f), 0.5f);
            lTween.TweenProperty(pArea, TweenProp.MODULATE, new Color(1, 1, 1, 1), 0.2f);
            lTween2.TweenProperty(pArea, TweenProp.SCALE, new Vector2(1f, 1f), 0.5f);
            lTween.SetEase(Tween.EaseType.In);
            lTween.SetTrans(Tween.TransitionType.Expo);
        }

		public void Boss(Boss pBoss)
		{
            var lTween = CreateTween().SetLoops();
            var lTween2 = CreateTween();
            lTween.TweenProperty(pBoss, TweenProp.MODULATE, new Color(0, 0, 0, 1), 1f);
            lTween.TweenProperty(pBoss, TweenProp.MODULATE, new Color(1, 1, 1, 1), 1f);
            lTween2.TweenProperty(pBoss, TweenProp.SCALE, new Vector2(2f, 2f), 0.1f);
            lTween2.TweenProperty(pBoss, TweenProp.SCALE, new Vector2(1f, 1f), 0.1f);
            lTween2.TweenProperty(pBoss, TweenProp.SCALE, new Vector2(0.5f, 0.5f),0.5f);
            lTween2.TweenProperty(pBoss, TweenProp.SCALE, new Vector2(1f, 1f), 2f);
            lTween2.TweenProperty(pBoss, TweenProp.SCALE, new Vector2(0f, 0f),5f);
            lTween.SetEase(Tween.EaseType.In);
            lTween.SetTrans(Tween.TransitionType.Expo);
        }
        public void HealthBoss(Node2D pAnim)
        {
            var lTween = CreateTween();
            var lTween2 = CreateTween();
            lTween.TweenProperty(pAnim, TweenProp.ROTATION,Mathf.Pi / 6, 0.3f);
            lTween.TweenProperty(pAnim, TweenProp.ROTATION, 0, 0.3f);
            lTween2.TweenProperty(pAnim, TweenProp.SCALE, new Vector2(0.3f, 0.3f), 0.3f);
            lTween2.TweenProperty(pAnim, TweenProp.SCALE, new Vector2(0.25f, 0.25f), 0.3f);
            lTween.SetEase(Tween.EaseType.InOut);
            lTween.SetTrans(Tween.TransitionType.Back);
            lTween2.SetEase(Tween.EaseType.InOut);
            lTween2.SetTrans(Tween.TransitionType.Back);
        }
        public void HealthAppear(Node2D pAnim)
        {
            pAnim.Visible = true;
            var lTween = CreateTween();
            lTween.TweenProperty(pAnim, TweenProp.MODULATE, new Color(1, 1, 1, 1), 1);
            lTween.SetEase(Tween.EaseType.InOut);
            lTween.SetTrans(Tween.TransitionType.Bounce);
        }
        public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;

		}

		protected override void Dispose(bool pDisposing)
		{
			instance = null;
			base.Dispose(pDisposing);
		}
	}
}
