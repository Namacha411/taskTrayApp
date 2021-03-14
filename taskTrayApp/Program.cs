using System;
using System.Threading;
using System.Windows.Forms;

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
            // 通常起動時
            var settingData = new SettingData();
            var setting = settingData.Deserialize(settingData.FileRead());
            AppTitle = setting.AppName;
            SoundsFolderPath = setting.FolderPath;
            MessageBox.Show(
                text: "設定ファイルを読み込みました\n起動します",
                caption: "通常起動時メッセージ"
            );

            // main
            var soundsFolder = new PlaySounds(SoundsFolderPath);
            var taskTray = new TaskTray(
                title: AppTitle,
                icon: taskTrayApp.Properties.Resources.Icon,
                span: TaskTray.TimeSpan.Hour,
                action: () =>
                {
                    soundsFolder.PlayTimeSignal();
                }
            );
            taskTray.Run();
            Application.Run();
        }
        finally
        {
            if (hasHandle) { mutex.ReleaseMutex(); }
            mutex.Close();
        }
    }
}
