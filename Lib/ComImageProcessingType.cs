﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ComImageProcessingType
{
    private int m_nId;
    private string m_strName;

    public int Id
    {
        set { m_nId = value; }
        get { return m_nId; }
    }

    public string Name
    {
        set { m_strName = value; }
        get { return m_strName; }
    }

    public ComImageProcessingType()
    {

    }

    public ComImageProcessingType(int _nId, string _strNmae)
    {
        m_nId = _nId;
        m_strName = _strNmae;
    }

    ~ComImageProcessingType()
    {
    }

    public object Clone()
    {
        return (ComImageProcessingType)MemberwiseClone();
    }
}