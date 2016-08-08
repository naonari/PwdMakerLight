using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PwdMakerLight.Services
{
    /// <summary>
    /// パスワード作成用のシングルトンサービス。
    /// </summary>
    class PasswordSingletonService
    {
        /// <summary>作成するパスワードの長さの規定値</summary>
        public static readonly int DEFAULT_LENGTH_VALUE = 8;

        /// <summary>パスワードに数字を利用するかの規定値</summary>
        public static readonly bool DEFAULT_USE_NUMRIC_VALUE = true;

        /// <summary>パスワードに大文字英字を利用するかの規定値</summary>
        public static readonly bool DEFAULT_USE_UPPERCASE_VALUE = true;

        /// <summary>パスワードに小文字英字を利用するかの規定値</summary>
        public static readonly bool DEFAULT_USE_LOWERCASE_VALUE = true;

        /// <summary>パスワードに記号を利用するかの規定値</summary>
        public static readonly bool DEFAULT_USE_SYMBOL_VALUE = false;

        /// <summary>クリップボード出力するかの規定値</summary>
        public static readonly bool DEFAULT_CLIPBOARD_VALUE = true;

        /// <summary>テキストファイル出力するかの規定値</summary>
        public static readonly bool DEFAULT_TEXT_FILE_VALUE = false;

        /// <summary>
        /// インスタンス化禁止。
        /// </summary>
        private PasswordSingletonService()
        {
        }

        // 単一のインスタンス。
        private static PasswordSingletonService _singleton = new PasswordSingletonService();

        /// <summary>
        /// 単一のインスタンスを取得します。
        /// </summary>
        /// <returns>単一のインスタンスを返します。</returns>
        public static PasswordSingletonService GetInstance()
        {
            // 単一のインスタンスを返します。
            return _singleton;
        }

        /// <summary>
        /// パスワード作成処理を行います。
        /// </summary>
        /// <param name="pwdLength">パスワードの文字数を設定します。</param>
        /// <param name="useNumric">数字の使用の可否を設定します。</param>
        /// <param name="useUpperCase">英字大文字の使用の可否を設定します。</param>
        /// <param name="useLowerCase">英字小文字の使用の可否を設定します。</param>
        /// <param name="useSymbol">記号の使用の可否を設定します。</param>
        /// <returns>処理の成否を返します。</returns>
        /// <remarks></remarks>
        public string MakePassword(int pwdLength, bool useNumric, bool useUpperCase, bool useLowerCase, bool useSymbol)
        {
            // パスワード作成処理を行います。
            return MakePasswordCore(pwdLength, useNumric, useUpperCase, useLowerCase, useSymbol);
        }

        /// <summary>
        /// パスワード作成処理を行います。
        /// </summary>
        /// <remarks>作成したパスワードの文字列リストを返します。</remarks>
        public string MakePasswordByBatchProcess(int lengthValue)
        {
            // 設定ファイルシングルトンのインスタンスを取得します。
            var css = ConfigSingletonService.GetInstance();

            // パスワード作成処理を行います。
            return MakePasswordCore(lengthValue,
                                    css.GetDefaultValue("UseNumric", DEFAULT_USE_NUMRIC_VALUE),
                                    css.GetDefaultValue("UseUppercase", DEFAULT_USE_UPPERCASE_VALUE),
                                    css.GetDefaultValue("UseLowercase", DEFAULT_USE_LOWERCASE_VALUE),
                                    css.GetDefaultValue("UseSymbol", DEFAULT_USE_SYMBOL_VALUE));
        }

        //パスワードテンプレート文字列の定数
        private static readonly string TMP_NUMBER = "0123456789";
        private static readonly string TMP_UPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string TMP_LOWER = "abcdefghijklmnopqrstuvwxyz";
        private static readonly string TMP_SYMBOL = "+-*!#$%&@";

        /// <summary>
        /// パスワード作成処理を行います。
        /// </summary>
        /// <param name="pwdLength">パスワードの文字数を設定します。</param>
        /// <param name="useNumric">数字の使用の可否を設定します。</param>
        /// <param name="useUpperCase">英字大文字の使用の可否を設定します。</param>
        /// <param name="useLowerCase">英字小文字の使用の可否を設定します。</param>
        /// <param name="useSymbol">記号の使用の可否を設定します。</param>
        /// <returns>処理の成否を返します。</returns>
        /// <remarks>作成したパスワードを返します。</remarks>
        private string MakePasswordCore(int pwdLength, bool useNumric, bool useUpperCase, bool useLowerCase, bool useSymbol)
        {
            try
            {
                // パスワードテンプレートの変数の宣言
                var template = string.Empty;
                var firstTemplate = string.Empty;

                // 数字を使用するか判定し、パスワードテンプレート変数に数字を設定します。
                if (useNumric) template = TMP_NUMBER;

                // 大文字英字を使用するか判定し、パスワードテンプレート変数に大文字英字を設定します。
                if (useUpperCase) template += TMP_UPPER;

                // 子文字英字を使用するか判定し、パスワードテンプレート変数に子文字英字を設定します。
                if (useLowerCase) template +=TMP_LOWER;

                // 先頭文字テンプレートを設定します。
                firstTemplate = template;

                // 記号を使用するか判定し、パスワードテンプレート変数に記号を設定します。
                if (useSymbol) template += TMP_SYMBOL;

                // パスワード作成に使用する変数の宣言
                var random = new Random();
                var sb = new StringBuilder(pwdLength);

                // 結果値を格納する変数の宣言
                string result = null;

                // nudLengthの値の長さを作成します。
                for (int i = 0; i <= pwdLength - 1; i++)
                {
                    if (i == 0 && !string.IsNullOrEmpty(firstTemplate))
                    {
                        // パスワード作成処理をします。
                        int randamInteger = random.Next() % firstTemplate.Length;
                        sb.Append(firstTemplate.Substring(randamInteger, 1));
                    }
                    else
                    {
                        // パスワード作成処理をします。
                        int randamInteger = random.Next() % template.Length;
                        sb.Append(template.Substring(randamInteger, 1));
                    }
                }

                // 作成したパスワードを結果値に格納します。
                result = sb.ToString();

                return result;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// クリップボードに文字列リストを設定します。
        /// </summary>
        /// <param name="stringList">設定する文字列。</param>
        public void SetClipboard(string str)
        {
            // 文字列を区切り文字にて連結しクリップボードに設定します。
            Clipboard.SetDataObject(str, true);
        }
    }
}
