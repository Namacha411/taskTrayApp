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
    class SettingFile
    {
        public string AppName { get; set; }
        public string FolderPath { get; set; }

        public SettingFile() { }

        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public SettingFile Deserialize(string json)
        {
            return JsonSerializer.Deserialize<SettingFile>(json);
        }

        private void FileWrite(string path = @".\setting.json")
        {
            string json = new SettingFile() { AppName = "appname", FolderPath = @"C:\examplePath\" }.Serialize();
            using (StreamWriter writer = new StreamWriter(path, true, Encoding.UTF8, 1024))
            {
                writer.Write(json);
            }
        }

        public string FileRead(string path = @".\setting.json")
        {
            string res;
            if (!File.Exists(path)) {
                FileWrite();
                Application.Exit();
                return "";
            }
            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                res = reader.ReadToEnd();
            }
            return res;
        }

        public SettingFile FileReadAndDeserialize()
        {
            var json = FileRead();
            return Deserialize(json);
        }
    }
}
