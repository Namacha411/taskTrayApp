using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            return ExistSettingFile() == false;
        }

        /// <summary>
        /// 設定ファイルの有無
        /// </summary>
        /// <returns>あればtrue、なければfalse</returns>
        public static bool ExistSettingFile(string path = SettingData.defaultSettingFilePath)
        {
            return File.Exists(path);
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
        /// デフォルトの設定データ
        /// </summary>
        /// <returns>デフォルトの設定データ</returns>
        private static SettingData DefaultSettingData()
        {
            var data = new SettingData() { 
                AppName = "appname",
                FolderPath = @"C:\SoundPath\",
                IconPath = @"C:\IconPath\"
            };
            return data;
        }
    }
}
