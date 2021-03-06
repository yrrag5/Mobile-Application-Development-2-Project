﻿// Author: Garry Cummins
// ID: G00335806
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BattleTanks
{
    public class TextFilter : IMessageFilter
    {
        public bool this[Keys k]
        {
            get
            {
                bool clicked;

                if (_KeyTable.TryGetValue(k, out clicked))
                {
                    return clicked;
                }
                return false;
            }
        }//bool
        //Setting key press variables
        const int keyPressDown = 0x0100;
        const int keyPressUp = 0x0101;

        public bool MessageFilter(ref Message m)
        {
            if (m.Msg == keyPressDown)
            {
                _KeyTable[(Keys)m.WParam] = true;
            }// if

            if (m.Msg == keyPressUp)
            {
                _KeyTable[(Keys)m.WParam] = false;
            }

            return false;
        }// MessageFilter

        public bool PreFilterMessage(ref Message m)
        {
            throw new NotImplementedException();
        }

        Dictionary<Keys, bool> _KeyTable = new Dictionary<Keys, bool>();
    }// TextFilter
    
}
