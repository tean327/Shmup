using Godot;
using System;
using System.Collections.Generic;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class StartingButton : UIUX
	{
        [Export] private PackedScene settingsScn;
        const string CREDIT_PATH = "res://Scenes/crédits.tscn";
        const string HELP_PATH = "res://Scenes/help.tscn";
        VBoxContainer BoxButton;
		Button startButton;
		Button settingsButton;
		Button exitButton;
		Sprite2D Controller;
        Button Credits;
        Button help;
		bool isController = true;
        List<Button> buttons;

        public override void _Ready()
		{
            BoxButton = GetChild(0) as VBoxContainer;
			startButton = BoxButton.GetChild(0) as Button;
            settingsButton = BoxButton.GetChild(1) as Button;
			exitButton = BoxButton.GetChild(2) as Button;
            Credits = GetChild(7) as Button;
            help = GetChild(8) as Button;
            startButton.GrabFocus();

            startButton.Pressed += Start;
            settingsButton.Pressed += SettingsPressed;
            exitButton.Pressed += ExitGame;
            Credits.Pressed += CreditsPressed;
            help.Pressed += Help_Pressed;


            Controller = GetChild(2) as Sprite2D;

            TranslationServer.SetLocale("fr");

            buttons = new List<Button>() { startButton, settingsButton, exitButton };
            SoundManager.GetInstance().UiMusic.Play();
        }

        private void Help_Pressed()
        {
            GetTree().ChangeSceneToFile(HELP_PATH);
        }

        private void CreditsPressed()
        {
            GetTree().ChangeSceneToFile(CREDIT_PATH);
        }

        private void SettingsPressed()
        {
            SoundManager.GetInstance().UiClick.Play();
            BoxButton.Visible = false;
            AddChild(settingsScn.Instantiate() as AudioSettings);
        }

        private void ExitGame()
        {
            SoundManager.GetInstance().UiClick.Play();
            GetTree().Quit();
        }


        private void Start()
        {
            SoundManager.GetInstance().UiClick.Play();
            SoundManager.GetInstance().UiStart.Play();
            SoundManager.GetInstance().UiMusic.Stop();
            GetTree().ChangeSceneToFile("res://Scenes/main.tscn");
        }
        public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
			if(Input.IsAnythingPressed() && isController)
			{
                startButton.GrabFocus();
				Controller.Visible = false;
				BoxButton.Visible = true;
				Controller.QueueFree();
				isController = false;
			}
            foreach (Button lButton in buttons)
            {
                Sprite2D lArrow = lButton.GetChild(0) as Sprite2D;
                if (lButton.HasFocus())
                {
                    lArrow.Visible = true;
                }
                else lArrow.Visible = false;
            }
        }

		protected override void Dispose(bool pDisposing)
		{

		}
	}
}
