using System;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.Diagnostics;

namespace taskTrayApp
{
    /// <summary>
    /// タスクトレイで起動するために必要な設定等を行い起動する
    /// </summary>
    public class TaskTray : Form
    {
        private string Title;
        private Icon TrayIcon;
        private Func<int> Span;
        private Action Action;
        private int IntervalTime;

        private Thread thread;

        /// <summary>
        /// コンストラクタ
        /// タスクトレイアプリの各種設定を行う
        /// </summary>
        /// <param name="title">アプリのタイトルを設定</param>
        /// <param name="icon">アプリアイコンを設定</param>
        /// <param name="span">
        /// タイムスパンを設定
        /// 子クラスのTimeSpan内の関数からタイムスパンを選ぶ
        /// </param>
        /// <param name="action">一定タイムスパンごとに呼び出される関数</param>
        public TaskTray(string title, Icon icon, Func<int> span, Action action, int intervalTime = 100)
        {
            Title = title;
            TrayIcon = icon;
            Span = span;
            Action = action;
            IntervalTime = intervalTime;
            InitComponents();
        }

        /// <summary>
        /// 各種設定を初期化、設定する
        /// </summary>
        private void InitComponents()
        {
            // タスクバーに表示
            ShowInTaskbar = false;

            // アイコンを右クリックしたときのメニュー
            var menu = new ContextMenuStrip();
            menu.Items.AddRange(new ToolStripMenuItem[]{
                new ToolStripMenuItem(
                    text: "Exit",
                    image: null,
                    onClick: (s, e) => { ExitApp(); },
                    name: "Exit"
                    ),
                new ToolStripMenuItem(
                    text: "Open sounds folder",
                    image: null,
                    onClick: (s, e) => { StartSettingFolderWindow(); },
                    name: "OpenSoundsFolder"
                    ),
                new ToolStripMenuItem(
                    text: "Version",
                    image: null,
                    onClick: (s, e) =>
                    {
                        MessageBox.Show(
                            text: "1.1",
                            caption: "Version"
                            );
                    },
                    name: "Version"
                    )
            });

            // アイコン関係
            NotifyIcon trayIcon = new NotifyIcon
            {
                Icon = TrayIcon,
                Visible = true,
                Text = Title,
                ContextMenuStrip = menu
            };
        }

        /// <summary>
        /// アプリを起動
        /// </summary>
        public void Run()
        {
            thread = new Thread(() => { interval(Span, Action, IntervalTime); });
            thread.Start();
        }

        /// <summary>
        /// 実行間隔を管理
        /// </summary>
        /// <param name="span">子クラスTimeSpanの関数を渡す</param>
        /// <param name="action">一定間隔ごとに実行する関数</param>
        private void interval(Func<int> span, Action action, int intervalTime)
        {
            int prev = span();
            while (true)
            {
                int now = span();
                if (prev != now)
                {
                    action();
                }
                prev = span();
                Thread.Sleep(intervalTime);
            }
        }

        /// <summary>
        /// アプリを強制終了
        /// </summary>
        private void ExitApp()
        {
            thread.Abort();
            Application.Exit();
        }

        /// <summary>
        /// 設定フォルダのウィンドウを起動
        /// </summary>
        public static void StartSettingFolderWindow()
        {
            Process.Start(SettingData.defaultSettingFolderPath);
        }

        /// <summary>
        /// action実行のタイムスパンを管理する
        /// </summary>
        public static class TimeSpan
        {
            /// <summary>
            /// 毎時00分にactionを実行
            /// </summary>
            public static int Hour()
            {
                return DateTime.Now.Hour;
            }

            /// <summary>
            /// 毎分00秒にactionを実行
            /// </summary>
            public static int Minuts()
            {
                return DateTime.Now.Minute;
            }

            /// <summary>
            /// 毎秒actionを実行
            /// milisecは保証しない
            /// </summary>
            public static int Second()
            {
                return DateTime.Now.Second;
            }
        }
    }
}
