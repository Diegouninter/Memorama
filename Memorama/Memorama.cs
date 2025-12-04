using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Memorama
{
    public partial class Memorama : Form
    {
        private List<Button> cards = new List<Button>();
        private Button firstClicked = null;
        private Button secondClicked = null;
        private int matchesFound = 0;
        private int secondsElapsed = 0;
        private const int rows = 6;
        private const int cols = 6;
        private const int totalPairs = (rows * cols) / 2;
        private int bestTime = int.MaxValue;

        private List<(string name, int time)> podium = new List<(string name, int time)>();
        private const int podiumLimit = 3;

        public Memorama()
        {
            InitializeComponent();
            ConfigureTimers();

            this.btnStart.Click += BtnStart_Click;
            this.btnRestart.Click += BtnRestart_Click;

            InitializeGame(startTimer: false);

            UpdatePodioListBox();

            this.Resize += (s, e) => LayoutCards();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            InitializeGame(startTimer: true);
            btnStart.Enabled = false;
            btnRestart.Enabled = true;
        }

        private void BtnRestart_Click(object sender, EventArgs e)
        {
            InitializeGame(startTimer: false);
            btnStart.Enabled = true;
            btnRestart.Enabled = false;
        }

        private void ConfigureTimers()
        {
            if (timer1 != null)
            {
                timer1.Tick -= Timer1_Tick;
                timer1.Interval = 1000;
                timer1.Tick += Timer1_Tick;
            }

            if (timer2 != null)
            {
                timer2.Tick -= Timer2_Tick;
                timer2.Interval = 700;
                timer2.Tick += Timer2_Tick;
            }
        }

        private void InitializeGame(bool startTimer = true)
        {
            if (timer1 != null) timer1.Stop();
            if (timer2 != null) timer2.Stop();

            var baseIcons = new List<string>
                        {
                            "🐶","🐱","🐭","🐹","🐰","🦊","🐻","🐼","🐨",
                            "🐯","🦁","🐮","🐷","🐸","🐵","🐔","🐧","🐢"
                        };

            var icons = baseIcons.Concat(baseIcons).ToList();
            Shuffle(icons);

            plMemorama.Controls.Clear();
            cards.Clear();

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    var index = r * cols + c;
                    var btn = new Button
                    {
                        Tag = icons[index],
                        Font = new Font("Segoe UI Emoji", 22F, FontStyle.Regular, GraphicsUnit.Point),
                        BackColor = Color.LightGray,
                        ForeColor = Color.Black,
                        Text = string.Empty,
                        FlatStyle = FlatStyle.Flat
                    };
                    btn.Click += Card_Click;
                    cards.Add(btn);
                    plMemorama.Controls.Add(btn);
                }
            }

            LayoutCards();

            matchesFound = 0;
            secondsElapsed = 0;
            lbPuntos.Text = $"Puntos: {matchesFound}";
            lbTimer.Text = $"Tiempo: {secondsElapsed}";
            firstClicked = null;
            secondClicked = null;

            if (startTimer && timer1 != null) timer1.Start();
        }

        private void Shuffle<T>(IList<T> list)
        {
            var rnd = new Random();
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
                T tmp = list[i];
                list[i] = list[j];
                list[j] = tmp;
            }
        }

        private void LayoutCards()
        {
            if (cards.Count == 0) return;

            var w = plMemorama.ClientSize.Width;
            var h = plMemorama.ClientSize.Height;
            var cellW = Math.Max(30, w / cols);
            var cellH = Math.Max(30, h / rows);

            for (int i = 0; i < cards.Count; i++)
            {
                int r = i / cols;
                int c = i % cols;
                var btn = cards[i];
                btn.Size = new Size(Math.Max(24, cellW - 6), Math.Max(24, cellH - 6));
                btn.Location = new Point(c * cellW + 3, r * cellH + 3);
            }
        }

        private void Card_Click(object sender, EventArgs e)
        {
            if (timer2 != null && timer2.Enabled) return;

            var clicked = sender as Button;
            if (clicked == null) return;
            if (clicked == firstClicked) return;
            if (!string.IsNullOrEmpty(clicked.Text)) return;

            RevealCard(clicked);

            if (firstClicked == null)
            {
                firstClicked = clicked;
                return;
            }

            secondClicked = clicked;

            if (firstClicked.Tag != null && secondClicked.Tag != null &&
                firstClicked.Tag.ToString() == secondClicked.Tag.ToString())
            {
                firstClicked.Enabled = false;
                secondClicked.Enabled = false;
                matchesFound++;
                lbPuntos.Text = $"Puntos: {matchesFound}";

                firstClicked = null;
                secondClicked = null;

                if (matchesFound == totalPairs)
                {
                    GameWon();
                }
            }
            else
            {
                if (timer2 != null) timer2.Start();
            }
        }

        private void RevealCard(Button btn)
        {
            if (btn == null) return;
            btn.Text = btn.Tag?.ToString() ?? string.Empty;
            btn.BackColor = Color.White;
        }

        private void HideIcon(Button btn)
        {
            if (btn == null) return;
            btn.Text = string.Empty;
            btn.BackColor = Color.LightGray;
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (timer2 != null) timer2.Stop();
            if (firstClicked != null) HideIcon(firstClicked);
            if (secondClicked != null) HideIcon(secondClicked);
            firstClicked = null;
            secondClicked = null;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            secondsElapsed++;
            lbTimer.Text = $"Tiempo: {secondsElapsed}";
        }

        private void GameWon()
        {
            if (timer1 != null) timer1.Stop();
            if (timer2 != null) timer2.Stop();

            MessageBox.Show($"¡Ganaste! Puntos: {matchesFound} - Tiempo: {secondsElapsed}s", "Victoria", MessageBoxButtons.OK, MessageBoxIcon.Information);

            var podiumIsFull = podium.Count >= podiumLimit;
            int worstOnPodium = podiumIsFull ? podium.Max(p => p.time) : int.MaxValue;

            if (secondsElapsed < worstOnPodium || !podiumIsFull)
            {
                string nombre = PromptForName("Introduce tu nombre para el podio:", "Nuevo récord");
                if (string.IsNullOrWhiteSpace(nombre))
                    nombre = "Jugador";

                AddToPodio(nombre, secondsElapsed);
                UpdatePodioListBox();
            }

            if (podium.Count > 0)
                bestTime = podium.Min(p => p.time);
            else
                bestTime = int.MaxValue;

            var result = MessageBox.Show("¿Jugar de nuevo?", "Nuevo juego", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                InitializeGame(startTimer: false);
                btnStart.Enabled = true;
                btnRestart.Enabled = false;
            }
            else
            {
                btnStart.Enabled = true;
                btnRestart.Enabled = false;
            }
        }

        private void AddToPodio(string name, int time)
        {
            podium.Add((name, time));
            podium = podium.OrderBy(p => p.time).Take(podiumLimit).ToList();
        }

        private void UpdatePodioListBox()
        {
            lbxPodio.Items.Clear();
            for (int i = 0; i < podium.Count; i++)
            {
                var entry = podium[i];
                lbxPodio.Items.Add($"{i + 1}º {entry.name} - {entry.time}s");
            }
        }

        private string PromptForName(string promptText, string caption)
        {
            using (var form = new Form())
            {
                form.Text = caption;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterParent;
                form.ClientSize = new Size(320, 110);
                form.MinimizeBox = false;
                form.MaximizeBox = false;
                form.ShowIcon = false;
                form.ShowInTaskbar = false;

                var lbl = new Label() { Left = 10, Top = 10, AutoSize = true, Text = promptText };
                var txt = new TextBox() { Left = 10, Top = lbl.Bottom + 8, Width = form.ClientSize.Width - 20 };
                var ok = new Button() { Text = "Aceptar", DialogResult = DialogResult.OK, Left = form.ClientSize.Width - 180, Width = 80, Top = txt.Bottom + 10 };
                var cancel = new Button() { Text = "Cancelar", DialogResult = DialogResult.Cancel, Left = form.ClientSize.Width - 90, Width = 80, Top = txt.Bottom + 10 };

                form.Controls.AddRange(new Control[] { lbl, txt, ok, cancel });
                form.AcceptButton = ok;
                form.CancelButton = cancel;

                var result = form.ShowDialog(this);
                if (result == DialogResult.OK)
                    return txt.Text.Trim();
                return string.Empty;
            }
        }
    }
}