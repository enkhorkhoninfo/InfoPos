using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ISM.Touch
{
    public interface ITouchCall
    {
        void Init(string buttonkey, TouchLinkItem item, Form parent, object param, ref bool cancel);
        void Call(string buttonkey, TouchLinkItem item, ref bool cancel);
    }
}
