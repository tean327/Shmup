using Godot;
using System;

// Author : Etan Grimault Martinez

namespace Com.IsartDigital.ProjectName {
	
	public partial class AudioSettings : UIUX
	{
		[Export] string Audio_Bus_Name_Music; 
		[Export] string Audio_Bus_Name_Sfx; 
		[Export] HSlider musicSlider;
		[Export] HSlider sfxSlider;
		[Export] Button returnButton;
		[Export] Button englishButton;
		[Export] Button frenchButton;
		int audio_bus_id_Music;
		int audio_bus_id_Sfx;
		public override void _Ready()
		{
            Visible = true;
            musicSlider.GrabFocus();
            returnButton.Pressed += OnButton_Pressed;
            audio_bus_id_Music = AudioServer.GetBusIndex(Audio_Bus_Name_Music);
            audio_bus_id_Sfx = AudioServer.GetBusIndex(Audio_Bus_Name_Sfx);
            frenchButton.Pressed += FrenchButton_Pressed;
            englishButton.Pressed += EnglishButton_Pressed;
        }

        private void EnglishButton_Pressed()
        {
            SoundManager.GetInstance().UiClick.Play();
            TranslationServer.SetLocale("en");
        }

        private void FrenchButton_Pressed()
        {
            SoundManager.GetInstance().UiClick.Play();
            TranslationServer.SetLocale("fr");
        }

        private void OnButton_Pressed()
        {
            SoundManager.GetInstance().UiStart.Play();
            QueueFree();
            GetParent<Control>().GetChild<VBoxContainer>(0).Visible = true;
            GetParent<Control>().GetChild<VBoxContainer>(0).GetChild<Button>(0).GrabFocus();
        }


        public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
            AudioServer.SetBusVolumeDb(audio_bus_id_Music, (float)musicSlider.Value);
            AudioServer.SetBusVolumeDb(audio_bus_id_Sfx, (float)sfxSlider.Value);
        }

		protected override void Dispose(bool pDisposing)
		{

		}
	}
}
