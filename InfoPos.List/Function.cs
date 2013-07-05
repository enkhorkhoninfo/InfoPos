using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfoPos.List
{
 public class Function
    {
        public string num2str(decimal number, string currency, string currencyUnit)
        {
            string str = "";
            string strUnit = "";
            string strVal = "";
            string strValUnit = "";
            string strAllValue = "";

            long num = Convert.ToInt64(number);
            strAllValue = number.ToString();


            #region[Variable]
            string[] numUnit = { "НЭГ ", "ХОЁР ", "ГУРАВ ", "ДӨРӨВ ", "ТАВ ", "ЗУРГАА ", "ДОЛОО ", "НАЙМ ", "ЕС " };
            string[] num10th = { "АРАВ ", "ХОРЬ ", "ГУЧ ", "ДӨЧ ", "ТАВЬ ", "ЖАР ", "ДАЛ ", "НАЯ ", "ЕР " };
            string[] numUnitAdd = { "НЭГ ", "ХОЁР ", "ГУРВАН ", "ДӨРВӨН ", "ТАВАН ", "ЗУРГААН ", "ДОЛООН ", "НАЙМАН ", "ЕСӨН " };
            string[] numUnit10Add = { "НЭГЭН ", "ХОЁР ", "ГУРВАН ", "ДӨРВӨН ", "ТАВАН ", "ЗУРГААН ", "ДОЛООН ", "НАЙМАН ", "ЕСӨН " };
            string[] num10thAdd = { "АРВАН ", "ХОРИН ", "ГУЧИН ", "ДӨЧИН ", "ТАВИН ", "ЖАРАН ", "ДАЛАН ", "НАЯН ", "ЕРЭН " };
            string num100 = "ЗУУ ";
            string num100Add = "ЗУУН ";
            string num1000 = "МЯНГА ";
            string num1000_1000 = "САЯ ";
            string num1000_1000_1000 = "ТЭРБУМ ";
            string num1000_1000_1000_1000 = "ТРИЛЛИОН ";
            string numZero = "ТЭГ ";
            #endregion

            string numStr = num.ToString();

            int length = numStr.Length;
            switch (strAllValue.Length - length)
            {
                case 0:
                    break;
                case 2:
                    strValUnit = strAllValue.Substring(length + 1, 1);
                    if (Convert.ToInt32(strValUnit) != 0)
                    {
                        strUnit = num10thAdd[Convert.ToInt32(strValUnit) - 1];
                        strUnit = strUnit + currencyUnit;
                    }
                    break;
                case 3:
                    strValUnit = strAllValue.Substring(length + 1, 2);
                    if (Convert.ToInt32(strValUnit) != 0)
                    {
                        strUnit = numL3strAdd(Convert.ToInt32(strValUnit));
                        strUnit = strUnit + currencyUnit;
                    }
                    break;
                default:
                    strUnit = "Мөнгөн дүнгийн нэгжийн тоо хэтэрсэн байна .";
                    break;
            }
            if (num != 0)
            {

                switch (length)
                {
                    case 1:
                        str = numL3strAdd(Convert.ToInt32(num));
                        break;
                    case 2:
                        str = numL3strAdd(Convert.ToInt32(num));
                        break;
                    case 3:
                        str = numL3strAdd(Convert.ToInt32(num));
                        break;
                    case 4:
                        strVal = numStr.Substring(0, 1);
                        str = numUnitAdd[Convert.ToInt32(strVal) - 1] + num1000;
                        strVal = numStr.Substring(1, 3);
                        str = str + numL3strAdd(Convert.ToInt32(strVal));
                        break;
                    case 5:
                        strVal = numStr.Substring(0, 2);
                        str = numL3strAdd(Convert.ToInt32(strVal)) + num1000;
                        strVal = numStr.Substring(2, 3);
                        str = str + numL3strAdd(Convert.ToInt32(strVal));
                        break;
                    case 6:
                        strVal = numStr.Substring(0, 3);
                        str = numL3strAdd(Convert.ToInt32(strVal)) + num1000;
                        strVal = numStr.Substring(3, 3);
                        str = str + numL3strAdd(Convert.ToInt32(strVal));
                        break;

                    case 7:
                        strVal = numStr.Substring(0, 1);
                        str = numUnitAdd[Convert.ToInt32(strVal) - 1] + num1000_1000;
                        strVal = numStr.Substring(1, 3);
                        if (Convert.ToInt32(strVal) != 0)
                            str = str + numL3strAdd(Convert.ToInt32(strVal)) + num1000;
                        strVal = numStr.Substring(4, 3);
                        str = str + numL3strAdd(Convert.ToInt32(strVal));
                        break;
                    case 8:
                        strVal = numStr.Substring(0, 2);
                        str = numL3strAdd(Convert.ToInt32(strVal)) + num1000_1000;
                        strVal = numStr.Substring(2, 3);
                        if (Convert.ToInt32(strVal) != 0)
                            str = str + numL3strAdd(Convert.ToInt32(strVal)) + num1000;
                        strVal = numStr.Substring(5, 3);
                        str = str + numL3strAdd(Convert.ToInt32(strVal));
                        break;
                    case 9:
                        strVal = numStr.Substring(0, 3);
                        str = numL3strAdd(Convert.ToInt32(strVal)) + num1000_1000;
                        strVal = numStr.Substring(3, 3);
                        if (Convert.ToInt32(strVal) != 0)
                            str = str + numL3strAdd(Convert.ToInt32(strVal)) + num1000;
                        strVal = numStr.Substring(6, 3);
                        str = str + numL3strAdd(Convert.ToInt32(strVal));
                        break;

                    case 10:
                        strVal = numStr.Substring(0, 1);
                        str = numUnitAdd[Convert.ToInt32(strVal) - 1] + num1000_1000_1000;

                        strVal = numStr.Substring(1, 3);
                        if (Convert.ToInt32(strVal) != 0)
                            str = str + numL3strAdd(Convert.ToInt32(strVal)) + num1000_1000;

                        strVal = numStr.Substring(4, 3);
                        if (Convert.ToInt32(strVal) != 0)
                            str = str + numL3strAdd(Convert.ToInt32(strVal)) + num1000;

                        strVal = numStr.Substring(7, 3);
                        str = str + numL3strAdd(Convert.ToInt32(strVal));
                        break;
                    case 11:
                        strVal = numStr.Substring(0, 2);
                        str = numL3strAdd(Convert.ToInt32(strVal)) + num1000_1000_1000;

                        strVal = numStr.Substring(2, 3);
                        if (Convert.ToInt32(strVal) != 0)
                            str = str + numL3strAdd(Convert.ToInt32(strVal)) + num1000_1000;

                        strVal = numStr.Substring(5, 3);
                        if (Convert.ToInt32(strVal) != 0)
                            str = str + numL3strAdd(Convert.ToInt32(strVal)) + num1000;

                        strVal = numStr.Substring(8, 3);
                        str = str + numL3strAdd(Convert.ToInt32(strVal));
                        break;
                    case 12:
                        strVal = numStr.Substring(0, 3);
                        str = numL3strAdd(Convert.ToInt32(strVal)) + num1000_1000_1000;

                        strVal = numStr.Substring(3, 3);
                        if (Convert.ToInt32(strVal) != 0)
                            str = str + numL3strAdd(Convert.ToInt32(strVal)) + num1000_1000;

                        strVal = numStr.Substring(6, 3);
                        if (Convert.ToInt32(strVal) != 0)
                            str = str + numL3strAdd(Convert.ToInt32(strVal)) + num1000;

                        strVal = numStr.Substring(9, 3);
                        str = str + numL3strAdd(Convert.ToInt32(strVal));
                        break;

                    case 13:
                        strVal = numStr.Substring(0, 1);
                        str = numUnitAdd[Convert.ToInt32(strVal) - 1] + num1000_1000_1000_1000;

                        strVal = numStr.Substring(1, 3);
                        if (Convert.ToInt32(strVal) != 0)
                            str = str + numL3strAdd(Convert.ToInt32(strVal)) + num1000_1000_1000;

                        strVal = numStr.Substring(4, 3);
                        if (Convert.ToInt32(strVal) != 0)
                            str = str + numL3strAdd(Convert.ToInt32(strVal)) + num1000_1000;

                        strVal = numStr.Substring(7, 3);
                        if (Convert.ToInt32(strVal) != 0)
                            str = str + numL3strAdd(Convert.ToInt32(strVal)) + num1000;

                        strVal = numStr.Substring(10, 3);
                        str = str + numL3strAdd(Convert.ToInt32(strVal));
                        break;
                    case 14:
                        strVal = numStr.Substring(0, 2);
                        str = numL3strAdd(Convert.ToInt32(strVal)) + num1000_1000_1000_1000;

                        strVal = numStr.Substring(2, 3);
                        if (Convert.ToInt32(strVal) != 0)
                            str = str + numL3strAdd(Convert.ToInt32(strVal)) + num1000_1000_1000;

                        strVal = numStr.Substring(5, 3);
                        if (Convert.ToInt32(strVal) != 0)
                            str = str + numL3strAdd(Convert.ToInt32(strVal)) + num1000_1000;

                        strVal = numStr.Substring(8, 3);
                        if (Convert.ToInt32(strVal) != 0)
                            str = str + numL3strAdd(Convert.ToInt32(strVal)) + num1000;

                        strVal = numStr.Substring(11, 3);
                        str = str + numL3strAdd(Convert.ToInt32(strVal));
                        break;
                    case 15:
                        strVal = numStr.Substring(0, 3);
                        str = numL3strAdd(Convert.ToInt32(strVal)) + num1000_1000_1000_1000;

                        strVal = numStr.Substring(3, 3);
                        if (Convert.ToInt32(strVal) != 0)
                            str = str + numL3strAdd(Convert.ToInt32(strVal)) + num1000_1000_1000;

                        strVal = numStr.Substring(6, 3);
                        if (Convert.ToInt32(strVal) != 0)
                            str = str + numL3strAdd(Convert.ToInt32(strVal)) + num1000_1000;

                        strVal = numStr.Substring(9, 3);
                        if (Convert.ToInt32(strVal) != 0)
                            str = str + numL3strAdd(Convert.ToInt32(strVal)) + num1000;

                        strVal = numStr.Substring(12, 3);
                        str = str + numL3strAdd(Convert.ToInt32(strVal));
                        break;
                    default:
                        str = "Мөнгөн дүнгийн орон хэтэрлээ хамгийн ихдээ арван таван оронтой байна.";
                        break;
                }
            }
            else
                str = numZero;
            str = str + currency + " ";
            return str + strUnit;
        }
        public string numL3str(int num)
        {
            #region[Variable]
            string[] numUnit = { "НЭГ ", "ХОЁР ", "ГУРАВ ", "ДӨРӨВ ", "ТАВ ", "ЗУРГАА ", "ДОЛОО ", "НАЙМ ", "ЕС " };
            string[] num10th = { "АРАВ ", "ХОРЬ ", "ГУЧ ", "ДӨЧ ", "ТАВЬ ", "ЖАР ", "ДАЛ ", "НАЯ ", "ЕР " };
            string[] numUnitAdd = { "НЭГ ", "ХОЁР ", "ГУРВАН ", "ДӨРВӨН ", "ТАВАН ", "ЗУРГААН ", "ДОЛООН ", "НАЙМАН ", "ЕСӨН " };
            string[] numUnit10Add = { "НЭГЭН ", "ХОЁР ", "ГУРВАН ", "ДӨРВӨН ", "ТАВАН ", "ЗУРГААН ", "ДОЛООН ", "НАЙМАН ", "ЕСӨН " };
            string[] num10thAdd = { "АРВАН ", "ХОРИН ", "ГУЧИН ", "ДӨЧИН ", "ТАВИН ", "ЖАРАН ", "ДАЛАН ", "НАЯН ", "ЕРЭН " };
            string num100 = "ЗУУ ";
            string num100Add = "ЗУУН ";
            string num1000 = "МЯНГА ";
            string num1000_1000 = "САЯ ";
            string num1000_1000_1000 = "ТЭРБУМ ";
            string num1000_1000_1000_1000 = "ТРИЛЛИОН ";
            string numZero = "ТЭГ ";
            #endregion

            string str = "";
            string numStr = num.ToString();
            int length = numStr.Length;
            if (num != 0)
            {
                switch (length)
                {
                    case 1:
                        {
                            str = numUnit[num - 1];
                        }
                        break;
                    case 2:
                        {
                            if (num % 10 == 0)
                            {
                                str = num10th[num / 10 - 1];
                            }
                            else
                                str = num10thAdd[Convert.ToInt32(num / 10) - 1] + numUnit[num % 10 - 1];
                        }
                        break;
                    case 3:
                        {
                            if (num % 100 == 0)
                            {
                                str = numUnitAdd[num / 100 - 1];
                                str = str + num100;
                            }
                            else
                            {
                                str = numUnitAdd[Convert.ToInt32(num / 100) - 1] + num100Add;
                                int numVal = Convert.ToInt32(num % 100);

                                if (numVal % 10 == 0)
                                {
                                    str = str + num10th[numVal / 10 - 1];
                                }
                                else
                                    str = str + num10thAdd[Convert.ToInt32(numVal / 10) - 1] + numUnit[numVal % 10 - 1];
                            }

                        }
                        break;
                }
            }
            return str;
        }
        public string numL3strAdd(int num)
        {
            #region[Variable]
            string[] numUnit = { "НЭГ ", "ХОЁР ", "ГУРАВ ", "ДӨРӨВ ", "ТАВ ", "ЗУРГАА ", "ДОЛОО ", "НАЙМ ", "ЕС " };
            string[] num10th = { "АРАВ ", "ХОРЬ ", "ГУЧ ", "ДӨЧ ", "ТАВЬ ", "ЖАР ", "ДАЛ ", "НАЯ ", "ЕР " };
            string[] numUnitAdd = { "НЭГ ", "ХОЁР ", "ГУРВАН ", "ДӨРВӨН ", "ТАВАН ", "ЗУРГААН ", "ДОЛООН ", "НАЙМАН ", "ЕСӨН " };
            string[] numUnit10Add = { "НЭГЭН ", "ХОЁР ", "ГУРВАН ", "ДӨРВӨН ", "ТАВАН ", "ЗУРГААН ", "ДОЛООН ", "НАЙМАН ", "ЕСӨН " };
            string[] num10thAdd = { "АРВАН ", "ХОРИН ", "ГУЧИН ", "ДӨЧИН ", "ТАВИН ", "ЖАРАН ", "ДАЛАН ", "НАЯН ", "ЕРЭН " };
            string num100 = "ЗУУ ";
            string num100Add = "ЗУУН ";
            string num1000 = "МЯНГА ";
            string num1000_1000 = "САЯ ";
            string num1000_1000_1000 = "ТЭРБУМ ";
            string num1000_1000_1000_1000 = "ТРИЛЛИОН ";
            string numZero = "ТЭГ ";
            #endregion

            string str = "";
            string numStr = num.ToString();
            int length = numStr.Length;
            if (num != 0)
            {
                switch (length)
                {
                    case 1:
                        {
                            str = numUnitAdd[num - 1];
                        }
                        break;
                    case 2:
                        {
                            if (num % 10 == 0)
                            {
                                str = num10thAdd[num / 10 - 1];
                            }
                            else
                                str = num10thAdd[Convert.ToInt32(num / 10) - 1] + numUnit10Add[num % 10 - 1];
                        }
                        break;
                    case 3:
                        {
                            if (num % 100 == 0)
                            {
                                str = numUnitAdd[num / 100 - 1];
                                str = str + num100Add;
                            }
                            else
                            {
                                str = numUnitAdd[Convert.ToInt32(num / 100) - 1] + num100Add;
                                int numVal = Convert.ToInt32(num % 100);

                                if (numVal >= 10)
                                {
                                    if (numVal % 10 == 0)
                                    {
                                        str = str + num10thAdd[numVal / 10 - 1];
                                    }
                                    else
                                        str = str + num10thAdd[Convert.ToInt32(numVal / 10) - 1] + numUnit10Add[numVal % 10 - 1];
                                }
                                else
                                    str = str + numUnit10Add[numVal - 1];
                            }

                        }
                        break;
                }
            }
            return str;
        }
    }
}
