using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace GameTools
{
    public static partial class Extension_String
    {
        public static void Example()
        {
            var emptyStr = string.Empty;
            emptyStr.IsNotNullAndEmpty();
            emptyStr.IsNullOrEmpty();
            emptyStr = emptyStr.AddEnd("appended").AddEnd("1").ToString();
            emptyStr.IsNullOrEmpty();
        }

        /// <summary>
        /// Check Whether string is null or empty
        /// </summary>
        /// <param name="selfStr"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string selfStr)
        {
            return string.IsNullOrEmpty(selfStr);
        }

        /// <summary>
        /// Check Whether string is null or empty
        /// </summary>
        /// <param name="selfStr"></param>
        /// <returns></returns>
        public static bool IsNotNullAndEmpty(this string selfStr)
        {
            return !string.IsNullOrEmpty(selfStr);
        }

        /// <summary>
        /// Check Whether string trim is null or empty
        /// </summary>
        /// <param name="selfStr"></param>
        /// <returns></returns>
        public static bool IsTrimNotNullAndEmpty(this string selfStr)
        {
            return !string.IsNullOrEmpty(selfStr.Trim());
        }

        /// <summary>
        /// 缓存
        /// </summary>
        private static readonly char[] mCachedSplitCharArray = { '.', ':' };

        /// <summary>
        /// Split
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="splitSymbol"></param>
        /// <returns></returns>
        public static string[] Split(this string selfStr, char splitSymbol)
        {
            mCachedSplitCharArray[0] = splitSymbol;
            return selfStr.Split(mCachedSplitCharArray);
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UppercaseFirst(this string str)
        {
            return char.ToUpper(str[0]) + str.Substring(1);
        }

        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string LowercaseFirst(this string str)
        {
            return char.ToLower(str[0]) + str.Substring(1);
        }

        /// <summary>
        /// String拆分转为Int数组
        /// </summary>
        /// <param name="splitStr"></param>
        /// <param name="splitSymbol"></param>
        /// <returns></returns>
        public static int[] StringSplitToIntArr(this string splitStr, char splitSymbol = ':')
        {
            int[] arrInt;
            string[] arrStr = splitStr.Split(splitSymbol);
            arrInt = new int[arrStr.Length];
            for (int i = 0; i < arrStr.Length; i++)
            {
                if (int.TryParse(arrStr[i], out int value))
                {
                    arrInt[i] = value;
                }
                else
                {
                    Debug.LogError($"该String不能转为Int{arrStr[i]}");
                    arrInt[i] = 0;
                }

            }
            return arrInt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToUnixLineEndings(this string str)
        {
            return str.Replace("\r\n", "\n").Replace("\r", "\n");
        }

        /// <summary>
        /// 转换成 CSV
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string ToCSV(this string[] values)
        {
            return string.Join(", ", values
                .Where(value => !string.IsNullOrEmpty(value))
                .Select(value => value.Trim())
                .ToArray()
            );
        }


        public static string[] ArrayFromCSV(this string values)
        {
            return values
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(value => value.Trim())
                .ToArray();
        }

        public static string ToSpacedCamelCase(this string text)
        {
            var sb = new StringBuilder(text.Length * 2);
            sb.Append(char.ToUpper(text[0]));
            for (var i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                {
                    sb.Append(' ');
                }

                sb.Append(text[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 有点不安全,编译器不会帮你排查错误。
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FillFormat(this string selfStr, params object[] args)
        {
            return string.Format(selfStr, args);
        }

        public static string JointStringArr(List<string> strs)
        {
            return JointStringArr(strs.ToArray());
        }
        public static string JointStringArr(string[] strs)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < strs.Length; i++)
            {
                sb.Append(strs[i]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 添加后缀
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="toAppend"></param>
        /// <returns></returns>
        public static string AddEnd(this string selfStr, string toAppend)
        {
            return new StringBuilder(selfStr).Append(toAppend).ToString();
        }

        /// <summary>
        /// 添加前缀
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="toPrefix"></param>
        /// <returns></returns>
        public static string AddPrefix(this string selfStr, string toPrefix)
        {
            return new StringBuilder(toPrefix).Append(selfStr).ToString();
        }

        /// <summary>
        /// 以某个符号连接字符串
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="toLine"></param>
        /// <param name="lineStr"></param>
        /// <returns></returns>
        public static string LinkStr(this string selfStr, string toLine, char lineChar = '/')
        {
            return new StringBuilder(selfStr).Append(lineChar).Append(toLine).ToString();
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="toAppend"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static StringBuilder AppendFormat(this string selfStr, string toAppend, params object[] args)
        {
            return new StringBuilder(selfStr).AppendFormat(toAppend, args);
        }

        /// <summary>
        /// 最后一个单词
        /// </summary>
        /// <param name="selfUrl"></param>
        /// <returns></returns>
        public static string LastWord(this string selfUrl, char sp = ':')
        {
            return selfUrl.Split(sp).Last();
        }

        /// <summary>
        /// 获取第一个单词
        /// </summary>
        /// <param name="selfUrl"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public static string FirstWord(this string selfUrl, char sp = ':')
        {
            return selfUrl.Split(sp).First();
        }

        /// <summary>
        /// 解析成数字类型
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="defaulValue"></param>
        /// <returns></returns>
        public static int ToInt(this string selfStr, int defaulValue = 0)
        {
            var retValue = defaulValue;
            return int.TryParse(selfStr, out retValue) ? retValue : defaulValue;
        }

        /// <summary>
        /// 解析到时间类型
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string selfStr, DateTime defaultValue = default(DateTime))
        {
            var retValue = defaultValue;
            return DateTime.TryParse(selfStr, out retValue) ? retValue : defaultValue;
        }


        /// <summary>
        /// 解析 Float 类型
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="defaulValue"></param>
        /// <returns></returns>
        public static float ToFloat(this string selfStr, float defaulValue = 0)
        {
            var retValue = defaulValue;
            return float.TryParse(selfStr, out retValue) ? retValue : defaulValue;
        }

        /// <summary>
        /// 是否存在中文字符
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool HasChinese(this string input)
        {
            return Regex.IsMatch(input, @"[\u4e00-\u9fa5]");
        }

        /// <summary>
        /// 是否存在空格
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool HasSpace(this string input)
        {
            return input.Contains(" ");
        }

        /// <summary>
        /// 是否存在指定char
        /// </summary>
        /// <param name="input"></param>
        /// <param name="judge"></param>
        /// <returns></returns>
        public static bool HasChar(this string input, char judge)
        {
            return input.Contains(judge);
        }

        /// <summary>
        /// 是否存在指定字符串
        /// </summary>
        /// <param name="input"></param>
        /// <param name="judge"></param>
        /// <returns></returns>
        public static bool HasString(this string input, string judge)
        {
            return input.Contains(judge);
        }

        /// <summary>
        /// 删除特定字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string RemoveString(this string str, params string[] targets)
        {
            return targets.Aggregate(str, (current, t) => current.Replace(t, string.Empty));
        }

        public static string RemoveLast(this string str, char sp)
        {
            string newStr = "";
            string[] arr = str.Split(sp);

            for (int i = 0; i < arr.Length; i++)
            {
                if (i < arr.Length - 1)
                {
                    newStr = newStr.AddEnd(arr[i]);
                }
                if (i < arr.Length - 2)
                {
                    newStr = newStr.AddEnd(sp.ToString());
                }
            }

            return newStr;
        }

        /// <summary>
        /// 返回颜色字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ColorString(this string str, string color)
        {
            return string.Format("<color=#{0}>{1}</color>", color, str);
        }

        /// <summary>
        /// 返回这两个字符是否相同byte对比，忽略大小写
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="compareStr"></param>
        /// <returns></returns>
        public static bool CompareOrdinalIgnoreCase(this string selfStr, string compareStr)
        {
            return string.Compare(selfStr, compareStr, StringComparison.OrdinalIgnoreCase) == 0;
        }
        /// <summary>
        /// 返回这两个字符是否相同byte对比
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="compareStar"></param>
        /// <returns></returns>
        public static bool CompareOrdinal(this string selfStr, string compareStar)
        {
            return string.Compare(selfStr, compareStar, StringComparison.Ordinal) == 0;
        }

        /// <summary>
        /// 字符串转int
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="CB"></param>
        /// <returns></returns>
        public static int StringToInt(this string selfStr,int defaultValue = 0)
        {
            if (int.TryParse(selfStr, out int CbValue))
            {
                return CbValue;
            }
            else
            {
                Debug.LogError($"{selfStr}StringToInt转换失败");
                return defaultValue;
            };
        }

        /// <summary>
        /// 字符串转Long
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long StringToLong(this string selfStr, long defaultValue = 0)
        {
            if (long.TryParse(selfStr, out long CbValue))
            {
                return CbValue;
            }
            else
            {
                Debug.LogError($"{selfStr}StringToLong转换失败");
                return defaultValue;
            };
        }

        /// <summary>
        /// 字符串转double
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double StringToDouble(this string selfStr, double defaultValue = 0)
        {
            if (double.TryParse(selfStr, out double CbValue))
            {
                return CbValue;
            }
            else
            {
                Debug.LogError($"{selfStr}StringToDouble转换失败");
                return defaultValue;
            };
        }

        /// <summary>
        /// 字符串转bool
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool StringToBool(this string selfStr, bool defaultValue = false)
        {
            if (bool.TryParse(selfStr, out bool CbValue))
            {
                return CbValue;
            }
            else
            {
                Debug.LogError($"{selfStr}StringToBool转换失败");
                return defaultValue;
            };
        }

        /// <summary>
        /// 字符串转字符串数组
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string[] StringToArrString(this string selfStr,string[] defaultValue = null)
        {
            try
            {
                string HandStr = selfStr.RemoveString(new string[] { "[", "]", "\"" });
                return HandStr.Split(',');
            }
            catch
            {
                Debug.LogError($"{selfStr}StringToArrString转换失败");
                return defaultValue;
            }
        }

        /// <summary>
        /// 字符串转int数组
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int[] StringToArrInt(this string selfStr, int[] defaultValue = null)
        {
            try
            {
                string[] ArrString = selfStr.StringToArrString();
                if(ArrString == null || ArrString.Length < 1)
                {
                    return defaultValue;
                }
                return Array.ConvertAll(ArrString, int.Parse);
            }
            catch
            {
                Debug.LogError($"{selfStr}StringToArrInt转换失败");
                return defaultValue;
            }
        }

        /// <summary>
        /// 字符串转Float数组
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float[] StringToArrFloat(this string selfStr, float[] defaultValue = null)
        {
            try
            {
                string[] ArrString = selfStr.StringToArrString();
                if (ArrString == null || ArrString.Length < 1)
                {
                    return defaultValue;
                }

                try
                {
                    return Array.ConvertAll(ArrString, s => float.Parse(s, CultureInfo.InvariantCulture));
                }
                catch (Exception e)
                {
                    Debug.LogError($"{selfStr}转换错误 {e.StackTrace}");
                    return Array.ConvertAll(ArrString, float.Parse);
                }
            }
            catch
            {
                Debug.LogError($"{selfStr}StringToArrFloat转换失败");
                return defaultValue;
            }
        }

        /// <summary>
        /// String 转 V3
        /// </summary>
        /// <param name="selfStr"></param>
        /// <returns></returns>
        public static Vector3 String2Vector3(this string selfStr)
        {
            float[] arrflt = StringToArrFloat(selfStr);
            if (arrflt.Length != 3)
            {
                Debug.LogError($"{selfStr}StringToVector3转换失败");
                return Vector3.zero;
            }
            return new Vector3(arrflt[0], arrflt[1], arrflt[2]);
        }

        /*
        /// <summary>
        /// 字符串转jsondata
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static JsonData StringToJsondata(this string selfStr, JsonData defaultValue = null)
        {
            try
            {
                return JsonMapper.ToObject(selfStr);
            }
            catch
            {
                Debug.LogError($"{selfStr}StringToJsondata转换失败");
                return defaultValue;
            }
        }
        */

        /// <summary>
        /// 获取两个字符之间的String
        /// </summary>
        /// <param name="sourse"></param>
        /// <param name="startstr"></param>
        /// <param name="endstr"></param>
        /// <returns></returns>
        public static string GetMidStr(this string sourse, string startstr, string endstr)
        {
            string result = string.Empty;
            int startindex, endindex;
            try
            {
                startindex = sourse.IndexOf(startstr);
                if (startindex == -1)
                    return result;
                string tmpstr = sourse.Substring(startindex + startstr.Length);
                endindex = tmpstr.IndexOf(endstr);
                if (endindex == -1)
                    return result;
                result = tmpstr.Remove(endindex);
            }
            catch (Exception ex)
            {
                Debug.LogError("MidStrEx Err:" + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 字符串是否包含
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="arrStr"></param>
        /// <returns></returns>
        public static bool HasInArrStr(this string selfStr, string[] arrStr)
        {
            for (int i = 0; i < arrStr.Length; i++)
            {
                if (selfStr.CompareOrdinal(arrStr[i]))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 字符串转二进制
        /// </summary>
        /// <param name="selfStr"></param>
        /// <returns></returns>

        public static string Str2Binary(this string selfStr)
        {
            byte[] data = Encoding.UTF8.GetBytes(selfStr);
            StringBuilder result = new StringBuilder(data.Length * 8);

            foreach (byte b in data)
            {
                result.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            }
            return result.ToString();
        }
        /// <summary>
        /// 二进制转字符串
        /// </summary>
        /// <param name="selfStr"></param>
        /// <returns></returns>

        public static string Binary2Str(this string selfStr)
        {
            System.Text.RegularExpressions.CaptureCollection cs =
                          System.Text.RegularExpressions.Regex.Match(selfStr, @"([01]{8})+").Groups[1].Captures;
            byte[] data = new byte[cs.Count];
            for (int i = 0; i < cs.Count; i++)
            {
                data[i] = Convert.ToByte(cs[i].Value, 2);
            }
            return Encoding.UTF8.GetString(data, 0, data.Length);
        }

        /// <summary>
        /// 字符串混淆
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        public static string Confuse(this string selfStr, Int16 shift)
        {
            var maxChar = Convert.ToInt32(char.MaxValue);
            var minChar = Convert.ToInt32(char.MinValue);

            var buffer = selfStr.ToCharArray();

            for (var i = 0; i < buffer.Length; i++)
            {
                var shifted = Convert.ToInt32(buffer[i]) + shift;

                if (shifted > maxChar)
                {
                    shifted -= maxChar;
                }
                else if (shifted < minChar)
                {
                    shifted += maxChar;
                }

                buffer[i] = Convert.ToChar(shifted);
            }

            return new string(buffer);
        }
    }
}
