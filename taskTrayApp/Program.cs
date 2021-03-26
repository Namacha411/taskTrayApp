using System;
using System.Threading;
using System.Windows.Forms;

using taskTrayApp;

/// <summary>
/// メインクラス
/// </summary>
public class MainClass
{
    private const string AppTitle = "Time Signal";

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
                MessageBox.Show(
                    text: "設定ファイルを作成し、開きます",
                    caption: "初回起動時メッセージ"
                );
                FirstBoot.Boot();
                Environment.Exit(0);
            }
            // 通常起動時
            var settingData = new SettingData();
            var setting = settingData.Deserialize(settingData.FileRead());
            var timeSignal = new TimesignalPlayer(setting.SoundPaths);
            MessageBox.Show(
                text: "設定ファイルを読み込みました\n起動します",
                caption: "通常起動時メッセージ"
            );

            // main
            var taskTray = new TaskTray(
                title: AppTitle,
                icon: taskTrayApp.Properties.Resources.Icon,
                span: TaskTray.TimeSpan.Hour,
                action: () =>
                {
                    timeSignal.PlayTimeSignal();
                },
                intervalTime: 1000
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
