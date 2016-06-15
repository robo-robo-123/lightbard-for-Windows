using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using ModernUITwitterApp.Class;


namespace ModernUITwitterApp.Pages.Settings
{
  /// <summary>
  /// Interaction logic for Mutes.xaml
  /// </summary>
  public partial class Mutes : UserControl
  {
    internal Tokens tokens;
    Tweets data = new Tweets();
    List<TweetClass.UserInfo> userPro;
    List<string> words = new List<string>();
    StringCollection words2 = new StringCollection();
    public Mutes()
    {
      InitializeComponent();
      tokens = data.getToken();

      //muteGrid.ItemsSource = tokens.Mutes.Users.List();
      //muteList.ItemsSource = tokens.Mutes.Users.List();
      mutelist();

    }

    private void mutelist()
    {
      userPro = new List<TweetClass.UserInfo>();
      foreach (var status in tokens.Mutes.Users.List())

      {
        data.AddInfo(userPro, status);

      }
      this.muteList.ItemsSource = userPro;
    }

    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {
      var item = this.muteList.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
        return;
      }
      else
      {
        try
        {
          tokens.Mutes.Users.Destroy(user_id => item.UserId);
          mutelist();
        }
        catch (Exception ex)
        {
          var dialog = new ModernDialog1("sippai");
          dialog.ShowDialog();
        }
      }
    }

    private void muteButton_Click(object sender, RoutedEventArgs e)
    {
      words.Add("猫");
      words2.Add(muteBox.Text);
      Properties.Settings.Default.mutewords = words2;

      StringCollection nameList = (StringCollection)Properties.Settings.Default.mutewords;
      string[] names = nameList.Cast<string>().ToArray();

      foreach(var word_ in names)
      {
        muteBlock.Text += word_;
      }

    }
  }
}
