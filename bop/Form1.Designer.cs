
namespace bop
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.bhopBox = new System.Windows.Forms.CheckBox();
            this.triggerBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.flashBox = new System.Windows.Forms.CheckBox();
            this.radarBox = new System.Windows.Forms.CheckBox();
            this.topBox = new System.Windows.Forms.CheckBox();
            this.thirdBox = new System.Windows.Forms.CheckBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.glowBox = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(390, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "1337 ch347";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // bhopBox
            // 
            this.bhopBox.AutoSize = true;
            this.bhopBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bhopBox.Location = new System.Drawing.Point(12, 41);
            this.bhopBox.Name = "bhopBox";
            this.bhopBox.Size = new System.Drawing.Size(75, 17);
            this.bhopBox.TabIndex = 1;
            this.bhopBox.Text = "auto bhop";
            this.bhopBox.UseVisualStyleBackColor = true;
            this.bhopBox.CheckedChanged += new System.EventHandler(this.bhopBox_CheckedChanged);
            // 
            // triggerBox
            // 
            this.triggerBox.AutoSize = true;
            this.triggerBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.triggerBox.Location = new System.Drawing.Point(12, 64);
            this.triggerBox.Name = "triggerBox";
            this.triggerBox.Size = new System.Drawing.Size(77, 17);
            this.triggerBox.TabIndex = 3;
            this.triggerBox.Text = "trigger bot";
            this.triggerBox.UseVisualStyleBackColor = true;
            this.triggerBox.CheckedChanged += new System.EventHandler(this.triggerBox_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 163);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(367, 40);
            this.button1.TabIndex = 4;
            this.button1.Text = "attach to csgo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "NOT ATTACHED";
            // 
            // flashBox
            // 
            this.flashBox.AutoSize = true;
            this.flashBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flashBox.Location = new System.Drawing.Point(12, 87);
            this.flashBox.Name = "flashBox";
            this.flashBox.Size = new System.Drawing.Size(64, 17);
            this.flashBox.TabIndex = 6;
            this.flashBox.Text = "no flash";
            this.flashBox.UseVisualStyleBackColor = true;
            this.flashBox.CheckedChanged += new System.EventHandler(this.flashBox_CheckedChanged);
            // 
            // radarBox
            // 
            this.radarBox.AutoSize = true;
            this.radarBox.Location = new System.Drawing.Point(106, 42);
            this.radarBox.Name = "radarBox";
            this.radarBox.Size = new System.Drawing.Size(77, 17);
            this.radarBox.TabIndex = 7;
            this.radarBox.Text = "radar hack";
            this.radarBox.UseVisualStyleBackColor = true;
            this.radarBox.CheckedChanged += new System.EventHandler(this.radarBox_CheckedChanged);
            // 
            // topBox
            // 
            this.topBox.AutoSize = true;
            this.topBox.Checked = true;
            this.topBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.topBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.topBox.Location = new System.Drawing.Point(286, 143);
            this.topBox.Name = "topBox";
            this.topBox.Size = new System.Drawing.Size(93, 17);
            this.topBox.TabIndex = 8;
            this.topBox.Text = "always on top";
            this.topBox.UseVisualStyleBackColor = true;
            this.topBox.CheckedChanged += new System.EventHandler(this.topBox_CheckedChanged);
            // 
            // thirdBox
            // 
            this.thirdBox.AutoSize = true;
            this.thirdBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thirdBox.Location = new System.Drawing.Point(106, 64);
            this.thirdBox.Name = "thirdBox";
            this.thirdBox.Size = new System.Drawing.Size(84, 17);
            this.thirdBox.TabIndex = 9;
            this.thirdBox.Text = "third person";
            this.thirdBox.UseVisualStyleBackColor = true;
            // 
            // trackBar1
            // 
            this.trackBar1.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.trackBar1.Location = new System.Drawing.Point(190, 42);
            this.trackBar1.Maximum = 360;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(199, 45);
            this.trackBar1.SmallChange = 5;
            this.trackBar1.TabIndex = 1;
            this.trackBar1.Value = 90;
            // 
            // glowBox
            // 
            this.glowBox.AutoSize = true;
            this.glowBox.Location = new System.Drawing.Point(106, 87);
            this.glowBox.Name = "glowBox";
            this.glowBox.Size = new System.Drawing.Size(109, 17);
            this.glowBox.TabIndex = 12;
            this.glowBox.Text = "glow doesnt work";
            this.glowBox.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(222, 69);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(41, 17);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "fov";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(390, 208);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.glowBox);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.thirdBox);
            this.Controls.Add(this.topBox);
            this.Controls.Add(this.radarBox);
            this.Controls.Add(this.flashBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.triggerBox);
            this.Controls.Add(this.bhopBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "csgo cheat";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox bhopBox;
        private System.Windows.Forms.CheckBox triggerBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox flashBox;
        private System.Windows.Forms.CheckBox radarBox;
        private System.Windows.Forms.CheckBox topBox;
        private System.Windows.Forms.CheckBox thirdBox;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.CheckBox glowBox;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

