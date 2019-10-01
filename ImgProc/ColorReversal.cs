using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

class ColorReversal : ComImgProc
{
    public ColorReversal(Bitmap _bitmap) : base(_bitmap)
    {
    }

    ~ColorReversal()
    {
    }

    public void Init()
    {
        base.m_bitmap = null;
        base.m_bitmapAfter = null;
    }

    public override bool GoImgProc(CancellationToken _token)
    {
        bool bRst = true;

        int nWidthSize = base.m_bitmap.Width;
        int nHeightSize = base.m_bitmap.Height;

        base.m_bitmapAfter = new Bitmap(base.m_bitmap);

        BitmapData bitmapData = base.m_bitmapAfter.LockBits(new Rectangle(0, 0, nWidthSize, nHeightSize), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

        int nIdxWidth;
        int nIdxHeight;

        unsafe
        {
            for (nIdxHeight = 0; nIdxHeight < nHeightSize; nIdxHeight++)
            {
                if (_token.IsCancellationRequested)
                {
                    bRst = false;
                    break;
                }

                for (nIdxWidth = 0; nIdxWidth < nWidthSize; nIdxWidth++)
                {
                    if (_token.IsCancellationRequested)
                    {
                        bRst = false;
                        break;
                    }

                    byte* pPixel = (byte*)bitmapData.Scan0 + nIdxHeight * bitmapData.Stride + nIdxWidth * 4;

                    pPixel[(int)ComInfo.Pixel.B] = (byte)(255 - pPixel[(int)ComInfo.Pixel.B]);
                    pPixel[(int)ComInfo.Pixel.G] = (byte)(255 - pPixel[(int)ComInfo.Pixel.G]);
                    pPixel[(int)ComInfo.Pixel.R] = (byte)(255 - pPixel[(int)ComInfo.Pixel.R]);
                }
            }
            base.m_bitmapAfter.UnlockBits(bitmapData);
        }

        return bRst;
    }
}