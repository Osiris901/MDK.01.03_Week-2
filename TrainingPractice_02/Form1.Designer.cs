namespace TrainingPractice_02
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.играToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameMenuButton = new System.Windows.Forms.ToolStripMenuItem();
            this.restartMenuButton = new System.Windows.Forms.ToolStripMenuItem();
            this.seedStartMenuButton = new System.Windows.Forms.ToolStripMenuItem();
            this.scoreboardMenuButton = new System.Windows.Forms.ToolStripMenuItem();
            this.параметрыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highlightMenuButton = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuButton = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuButton = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.gameStateLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.играToolStripMenuItem,
            this.helpMenuButton});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // играToolStripMenuItem
            // 
            this.играToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameMenuButton,
            this.restartMenuButton,
            this.seedStartMenuButton,
            this.scoreboardMenuButton,
            this.параметрыToolStripMenuItem,
            this.exitMenuButton});
            this.играToolStripMenuItem.Name = "играToolStripMenuItem";
            this.играToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.играToolStripMenuItem.Text = "Игра";
            // 
            // newGameMenuButton
            // 
            this.newGameMenuButton.Name = "newGameMenuButton";
            this.newGameMenuButton.Size = new System.Drawing.Size(175, 22);
            this.newGameMenuButton.Text = "Новая игра";
            this.newGameMenuButton.Click += new System.EventHandler(this.newGameMenuButton_Click);
            // 
            // restartMenuButton
            // 
            this.restartMenuButton.Name = "restartMenuButton";
            this.restartMenuButton.Size = new System.Drawing.Size(175, 22);
            this.restartMenuButton.Text = "Начать заново";
            this.restartMenuButton.Click += new System.EventHandler(this.restartMenuButton_Click);
            // 
            // seedStartMenuButton
            // 
            this.seedStartMenuButton.Name = "seedStartMenuButton";
            this.seedStartMenuButton.Size = new System.Drawing.Size(175, 22);
            this.seedStartMenuButton.Text = "Ввести код";
            this.seedStartMenuButton.Click += new System.EventHandler(this.seedStartMenuButton_Click);
            // 
            // scoreboardMenuButton
            // 
            this.scoreboardMenuButton.Name = "scoreboardMenuButton";
            this.scoreboardMenuButton.Size = new System.Drawing.Size(175, 22);
            this.scoreboardMenuButton.Text = "Таблица Рекордов";
            this.scoreboardMenuButton.Click += new System.EventHandler(this.scoreboardMenuButton_Click);
            // 
            // параметрыToolStripMenuItem
            // 
            this.параметрыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.highlightMenuButton});
            this.параметрыToolStripMenuItem.Name = "параметрыToolStripMenuItem";
            this.параметрыToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.параметрыToolStripMenuItem.Text = "Параметры";
            // 
            // highlightMenuButton
            // 
            this.highlightMenuButton.CheckOnClick = true;
            this.highlightMenuButton.Name = "highlightMenuButton";
            this.highlightMenuButton.Size = new System.Drawing.Size(209, 22);
            this.highlightMenuButton.Text = "Использовать подсветку";
            this.highlightMenuButton.CheckedChanged += new System.EventHandler(this.highlightMenuButton_CheckedChanged);
            // 
            // exitMenuButton
            // 
            this.exitMenuButton.Name = "exitMenuButton";
            this.exitMenuButton.Size = new System.Drawing.Size(175, 22);
            this.exitMenuButton.Text = "Выход";
            this.exitMenuButton.Click += new System.EventHandler(this.exitMenuButton_Click);
            // 
            // helpMenuButton
            // 
            this.helpMenuButton.Name = "helpMenuButton";
            this.helpMenuButton.Size = new System.Drawing.Size(68, 20);
            this.helpMenuButton.Text = "Помощь";
            this.helpMenuButton.Click += new System.EventHandler(this.helpMenuButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameStateLabel,
            this.timerLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // gameStateLabel
            // 
            this.gameStateLabel.Name = "gameStateLabel";
            this.gameStateLabel.Size = new System.Drawing.Size(118, 17);
            this.gameStateLabel.Text = "toolStripStatusLabel1";
            // 
            // timerLabel
            // 
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(118, 17);
            this.timerLabel.Text = "toolStripStatusLabel1";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Игра на внимательность 51";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem играToolStripMenuItem;
        private ToolStripMenuItem newGameMenuButton;
        private ToolStripMenuItem restartMenuButton;
        private ToolStripMenuItem scoreboardMenuButton;
        private ToolStripMenuItem параметрыToolStripMenuItem;
        private ToolStripMenuItem highlightMenuButton;
        private ToolStripMenuItem exitMenuButton;
        private ToolStripMenuItem helpMenuButton;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel gameStateLabel;
        private ToolStripStatusLabel timerLabel;
        private ToolStripMenuItem seedStartMenuButton;
        private System.Windows.Forms.Timer timer1;
    }
}