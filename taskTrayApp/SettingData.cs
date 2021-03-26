using System.Text;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace taskTrayApp
{
    /// <summary>
    /// アプリの設定に関係するクラス
    /// </summary>
    class SettingData
    {
        // デフォルトの設定ファイルのパスと名前
        public const string defaultSettingFolderPath = @".\config";
        public const string defaultSettingFilePath = @".\config\setting.json";

        public List<string> SoundPaths { get; set; }

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
            string json = JsonSerializer.Serialize(
                value: this,
                options: new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                    AllowTrailingCommas = true,
                    WriteIndented = true
                }
                );
            return json;
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
