using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

class GrayScale2Diff : ComImgProc
{
    public GrayScale2Diff(Bitmap _bitmap) : base(_bitmap)
    {
    }

    ~GrayScale2Diff()
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

        short[,] nMask =
        {
            {1,  1, 1},
            {1, -8, 1},
            {1,  1, 1}
        };

        int nWidthSize = base.m_bitmap.Width;
        int nHeightSize = base.m_bitmap.Height;
        int nMasksize = nMask.GetLength(0);

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

                    long lCalB = 0;
                    long lCalG = 0;
                    long lCalR = 0;
                    double dCalAve = 0.0;
                    int nIdxWidthMask;
                    int nIdxHightMask;


                    for (nIdxHightMask = 0; nIdxHightMask < nMasksize; nIdxHightMask++)
                    {
                        for (nIdxWidthMask = 0; nIdxWidthMask < nMasksize; nIdxWidthMask++)
                        {
                            if (nIdxWidth + nIdxWidthMask > 0 &&
                                nIdxWidth + nIdxWidthMask < nWidthSize &&
                                nIdxHeight + nIdxHightMask > 0 &&
                                nIdxHeight + nIdxHightMask < nHeightSize)
                            {
                                byte* pPixel2 = (byte*)bitmapData.Scan0 + (nIdxHeight + nIdxHightMask) * bitmapData.Stride + (nIdxWidth + nIdxWidthMask) * 4;

                                lCalB = pPixel2[(int)ComInfo.Pixel.B] * nMask[nIdxWidthMask, nIdxHightMask];
                                lCalG = pPixel2[(int)ComInfo.Pixel.G] * nMask[nIdxWidthMask, nIdxHightMask];
                                lCalR = pPixel2[(int)ComInfo.Pixel.R] * nMask[nIdxWidthMask, nIdxHightMask];

                                double dcalGray = (lCalB + lCalG + lCalR) / 3;
                                dCalAve = (dCalAve + dcalGray) / 2;
                            }
                        }
                    }
                    byte nGrayScale = ComFunc.DoubleToByte(dCalAve);

                    pPixel[(int)ComInfo.Pixel.B] = nGrayScale;
                    pPixel[(int)ComInfo.Pixel.G] = nGrayScale;
                    pPixel[(int)ComInfo.Pixel.R] = nGrayScale;
                }
            }
            base.m_bitmapAfter.UnlockBits(bitmapData);
        }

        return bRst;
    }
}