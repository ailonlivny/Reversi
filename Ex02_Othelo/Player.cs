using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    public class Player
    {
        int m_Color;
        string m_Name;

        public Player()
        {
            m_Name = null;
        }

        public int Color
        {
            get
            {
                return m_Color;
            }
            set
            {
                m_Color = value;
            }
        }
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }
    }
}
