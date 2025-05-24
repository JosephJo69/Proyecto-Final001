namespace Proyecto001_WF
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
            pictureBox1 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            TB_Prompt = new TextBox();
            btnBuscar = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.pngimg_com___chatgpt_PNG2;
            pictureBox1.Location = new Point(188, 9);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(346, 106);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial Narrow", 72F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(535, 9);
            label1.Name = "label1";
            label1.Size = new Size(78, 106);
            label1.TabIndex = 1;
            label1.Text = "2";
            // 
            // label2
            // 
            label2.BackColor = Color.FromArgb(128, 255, 255);
            label2.Font = new Font("Arial Rounded MT Bold", 26.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(188, 95);
            label2.Name = "label2";
            label2.Size = new Size(96, 42);
            label2.TabIndex = 2;
            label2.Text = "Beta";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(0, 0);
            label3.Name = "label3";
            label3.Size = new Size(38, 15);
            label3.TabIndex = 3;
            label3.Text = "label3";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = SystemColors.ButtonHighlight;
            label4.Location = new Point(259, 169);
            label4.Name = "label4";
            label4.Size = new Size(263, 22);
            label4.TabIndex = 4;
            label4.Text = "¿Con qué puedo ayudarte?";
            // 
            // TB_Prompt
            // 
            TB_Prompt.BackColor = SystemColors.WindowFrame;
            TB_Prompt.Location = new Point(188, 211);
            TB_Prompt.Name = "TB_Prompt";
            TB_Prompt.Size = new Size(425, 23);
            TB_Prompt.TabIndex = 5;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(358, 240);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(75, 23);
            btnBuscar.TabIndex = 6;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_ClickAsync;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(800, 450);
            Controls.Add(btnBuscar);
            Controls.Add(TB_Prompt);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox TB_Prompt;
        private Button btnBuscar;
    }
}
