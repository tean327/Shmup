using Godot;

namespace Com.IsartDigital.Utils.Effects
{
    public partial class Trail : Line2D
    {
    

        [Export(PropertyHint.Range, "0,100")] private int smoothness = 100;
        private float deltaSmoothness = 0f;

        [Export(PropertyHint.Range, "0.1,10")] private float vanish = 0.5f;
        private float deltaVanish = 0f;

        [Export] private bool alpha = false;
        [Export(PropertyHint.Range, "0.1,10")] private float alphaVanish = 0.5f;
        private float refAlpha;

        [Export] private int length = 15;

        [Export] public Node2D target;

        [Export] private bool reparent;
        [Export] private bool start;

        private Vector2 lastPosition;

        public override void _Ready()
        {
            ClearPoints();
            Position = Vector2.Zero;
            SetProcess(false);
            refAlpha = Modulate.A;
            target.TreeExiting += QueueFree;
            if (reparent) CallDeferred(nameof(ChangeDepth));
            if (start) Start();
        }

        public override void _Process(double delta)
        {
            float lFrequency = vanish / length;
            Vector2 lPosition = ToLocal(target.GlobalPosition);

            if (Points.Length > length) RemovePoint(Points.Length - 1);
            if (lastPosition == lPosition)
            {
                deltaVanish += (float)delta;
                if (alpha)
                {
                    Color lColor = Modulate;
                    lColor.A -= Mathf.Max(0, refAlpha * alphaVanish / 60);
                    Modulate = lColor;
                }
            }
            else if (alpha)
            {
                Color lColor = Modulate;
                lColor.A = refAlpha;
                Modulate = lColor;
            }

            if (deltaVanish > lFrequency)
            {
                int lLength = (int)(deltaVanish / lFrequency);
                for (int i = 0; i < lLength; i++)
                {
                    if (Points.Length == 0) break;
                    RemovePoint(Points.Length - 1);
                }
                deltaVanish -= lLength * lFrequency;
            }

            deltaSmoothness += (float)delta;
            if (deltaSmoothness > (100 - smoothness) / 500f)
            {
                deltaSmoothness = 0f;
                if (lPosition != lastPosition)
                {
                    AddPoint(lPosition, 0);
                    lastPosition = lPosition;
                }
            }
            else SetPointPosition(0, lPosition);

            if (reparent) ChangeDepth();
        }

        public void ChangeDepth()
        {
            Reparent(target.GetParent());
        }
        public void Start()
        {
            ClearPoints();
            SetProcess(true);
        }
        public void Pause()
        {
            SetProcess(false);
        }
        public void Resume()
        {
            SetProcess(true);
        }
        public void Stop()
        {
            Pause();
            ClearPoints();
        }

        public bool IsPlaying()
        {
            return IsProcessing();
        }

    }

}