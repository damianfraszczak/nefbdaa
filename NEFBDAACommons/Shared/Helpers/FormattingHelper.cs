using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NEFBDAACommons.Shared.Helpers
{
    public static class FormattingHelper
    {

        public static string DATE_FORMAT = "dd/MM/yyyy";
        public static string TIME_FORMAT = "HH:mm";
        public static string DATE_TIME_FORMAT = DATE_FORMAT + " " + TIME_FORMAT;

        public static string Format2DecimalPlaces(double val)
        {
            return val.ToString("0.##");
        }
        public static string FormatFileSizeInMb(long size)
        {
            return $"{Format2DecimalPlaces(MathHelper.ConvertBytesToMegabytes(size))} MB";
        }

        public static string ObjectToString(object obj)
        {
            var fields = obj.GetType().GetFields();
            var str = "";
            foreach (var f in fields)
            {
                str += f.Name + " = " + ParsingHelper.GetNotNullString(f.GetValue(obj)) + "\r\n";
            }
            return str;
        }
        public static string TextToHtml(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                text = "";
            }
            //simply replace /n with <br/>
            text = text.Replace("\n", "<br/>");
            return text;
        }

        public static string RemoveHtml(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                text = "";
            }
            Regex regex = new Regex("<!--(\n|.)*-->");
            var result = regex.Replace(text, String.Empty);
            regex = new Regex("\\<[^\\>]*\\>");
            return regex.Replace(result, String.Empty); ;
        }
        public static string SplitCamelCase(string s)
        {
            Regex upperCaseRegex = new Regex(@"[A-Z]{1}[a-z]*");
            MatchCollection matches = upperCaseRegex.Matches(s);
            var words = new List<string>();
            foreach (Match match in matches)
            {
                words.Add(match.Value);
            }
            return String.Join(" ", words.ToArray());
        }
        public static string ToTitleCase(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "";

            }
            else
            {
                //return TEXT_INFO.ToTitleCase(text);
                String[] words = text.Split(' ');
                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i].Length == 0) continue;

                    Char firstChar = Char.ToUpper(words[i][0]);
                    String rest = "";
                    if (words[i].Length > 1)
                    {
                        rest = words[i].Substring(1).ToLower();
                    }
                    words[i] = firstChar + rest;
                }
                return String.Join(" ", words);
            }
        }
        public static string GetDayName(DateTime? date)
        {
            if (date == null)
            {
                return "";
            }
            else
                return date.Value.ToString("dddd");
        }
        public static string GetMonthName(DateTime? date)
        {
            if (date == null)
            {
                return "";
            }
            else
                return date.Value.ToString("MMMM");
        }
        public static string GetFormattedDate(DateTime? date)
        {
            if (date == null)
            {
                return "";
            }
            else
                return date.Value.ToString(DATE_FORMAT);
        }
        public static string GetFormattedTime(DateTime? date)
        {
            if (date == null)
            {
                return "";
            }
            else
                return date.Value.ToString(TIME_FORMAT);
        }
        public static string GetFormattedDateTime(DateTime? date)
        {
            if (date == null)
            {
                return "";
            }
            else
                return date.Value.ToString(DATE_TIME_FORMAT);
        }
        public static string GetTrimmedToMaxLength(string text, int max)
        {
            if (text == null)
            {
                return "";
            }
            if (text.Length > max)
            {
                return text.Substring(0, max) + "....";
            }
            else
            {
                return text;
            }
        }


        public static string HtmlToPlainText(string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                html = "";
            }
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = html;
            //Decode html specific characters
            text = System.Net.WebUtility.HtmlDecode(text);
            if (ContainsHTMLElements(text))
            {
                //Remove tag whitespace/line breaks
                text = tagWhiteSpaceRegex.Replace(text, "><");
                //Replace <br /> with line breaks
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                //Strip formatting
                text = stripFormattingRegex.Replace(text, string.Empty);
            }


            return text;
        }
        public static bool ContainsHTMLElements(string text)
        {
            return Regex.IsMatch(text, "<(.|\n)*?>");
        }

        public static string AutoCloseHtmlTags(string inputHtml)
        {
            if (string.IsNullOrEmpty(inputHtml))
            {
                inputHtml = "";
            }
            var regexStartTag = new Regex(@"<(!--\u002E\u002E\u002E--|!DOCTYPE|a|abbr|" +
                  @"acronym|address|applet|area|article|aside|audio|b|base|basefont|bdi|bdo|big" +
                  @"|blockquote|body|br|button|canvas|caption|center|cite|code|col|colgroup|" +
                  @"command|datalist|dd|del|details|dfn|dialog|dir|div|dl|dt|em|embed|fieldset|" +
                  @"figcaption|figure|font|footer|form|frame|frameset|h1> to <h6|head|" +
                  @"header|hr|html|i|iframe|img|input|ins|kbd|keygen|label|legend|li|link|" +
                  @"map|mark|menu|meta|meter|nav|noframes|noscript|object|ol|optgroup|option|" +
                  @"output|p|param|pre|progress|q|rp|rt|ruby|s|samp|script|section|select|small|" +
                  @"source|span|strike|strong|style|sub|summary|sup|table|tbody|td|textarea|" +
                  @"tfoot|th|thead|time|title|tr|track|tt|u|ul|var|video|wbr)(\s\w+.*(\u0022|'))?>");
            var startTagCollection = regexStartTag.Matches(inputHtml);
            var regexCloseTag = new Regex(@"</(!--\u002E\u002E\u002E--|!DOCTYPE|a|abbr|" +
                  @"acronym|address|applet|area|article|aside|audio|b|base|basefont|bdi|bdo|" +
                  @"big|blockquote|body|br|button|canvas|caption|center|cite|code|col|colgroup|" +
                  @"command|datalist|dd|del|details|dfn|dialog|dir|div|dl|dt|em|embed|fieldset|" +
                  @"figcaption|figure|font|footer|form|frame|frameset|h1> to <h6|head|header" +
                  @"|hr|html|i|iframe|img|input|ins|kbd|keygen|label|legend|li|link|map|mark|menu|" +
                  @"meta|meter|nav|noframes|noscript|object|ol|optgroup|option|output|p|param|pre|" +
                  @"progress|q|rp|rt|ruby|s|samp|script|section|select|small|source|span|strike|" +
                  @"strong|style|sub|summary|sup|table|tbody|td|textarea|tfoot|th|thead|" +
                  @"time|title|tr|track|tt|u|ul|var|video|wbr)>");
            var closeTagCollection = regexCloseTag.Matches(inputHtml);
            var startTagList = new List<string>();
            var closeTagList = new List<string>();
            var resultClose = "";
            foreach (Match startTag in startTagCollection)
            {
                startTagList.Add(startTag.Value);
            }
            foreach (Match closeTag in closeTagCollection)
            {
                closeTagList.Add(closeTag.Value);
            }
            startTagList.Reverse();
            for (int i = 0; i < closeTagList.Count; i++)
            {
                if (startTagList[i] != closeTagList[i])
                {
                    int indexOfSpace = startTagList[i].IndexOf(
                             " ", System.StringComparison.Ordinal);
                    if (startTagList[i].Contains(" "))
                    {
                        startTagList[i].Remove(indexOfSpace);
                    }
                    startTagList[i] = startTagList[i].Replace("<", "</");
                    resultClose += startTagList[i] + ">";
                    resultClose = resultClose.Replace(">>", ">");
                }
            }
            return inputHtml + resultClose;
        }



    }
}
