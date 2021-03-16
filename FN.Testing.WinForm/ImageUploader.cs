using FN.Test.Functions;
using FN.Testing.Entities.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FN.Testing.WinForm
{
    public partial class ImageUploader : Form
    {
        string UploadPath = string.Empty;
        public ImageUploader()
        {
            InitializeComponent();
        }
        private void ImageUploader_Load(object sender, EventArgs e)
        {
            UploadPath = System.Configuration.ConfigurationManager.AppSettings["UploadPath"];
            button2.Enabled = button1.Enabled = !string.IsNullOrEmpty(UploadPath);
            cmbSizeMax.Items.AddRange(new ComboBoxItem[] {
                new ComboBoxItem{ Text = "100Kb", Value = 102400},
                new ComboBoxItem{ Text = "200Kb", Value = 204800},
                new ComboBoxItem{ Text = "300Kb", Value = 307200}
            });
            cmbSizeMax.SelectedIndex = 2;

            cmbPercent.Items.AddRange(new ComboBoxItem[] {
                new ComboBoxItem{ Text = "100%", Value = 1.00 },
                new ComboBoxItem{ Text = "75%", Value = 0.75 },
                new ComboBoxItem{ Text = "50%", Value = 0.50 },
                new ComboBoxItem{ Text = "25%", Value = 0.25 }
            });
            cmbPercent.SelectedIndex = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                oImagePath.Text = openFileDialog.FileName;
                ShowImage(oImagePath.Text, imagePreview, oImageProperties);
            }
        }        
        private void ShowImage(string imagePath, PictureBox picturePreview, TextBox propertiesPreview)
        {
            if (picturePreview.Image != null) picturePreview.Image.Dispose();
            StringBuilder imgProperties = new StringBuilder();
            using (Image imgTemp = Image.FromFile(imagePath))
            {
                Bitmap img = new Bitmap(imgTemp);
                picturePreview.Image = img;
                ImageFormat format = img.RawFormat;
                imgProperties.AppendLine(string.Concat("Image Type : ", Path.GetExtension(imagePath).ToUpper()));
                imgProperties.AppendLine(string.Concat("Image width : ", img.Width.ToString()));
                imgProperties.AppendLine(string.Concat("Image height : ", img.Height.ToString()));
                imgProperties.AppendLine(string.Concat("Image resolution : ", (img.VerticalResolution * img.HorizontalResolution).ToString()));
                imgProperties.AppendLine(string.Concat("Image Pixel depth : ", Image.GetPixelFormatSize(img.PixelFormat).ToString()));
                imgProperties.AppendLine(string.Concat("Image size : ", new Generic().ConvertSize(imagePath)));
                propertiesPreview.Text = imgProperties.ToString();
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            string compressedImagePath;
            string ext = Path.GetExtension(oImagePath.Text).ToUpper();
            if (ext == ".PNG" || ext == ".JPG")
            {
                Image image = LoadImageFromPath(oImagePath.Text);
                image = new ImageResizer().ResizeImage(image, 
                    (int)(image.Width * (float)((ComboBoxItem)cmbPercent.SelectedItem).Value),
                    (int)(image.Height * (float)((ComboBoxItem)cmbPercent.SelectedItem).Value));
                compressedImagePath = new Generic().SaveImageToFile(image, UploadPath);
                compressedImagePath = new ImageResizer().ScaleImage(compressedImagePath, (int)((ComboBoxItem)cmbSizeMax.SelectedItem).Value, true);
                ShowImage(compressedImagePath, imageSaved, dImageProperties);
            }
        }
        private Image LoadImageFromPath(string SoucePath)
        {
            Bitmap bmp1;
            using (Bitmap bmpTmp = new Bitmap(SoucePath))
            {
                bmp1 = new Bitmap(bmpTmp);
            }
            return bmp1;
        }
        private string GetNewFilePath(string rootPath, bool fullPath = true)
        {
            return Path.Combine(
                fullPath ? rootPath : string.Empty, 
                string.Concat(Guid.NewGuid().ToString(), ".jpg")
            );
        }
        private async void button3_Click(object sender, EventArgs e)
        {
            string uploadUri = System.Configuration.ConfigurationManager.AppSettings["WebServiceUri"];
            imageSaved.Image = null;
            linkUploadedImage.Visible = false;
            byte[] bytes = File.ReadAllBytes(oImagePath.Text);
            string file = Convert.ToBase64String(bytes);
            Upload upload = new Upload
            {
                File = file,
                FileName = GetNewFilePath(string.Empty, false),
                UploadDate = DateTime.Now
            };

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(uploadUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("uploads", upload);
            response.EnsureSuccessStatusCode();
            linkUploadedImage.Text = response.Content.ReadAsStringAsync().Result.Replace("\"", string.Empty);
            linkUploadedImage.Visible = true;
            //MessageBox.Show(linkUploadedImage.Text);
            imageSaved.Load(linkUploadedImage.Text);
        }

        private void linkUploadedImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo psInfo = new ProcessStartInfo
            {
                FileName = linkUploadedImage.Text,
                UseShellExecute = true
            };
            Process.Start(psInfo);
            //System.Diagnostics.Process.Start(linkUploadedImage.Text);
        }
    }
}
