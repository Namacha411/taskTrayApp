using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using System.Threading;

namespace taskTrayApp
{
    /// <summary>
    /// アプリの設定に関係するクラス
    /// </summary>
    class SettingData
    {
        // デフォルトの設定ファイルのパスと名前
        public const string defaultSettingFilePath = @".\setting.json";

        public string AppName { get; set; }
        public string FolderPath { get; set; }
        public string IconPath { get; set; }

        /// <summary>
        /// jsonデシリアライズ用デフォルトコンストラクタ
        /// 消すとデシリアライズできない
        /// </summary>
        public SettingData() { }

        /// <summary>
        /// 自身をシリアライズ
        /// </summary>
        /// <returns>シリアライズ後のjson string</returns>
        public string Serialize()
        {
            return JsonSerializer.Serialize(
                value: this,
                options: new JsonSerializerOptions
                {
                    AllowTrailingCommas = true,
                    WriteIndented = true,
                }
                );
        }

        /// <summary>
        /// jsonファイルをデシリアライズ
        /// </summary>
        /// <param name="json">デシリアライズするデータ、string</param>
        /// <returns>デシリアライズ後の設定データ</returns>
        public SettingData Deserialize(string json)
        {
            return JsonSerializer.Deserialize<SettingData>(json);
        }

        /// <summary>
        /// 設定ファイルの読み込み
        /// </summary>
        /// <param name="path">jsonファイルの保存先のパスとファイル名</param>
        /// <returns>ファイル内のjsonデータ</returns>
        public string FileRead(string path = defaultSettingFilePath)
        {
            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
