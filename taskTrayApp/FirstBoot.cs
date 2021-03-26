using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace taskTrayApp
{
    /// <summary>
    /// 初回起動時の動作に関連するクラス
    /// </summary>
    public static class FirstBoot
    {
        /// <summary>
        /// 初回起動かどうか判定
        /// 設定ファイルの有無で判断
        /// </summary>
        /// <returns>初回ならtrue、そうでなければfalse</returns>
        public static bool isFirstBoot()
        {
            return ExistSettingFile() == false || ExistSettingFolder() == false;
        }

        /// <summary>
        /// 初回起動時に実行することで設定フォルダ・ファイルを作成
        /// </summary>
        public static void Boot()
        {
            if (ExistSettingFolder() == false)
            {
                GenerateDefaultSettingFolder();
            }
            if (ExistSettingFile() == false)
            {
                GenerateDefaultSettingFile();
            }
        }
        /// <summary>
        /// 設定ファイルの有無
        /// </summary>
        /// <returns>あればtrue、なければfalse</returns>
        private static bool ExistSettingFile(string path = SettingData.defaultSettingFilePath)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// 設定フォルダの有無
        /// </summary>
        /// <returns>あればtrue、なければfalse</returns>
        private static bool ExistSettingFolder(string path = SettingData.defaultSettingFolderPath)
        {
            return Directory.Exists(path);
        }

        /// <summary>
        /// デフォルトの設定データをjsonファイルとして出力
        /// </summary>
        /// <param name="path">jsonファイルの保存先のパスとファイル名</param>
        public static void GenerateDefaultSettingFile(string path = SettingData.defaultSettingFilePath)
        {
            var data = DefaultSettingData();
            var json = data.Serialize();
            using (StreamWriter writer = new StreamWriter(path, true, Encoding.UTF8, 1024))
            {
                writer.Write(json);
            }
        }

        /// <summary>
        /// デフォルトの設定フォルダを作成
        /// </summary>
        public static void GenerateDefaultSettingFolder(string path = SettingData.defaultSettingFolderPath)
        {
            Directory.CreateDirectory(path);
        }

        /// <summary>
        /// デフォルトの設定データ
        /// </summary>
        /// <returns>デフォルトの設定データ</returns>
        private static SettingData DefaultSettingData()
        {
            var data = new SettingData() { 
                SoundPaths = new List<string>
                {
                    "午前0時.mp3",
                    "午前1時.mp3",
                    "午前2時.mp3",
                    "午前3時.mp3",
                    "午前4時.mp3",
                    "午前5時.mp3",
                    "午前6時.mp3",
                    "午前7時.mp3",
                    "午前8時.mp3",
                    "午前9時.mp3",
                    "午前10時.mp3",
                    "午前11時.mp3",
                    "午後0時.mp3",
                    "午後1時.mp3",
                    "午後2時.mp3",
                    "午後3時.mp3",
                    "午後4時.mp3",
                    "午後5時.mp3",
                    "午後6時.mp3",
                    "午後7時.mp3",
                    "午後8時.mp3",
                    "午後9時.mp3",
                    "午後10時.mp3",
                    "午後11時.mp3",
                }
            };
            return data;
        }
    }
}
