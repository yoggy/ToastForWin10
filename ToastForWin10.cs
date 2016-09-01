//
// ToastForWin10.exe - アクションセンターにメッセージを表示するだけのプログラム
//
// Windows.winmdの参照の設定方法メモ
//
//   (1) csprojファイルに<TargetPlatformVersion>設定を追加する
//       この設定はVisualStudioからはできないので要注意。
//       タグ<PropertyGroup>の中に <TargetPlatformVersion>8.0</TargetPlatformVersion> を追加する
//       csprojファイル編集後にVisualStudioでプロジェクトファイルを開く
//
//   (2) Windows.winmdの追加
//       Visual Studioのメニュー"Poject"→"Add Reference..."を開き、
//       左タブのWindows→Coreの中に"Windows"という名前で
//       Windows.winmdが表示されているので、チェックを入れて参照に追加する。
//
//   (3) System.Runtime.WindowsRuntime.dllの追加
//       左側タブのReferenceを選択後、ダイアログ右下の"Browse..."ボタンを押す。
//       次のファイルを直接選択し、参照へ追加する。
//       C:\Program Files(x86)\Reference Assemblies\Microsoft\Framework\.NETCore\v4.5\System.Runtime.WindowsRuntime.dll
//
// GitHub
//     https://github.com/yoggy/ToastForWin10
//
// license
//     Copyright (c) 2016 yoggy <yoggy0@gmail.com>
//     Released under the MIT license
//     http://opensource.org/licenses/mit-license.php;
//
using System;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace ToastForWin10
{
    class ToastForWin10
    {
        static void usage()
        {
            Console.WriteLine("usage : ToastForWin10.exe [title] [message]");
            Console.WriteLine();
            Environment.Exit(0);
        }

        static void Main(string[] args)
        {
            if (args.Length < 2) usage();

            string title = args[0];
            string message = args[1];

            XmlDocument xml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            //Console.WriteLine(xml.GetXml());

            var texts = xml.GetElementsByTagName("text");
            texts[0].AppendChild(xml.CreateTextNode(title));
            texts[1].AppendChild(xml.CreateTextNode(message));

            ToastNotification toast = new ToastNotification(xml);
            ToastNotificationManager.CreateToastNotifier(title).Show(toast);
        }
    }
}
