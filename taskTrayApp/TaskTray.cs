using System;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace taskTrayApp
{
    public class TaskTray : Form
    {
        private string Title { get; set; }
        private Icon TrayIcon { get; set; }
        private Func<int> Span { get; set; }
        private Action Action { get; set; }

        private Thread thread;

        public TaskTray(string title, Icon icon, Func<int> span, Action action)
        {
            Title = title;
            TrayIcon = icon;
            Span = span;
            Action = action;
            InitComponents();
            Run();
        }

        private void InitComponents()
        {
            ShowInTaskbar = false;

            var menu = new ContextMenuStrip();
            menu.Items.AddRange(new ToolStripMenuItem[]{
                new ToolStripMenuItem("Exit", null, (s,e)=>{ ExitApp(); }, "Exit")
            });

            NotifyIcon trayIcon = new NotifyIcon
            {
                Icon = TrayIcon,
                Visible = true,
                Text = Title,
                ContextMenuStrip = menu
            };
        }

        private void Run()
        {
            thread = new Thread(() => { interval(Action, Span); });
            thread.Start();
        }

        private void interval(Action action, Func<int> span)
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
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void ExitApp()
        {
            thread.Abort();
            Application.Exit();
        }

        public static class TimeSpan
        {
            public static int Hour()
            {
                return DateTime.Now.Hour;
            }

            public static int Minuts()
            {
                return DateTime.Now.Minute;
            }

            public static int Second()
            {
                return DateTime.Now.Second;
            }
        }
    }
}
