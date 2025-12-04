namespace Memorama
{
    partial class Memorama
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.plMemorama = new System.Windows.Forms.Panel();
            this.lbPuntos = new System.Windows.Forms.Label();
            this.lbTimer = new System.Windows.Forms.Label();
            this.lbxPodio = new System.Windows.Forms.ListBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnRestart = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // plMemorama
            // 
            this.plMemorama.Location = new System.Drawing.Point(20, 17);
            this.plMemorama.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.plMemorama.Name = "plMemorama";
            this.plMemorama.Size = new System.Drawing.Size(843, 658);
            this.plMemorama.TabIndex = 0;
            // 
            // lbPuntos
            // 
            this.lbPuntos.AutoSize = true;
            this.lbPuntos.Location = new System.Drawing.Point(1040, 72);
            this.lbPuntos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbPuntos.Name = "lbPuntos";
            this.lbPuntos.Size = new System.Drawing.Size(76, 20);
            this.lbPuntos.TabIndex = 1;
            this.lbPuntos.Text = "Puntos: 0";
            // 
            // lbTimer
            // 
            this.lbTimer.AutoSize = true;
            this.lbTimer.Location = new System.Drawing.Point(918, 72);
            this.lbTimer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTimer.Name = "lbTimer";
            this.lbTimer.Size = new System.Drawing.Size(78, 20);
            this.lbTimer.TabIndex = 2;
            this.lbTimer.Text = "Tiempo: 0";
            // 
            // lbxPodio
            // 
            this.lbxPodio.FormattingEnabled = true;
            this.lbxPodio.ItemHeight = 20;
            this.lbxPodio.Location = new System.Drawing.Point(909, 126);
            this.lbxPodio.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbxPodio.Name = "lbxPodio";
            this.lbxPodio.Size = new System.Drawing.Size(246, 144);
            this.lbxPodio.TabIndex = 3;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(890, 17);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(105, 35);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Iniciar";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // btnRestart
            // 
            this.btnRestart.Enabled = false;
            this.btnRestart.Location = new System.Drawing.Point(1010, 17);
            this.btnRestart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(128, 35);
            this.btnRestart.TabIndex = 5;
            this.btnRestart.Text = "Reiniciar";
            this.btnRestart.UseVisualStyleBackColor = true;
            // 
            // Memorama
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lbxPodio);
            this.Controls.Add(this.lbTimer);
            this.Controls.Add(this.lbPuntos);
            this.Controls.Add(this.plMemorama);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Memorama";
            this.Text = "Memorama";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel plMemorama;
        private System.Windows.Forms.Label lbPuntos;
        private System.Windows.Forms.Label lbTimer;
        private System.Windows.Forms.ListBox lbxPodio;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
    }
}

