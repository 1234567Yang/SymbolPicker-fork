using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace SymbolPicker
{
    class DebugTextWriter : TextWriter
    {
        public override Encoding Encoding => Encoding.UTF8;

        public override void Write(char value) => Debug.Write(value);
        public override void Write(string value) => Debug.Write(value);
        public override void WriteLine(string value) => Debug.WriteLine(value);
    }



    internal static class Program
    {
        public static Settings SettingPage;
        public static Form1 MainForm;

        public static Mutex mutex = new Mutex(false, "Global\\_SymbolPicker_");
        public const int MUTEXMESSAGE = 0x8000 + 100; //WM_APP 0x8000 ~ 0xBFFF


        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Console.SetOut(new DebugTextWriter());

            Console.WriteLine("Start");

            Thread.Sleep(100); //maybe restart
            if (!mutex.WaitOne(500, false)) //500ms£¬maybe settings restart
            {
                SendMutexMsgToWindow();
                Environment.Exit(0);
            }




            //if (Environment.OSVersion.Version.Major >= 6)
            //    SetProcessDPIAware();


            ApplicationConfiguration.Initialize();
            MainForm = new Form1();
            Application.Run(MainForm);
        }

        //#region dpi stuff
        //[DllImport("user32.dll")]
        //private static extern bool SetProcessDPIAware();
        //#endregion

        #region mutex send message
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private static void SendMutexMsgToWindow()
        {
            IntPtr targetWindowHandle = FindWindow(null, "Symbol Picker");
            if (targetWindowHandle != IntPtr.Zero)
            {
                SendMessage(targetWindowHandle, MUTEXMESSAGE, IntPtr.Zero, IntPtr.Zero);
            }
            else
            {
                MessageBox.Show("The app is already running, click the icon in the tray to see.");
            }
        }
        #endregion
    }
}
