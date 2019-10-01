using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessingWinFormCoreCSharp
{
    public partial class FormHistgram : Form
    {
        private int[,] m_nHistgram = new int[(int)ComInfo.PictureType.MAX, ComInfo.RGB_MAX];
        private Bitmap m_bitmapOrg;
        private Bitmap m_bitmapAfter;
        private bool m_bIsOpen;

        public Bitmap BitmapOrg
        {
            set { m_bitmapOrg = value; }
            get { return m_bitmapOrg; }
        }

        public Bitmap BitmapAfter
        {
            set { m_bitmapAfter = value; }
            get { return m_bitmapAfter; }
        }

        public bool IsOpen
        {
            set { m_bIsOpen = value; }
            get { return m_bIsOpen; }
        }

        public FormHistgram()
        {
            InitializeComponent();

            chart.ContextMenuStrip = contextMenu;
        }

        ~FormHistgram()
        {
            m_bitmapOrg.Dispose();
            m_bitmapAfter.Dispose();
            m_bIsOpen = false;
        }

        public void DrawHistgram()
        {
            InitHistgram();

            CalHistgram();

            LineSeries lineSeriesChart1 = new LineSeries()
            {
                Values = new ChartValues<int>(),
                Title = "Original Image"
            };
            LineSeries lineSeriesChart2 = new LineSeries()
            {
                Values = new ChartValues<int>(),
                Title = "After Image"
            };

            for (int nIdx = 0; nIdx < (m_nHistgram.Length >> 1); nIdx++)
            {
                lineSeriesChart1.Values.Add(m_nHistgram[(int)ComInfo.PictureType.Original, nIdx]);
                lineSeriesChart2.Values.Add(m_nHistgram[(int)ComInfo.PictureType.After, nIdx]);
            }
            chart.Series.Clear();
            //chart.Series.Add(lineSeriesChart1);
            //chart.Series.Add(lineSeriesChart2);

            return;
        }

        public void CalHistgram()
        {
            int nWidthSize = m_bitmapOrg.Width;
            int nHeightSize = m_bitmapOrg.Height;

            BitmapData bitmapDataOrg = m_bitmapOrg.LockBits(new Rectangle(0, 0, nWidthSize, nHeightSize), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            BitmapData bitmapDataAfter = null;
            if (m_bitmapAfter != null)
            {
                bitmapDataAfter = m_bitmapAfter.LockBits(new Rectangle(0, 0, nWidthSize, nHeightSize), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            }

            int nIdxWidth;
            int nIdxHeight;

            unsafe
            {
                for (nIdxHeight = 0; nIdxHeight < nHeightSize; nIdxHeight++)
                {
                    for (nIdxWidth = 0; nIdxWidth < nWidthSize; nIdxWidth++)
                    {
                        byte* pPixel = (byte*)bitmapDataOrg.Scan0 + nIdxHeight * bitmapDataOrg.Stride + nIdxWidth * 4;
                        byte nGrayScale = (byte)((pPixel[(int)ComInfo.Pixel.B] + pPixel[(int)ComInfo.Pixel.G] + pPixel[(int)ComInfo.Pixel.R]) / 3);

                        m_nHistgram[(int)ComInfo.PictureType.Original, nGrayScale] += 1;

                        if (m_bitmapAfter != null)
                        {
                            pPixel = (byte*)bitmapDataAfter.Scan0 + nIdxHeight * bitmapDataAfter.Stride + nIdxWidth * 4;
                            nGrayScale = (byte)((pPixel[(int)ComInfo.Pixel.B] + pPixel[(int)ComInfo.Pixel.G] + pPixel[(int)ComInfo.Pixel.R]) / 3);

                            m_nHistgram[(int)ComInfo.PictureType.After, nGrayScale] += 1;
                        }
                    }
                }
                m_bitmapOrg.UnlockBits(bitmapDataOrg);
                if (m_bitmapAfter != null)
                {
                    m_bitmapAfter.UnlockBits(bitmapDataAfter);
                }
            }
        }

        public void InitHistgram()
        {
            for (int nIdx = 0; nIdx < (m_nHistgram.Length >> 1); nIdx++)
            {
                m_nHistgram[(int)ComInfo.PictureType.Original, nIdx] = 0;
                m_nHistgram[(int)ComInfo.PictureType.After, nIdx] = 0;
            }
        }

        private void OnClickMenu(object sender, EventArgs e)
        {
            string strHeader = sender.ToString();

            switch (strHeader)
            {
                case ComInfo.MENU_SAVE_CSV:
                    SaveCsv();
                    break;
                default:
                    break;
            }

            return;
        }

        public void SaveCsv()
        {
            ComSaveFileDialog saveDialog = new ComSaveFileDialog();
            saveDialog.Filter = "CSV|*.csv";
            saveDialog.Title = "Save the csv file";
            saveDialog.FileName = "default.csv";
            if (saveDialog.ShowDialog() == true)
            {
                String strDelmiter = ",";
                StringBuilder stringBuilder = new StringBuilder();
                for (int nIdx = 0; nIdx < (m_nHistgram.Length >> 1); nIdx++)
                {
                    stringBuilder.Append(nIdx).Append(strDelmiter);
                    stringBuilder.Append(m_nHistgram[(int)ComInfo.PictureType.Original, nIdx]).Append(strDelmiter);
                    stringBuilder.Append(m_nHistgram[(int)ComInfo.PictureType.After, nIdx]).Append(strDelmiter);
                    stringBuilder.Append(Environment.NewLine);
                }
                if (!saveDialog.StreamWrite(stringBuilder.ToString()))
                {
                    MessageBox.Show(this, "Save CSV File Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            return;
        }
    }
}