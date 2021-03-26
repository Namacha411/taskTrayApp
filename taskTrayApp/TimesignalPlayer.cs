using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools;

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
            if (!canPlay(SoundPaths[hour])) { return; }
            mediaPlayer.URL = SoundPaths[hour];
            mediaPlayer.controls.play();
        }

        /// <summary>
        /// 音が再生できるかどうか
        /// </summary>
        /// <param name="soundPath">音ファイルのパス</param>
        /// <returns>再生できればtrue、できなければfalse</returns>
        private bool canPlay(string soundPath)
        {
            return File.Exists(soundPath);
        }
    }
}
