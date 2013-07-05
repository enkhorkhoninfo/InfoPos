using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EServ.Shared;
using ISM.Touch;

namespace InfoPos.Reg
{
    public enum EnumMessage
    {
        AMOUNT_NOT_ENTERED = 50001,
        RECORD_NOT_SELECTED = 50002,
        CUSTOMER_NOT_SELECTED = 50003,
        INTERNAL_ERROR = 50999
    }

    public class Msg
    {
        public static Result Get(EnumMessage errno)
        {
            Result res = null;
            switch (errno)
            {
                case EnumMessage.RECORD_NOT_SELECTED:
                    res = new Result((int)errno, "Барьцаа сонгогдоогүй байна!");
                    break;
                case EnumMessage.CUSTOMER_NOT_SELECTED:
                    res = new Result((int)errno, "Харилцагч сонгогдоогүй байна!");
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
