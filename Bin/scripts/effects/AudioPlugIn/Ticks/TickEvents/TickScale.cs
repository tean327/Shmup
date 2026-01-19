using Godot;
using System;

// Author : Julien Fournier

namespace Com.IsartDigital.ProjectName
{
	public partial class TickScale : TickEvent
    {
        private Vector2 _startScale;
        private Vector2 _targetScale;
        private float _lerpTimer = 0f;
        private const float LerpDuration = 0.2f; // Durée de l'interpolation en secondes
        private bool _isLerping = false;

        public override void _Ready()
        {
            base._Ready();
           //_startScale = square.Scale; // Initialisation de l'échelle de départ
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

                // Interpoler l'échelle de square
                //square.Scale = _startScale.Lerp(_targetScale, lerpFactor);
            }
        }

        protected override void OnBeat()
        {
            base.OnBeat();

            // Définir l'échelle de départ actuelle et la nouvelle échelle cible
            //_startScale = square.Scale;
            float randomScale = rand.RandfRange(0.3f, 2f);
            _targetScale = new Vector2(randomScale, randomScale);

            // Réinitialiser le timer de lerp et démarrer l'interpolation
            _lerpTimer = 0f;
            _isLerping = true;
        }
    }
}
