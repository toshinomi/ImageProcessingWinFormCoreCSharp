using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;

namespace ImageProcessingWinFormCoreCSharp
{
    public partial class FormMain : Form
    {
        private Bitmap m_bitmap;
        private object m_imgProc;
        private string m_strOpenFileName;
        private CancellationTokenSource m_tokenSource;
        private string m_strCurImgName;
        private FormHistgramOxyPlot m_histgram;

        public FormMain()
        {
            InitializeComponent();

            btnFileSelect.Enabled = true;
            btnAllClear.Enabled = true;
            btnStart.Enabled = false;
            btnStop.Enabled = false;
            btnSaveImage.Enabled = false;
            btnShowHistgram.Enabled = false;

            pictureBoxStatus.Visible = false;

            SetToolTip();

            m_bitmap = null;
            m_tokenSource = null;
            m_imgProc = null;

            m_strCurImgName = Properties.Settings.Default.ImgTypeSelectName;
            this.Text = "Image Processing ( " + m_strCurImgName + " )";

            sliderThresh.Enabled = m_strCurImgName == ComInfo.IMG_NAME_BINARIZATION ? true : false;
        }

        ~FormMain()
        {
            m_bitmap = null;
            m_tokenSource = null;
            m_imgProc = null;
        }

        public bool SelectLoadImage(string _strImgName)
        {
            bool bRst = true;

            if (m_imgProc != null)
            {
                m_imgProc = null;
            }

            switch (_strImgName)
            {
                case ComInfo.IMG_NAME_EDGE_DETECTION:
                    m_imgProc = new EdgeDetection(m_bitmap);
                    break;
                case ComInfo.IMG_NAME_GRAY_SCALE:
                    m_imgProc = new GrayScale(m_bitmap);
                    break;
                case ComInfo.IMG_NAME_BINARIZATION:
                    m_imgProc = new Binarization(m_bitmap);
                    break;
                case ComInfo.IMG_NAME_GRAY_SCALE_2DIFF:
                    m_imgProc = new GrayScale2Diff(m_bitmap);
                    break;
                case ComInfo.IMG_NAME_COLOR_REVERSAL:
                    m_imgProc = new ColorReversal(m_bitmap);
                    break;
                default:
                    break;
            }

            return bRst;
        }

        public Bitmap SelectGetBitmap(string _strImgName)
        {
            Bitmap bitmap = null;

            switch (_strImgName)
            {
                case ComInfo.IMG_NAME_EDGE_DETECTION:
                    EdgeDetection edge = (EdgeDetection)m_imgProc;
                    bitmap = edge.BitmapAfter;
                    break;
                case ComInfo.IMG_NAME_GRAY_SCALE:
                    GrayScale gray = (GrayScale)m_imgProc;
                    bitmap = gray.BitmapAfter;
                    break;
                case ComInfo.IMG_NAME_BINARIZATION:
                    Binarization binarization = (Binarization)m_imgProc;
                    bitmap = binarization.BitmapAfter;
                    break;
                case ComInfo.IMG_NAME_GRAY_SCALE_2DIFF:
                    GrayScale2Diff gray2Diff = (GrayScale2Diff)m_imgProc;
                    bitmap = gray2Diff.BitmapAfter;
                    break;
                case ComInfo.IMG_NAME_COLOR_REVERSAL:
                    ColorReversal colorReversal = (ColorReversal)m_imgProc;
                    bitmap = colorReversal.BitmapAfter;
                    break;
                default:
                    break;
            }

            return bitmap;
        }

        public bool SelectGoImgProc(ComImgInfo _comImgInfo, CancellationToken _token)
        {
            bool bRst = true;

            switch (_comImgInfo.CurImgName)
            {
                case ComInfo.IMG_NAME_EDGE_DETECTION:
                    EdgeDetection edge = (EdgeDetection)m_imgProc;
                    bRst = edge.GoImgProc(_token);
                    break;
                case ComInfo.IMG_NAME_GRAY_SCALE:
                    GrayScale gray = (GrayScale)m_imgProc;
                    bRst = gray.GoImgProc(_token);
                    break;
                case ComInfo.IMG_NAME_BINARIZATION:
                    Binarization binarization = (Binarization)m_imgProc;
                    binarization.Thresh = _comImgInfo.BinarizationInfo.Thresh;
                    bRst = binarization.GoImgProc(_token);
                    break;
                case ComInfo.IMG_NAME_GRAY_SCALE_2DIFF:
                    GrayScale2Diff gray2Diff = (GrayScale2Diff)m_imgProc;
                    bRst = gray2Diff.GoImgProc(_token);
                    break;
                case ComInfo.IMG_NAME_COLOR_REVERSAL:
                    ColorReversal colorReversal = (ColorReversal)m_imgProc;
                    bRst = colorReversal.GoImgProc(_token);
                    break;
                default:
                    break;
            }

            return bRst;
        }

        public void SetToolTip()
        {
            toolTipBtnFileSelect.InitialDelay = 1000;
            toolTipBtnFileSelect.ReshowDelay = 1000;
            toolTipBtnFileSelect.AutoPopDelay = 10000;
            toolTipBtnFileSelect.ShowAlways = false;
            toolTipBtnFileSelect.SetToolTip(btnFileSelect, "Please select a file to open.\nDisplay the image on the original image.");

            toolTipBtnAllClear.InitialDelay = 1000;
            toolTipBtnAllClear.ReshowDelay = 1000;
            toolTipBtnAllClear.AutoPopDelay = 10000;
            toolTipBtnAllClear.ShowAlways = false;
            toolTipBtnAllClear.SetToolTip(btnAllClear, "Clear the display.");

            toolTipBtnStart.InitialDelay = 1000;
            toolTipBtnStart.ReshowDelay = 1000;
            toolTipBtnStart.AutoPopDelay = 10000;
            toolTipBtnStart.ShowAlways = false;
            toolTipBtnStart.SetToolTip(btnStart, "Perform unstable filter processing.");

            toolTipBtnStop.InitialDelay = 1000;
            toolTipBtnStop.ReshowDelay = 1000;
            toolTipBtnStop.AutoPopDelay = 10000;
            toolTipBtnStop.ShowAlways = false;
            toolTipBtnStop.SetToolTip(btnStop, "Processing stop.");

            toolTipBtnSaveImage.InitialDelay = 1000;
            toolTipBtnSaveImage.ReshowDelay = 1000;
            toolTipBtnSaveImage.AutoPopDelay = 10000;
            toolTipBtnSaveImage.ShowAlways = false;
            toolTipBtnSaveImage.SetToolTip(btnSaveImage, "Saving image.");

            toolTipBtnShowHistgram.InitialDelay = 1000;
            toolTipBtnShowHistgram.ReshowDelay = 1000;
            toolTipBtnShowHistgram.AutoPopDelay = 10000;
            toolTipBtnShowHistgram.ShowAlways = false;
            toolTipBtnShowHistgram.SetToolTip(btnShowHistgram, "Show Histgram.");

            return;
        }

        public void SetButtonEnable()
        {
            btnFileSelect.Enabled = true;
            btnAllClear.Enabled = true;
            btnStart.Enabled = true;
            btnStop.Enabled = false;

            return;
        }

        public void SetTextTime(long _lTime)
        {
            textBoxTime.Text = _lTime.ToString();

            return;
        }

        public void SetPictureBoxStatus()
        {
            pictureBoxStatus.Visible = false;

            return;
        }

        public async Task<bool> TaskWorkImageProcessing()
        {
            m_tokenSource = new CancellationTokenSource();
            CancellationToken token = m_tokenSource.Token;
            ComImgInfo imgInfo = new ComImgInfo();
            ComBinarizationInfo binarizationInfo = new ComBinarizationInfo();
            binarizationInfo.Thresh = (byte)sliderThresh.Value;
            imgInfo.CurImgName = m_strCurImgName;
            imgInfo.BinarizationInfo = binarizationInfo;
            bool bRst = await Task.Run(() => SelectGoImgProc(imgInfo, token));
            return bRst;
        }

        public void LoadImage()
        {
            m_bitmap = new Bitmap(m_strOpenFileName);
            SelectLoadImage(m_strCurImgName);

            return;
        }

        public void OnFormClosingFormMain(object sender, FormClosingEventArgs e)
        {
            if (m_tokenSource != null)
            {
                e.Cancel = true;
            }

            if (m_histgram != null)
            {
                m_histgram.Close();
                m_histgram = null;
            }

            return;
        }

        public void OnClickBtnFileSelect(object sender, EventArgs e)
        {
            ComOpenFileDialog openFileDlg = new ComOpenFileDialog();
            openFileDlg.Filter = "JPG|*.jpg|PNG|*.png";
            openFileDlg.Title = "Open the file";
            if (openFileDlg.ShowDialog() == true)
            {
                pictureBoxOriginal.Image = null;
                pictureBoxAfter.Image = null;
                m_strOpenFileName = openFileDlg.FileName;
                try
                {
                    LoadImage();
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "Open File Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                pictureBoxOriginal.ImageLocation = m_strOpenFileName;
                btnStart.Enabled = true;
                textBoxTime.Text = "";

                if (m_histgram == null)
                {
                    m_histgram = new FormHistgramOxyPlot();
                }
                else
                {
                    m_histgram.Close();
                    m_histgram = null;
                    m_histgram = new FormHistgramOxyPlot();
                }

                m_histgram.BitmapOrg = (Bitmap)new Bitmap(m_strOpenFileName).Clone();
                if (SelectGetBitmap(m_strCurImgName) != null)
                {
                    m_histgram.BitmapAfter = (Bitmap)SelectGetBitmap(m_strCurImgName).Clone();
                }
                m_histgram.DrawHistgram();
                m_histgram.IsOpen = true;
                m_histgram.Show();
            }
            return;
        }

        private void OnClickBtnAllClear(object sender, EventArgs e)
        {
            pictureBoxOriginal.ImageLocation = null;
            pictureBoxAfter.Image = null;

            m_bitmap = null;
            m_strOpenFileName = "";

            textBoxTime.Text = "";

            btnFileSelect.Enabled = true;
            btnAllClear.Enabled = true;
            btnStart.Enabled = false;
            btnSaveImage.Enabled = false;

            if (m_histgram != null)
            {
                m_histgram.Close();
            }

            return;
        }

        private async void OnClickBtnStart(object sender, EventArgs e)
        {
            pictureBoxAfter.Image = null;

            btnFileSelect.Enabled = false;
            btnAllClear.Enabled = false;
            btnStart.Enabled = false;
            menuMain.Enabled = false;

            textBoxTime.Text = "";

            pictureBoxStatus.Visible = true;

            LoadImage();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            btnStop.Enabled = true;
            btnSaveImage.Enabled = false;
            btnShowHistgram.Enabled = false;
            bool bResult = await TaskWorkImageProcessing();
            if (bResult)
            {
                pictureBoxOriginal.ImageLocation = m_strOpenFileName;
                pictureBoxAfter.Image = SelectGetBitmap(m_strCurImgName);

                stopwatch.Stop();

                Invoke(new Action<long>(SetTextTime), stopwatch.ElapsedMilliseconds);
                btnSaveImage.Enabled = true;

                m_histgram.BitmapOrg = (Bitmap)new Bitmap(m_strOpenFileName).Clone();
                if (SelectGetBitmap(m_strCurImgName) != null)
                {
                    m_histgram.BitmapAfter = (Bitmap)SelectGetBitmap(m_strCurImgName).Clone();
                }
                if (m_histgram.IsOpen == true)
                {
                    m_histgram.DrawHistgram();
                }
            }
            Invoke(new Action(SetPictureBoxStatus));
            Invoke(new Action(SetButtonEnable));
            menuMain.Enabled = true;
            btnShowHistgram.Enabled = true;

            stopwatch = null;
            m_tokenSource = null;

            return;
        }

        private void OnClickBtnStop(object sender, EventArgs e)
        {
            if (m_tokenSource != null)
            {
                m_tokenSource.Cancel();
            }

            return;
        }

        public Bitmap GetImage(string _strImgName)
        {
            Bitmap bitmap = null;
            switch (m_strCurImgName)
            {
                case ComInfo.IMG_NAME_EDGE_DETECTION:
                    EdgeDetection edge = (EdgeDetection)m_imgProc;
                    if (edge != null)
                    {
                        bitmap = edge.BitmapAfter;
                    }
                    break;
                case ComInfo.IMG_NAME_GRAY_SCALE:
                    GrayScale gray = (GrayScale)m_imgProc;
                    if (gray != null)
                    {
                        bitmap = gray.BitmapAfter;
                    }
                    break;
                case ComInfo.IMG_NAME_BINARIZATION:
                    Binarization binarization = (Binarization)m_imgProc;
                    if (binarization != null)
                    {
                        bitmap = binarization.BitmapAfter;
                    }
                    break;
                case ComInfo.IMG_NAME_GRAY_SCALE_2DIFF:
                    GrayScale2Diff gray2Diff = (GrayScale2Diff)m_imgProc;
                    if (gray2Diff != null)
                    {
                        bitmap = gray2Diff.BitmapAfter;
                    }
                    break;
                case ComInfo.IMG_NAME_COLOR_REVERSAL:
                    ColorReversal colorReversal = (ColorReversal)m_imgProc;
                    if (colorReversal != null)
                    {
                        bitmap = colorReversal.BitmapAfter;
                    }
                    break;
                default:
                    break;
            }

            return bitmap == null ? bitmap : (Bitmap)bitmap.Clone();
        }

        private void OnClickBtnSaveImage(object sender, EventArgs e)
        {
            ComSaveFileDialog saveDialog = new ComSaveFileDialog();
            saveDialog.Filter = "PNG|*.png";
            saveDialog.Title = "Save the file";
            if (saveDialog.ShowDialog() == true)
            {
                string strFileName = saveDialog.FileName;
                var bitmap = GetImage(m_strCurImgName);
                if (bitmap != null)
                {
                    try
                    {
                        bitmap.Save(strFileName, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(this, "Save Image File Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    bitmap.Dispose();
                }
            }

            return;
        }

        private void OnClickBtnShowHistgram(object sender, EventArgs e)
        {
            if (m_bitmap == null)
            {
                return;
            }

            if (m_histgram != null)
            {
                m_histgram.Close();
                m_histgram = null;
                m_histgram = new FormHistgramOxyPlot();
            }

            m_histgram.BitmapOrg = (Bitmap)new Bitmap(m_strOpenFileName).Clone();
            if (SelectGetBitmap(m_strCurImgName) != null)
            {
                m_histgram.BitmapAfter = (Bitmap)SelectGetBitmap(m_strCurImgName).Clone();
            }
            m_histgram.DrawHistgram();
            m_histgram.IsOpen = true;
            m_histgram.Show();

            return;
        }

        private void OnClickMenu(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            string strText = menuItem.Text;

            switch (strText)
            {
                case ComInfo.MENU_FILE_END:
                    Close();
                    break;
                case ComInfo.MENU_SETTING_IMAGE_PROCESSING:
                    ShowSettingImageProcessing();
                    break;
                default:
                    break;
            }

            return;
        }

        public void ShowSettingImageProcessing()
        {
            FormSettingImageProcessing win = new FormSettingImageProcessing();
            var dialogResult = win.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                m_strCurImgName = (string)win.CmbBoxImageProcessingType.SelectedItem;
                this.Text = "Image Processing ( " + m_strCurImgName + " )";

                sliderThresh.Enabled = m_strCurImgName == ComInfo.IMG_NAME_BINARIZATION ? true : false;

                pictureBoxAfter.Image = null;
                btnSaveImage.Enabled = false;
                SelectLoadImage(m_strCurImgName);
                if (m_histgram != null && m_histgram.IsOpen == true)
                {
                    OnClickBtnShowHistgram(this, null);
                }
            }

            return;
        }

        private void OnScrollSliderThresh(object sender, EventArgs e)
        {
            var trackBar = (TrackBar)sender;
            labelValue.Text = trackBar.Value.ToString();
        }

        private void OnSliderPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (pictureBoxAfter.Image != null)
            {
                ParamAjust();
            }
        }

        private void OnSliderMouseUp(object sender, MouseEventArgs e)
        {
            if (pictureBoxAfter.Image != null)
            {
                ParamAjust();
            }
        }

        private async void ParamAjust()
        {
            pictureBoxAfter.Image = null;

            btnFileSelect.Enabled = false;
            btnAllClear.Enabled = false;
            btnStart.Enabled = false;
            menuMain.Enabled = false;

            LoadImage();

            btnStop.Enabled = true;
            btnSaveImage.Enabled = false;
            bool bResult = await TaskWorkImageProcessing();
            if (bResult)
            {
                pictureBoxOriginal.ImageLocation = m_strOpenFileName;
                pictureBoxAfter.Image = SelectGetBitmap(m_strCurImgName);

                btnSaveImage.Enabled = true;

                m_histgram.BitmapOrg = (Bitmap)new Bitmap(m_strOpenFileName).Clone();
                if (SelectGetBitmap(m_strCurImgName) != null)
                {
                    m_histgram.BitmapAfter = (Bitmap)SelectGetBitmap(m_strCurImgName).Clone();
                }
                if (m_histgram.IsOpen == true)
                {
                    m_histgram.DrawHistgram();
                }
            }
            Invoke(new Action(SetButtonEnable));
            menuMain.Enabled = true;
            btnShowHistgram.Enabled = true;

            m_tokenSource = null;

            return;
        }
    }
}