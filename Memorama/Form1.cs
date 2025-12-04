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
            InitializeGame();
            this.Resize += (s, e) => LayoutCards(); // reajustar al cambiar tamaño
        }

        private void ConfigureTimers()
        {
            if (timer1 != null)
            {
                // evitar múltiples suscripciones si ConfigureTimers se llama otra vez
                timer1.Tick -= Timer1_Tick;
                timer1.Interval = 1000; // segundos transcurridos
                timer1.Tick += Timer1_Tick;
            }

            if (timer2 != null)
            {
                timer2.Tick -= Timer2_Tick;
                timer2.Interval = 700; // delay para ocultar pareja incorrecta
                timer2.Tick += Timer2_Tick;
            }
        }

        private void InitializeGame()
        {
            // Detener timers mientras se reinicia
            if (timer1 != null) timer1.Stop();
            if (timer2 != null) timer2.Stop();

            // Lista de iconos (18 diferentes para 6x6). Se usan emojis para no necesitar recursos externos.
            var baseIcons = new List<string>
            {
                "🐶","🐱","🐭","🐹","🐰","🦊","🐻","🐼","🐨",
                "🐯","🦁","🐮","🐷","🐸","🐵","🐔","🐧","🐢"
            };

            // Duplicar y mezclar usando Fisher-Yates para mayor aleatoriedad
            var icons = baseIcons.Concat(baseIcons).ToList();
            Shuffle(icons);

            // Limpiar panel y listas
            plMemorama.Controls.Clear();
            cards.Clear();

            // Crear botones (cartas)
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    var index = r * cols + c;
                    var btn = new Button
                    {
                        Tag = icons[index],
                        // Usar fuente con mejor soporte de emojis si está disponible
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

            // reiniciar estado
            matchesFound = 0;
            secondsElapsed = 0;
            lbPuntos.Text = $"Puntos: {matchesFound}";
            lbTimer.Text = $"Tiempo: {secondsElapsed}";
            firstClicked = null;
            secondClicked = null;

            if (timer1 != null) timer1.Start();
        }

        // Fisher-Yates shuffle
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
            // si estamos esperando a ocultar por mismatch, bloquear clicks
            if (timer2 != null && timer2.Enabled) return;

            var clicked = sender as Button;
            if (clicked == null) return;
            if (clicked == firstClicked) return;
            if (!string.IsNullOrEmpty(clicked.Text)) return; // ya descubierta

            ShowIcon(clicked);

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

        private void ShowIcon(Button btn)
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

            // Comprobar récord: menor tiempo
            if (secondsElapsed < bestTime)
            {
                string nombre;
                try
                {
                    nombre = Interaction.InputBox("Has batido el récord. Introduce tu nombre:", "Nuevo récord", "Jugador");
                }
                catch
                {
                    nombre = "Jugador";
                }
            }

            // Reiniciar o crear nuevo juego
            var result = MessageBox.Show("¿Jugar de nuevo?", "Nuevo juego", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                InitializeGame();
            }
        }
    }
}
