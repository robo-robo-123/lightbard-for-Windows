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
using CoreTweet;

namespace ModernUITwitterApp.Pages.Settings
{
  /// <summary>
  /// Interaction logic for AccountSettingPage.xaml
  /// </summary>
  public partial class AccountSettingPage : UserControl
  {
    OAuth.OAuthSession session;
    internal Tokens tokens;
    public AccountSettingPage()
    {
      InitializeComponent();
    }

    private void registButton_Click(object sender, RoutedEventArgs e)
    {
      initAuthrize();
    }

    private void initAuthrize()

    {

      try

      {

        session = OAuth.Authorize(Properties.Settings.Default.ApiKey, Properties.Settings.Default.ApiSecret);

        authWeb.Source = session.AuthorizeUri;

        //pinURITextBox.Text = session.AuthorizeUri.ToString();

        pinTextBox.Clear();

        // System.Diagnostics.Process.Start(session.AuthorizeUri.ToString());

      }

      catch (Exception ex)

      {

        MessageBox.Show(ex.Message);

        //Close();

      }

    }

    private void cancelButton_Click(object sender, RoutedEventArgs e)
    {
      Properties.Settings.Default.AccessToken = null;

      Properties.Settings.Default.AccessTokenSecret = null;

      Properties.Settings.Default.ScreenName = null;

      Properties.Settings.Default.Save();

      var dialog = new ModernDialog1("ユーザ情報を削除しました");
      dialog.ShowDialog();
    }

    private void okButton_Click(object sender, RoutedEventArgs e)
    {
      if (string.IsNullOrEmpty(pinTextBox.Text)

          || System.Text.RegularExpressions.Regex.IsMatch(pinTextBox.Text, @"\D"))

      {

//        MessageBox.Show("Type numeric characters");

        var dialog = new ModernDialog1("Type numeric characters");
        dialog.ShowDialog();

        pinTextBox.Clear();

        return;

      }



      try

      {

        // PIN認証

        //MainWindow2 owner = (MainWindow2)this.Owner;
        //SettingWindow owner = (SettingWindow)this.Owner;

        tokens = session.GetTokens(pinTextBox.Text);

        // トークン保存

        Properties.Settings.Default.AccessToken = tokens.AccessToken;

        Properties.Settings.Default.AccessTokenSecret = tokens.AccessTokenSecret;

        Properties.Settings.Default.ScreenName = tokens.ScreenName;

        Properties.Settings.Default.Save();

        // 表示調整

        //owner.updatescreennameLabel(owner.tokens.ScreenName);



        //  MessageBox.Show("verified: " + tokens.ScreenName);
        var dialog = new ModernDialog1("verified: " + tokens.ScreenName + "一旦このアプリを再起動してください。");
        dialog.ShowDialog();
        pinTextBox.Clear();

        // Close();

      }

      catch (Exception ex)

      {

        // やり直し

        //MessageBox.Show(ex.Message);
        var dialog = new ModernDialog1(ex.Message);
        dialog.ShowDialog();

        initAuthrize();

      }



    }

    private void startButton_Click(object sender, RoutedEventArgs e)
    {
      initAuthrize();
    }

  }

}
