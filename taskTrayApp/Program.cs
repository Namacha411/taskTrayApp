﻿using System;
using System.Threading;
using System.Windows.Forms;

using static taskTrayApp.MyIconUtil;
using taskTrayApp;

class MainClass
{
    private static string AppTitle { get; set; }
    private static string SoundsFolderPath { get; set; }

    private readonly static string mutexName = "TaskTrayTimeSignal";

    [STAThread]
    static void Main(string[] args)
    {
        var mutex = new Mutex(false, mutexName);

        bool hasHandle = false;
        try
        {
            try { hasHandle = mutex.WaitOne(0, false); }
            catch (AbandonedMutexException) { hasHandle = true; }
            if (hasHandle == false) { return; }

            var setting = new SettingFile().FileReadAndDeserialize();
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