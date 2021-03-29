using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Dont_poweroff
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        [DllImport("user32.dll")]
        static extern void SetCursorPos(int x, int y);

        static public void DontPowerOff()
        {
            SetCursorPos(Cursor.Position.X + 1, Cursor.Position.Y + 1); // change pos
            SetCursorPos(Cursor.Position.X - 1, Cursor.Position.Y - 1); // go back
        }

        static void Main(string[] args)
        {
            // hide app
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);

            int delay;
            if (args.Length == 0 || int.Parse(args[0]) > 16 || int.Parse(args[0]) < 1)
            {
                delay = 7 * 1000 * 60;
            }
            else
            {
                delay = int.Parse(args[0]) * 1000 * 60;
            }

            DontPowerOff();

            Task.Delay(delay).GetAwaiter().GetResult();

            while (true)
            {
                DontPowerOff();

                Task.Delay(delay).GetAwaiter().GetResult(); // delay
            }
        }
    }
}
