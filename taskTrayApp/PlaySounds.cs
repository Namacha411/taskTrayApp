using System;

using WMPLib;

namespace taskTrayApp
{
    public class PlaySounds
    {
        private string FolderPath { get; set; }

        public PlaySounds(string folderPath) { FolderPath = folderPath; }

        public void PlayTimeSignal()
        {
            var hour = DateTime.Now.Hour;
            PlayMp3(HourToFileName(hour));
        }

        private void PlayMp3(string filename)
        {
            WindowsMediaPlayer mediaPlayer = new WindowsMediaPlayer();
            var fullPath = $"{FolderPath}{filename}.mp3";
            mediaPlayer.URL = fullPath;
            mediaPlayer.controls.play();
        }

        private string HourToFileName(int hour)
        {
            if (hour >= 12)
            {
                return $"午後{hour % 12}時";
            }
            return $"午前{hour}時";
        }
    }
}
