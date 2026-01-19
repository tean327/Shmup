using Godot;
using System;
using static Godot.OpenXRInterface;

// Author : Julien Fournier

namespace Com.IsartDigital.ProjectName
{
    public partial class TickMove : TickEvent
    {
        Vector2 WindowSize;
        private Vector2 _startPosition;
        private Vector2 _targetPosition;
        Vector2 bossSize;
        private float _lerpTimer = 0f;
        [Export] private Boss boss;
        [Export] private float LerpDuration = 0.5f; // Durée de l'interpolation en secondes
        private bool _isLerping = false;
        int count = 1;
        public override void _Ready()
        {
            base._Ready();
            if(GetWindow().Size > DisplayServer.ScreenGetSize())  WindowSize = DisplayServer.ScreenGetSize();
            else WindowSize = GetWindow().Size;
                GD.Print(WindowSize);
            _startPosition = boss.Position;
        }

        public override void _Process(double delta)
        {
            base._Process(delta);
            if (_isLerping)
            {
                // Incrémenter le timer de lerp
                _lerpTimer += (float)delta;
                float lerpFactor = _lerpTimer / LerpDuration;

                // Vérifier si le lerp est terminé
                if (lerpFactor >= 1f)
                {
                    lerpFactor = 1f;
                    _isLerping = false; // Arrêter l'interpolation une fois terminée
                }

                // Interpoler la position de square
                boss.Position = _startPosition.Lerp(_targetPosition, lerpFactor);
            } 
        }


        protected override void OnBeat()
        {
            int lFactor;
            if (count % 2 == 0) lFactor = 1;
            else lFactor = -1;
                base.OnBeat();
            // Définir la position de départ actuelle et la nouvelle position cible
            _startPosition = boss.Position;
            GD.Print(_targetPosition);
            var viewportSize = GetViewport().GetVisibleRect().Size;
            if (boss.state == BossStates.STATE1)
            {
                _targetPosition = boss.Position + new Vector2(0, 100 * lFactor);
            }
            else if (boss.state == BossStates.STATE2)
            {
                float lLerp;
                lLerp = Mathf.Lerp(0, 2 * Mathf.Pi, rand.RandfRange(0, 2 * Mathf.Pi));
                    _targetPosition = boss.Position + new Vector2(Mathf.Cos(lLerp), Mathf.Sin(lLerp)) * 80;
                 if(_targetPosition.Y >= WindowSize.Y - 80 || _targetPosition.Y <= 80) _targetPosition = WindowSize /2;
            }
            else _targetPosition = new Vector2(WindowSize.X - 200, (WindowSize.Y / 2) - 50);
                // Réinitialiser le timer de lerp et démarrer l'interpolation
                _lerpTimer = 0f;
            _isLerping = true;
            count++;
        }
    }
}
