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
  /// Interaction logic for User.xaml
  /// </summary>
  public partial class User : UserControl
  {
    internal Tokens tokens;


    List<TweetClass.TweetInfo> tweet;
    List<TweetClass.TweetInfo> userTweet;

    List<TweetClass.UserInfo> userPro2;

    List<TweetClass.UserInfo> user;
    List<TweetClass.UserInfo> userPro;

    public string userId { get; set; }
    public string ScreenName { get; set; }
    UserResponse showedUser;

    public User()
    {
      InitializeComponent();
      var data = new Tweets();

      tokens = data.getToken();

      if (!string.IsNullOrEmpty(Properties.Settings.Default.UserId))
      {
        //tweetbox.Text = Properties.Settings.Default.UserId;
        userId = Properties.Settings.Default.UserId;
        Properties.Settings.Default.UserId = null;
        //userInfo();
      }
      else
      {

        ScreenName = Properties.Settings.Default.ScreenName;
        //var accountUser = tokens.Users.Show(screen_name => ScreenName);
        //userId = accountUser.Id.ToString();
        userInfo2();
      }

    }

    private void userInfo2()
    {

      if (tokens != null)

      {


        userPro = new List<TweetClass.UserInfo>();


        try

        {

          showedUser = tokens.Users.Show(screen_name => ScreenName);


          userPro.Add(new TweetClass.UserInfo
          {
            UserName = showedUser.Name,
            UserId = showedUser.Id,
            ScreenName = "@" + showedUser.ScreenName,
            ProfileImageUrl = showedUser.ProfileImageUrlHttps.OriginalString,
            FollowCount = showedUser.FollowersCount,
            FavCount = showedUser.FavouritesCount,
            FollowerCount = showedUser.FriendsCount,
            Prof = showedUser.Description

          }
);

          this.userInfoView.ItemsSource = userPro;

          //userTimeline2();
        }

        catch (Exception ex)

        {


        }

      }

    }

    private void timeLineButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void followerButton_Click(object sender, RoutedEventArgs e)
    {
      followerTimeline();
    }

    private void fllowButton_Click(object sender, RoutedEventArgs e)
    {
      followTimeline();
    }

    private void followTimeline()
    {

      userTLView.Visibility = Visibility.Visible; ;

      if (tokens != null)

      {
        //this.userTLView.Items.Clear();

        userPro2 = new List<TweetClass.UserInfo>();
        var data = new Tweets();
        try

        {

          // var showedUser = tokens.Favorites.List(screen_name => ScreenName, count => 200);

          foreach (var status in tokens.Friends.List(screen_name => ScreenName, count => 200))

          {
            data.AddInfo(userPro2, status);

          }
          this.userTLView.ItemsSource = userPro2;
        }

        catch (Exception ex)

        {


        }

      }
    }

    private void followerTimeline()
    {

      userTLView.Visibility = Visibility.Visible; ;

      if (tokens != null)

      {
        //this.userTLView.Items.Clear();

        userPro2 = new List<TweetClass.UserInfo>();
        var data = new Tweets();
        try

        {

          // var showedUser = tokens.Favorites.List(screen_name => ScreenName, count => 200);

          foreach (var status in tokens.Followers.List(screen_name => ScreenName, count => 200))

          {
            data.AddInfo(userPro2, status);

          }
          this.userTLView.ItemsSource = userPro2;
        }

        catch (Exception ex)

        {


        }

      }
    }

    private void favButton_Click(object sender, RoutedEventArgs e)
    {

    }
  }
}
