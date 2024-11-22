﻿/*
 * This file is part of Playgama Bridge.
 *
 * Playgama Bridge is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 *
 * Playgama Bridge is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with Playgama Bridge. If not, see <https://www.gnu.org/licenses/>.
*/

#if UNITY_WEBGL
using System;
using System.Collections.Generic;
using System.Text;

namespace Playgama.Common
{
    public static class JsonHelper
    {
        public static string SurroundWithBraces(this string json)
        {
            return "{" + json + "}";
        }

        public static string SurroundWithKey(this string json, string key, bool quotes = false)
        {
            if (quotes)
            {
                json = "\"" + json + "\"";
            }
            
            return "\"" + key + "\":" + json;
        }

        public static string ConvertBooleanToCSharp(this string json)
        {
            return json.Replace("true", "True").Replace("false", "False");
        }

        public static string ToJson(this Dictionary<string, object> data)
        {
            var sb = new StringBuilder();

            if (data != null)
            {
                var isFirst = true;
                
                foreach (var item in data)
                {
                    if (!isFirst)
                    {
                        sb.Append(",");
                    }

                    isFirst = false;

                    sb.Append(item.Value.ToJson().SurroundWithKey(item.Key));
                }
            }

            return sb.ToString().SurroundWithBraces();
        }

        private static string ToJson(this Array data)
        {
            var sb = new StringBuilder();
            sb.Append("[");
            var isFirst = true;

            foreach (var item in data)
            {
                if (!isFirst)
                {
                    sb.Append(",");
                }
                isFirst = false;

                sb.Append(item.ToJson());
            }

            sb.Append("]");
            return sb.ToString();
        }

        private static string ToJson(this object data)
        {
            return data switch
            {
                null => "null",
                string s => "\"" + EscapeString(s) + "\"",
                int or float or double => data.ToString(),
                bool b => b ? "true" : "false",
                Array array => array.ToJson(),
                Dictionary<string, object> objects => objects.ToJson(),
                _ => "null"
            };
        }

        private static string EscapeString(string str)
        {
            return str.Replace("\\", "\\\\").Replace("\"", "\\\"");
        }
    }
}
#endif