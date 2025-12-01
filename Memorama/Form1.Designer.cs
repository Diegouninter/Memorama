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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // plMemorama
            // 
            this.plMemorama.Location = new System.Drawing.Point(13, 11);
            this.plMemorama.Name = "plMemorama";
            this.plMemorama.Size = new System.Drawing.Size(562, 428);
            this.plMemorama.TabIndex = 0;
            // 
            // lbPuntos
            // 
            this.lbPuntos.AutoSize = true;
            this.lbPuntos.Location = new System.Drawing.Point(612, 23);
            this.lbPuntos.Name = "lbPuntos";
            this.lbPuntos.Size = new System.Drawing.Size(52, 13);
            this.lbPuntos.TabIndex = 1;
            this.lbPuntos.Text = "Puntos: 0";
            // 
            // lbTimer
            // 
            this.lbTimer.AutoSize = true;
            this.lbTimer.Location = new System.Drawing.Point(612, 47);
            this.lbTimer.Name = "lbTimer";
            this.lbTimer.Size = new System.Drawing.Size(54, 13);
            this.lbTimer.TabIndex = 2;
            this.lbTimer.Text = "Tiempo: 0";
            // 
            // lbxPodio
            // 
            this.lbxPodio.FormattingEnabled = true;
            this.lbxPodio.Location = new System.Drawing.Point(593, 84);
            this.lbxPodio.Name = "lbxPodio";
            this.lbxPodio.Size = new System.Drawing.Size(165, 95);
            this.lbxPodio.TabIndex = 3;
            // 
            // Memorama
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbxPodio);
            this.Controls.Add(this.lbTimer);
            this.Controls.Add(this.lbPuntos);
            this.Controls.Add(this.plMemorama);
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
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
    }
}

