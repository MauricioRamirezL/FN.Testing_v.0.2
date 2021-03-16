
namespace FN.Testing.WinForm
{
    partial class ImageUploader
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
            this.imagePreview = new System.Windows.Forms.PictureBox();
            this.oImagePath = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSizeMax = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.dImageProperties = new System.Windows.Forms.TextBox();
            this.imageSaved = new System.Windows.Forms.PictureBox();
            this.oImageProperties = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbPercent = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.linkUploadedImage = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.imagePreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageSaved)).BeginInit();
            this.SuspendLayout();
            // 
            // imagePreview
            // 
            this.imagePreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imagePreview.Location = new System.Drawing.Point(4, 40);
            this.imagePreview.Name = "imagePreview";
            this.imagePreview.Size = new System.Drawing.Size(624, 306);
            this.imagePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imagePreview.TabIndex = 15;
            this.imagePreview.TabStop = false;
            // 
            // oImagePath
            // 
            this.oImagePath.Location = new System.Drawing.Point(103, 7);
            this.oImagePath.Name = "oImagePath";
            this.oImagePath.ReadOnly = true;
            this.oImagePath.Size = new System.Drawing.Size(247, 23);
            this.oImagePath.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(4, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Select image";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "JPG files | *.jpg";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(620, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 15);
            this.label2.TabIndex = 23;
            this.label2.Text = "Size lim.";
            // 
            // cmbSizeMax
            // 
            this.cmbSizeMax.FormattingEnabled = true;
            this.cmbSizeMax.Location = new System.Drawing.Point(671, 7);
            this.cmbSizeMax.Name = "cmbSizeMax";
            this.cmbSizeMax.Size = new System.Drawing.Size(66, 23);
            this.cmbSizeMax.TabIndex = 22;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(740, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 23);
            this.button2.TabIndex = 19;
            this.button2.Text = "Compress image";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dImageProperties
            // 
            this.dImageProperties.Location = new System.Drawing.Point(634, 352);
            this.dImageProperties.Multiline = true;
            this.dImageProperties.Name = "dImageProperties";
            this.dImageProperties.ReadOnly = true;
            this.dImageProperties.Size = new System.Drawing.Size(210, 180);
            this.dImageProperties.TabIndex = 18;
            // 
            // imageSaved
            // 
            this.imageSaved.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageSaved.Location = new System.Drawing.Point(4, 352);
            this.imageSaved.Name = "imageSaved";
            this.imageSaved.Size = new System.Drawing.Size(624, 306);
            this.imageSaved.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageSaved.TabIndex = 17;
            this.imageSaved.TabStop = false;
            // 
            // oImageProperties
            // 
            this.oImageProperties.Location = new System.Drawing.Point(634, 40);
            this.oImageProperties.Multiline = true;
            this.oImageProperties.Name = "oImageProperties";
            this.oImageProperties.ReadOnly = true;
            this.oImageProperties.Size = new System.Drawing.Size(210, 180);
            this.oImageProperties.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(534, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 15);
            this.label4.TabIndex = 27;
            this.label4.Text = "Perc.";
            // 
            // cmbPercent
            // 
            this.cmbPercent.FormattingEnabled = true;
            this.cmbPercent.Location = new System.Drawing.Point(567, 8);
            this.cmbPercent.Name = "cmbPercent";
            this.cmbPercent.Size = new System.Drawing.Size(55, 23);
            this.cmbPercent.TabIndex = 26;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(684, 274);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(104, 23);
            this.button3.TabIndex = 28;
            this.button3.Text = "Upload image";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // linkUploadedImage
            // 
            this.linkUploadedImage.AutoSize = true;
            this.linkUploadedImage.Location = new System.Drawing.Point(634, 549);
            this.linkUploadedImage.Name = "linkUploadedImage";
            this.linkUploadedImage.Size = new System.Drawing.Size(110, 15);
            this.linkUploadedImage.TabIndex = 29;
            this.linkUploadedImage.TabStop = true;
            this.linkUploadedImage.Text = "linkUploadedImage";
            this.linkUploadedImage.Visible = false;
            this.linkUploadedImage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkUploadedImage_LinkClicked);
            // 
            // ImageUploader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 665);
            this.Controls.Add(this.linkUploadedImage);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbPercent);
            this.Controls.Add(this.imagePreview);
            this.Controls.Add(this.oImagePath);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbSizeMax);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dImageProperties);
            this.Controls.Add(this.imageSaved);
            this.Controls.Add(this.oImageProperties);
            this.Name = "ImageUploader";
            this.Text = "Image Uploader";
            this.Load += new System.EventHandler(this.ImageUploader_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imagePreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageSaved)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imagePreview;
        private System.Windows.Forms.TextBox oImagePath;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSizeMax;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox dImageProperties;
        private System.Windows.Forms.PictureBox imageSaved;
        private System.Windows.Forms.TextBox oImageProperties;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbPercent;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.LinkLabel linkUploadedImage;
    }
}