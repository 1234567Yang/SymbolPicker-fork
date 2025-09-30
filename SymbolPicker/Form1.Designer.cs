namespace SymbolPicker
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            textBox_search = new TextBox();
            flowLayoutPanel_all = new FlowLayoutPanel();
            textBox_opt = new TextBox();
            flowLayoutPanel_recent = new FlowLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            notifyIcon_tray = new NotifyIcon(components);
            contextMenuStrip_tray = new ContextMenuStrip(components);
            toolStripMenuItem_show = new ToolStripMenuItem();
            toolStripMenuItem_settings = new ToolStripMenuItem();
            toolStripMenuItem_exit = new ToolStripMenuItem();
            label_favorite_sign = new Label();
            flowLayoutPanel_fav = new FlowLayoutPanel();
            label_fav_add = new Label();
            label_fav_minus = new Label();
            label_hint = new Label();
            toolStripMenuItem_about = new ToolStripMenuItem();
            contextMenuStrip_tray.SuspendLayout();
            SuspendLayout();
            // 
            // textBox_search
            // 
            textBox_search.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox_search.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_search.Location = new Point(5, 5);
            textBox_search.Name = "textBox_search";
            textBox_search.Size = new Size(306, 31);
            textBox_search.TabIndex = 2;
            textBox_search.TextChanged += textBox_search_TextChanged;
            textBox_search.Enter += textBox_search_Enter;
            textBox_search.KeyDown += textBox_KeyDown;
            textBox_search.Leave += textBox_search_Leave;
            // 
            // flowLayoutPanel_all
            // 
            flowLayoutPanel_all.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel_all.AutoScroll = true;
            flowLayoutPanel_all.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            flowLayoutPanel_all.Location = new Point(5, 315);
            flowLayoutPanel_all.Name = "flowLayoutPanel_all";
            flowLayoutPanel_all.Size = new Size(306, 154);
            flowLayoutPanel_all.TabIndex = 1;
            // 
            // textBox_opt
            // 
            textBox_opt.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox_opt.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_opt.Location = new Point(5, 480);
            textBox_opt.Name = "textBox_opt";
            textBox_opt.Size = new Size(307, 31);
            textBox_opt.TabIndex = 0;
            // 
            // flowLayoutPanel_recent
            // 
            flowLayoutPanel_recent.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel_recent.AutoScroll = true;
            flowLayoutPanel_recent.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            flowLayoutPanel_recent.Location = new Point(5, 205);
            flowLayoutPanel_recent.Name = "flowLayoutPanel_recent";
            flowLayoutPanel_recent.Size = new Size(306, 80);
            flowLayoutPanel_recent.TabIndex = 2;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(4, 292);
            label1.Name = "label1";
            label1.Size = new Size(37, 21);
            label1.TabIndex = 3;
            label1.Text = "all";
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(4, 177);
            label2.Name = "label2";
            label2.Size = new Size(78, 28);
            label2.TabIndex = 4;
            label2.Text = "recent";
            // 
            // notifyIcon_tray
            // 
            notifyIcon_tray.ContextMenuStrip = contextMenuStrip_tray;
            notifyIcon_tray.Icon = (Icon)resources.GetObject("notifyIcon_tray.Icon");
            notifyIcon_tray.Text = "Symbol Picker";
            notifyIcon_tray.Visible = true;
            notifyIcon_tray.Click += notifyIcon_tray_Click;
            // 
            // contextMenuStrip_tray
            // 
            contextMenuStrip_tray.ImageScalingSize = new Size(24, 24);
            contextMenuStrip_tray.Items.AddRange(new ToolStripItem[] { toolStripMenuItem_about, toolStripMenuItem_show, toolStripMenuItem_settings, toolStripMenuItem_exit });
            contextMenuStrip_tray.Name = "contextMenuStrip_tray";
            contextMenuStrip_tray.Size = new Size(241, 165);
            // 
            // toolStripMenuItem_show
            // 
            toolStripMenuItem_show.Name = "toolStripMenuItem_show";
            toolStripMenuItem_show.Size = new Size(240, 32);
            toolStripMenuItem_show.Text = "Show";
            toolStripMenuItem_show.Click += toolStripMenuItem_show_Click;
            // 
            // toolStripMenuItem_settings
            // 
            toolStripMenuItem_settings.Name = "toolStripMenuItem_settings";
            toolStripMenuItem_settings.Size = new Size(240, 32);
            toolStripMenuItem_settings.Text = "Settings";
            toolStripMenuItem_settings.Click += toolStripMenuItem_settings_Click;
            // 
            // toolStripMenuItem_exit
            // 
            toolStripMenuItem_exit.Name = "toolStripMenuItem_exit";
            toolStripMenuItem_exit.Size = new Size(240, 32);
            toolStripMenuItem_exit.Text = "Exit";
            toolStripMenuItem_exit.Click += toolStripMenuItem_exit_Click;
            // 
            // label_favorite_sign
            // 
            label_favorite_sign.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label_favorite_sign.Location = new Point(8, 45);
            label_favorite_sign.Name = "label_favorite_sign";
            label_favorite_sign.Size = new Size(74, 28);
            label_favorite_sign.TabIndex = 6;
            label_favorite_sign.Text = "favorite";
            // 
            // flowLayoutPanel_fav
            // 
            flowLayoutPanel_fav.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel_fav.AutoScroll = true;
            flowLayoutPanel_fav.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            flowLayoutPanel_fav.Location = new Point(5, 75);
            flowLayoutPanel_fav.Name = "flowLayoutPanel_fav";
            flowLayoutPanel_fav.Size = new Size(306, 100);
            flowLayoutPanel_fav.TabIndex = 5;
            // 
            // label_fav_add
            // 
            label_fav_add.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label_fav_add.BackColor = Color.YellowGreen;
            label_fav_add.Cursor = Cursors.Cross;
            label_fav_add.ForeColor = Color.DarkGreen;
            label_fav_add.Location = new Point(281, 45);
            label_fav_add.Name = "label_fav_add";
            label_fav_add.Size = new Size(30, 22);
            label_fav_add.TabIndex = 7;
            label_fav_add.Text = "➕";
            label_fav_add.Click += label_fav_add_Click;
            // 
            // label_fav_minus
            // 
            label_fav_minus.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label_fav_minus.BackColor = Color.Pink;
            label_fav_minus.Cursor = Cursors.Cross;
            label_fav_minus.ForeColor = Color.Crimson;
            label_fav_minus.Location = new Point(251, 45);
            label_fav_minus.Name = "label_fav_minus";
            label_fav_minus.Size = new Size(30, 22);
            label_fav_minus.TabIndex = 8;
            label_fav_minus.Text = "➖";
            label_fav_minus.Click += label_fav_minus_Click;
            // 
            // label_hint
            // 
            label_hint.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_hint.BackColor = SystemColors.AppWorkspace;
            label_hint.Font = new Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point);
            label_hint.Location = new Point(5, 514);
            label_hint.Margin = new Padding(0);
            label_hint.Name = "label_hint";
            label_hint.Size = new Size(307, 27);
            label_hint.TabIndex = 9;
            label_hint.Text = "Hint";
            label_hint.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // toolStripMenuItem_about
            // 
            toolStripMenuItem_about.Name = "toolStripMenuItem_about";
            toolStripMenuItem_about.Size = new Size(240, 32);
            toolStripMenuItem_about.Text = "About";
            toolStripMenuItem_about.Click += toolStripMenuItem_about_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(318, 546);
            Controls.Add(label_hint);
            Controls.Add(label_fav_minus);
            Controls.Add(label_fav_add);
            Controls.Add(label_favorite_sign);
            Controls.Add(flowLayoutPanel_fav);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(flowLayoutPanel_recent);
            Controls.Add(textBox_opt);
            Controls.Add(flowLayoutPanel_all);
            Controls.Add(textBox_search);
            Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Symbol Picker";
            TopMost = true;
            Deactivate += Form1_Deactivate;
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            contextMenuStrip_tray.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox textBox_search;
        private FlowLayoutPanel flowLayoutPanel_all;
        private TextBox textBox_opt;
        private FlowLayoutPanel flowLayoutPanel_recent;
        private Label label1;
        private Label label2;
        private NotifyIcon notifyIcon_tray;
        private ContextMenuStrip contextMenuStrip_tray;
        private ToolStripMenuItem toolStripMenuItem_settings;
        private ToolStripMenuItem toolStripMenuItem_exit;
        private ToolStripMenuItem toolStripMenuItem_show;
        private Label label_favorite_sign;
        private FlowLayoutPanel flowLayoutPanel_fav;
        private Label label_fav_add;
        private Label label_fav_minus;
        private Label label_hint;
        private ToolStripMenuItem toolStripMenuItem_about;
    }
}