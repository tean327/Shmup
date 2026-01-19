using Godot;
using System;

// Author : Julien Fournier

namespace Com.IsartDigital.ProjectName
{
	public partial class TickPatternBossA : TickEvent
	{
        Timer grow = new Timer();
        int growTimes = 4;
        int currentGrow = 0;

        public override void _Ready()
        {
            base._Ready();
            grow.WaitTime = 0.3f;
            grow.Autostart = false;
            grow.OneShot = false;

            grow.Timeout += OnGrow;
            //square.AddChild(grow);
        }

        protected override void OnBeat()
        {
            base.OnBeat();
            GD.Print("ENTER BOSS PATTERN");
            grow.Start();
            
        }

        void OnGrow()
        {
            GD.Print("GROW");
            if(currentGrow < growTimes)
            {
                GD.Print("SCALE");

                //square.Scale += new Vector2(2,2);
                currentGrow++;
            }
            else 
            { 
                GD.Print("FREE");
                QueueFree();
                //square.QueueFree();
                
            }
        }
    }
}
