using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing.QrCode;

namespace PharmacyStore
{
    public partial class BarcodeScannerForm : Form
    {

        public BarcodeScannerForm()
        {
            InitializeComponent();
        }
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;

        private void BarcodeScannerForm_Load(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Device in filterInfoCollection)
                comboBox1.Items.Add(Device.Name);
            comboBox1.SelectedIndex = 0;
            videoCaptureDevice = new VideoCaptureDevice();
        }

        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[comboBox1.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += FinalFrame_NewFrame;
            videoCaptureDevice.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            QRCodeReader Reader = new QRCodeReader();
            //BarcodeReader Reader = new BarcodeReader();
            Result result = Reader.decode(pictureBox1.Image as );
            if (result != null)
                result_TextBox.Text = result.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
        }

        private void BarcodeScannerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoCaptureDevice.IsRunning == true)
                videoCaptureDevice.Stop();
        }
    }
}
