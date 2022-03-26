using System.Diagnostics;
using TrainingPractice_02.Models;
using TrainingPractice_02.Views;

namespace TrainingPractice_02
{
    public partial class Form1 : Form
    {
        private GridView _gridView;
        private int _activeSeed = 0;
        private int _playtime = 0;
        private int _gameState;
        public readonly Point GridStart;

        public int GameState
        {
            get => _gameState;

            set
            {
                _gameState = value;
                gameStateLabel.Text = "Следующее число: " + value;

                if (value == 52)
                {
                    Win();
                }
            }
        }
        
        public Form1()
        {
            InitializeComponent();
            
            Size = new Size(640, 480);
            timerLabel.Alignment = ToolStripItemAlignment.Right;
            timerLabel.Text = "Игра начинается...";

            var gridWidth = Constants.Width * Constants.GridCols;
            var gridHeight = Constants.Height * Constants.GridRows;
            
            GridStart = new Point((ClientRectangle.Width - gridWidth) / 2, (ClientRectangle.Height - gridHeight) / 2);

            Restart();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            _gridView.Draw(e.Graphics);
        }

        private void RegenerateSeed()
        {
            _activeSeed = DateTime.Now.Millisecond * DateTime.Now.Second;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            var (o, i, val) = _gridView.GetValueAt(e.X, e.Y);


            if (o == -1)
            {
                return;
            }
            
            if (val == GameState)
            {
                _gridView.ActivateAt(o, i);
                GameState++;
            }
            else
            {
                var res = MessageBox.Show("К сожалению, вы проиграли.\nВ следующий раз обязательно получится!\n\nХотите начать новую игру? (Да - Будет сгенерировано новое поле, Нет - Перезапустить текущую игру, Отмена - Выход)",
                    "Поражение",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (res == DialogResult.Cancel)
                {
                    Application.Exit();
                }

                if (res == DialogResult.Yes)
                {
                    RegenerateSeed();
                }

                Restart();
            }

            Invalidate();
        }

        private void highlightMenuButton_CheckedChanged(object sender, EventArgs e)
        {
            _gridView.ToggleHighlight(highlightMenuButton.Checked);
            Invalidate();
        }

        private void newGameMenuButton_Click(object sender, EventArgs e)
        {
            RegenerateSeed();

            Restart();
        }

        private void restartMenuButton_Click(object sender, EventArgs e)
        {
            Restart();
        }

        private void Restart()
        {
            timerLabel.Text = "Перезапускаем игру...";
            timer1.Stop();

            var grid = new Grid(seed: _activeSeed);
            _gridView = new GridView(grid, GridStart.X, GridStart.Y);
            _gridView.ToggleHighlight(highlightMenuButton.Checked);
            GameState = 1;
            _playtime = 0;
            
            timer1.Start();
            Invalidate();
        }

        private void Win()
        {
            var res = MessageBox.Show($"Поздравляем! Вам удалось пройти игру за {TimeSpan.FromSeconds(_playtime)}. Ваш уникальный код - {_activeSeed}.\n\nХотите сохранить свой рекорд?",
                "Победа!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);

            if (res == DialogResult.Yes)
            {
                var dir = Environment.CurrentDirectory;
                var fi = new FileInfo(dir + Path.DirectorySeparatorChar + "scores.csv");
                if (fi.Exists)
                {
                    using (var sw = fi.AppendText())
                    {
                        sw.WriteLine($"{DateTime.Now};{Environment.UserName};{_activeSeed};{_playtime}");
                    }
                }
                else
                {
                    using (var sw = fi.CreateText())
                    {
                        sw.WriteLine("Date;Player;Seed;Time");
                        sw.WriteLine($"{DateTime.Now};{Environment.UserName};{_activeSeed};{_playtime}");
                    }
                }
            }

            RegenerateSeed();
            Restart();
        }

        private void exitMenuButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void helpMenuButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "На таблице приведены числа натурального ряда от 1 до 51. Попробуйте разыскать и пересчитать их в возрастающем порядке: 1, 2, 3 и т. д.\n\nСможете ли вы это сделать быстрее, чем за 1.5 минуты?",
                "Помощь",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void seedStartMenuButton_Click(object sender, EventArgs e)
        {
            var form = new SeedStartForm();
            form.Owner = this;
            if (form.ShowDialog() == DialogResult.OK)
            {
                _activeSeed = form.Seed;
                Restart();
            }
            
        }

        private void scoreboardMenuButton_Click(object sender, EventArgs e)
        {
            var scoreboard = new Scoreboard(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "scores.csv");
            scoreboard.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _playtime++;
            var ts = TimeSpan.FromSeconds(_playtime);
            timerLabel.Text = $"Время в игре: {ts}";
        }
    }
}