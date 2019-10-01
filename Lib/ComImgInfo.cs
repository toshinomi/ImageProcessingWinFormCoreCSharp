﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ComImgInfo
{
    private string m_strCurImgName;
    private ComEdgeDetectionInfo m_edgeDetectoinInfo;
    private ComGrayScaleInfo m_grayScaleInfo;
    private ComBinarizationInfo m_binarizationInfo;

    public string CurImgName
    {
        set { m_strCurImgName = value; }
        get { return m_strCurImgName; }
    }

    public ComEdgeDetectionInfo EdgeDetectoinInfo
    {
        set { m_edgeDetectoinInfo = value; }
        get { return m_edgeDetectoinInfo; }
    }

    public ComGrayScaleInfo GrayScaleInfo
    {
        set { m_grayScaleInfo = value; }
        get { return m_grayScaleInfo; }
    }

    public ComBinarizationInfo BinarizationInfo
    {
        set { m_binarizationInfo = value; }
        get { return m_binarizationInfo; }
    }

    public ComImgInfo()
    {
        m_edgeDetectoinInfo = new ComEdgeDetectionInfo();
        m_grayScaleInfo = new ComGrayScaleInfo();
        m_binarizationInfo = new ComBinarizationInfo();
    }

    ~ComImgInfo()
    {
    }
}

public class ComEdgeDetectionInfo
{
    public ComEdgeDetectionInfo()
    {
    }

    ~ComEdgeDetectionInfo()
    {
    }
}

public class ComGrayScaleInfo
{
    public ComGrayScaleInfo()
    {
    }

    ~ComGrayScaleInfo()
    {
    }
}

public class ComBinarizationInfo
{
    private byte m_nThresh;
    public byte Thresh
    {
        set { m_nThresh = value; }
        get { return m_nThresh; }
    }

    public ComBinarizationInfo()
    {
    }

    ~ComBinarizationInfo()
    {
    }
}