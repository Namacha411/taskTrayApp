using System;
using System.Collections.Generic;

using WMPLib;

namespace taskTrayApp
{
    public class TimesignalPlayer
    {
        private WindowsMediaPlayer mediaPlayer;
        public List<string> SoundPaths;

        /// <summary>
        /// コンストラクタ
        /// 音を読み込み、プレーヤーを設定する
        /// </summary>
        /// <param name="soundPaths">再生する音のパス</param>
        public TimesignalPlayer(List<string> soundPaths)
        {
            mediaPlayer = new WindowsMediaPlayer();
            SoundPaths = new List<string>(soundPaths);
        }

        /// <summary>
        /// 時報の再生
        /// </summary>
        public void PlayTimeSignal()
        {
            var hour = DateTime.Now.Hour;
            mediaPlayer.URL = SoundPaths[hour];
            mediaPlayer.controls.play();
        }
    }
}
