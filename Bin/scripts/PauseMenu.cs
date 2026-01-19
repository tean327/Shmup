using Godot;
using System;
using System.Collections.Generic;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class PauseMenu : UIUX
	{
        [Export] private PackedScene settingsScn;
        AudioSettings settings;

       [Export] VBoxContainer BoxButton;
        [Export] Button ContinueButton;
        [Export] Button ControlsButton;
        [Export] Button RestartButton;
        [Export] Button SettingsButton;
        [Export] Button ReturnButton;
        [Export] Sprite2D Manet;
        [Export] Panel panel;
        [Export] Button Cross;

        List<Button> buttons;

        const string MENU_PATH = "res://Scenes/StartUi.tscn";

        bool IsControlOpen = false;


        public override void _Ready()
		{
            GlobalPosition = GetWindow().Size / 2;
            buttons = new List<Button>() { ContinueButton, ControlsButton, RestartButton, SettingsButton, ReturnButton};
            ContinueButton.GrabFocus();
            SoundManager.GetInstance().UiMusic.Play();
            SoundManager.GetInstance().UiPause.Play();
            ContinueButton.Pressed += ContinueGame;
            Cross.Pressed += ContinueGame;
            RestartButton.Pressed += RestartGame;
            SettingsButton.Pressed += Settings_Pressed;
            ControlsButton.Pressed += Controls;
            ReturnButton.Pressed += GoToMenu;
        }

        private void GoToMenu()
        {
            Main.GameContainer.GetTree().Paused = false;
            GetTree().ChangeSceneToFile(MENU_PATH);
        }

        private void Settings_Pressed()
        {
            SoundManager.GetInstance().UiClick.Play();
            settings = settingsScn.Instantiate() as AudioSettings;
            AddChild(settings);
            BoxButton.Visible = false;
            settings.Visible = true;
        }

        private void Controls()
        {
            SoundManager.GetInstance().UiClick.Play();
            BoxButton.Visible = false;
            Manet.Visible = true;
            IsControlOpen = true;
        }

        private void RestartGame()
        {
            SoundManager.GetInstance().UiMusic.Stop();
            SoundManager.GetInstance().UiClick.Play();
            healthBar.HealthBarState = 0;
            Main.score = 0;
            GetTree().Paused = false;
            GetTree().ReloadCurrentScene();
            QueueFree();
        }

        private void ContinueGame()
        {
            SoundManager.GetInstance().UiMusic.Stop();
            SoundManager.GetInstance().UiClick.Play();
            GetTree().Paused = false;
            QueueFree();
        }

        public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
            if (Input.IsAnythingPressed() && IsControlOpen)
            {
                BoxButton.Visible = true;
                ContinueButton.GrabFocus();
                Manet.Visible = false;
                IsControlOpen = false;
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
