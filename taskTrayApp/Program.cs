using System;
using System.Threading;
using System.Windows.Forms;

using static taskTrayApp.MyIconUtil;
using taskTrayApp;

class MainClass
{
    private static string AppTitle { get; set; }
    private static string SoundsFolderPath { get; set; }

    [STAThread]
    static void Main(string[] args)
    {
        // mutexによりアクセス競合を防ぐ
        const string mutexName = "TaskTrayTimeSignal";
        var mutex = new Mutex(false, mutexName);

        bool hasHandle = false;
        try
        {
            try { hasHandle = mutex.WaitOne(0, false); }
            catch (AbandonedMutexException) { hasHandle = true; }
            if (hasHandle == false) { return; }

            // 起動時動作
            if (FirstBoot.isFirstBoot())
            {
                // 初回起動時
                FirstBoot.GenerateDefaultSettingFile();
                MessageBox.Show(
                    text: "設定ファイルを作成しました\n終了します",
                    caption: "初回起動時メッセージ"
                    );
                Environment.Exit(0);
            }
            // 通常起動時の設定読み込み
            var settingData = new SettingData();
            var setting = settingData.Deserialize(settingData.FileRead());
            AppTitle = setting.AppName;
            SoundsFolderPath = setting.FolderPath;

            // main
            var soundsFolder = new PlaySounds(SoundsFolderPath);
            new TaskTray(
                title: AppTitle,
                icon: Create16x16Icon(iconDot),
                span: TaskTray.TimeSpan.Hour,
                action: () =>
                {
                    soundsFolder.PlayTimeSignal();
                }
            );
            Application.Run();
        }
        finally
        {
            if (hasHandle) { mutex.ReleaseMutex(); }
            mutex.Close();
        }
    }
}
