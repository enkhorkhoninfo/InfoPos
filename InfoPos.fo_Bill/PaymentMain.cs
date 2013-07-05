using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EServ.Shared;
using ISM.Touch;

namespace InfoPos.Bill
{
    public enum EnumMessage
    {
        AMOUNT_NOT_ENTERED = 50001,
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
                case EnumMessage.AMOUNT_NOT_ENTERED:
                    res = new Result((int)errno, "Төлбөрийн дүнг оруулна уу!");
                    break;
                case EnumMessage.INTERNAL_ERROR:
                    res = new Result((int)errno, "Дотоод алдаа гарлаа!");
                    break;
                default:
                    res = new Result(99999, "Тодорхойгүй алдаа!");
                    break;
            }
            return res;
        }
    }
}
