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

namespace ModernUITwitterApp.Pages
{
  /// <summary>
  /// Interaction logic for Search.xaml
  /// </summary>
  public partial class Search : UserControl
  {
    internal Tokens tokens;

    List<TweetClass.TweetInfo> tweet;
    List<TweetClass.TweetInfo> userTweet;

    List<TweetClass.UserInfo> user;
    List<TweetClass.UserInfo> userPro;
    public long? ReplyId { get; set; }

    public string userId { get; set; }
    public string ScreenName { get; set; }

    public Search()
    {
      InitializeComponent();
      var data = new Tweets();

      tokens = data.getToken();
    }

    private void searchButtom_Click(object sender, RoutedEventArgs e)
    {

      if (tokens != null)
      {
        tweet = new List<TweetClass.TweetInfo>();
        var data = new Tweets();
        try
        {
          foreach (var status in tokens.Search.Tweets(q => searchbox.Text, count => 200, lang => "ja"))
          {
            data.Addtweet(tweet, status);
          }
          listView.ItemsSource = tweet;
        }
        catch (Exception ex)
        {
          //viewTextBox.AppendText(ex.Message);
        }
      }


    }

    private void userSearchButtom_Click(object sender, RoutedEventArgs e)
    {
      if (tokens != null)
      {
        user = new List<TweetClass.UserInfo>();
        var data = new Tweets();
        try
        {
          foreach (var status in tokens.Users.Search(q => searchbox.Text, count => 200))
          {
            user.Add(new TweetClass.UserInfo
            {
              UserName = status.Name,
              UserId = status.Id,
              ScreenName = "@" + status.ScreenName,
              ProfileImageUrl = status.ProfileImageUrlHttps.OriginalString,
              FollowCount = status.FollowersCount,
              FavCount = status.FavouritesCount,
              FollowerCount = status.FriendsCount,
              Prof = status.Description

            });
          }
          userInfoView.ItemsSource = user;
        }
        catch (Exception ex)
        {
          //viewTextBox.AppendText(ex.Message);
        }
      }

    }
  }
}
