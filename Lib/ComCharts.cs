using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

abstract class ComCharts
{
    protected int[,] m_nHistgram;
    protected Bitmap m_bitmapOrg;
    protected Bitmap m_bitmapAfter;

    public ComCharts()
    {
        m_nHistgram = new int[(int)ComInfo.PictureType.MAX, ComInfo.RGB_MAX];
    }

    ~ComCharts()
    {
    }

    public void CalHistgram()
    {
        int nWidthSize = m_bitmapOrg.Width;
        int nHeightSize = m_bitmapOrg.Height;

        BitmapData bitmapDataOrg = m_bitmapOrg.LockBits(new Rectangle(0, 0, nWidthSize, nHeightSize), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
        BitmapData bitmapDataAfter = null;
        if (m_bitmapAfter != null)
        {
            bitmapDataAfter = m_bitmapAfter.LockBits(new Rectangle(0, 0, nWidthSize, nHeightSize), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
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

    public bool SaveCsv()
    {
        bool bRst = true;
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
                bRst = false;
            }
        }

        return bRst;
    }
}
