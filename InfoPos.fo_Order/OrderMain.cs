using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EServ.Shared;
using ISM.Touch;

namespace InfoPos.Order
{
    public enum EnumMessage
    {
        PRODUCT_NOT_SELECTED = 50001,
        SALES_NOT_SELECTED = 50002,
        INTERNAL_ERROR = 50999
    }

    public class Msg
    {
        public static Result Get(EnumMessage errno)
        {
            Result res = null;
            switch (errno)
            {
                case EnumMessage.SALES_NOT_SELECTED:
                    res = new Result((int)errno, "Борлуулалт сонгогдоогүй байна!");
                    break;
                case EnumMessage.PRODUCT_NOT_SELECTED:
                    res = new Result((int)errno, "Түрээсийн хэрэгсэл сонгогдоогүй байна!");
                    break;
                case EnumMessage.INTERNAL_ERROR:
                    res = new Result((int)errno, "Дотоод алдаа гарлаа!");
                    break;
                default :
                    res = new Result(99999, "Тодорхойгүй алдаа!");
                    break;
            }
            return res;
        }
    }
}
