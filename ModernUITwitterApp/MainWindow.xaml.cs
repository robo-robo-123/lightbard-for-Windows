using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ModernUITwitterApp
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : ModernWindow
  {
    public string test = null;
    public MainWindow()
    {
      InitializeComponent();

      if (!Properties.Settings.Default.IsUpgrade)
      {
        // 前バージョンの情報取得する
        Properties.Settings.Default.Upgrade();

        // アップグレードを行った事をセットする
        Properties.Settings.Default.IsUpgrade = true;
        Properties.Settings.Default.Save();

        //var dialog = new ModernDialog1("設定ファイルのアップグレードが行われました");
        //dialog.ShowDialog();
      }

      if (!string.IsNullOrEmpty(Properties.Settings.Default.ScreenName))
      {
        mainLinkGroup.DisplayName = Properties.Settings.Default.ScreenName;
      }

    }

    private void ModernWindow_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Up)
      {
        test = test + "↑";
        if (test == "↑↑↓↓←→←→BA")
        {
        var dialog = new ModernDialog1(test);
        dialog.ShowDialog();
        }

      }
      else if (e.Key == Key.Down)
      {
        test = test + "↓";
        if (test == "↑↑↓↓←→←→BA")
        {
          var dialog = new ModernDialog1(test);
          dialog.ShowDialog();
        }
      }
      else if (e.Key == Key.Left)
      {
        test = test + "←";
        if (test == "↑↑↓↓←→←→BA")
        {
          var dialog = new ModernDialog1(test);
          dialog.ShowDialog();
        }
      }
      else if (e.Key == Key.Right)
      {
        test = test + "→";
        if (test == "↑↑↓↓←→←→BA")
        {
          var dialog = new ModernDialog1(test);
          dialog.ShowDialog();
        }
      }
      else if (e.Key == Key.B)
      {
        test = test + "B";
        if (test == "↑↑↓↓←→←→BA")
        {
          var dialog = new ModernDialog1(test);
          dialog.ShowDialog();
        }
      }
      else if (e.Key == Key.A)
      {
        test = test + "A";
        if (test == "↑↑↓↓←→←→BA")
        {
          var dialog = new ModernDialog1(test);
          dialog.ShowDialog();
        }
      }
      else
      {
        test = null;
      }
    }
  }
}
