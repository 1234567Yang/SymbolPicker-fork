using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Simulator;



namespace SymbolPicker
{
    public partial class Form1 : Form
    {
        private static List<Symbol> allSymbols = new List<Symbol>();
        private static List<Button> allSymbolButtons = new List<Button>();

        private static List<Symbol> recentSymbols = new List<Symbol>();
        private static List<Button> recentSymbolButtons = new List<Button>();

        private static List<Symbol> favSymbols = new List<Symbol>();
        private static List<Button> favSymbolButtons = new List<Button>();


        private static string allPath = Application.StartupPath + @"\symbols.txt";
        private static string recentPath = Application.StartupPath + @"\recent.txt";
        private static string favPath = Application.StartupPath + @"\fav.txt";

        private static readonly Size BaseButtonSize = new Size(40, 40);
        private static readonly int BaseButtonPadding = 1;


        private static Keys showHideHotKey;
        private const int API_HOTKEY_SHOWHIDEHOTKEYCODE = 1;


        private const int RECENT_SYMBOL_KEEPCOUNT = 15;


        private bool isOperatingFav = false;
        private bool favOperation = true; // true -> user wants to add; false -> user wants to remove

        #region test
        private void TestInit()
        {
            //Console.WriteLine(11);
            //Console.WriteLine(11);
            //Console.WriteLine(11);
            //Console.WriteLine(11);
            //Console.WriteLine(11);
            //MessageBox.Show("1111");
            //Trace.WriteLine("WTF WHY IT WILL NOT CONSOLE WRITELINE ONLY IN THIS PROJECT");
            //Console.WriteLine("Oh wait, Im idiot im on release mode");


            //Trace.WriteLine(KeyboardSimulator.SimulateKeyboard(0, KeyEventFlags.UNICODE, 'ÎÒ', true, 0));
            //Trace.WriteLine(KeyboardSimulator.SimulateKeyboard(0, KeyEventFlags.KEYUP));
        }

        #endregion

        #region init

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TestInit();


            Program.SettingPage = new Settings();
            Program.SettingPage.Show(); //only show first can let it init
            Program.SettingPage.Hide();
            Thread.Sleep(500); // let settings load

            //check Mutex here


            LoadAllButtons();
            LoadRecentButtons();
            LoadFavButtons();

            // AlwaysTopMost(); conflict with setting window, abandoned


            if (Program.SettingPage.checkBox_reg_hotkey.Checked)
            {
                if (char.IsLetterOrDigit(Program.SettingPage.textBox_hotkey.Text[0]))
                {
                    showHideHotKey = (Keys)Enum.Parse(typeof(Keys), Program.SettingPage.textBox_hotkey.Text.ToUpper());
                    if (!HotKey.API_RegisterHotKey(this.Handle, API_HOTKEY_SHOWHIDEHOTKEYCODE, HotKey.control.Ctrl, showHideHotKey))
                    {
                        MessageBox.Show("Can not reg hot key!");
                    }
                    else
                    {
                        //Tray notify
                        this.notifyIcon_tray.Text += $" ( Ctrl + {Program.SettingPage.textBox_hotkey.Text[0]})";
                        this.toolStripMenuItem_show.Text += $" ( Ctrl + {Program.SettingPage.textBox_hotkey.Text[0]})";
                    }
                }
                else
                {
                    MessageBox.Show("Can not parse which hot key!");
                }
            }

            this.Opacity = Program.SettingPage.trackBar_intrans.Value / 100.0;
            //MessageBox.Show((Program.SettingPage.trackBar_intrans.Value).ToString());

            FadeWindow(false);

            this.label_hint.Focus(); // for SetNoActivate, because the default focus is the search box, and if it's focused to the search box and now it's not activated, it will cause some user experience problem
            SetNoActivate(this.Handle);
        }

        private Button CreateOneButton(string tag, string txt)
        {
            Button button = new Button();
            button.Size = BaseButtonSize;
            button.Click += Button_Click;
            button.Tag = tag;
            button.Padding = new Padding(
                    0,                  // 左边距
                    BaseButtonPadding,      // 上边距
                    0,                  // 右边距
                    0                   // 下边距
                ); ;

            button.Text = txt;

            return button;
        }
        private void LoadButtons(string path, List<Symbol> lssym, List<Button> lsbtn, FlowLayoutPanel layout, string tag)
        {
            if (!File.Exists(path)) return; //¿ÉÄÜÊÇ ¡¾×î½üÊ¹ÓÃ¡¿ »¹Ã»Ìí¼Ó
            try
            {
                string[] linesFromTextFile = File.ReadAllLines(path);
                for (int i = 0; i < linesFromTextFile.Length; i += 2)
                {
                    string name = linesFromTextFile[i] + linesFromTextFile[i + 1]; // also the symbol itself
                    string img = linesFromTextFile[i + 1]; //gets the second line in the section

                    Symbol symbol = new Symbol(name, img);
                    lssym.Add(symbol);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            for (int i = 0; i < lssym.Count; i++)
            {
                Button button = CreateOneButton(tag, lssym[i].img);
                lsbtn.Add(button);
            }

            AddButtonsToLayout(lsbtn, layout);

        }
        private void AddButtonsToLayout(List<Button> lsbtn, FlowLayoutPanel layout)
        {
            layout.Controls.Clear();
            for (int i = 0; i < lsbtn.Count; i++)
            {
                //ButtonÃ»·¨Clone
                layout.Controls.Add(lsbtn[i]);
            }
        }
        private void LoadAllButtons()
        {
            LoadButtons(allPath, allSymbols, allSymbolButtons, flowLayoutPanel_all, "all");
        }

        private void LoadRecentButtons()
        {
            LoadButtons(recentPath, recentSymbols, recentSymbolButtons, flowLayoutPanel_recent, "recent");
        }
        private void LoadFavButtons()
        {
            LoadButtons(favPath, favSymbols, favSymbolButtons, flowLayoutPanel_fav, "fav");
        }


        private void AlwaysTopMost()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    this.Invoke(new Action(() =>
                    {
                        this.TopMost = true;
                    }));

                    Thread.Sleep(500);
                }
            });
        }

        #endregion

        #region end
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Program.SettingPage.checkBox_tray.Checked)
            {
                e.Cancel = true;
                FadeWindow(true);
            }
            else
            {
                EndProgram();
            }
        }
        public void EndProgram()
        {
            SaveSymbol(recentSymbols, recentPath);
            SaveSymbol(favSymbols, favPath);
            Program.mutex.ReleaseMutex();
            HotKey.API_UnregisterHotKey(this.Handle, API_HOTKEY_SHOWHIDEHOTKEYCODE);
            Environment.Exit(0); //because the thread in AlwaysTopMost
        }
        public static void SaveSymbol(List<Symbol> ls, string filepath)
        {
            string txt = "";
            foreach (Symbol symbol in ls)
            {
                txt += symbol.name + "\n" + symbol.img + "\n";
            }
            try
            {
                File.WriteAllText(filepath, txt);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Can not save {filepath} symbols: " + ex.Message, "Error: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        #endregion

        #region handleOutput
        private void Button_Click(object? sender, EventArgs e)
        {
            if (isOperatingFav)
            { // user is operating the fav box
                Symbol operatingFavSymbol = allSymbols.FirstOrDefault(x => x.img == ((Button)sender).Text);
                if (operatingFavSymbol == null) // if cant find the symbol
                {
                    MessageBox.Show("Unknown error: Can't find the symbol in allSymbols.");
                    isOperatingFav = false;
                    return;
                }

                bool isContainedInFavSymbolsNow = favSymbols.FirstOrDefault(x => x.img == operatingFavSymbol.img) != null;

                if (favOperation) // user wants to add
                {
                    if (isContainedInFavSymbolsNow)
                    {
                        label_hint.Text = "It's already added!";
                        isOperatingFav = false;
                        return;
                    }

                    // Add

                    favSymbols.Add(operatingFavSymbol);
                    Button btn = CreateOneButton("fav", operatingFavSymbol.img);
                    favSymbolButtons.Add(btn);



                    label_hint.Text = "Added to the favorite list";
                }
                else // users wants to remove
                {
                    if (!isContainedInFavSymbolsNow)
                    {
                        label_hint.Text = "It's not in the favorite list!";
                        isOperatingFav = false;
                        return;
                    }

                    // Remove

                    Symbol removeFavS = favSymbols.FirstOrDefault(x => x.img == operatingFavSymbol.img); // they are different objects
                    favSymbols.Remove(removeFavS);

                    Button btnToRemove = favSymbolButtons.FirstOrDefault(x => x.Text == operatingFavSymbol.img);
                    if (btnToRemove == null)
                    {
                        MessageBox.Show("Unknown error: Can't find btnToRemove.");
                    }
                    favSymbolButtons.Remove(btnToRemove);


                    label_hint.Text = "Removed from the favorite list";
                }




                // flash the flowLayout
                AddButtonsToLayout(favSymbolButtons, flowLayoutPanel_fav);

                // save fav buttons to file
                SaveSymbol(favSymbols, favPath);



                isOperatingFav = false;

                return;
            }


            SetNoActivate(this.Handle);
            ShownAndInputToTextbox((Button)sender);
            textBox_search.Text = "";

            #region recent

            #region symbols
            Symbol removeS = recentSymbols.FirstOrDefault(x => x.img == ((Button)sender).Text);
            if (removeS != null)
            {
                recentSymbols.Remove(removeS);
            }

            Symbol addS = allSymbols.FirstOrDefault(x => x.img == ((Button)sender).Text);
            if (addS != null)
            {
                recentSymbols.Insert(0, addS);
            }
            else
            {
                MessageBox.Show("Can not add symbol to recent!");
            }

            if (recentSymbols.Count > RECENT_SYMBOL_KEEPCOUNT)
            {
                recentSymbols.RemoveAt(recentSymbols.Count - 1);
            }
            #endregion
            #region button

            Button recentBtn = recentSymbolButtons.FirstOrDefault(x => x.Text == ((Button)sender).Text);
            if (recentBtn != null)
            {
                recentSymbolButtons.Remove(recentBtn);
                recentSymbolButtons.Insert(0, recentBtn);
            }
            else
            {
                Button btn = CreateOneButton("recent", addS.img);
                recentSymbolButtons.Insert(0, btn);
            }

            if (recentSymbolButtons.Count > RECENT_SYMBOL_KEEPCOUNT)
            {
                recentSymbolButtons.RemoveAt(recentSymbolButtons.Count - 1);
                //MessageBox.Show(recentSymbolButtons.Count + "");
            }

            AddButtonsToLayout(recentSymbolButtons, flowLayoutPanel_recent);

            #endregion

            SaveSymbol(recentSymbols, recentPath);

            #endregion

        }

        private void ShownAndInputToTextbox(Button sender)
        {
            this.textBox_opt.Text = sender.Text;
            //Clipboard.SetText(sender.Text);
            //this.Hide(); // Òþ²Ø´°¿Ú Ê§È¥½¹µã
            Thread.Sleep(10);

            //uint resultDown = KeyboardSimulator.SimulateKeyboard(0x41, KeyEventFlags.KEYDOWN);
            //MessageBox.Show(resultDown + "");
            //KeyboardSimulator.SimulateKeyboard(0, KeyEventFlags.UNICODE, sender.Text[0], true, 0);
            //KeyboardSimulator.SimulateKeyboard(0, KeyEventFlags.KEYUP);

            SimulateUnicodeInput(sender.Text);

            // this.Show(); // ÖØÐÂÏÔÊ¾´°¿Ú
        }

        public static void SimulateUnicodeInput(string text)
        {
            foreach (var c in text)
            {
                ushort unicode = (ushort)c; // Ö±½Ó×ª»»Îª Unicode£¨½öÊÊÓÃÓÚ U+0000 ~ U+FFFF£©

                // ·¢ËÍ°´¼ü°´ÏÂÊÂ¼þ
                KeyboardSimulator.SimulateKeyboard(0, KeyEventFlags.UNICODE, unicode, true);

                // ·¢ËÍ°´¼üÊÍ·ÅÊÂ¼þ
                KeyboardSimulator.SimulateKeyboard(0, KeyEventFlags.UNICODE | KeyEventFlags.KEYUP, unicode, true);
            }
        }

        #endregion

        #region fav_icon

        private void label_fav_add_Click(object sender, EventArgs e)
        {
            if (isOperatingFav)
            {
                this.label_hint.Text = "Cancled";
                isOperatingFav = false;
                return;
            }
            this.label_hint.Text = "Click any sign to add, click +/- again to cancle";
            isOperatingFav = true;
            favOperation = true; // user wants to add
        }

        private void label_fav_minus_Click(object sender, EventArgs e)
        {
            if (isOperatingFav)
            {
                this.label_hint.Text = "Cancled";
                isOperatingFav = false;
                return;
            }
            this.label_hint.Text = "Click any sign to remove, click +/- again to cancle";
            isOperatingFav = true;
            favOperation = false; // user wants to remove
        }

        #endregion

        #region handle input

        public void FilterButtons(string text)
        {
            //this.label_loading.Visible = true; ²»ÐèÒªÁË£¬ÒÑ¾­ºÜ¿ìÁË
            this.Refresh();
            flowLayoutPanel_all.SuspendLayout();  // ÔÝÍ£ UI ¸üÐÂ£¬Ìá¸ßÐÔÄÜ
            // flowLayoutPanel.Controls.Clear(); // Çå³ý¾ÉµÄ°´Å¥


            if (string.IsNullOrEmpty(text)) //¸ü¿ì
            {
                for (int i = 0; i < allSymbols.Count; i++)
                {
                    allSymbolButtons[i].Visible = true;
                }
            }
            else
            {
                //Stopwatch sw = Stopwatch.StartNew();

                for (int i = 0; i < allSymbols.Count; i++)
                {
                    if (allSymbols[i].name.Contains(text))
                    {
                        allSymbolButtons[i].Visible = true;
                    }
                    else
                    {
                        allSymbolButtons[i].Visible = false;
                    }
                }
                //sw.Stop();
                //Trace.WriteLine(sw.ElapsedMilliseconds);

            }



            flowLayoutPanel_all.ResumeLayout();  // »Ö¸´ UI ¸üÐÂ
            //this.label_loading.Visible = false;
            this.Refresh();
        }



        private CancellationTokenSource CancellationTokenSource;


        private async void textBox_search_TextChanged(object sender, EventArgs e)
        {
            // È¡ÏûÖ®Ç°µÄÈÎÎñ
            CancellationTokenSource?.Cancel();
            CancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = CancellationTokenSource.Token;

            try
            {
                await Task.Delay(150, token);
                if (!token.IsCancellationRequested)
                {
                    FilterButtons(textBox_search.Text); // Ö´ÐÐËÑË÷
                }
            }
            catch (TaskCanceledException)
            {
                // ÈÎÎñ±»È¡Ïû£¬²»×öÈÎºÎ´¦Àí
            }
        }

        #endregion

        #region window setting


        #region no focus
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_NOACTIVATE = 0x08000000;

        public static void SetNoActivate(IntPtr hWnd)
        {
            int exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
            SetWindowLong(hWnd, GWL_EXSTYLE, exStyle | WS_EX_NOACTIVATE);
        }

        public static void CancleNoActivate(IntPtr hWnd)
        {
            int exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
            SetWindowLong(hWnd, GWL_EXSTYLE, exStyle & ~WS_EX_NOACTIVATE);
        }


        //const int WS_EX_NOACTIVATE = 0x08000000;
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= WS_EX_NOACTIVATE; // Ä¬ÈÏ²»»ñÈ¡½¹µã
        //        return cp;
        //    }
        //}

        #endregion

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                textBox_search_Leave(null, null);
                this.WindowState = FormWindowState.Minimized;
            }
        }
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape)
            {
                textBox_opt.Focus();
            }
            if (e.KeyCode == Keys.Escape)
            {
                textBox_search_Leave(null, null);
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void textBox_search_Enter(object sender, EventArgs e)
        {
            Trace.WriteLine(1);
            CancleNoActivate(this.Handle);
            this.Activate();
        }

        private void textBox_search_Leave(object sender, EventArgs e)
        {
            Trace.WriteLine(2);
            SetNoActivate(this.Handle);
            this.Hide();
            this.Show(); //cancle activate
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            label_hint.Focus(); // if the user is inputting at the textbox, then we can let it lose focus
        }
        #endregion


        #region hotkey
        public static class HotKey
        {
            [DllImport("user32")]
            private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint control, Keys vk);

            //½â³ý×¢²áÈÈ¼üµÄapi
            [DllImport("user32")]
            private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

            public static bool API_RegisterHotKey(IntPtr hWnd, int id, control control, Keys vk)
            {
                return RegisterHotKey(hWnd, id, (uint)control, vk);
            }
            public static bool API_UnregisterHotKey(IntPtr hWnd, int id)
            {
                return UnregisterHotKey(hWnd, id);
            }
            public enum control : uint
            {
                None = 0,
                Alt = 1,
                Ctrl = 2,
                Shift = 4,
                Windows = 8
            }
        }


        protected override void WndProc(ref Message m) //ÖØÐ´´°¿ÚÏûÏ¢
        {
            switch (m.Msg)
            {
                case 0x0312: //hotkey
                    switch ((int)m.WParam)
                    {
                        case API_HOTKEY_SHOWHIDEHOTKEYCODE:

                            label1.Focus(); //×ÜÊÇlabel1¾ÍºÃÁË
                            if (this.Visible == false) //WindowState == FormWindowState.Minimized
                            {
                                // label1.Focus();
                                //this.Visible = true; //WindowState = FormWindowState.Normal
                                FadeWindow(false);
                                // textBox_search.Focus();
                            }
                            else
                            {
                                // label1.Focus(); //È¡ÏûsearchµÄFocus£¨ÈÃ´°¿ÚÊ§È¥½¹µã£¬ÒòÎªFocusÄÇÀïÓÃÁË¸öÊÂ¼þ£¬Èç¹ûÃ»ÓÐÁË½¹µã¾ÍÉèÖÃ±¾´°¿ÚÎÞ½¹µã£©
                                //this.Visible = false; //WindowState = FormWindowState.Minimized
                                FadeWindow(true);
                            }
                            break;
                    }

                    break;
                case Program.MUTEXMESSAGE:
                    if (this.Visible == false)
                    {
                        FadeWindow(false);
                    }
                    else
                    {
                        this.TopMost = true;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        private bool startedFade = false;
        private async void FadeWindow(bool fadeOut)
        {
            if (startedFade) return;
            startedFade = true;
            double to = this.Opacity;
            double original = this.Opacity;

            if (!fadeOut)
            {
                this.Opacity = 0;
                this.Visible = true;
            }
            int time = 12;
            for (int i = 0; i < time; i++)
            {
                if (fadeOut)
                {
                    if (this.Opacity - 1.0 / time >= 0) this.Opacity -= 1.0 / time;
                }
                else
                {
                    if (this.Opacity + 1.0 / time <= original) this.Opacity += 1.0 / time;
                }
                await Task.Delay(1);
            }
            if (fadeOut)
            {
                this.Opacity = 0;
                this.Visible = false;
                this.Opacity = original;
            }
            else
            {
                this.Opacity = to;

                this.TopMost = true;
            }

            startedFade = false;
        }
        #endregion

        #region tray
        private void notifyIcon_tray_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = e as MouseEventArgs;
            if (me != null && me.Button == MouseButtons.Left)
            {
                FadeWindow(false);
            }
        }
        private void toolStripMenuItem_show_Click(object sender, EventArgs e)
        {
            FadeWindow(false);
        }
        private void toolStripMenuItem_settings_Click(object sender, EventArgs e)
        {
            FadeWindow(true);
            Program.SettingPage.Show();
        }
        private void toolStripMenuItem_exit_Click(object sender, EventArgs e)
        {
            EndProgram();
        }
        #endregion

    }

    public class Symbol
    {
        public string name;
        public string img;
        public Symbol(string name, string img)
        {
            this.name = name;
            this.img = img;
        }
    }

}

