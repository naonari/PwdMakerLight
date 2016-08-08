using System;
using System.Linq;
using System.Windows.Forms;
using PwdMakerLight.Services;

namespace PwdMakerLight
{
    static class Program
    {
        // コマンドライン引数のインデックス。
        private static readonly int COMMAND_INDEX = 1;
        private static readonly string BATCH_COMMAND_LOWER_VALUE = "batch";
        private static readonly int LENGTH_INDEX = 2;

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // コマンドライン引数用の変数。
            int lengthValue;

            // コマンドライン引数の判定します。
            if (Environment.GetCommandLineArgs().Count() == 3 &&
                BATCH_COMMAND_LOWER_VALUE.Equals(Environment.GetCommandLineArgs()[COMMAND_INDEX].ToLower()) &&
                int.TryParse(Environment.GetCommandLineArgs()[LENGTH_INDEX], out lengthValue))
            {
                // パスワードシングルトンのインスタンスを取得します。
                var pss = PasswordSingletonService.GetInstance();

                // バッチ起動をします。
                var list = pss.MakePasswordByBatchProcess(lengthValue);

                // 作成したパスワードをクリップボードに設定します。
                if (list != null) pss.SetClipboard(list);
            }
            else
            {
                // 画面起動をします。
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Presentations.PwdMakerLight());
            }
        }
    }
}
