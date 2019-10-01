using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

abstract public class ComImgProc
{
    protected Bitmap m_bitmap;
    protected Bitmap m_bitmapAfter;

    public ComImgProc(Bitmap _bitmap)
    {
        m_bitmap = _bitmap;
    }

    ~ComImgProc()
    {
        m_bitmap = null;
    }

    public Bitmap Bitmap
    {
        set { m_bitmap = value; }
        get { return m_bitmap; }
    }

    public Bitmap BitmapAfter
    {
        set { m_bitmapAfter = value; }
        get { return m_bitmapAfter; }
    }

    abstract public bool GoImgProc(CancellationToken _token);
}