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
using ModernUITwitterApp.Class;

namespace ModernUITwitterApp.Pages.Settings
{
  /// <summary>
  /// Interaction logic for Blocks.xaml
  /// </summary>
  public partial class Blocks : UserControl
  {
    internal Tokens tokens;
    Tweets data = new Tweets();
    List<TweetClass.UserInfo> userPro;
    public Blocks()
    {
      InitializeComponent();
      tokens = data.getToken();

      blockGrid.ItemsSource = tokens.Blocks.Ids();// .List();
      blockList.ItemsSource = tokens.Blocks.Ids();// .List();
     // blocklist();
    }

    private void blocklist()
    {
      userPro = new List<TweetClass.UserInfo>();
      foreach (var status in tokens.Blocks.List())

      {
      //  data.AddInfo(userPro, status);

      }
      this.blockList.ItemsSource = userPro;
    }

    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {
      var item = this.blockList.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
        return;
      }
      else
      {
        try
        {
          tokens.Blocks.Destroy(user_id => item.UserId);
          blocklist();
        }
        catch (Exception ex)
        {
          var dialog = new ModernDialog1("sippai");
          dialog.ShowDialog();
        }
      }

    }
  }
}
