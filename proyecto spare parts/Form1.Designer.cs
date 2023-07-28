namespace proyecto_spare_parts
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
			this.button1 = new System.Windows.Forms.Button();
			this.btn_jukebox = new System.Windows.Forms.PictureBox();
			this.pb_tv_stream = new System.Windows.Forms.PictureBox();
			this.pb_tv = new System.Windows.Forms.PictureBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.btn_jukebox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pb_tv_stream)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pb_tv)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 165);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Counter";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// btn_jukebox
			// 
			this.btn_jukebox.Image = global::proyecto_spare_parts.Properties.Resources.music_jukebox;
			this.btn_jukebox.Location = new System.Drawing.Point(1743, 506);
			this.btn_jukebox.Name = "btn_jukebox";
			this.btn_jukebox.Size = new System.Drawing.Size(100, 124);
			this.btn_jukebox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.btn_jukebox.TabIndex = 6;
			this.btn_jukebox.TabStop = false;
			this.btn_jukebox.Click += new System.EventHandler(this.btn_jukebox_Click);
			// 
			// pb_tv_stream
			// 
			this.pb_tv_stream.Location = new System.Drawing.Point(1282, 31);
			this.pb_tv_stream.Name = "pb_tv_stream";
			this.pb_tv_stream.Size = new System.Drawing.Size(161, 51);
			this.pb_tv_stream.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pb_tv_stream.TabIndex = 5;
			this.pb_tv_stream.TabStop = false;
			// 
			// pb_tv
			// 
			this.pb_tv.Image = global::proyecto_spare_parts.Properties.Resources.kaden_tv_wide_frame1;
			this.pb_tv.Location = new System.Drawing.Point(461, 218);
			this.pb_tv.Name = "pb_tv";
			this.pb_tv.Size = new System.Drawing.Size(196, 124);
			this.pb_tv.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pb_tv.TabIndex = 4;
			this.pb_tv.TabStop = false;
			this.pb_tv.Visible = false;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox1.Image = global::proyecto_spare_parts.Properties.Resources.pizza_place_bg;
			this.pictureBox1.Location = new System.Drawing.Point(-1, -6);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(1855, 636);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 7;
			this.pictureBox1.TabStop = false;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.LavenderBlush;
			this.ClientSize = new System.Drawing.Size(1866, 642);
			this.Controls.Add(this.btn_jukebox);
			this.Controls.Add(this.pb_tv_stream);
			this.Controls.Add(this.pb_tv);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.pictureBox1);
			this.Name = "Form1";
			this.Text = "I cook da pizza";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.btn_jukebox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pb_tv_stream)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pb_tv)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pb_tv;
        private System.Windows.Forms.PictureBox pb_tv_stream;
        private System.Windows.Forms.PictureBox btn_jukebox;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

