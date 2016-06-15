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
using System.Collections.ObjectModel;
using CoreTweet.Core;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace ModernUITwitterApp.Pages
{
  /// <summary>
  /// Interaction logic for Home.xaml
  /// </summary>
  public partial class Home : UserControl
  {

    internal Tokens tokens;

    public class Tweet
    {
      public string UserName { get; set; }
      public long? UserId { get; set; }
      public string Text { get; set; }
      public string ScreenName { get; set; }
      public long Id { get; set; }
      public string ProfileImageUrl { get; set; }
      public string Date { get; set; }
      public string Via { get; set; }
      // public Status retUser { get; set; }
      public string Url { get; set; }
      public string RetweetUser { get; set; }
      // public string ProfileImageUrl { get; set; }
    }

    List<Tweet> tweet;
    List<Tweet> userTweet;

    public class UserInfo
    {
      public string UserName { get; set; }
      public long? UserId { get; set; }
      public string Text { get; set; }
      public string ScreenName { get; set; }
      public long Id { get; set; }
      public string ProfileImageUrl { get; set; }
      public string Prof { get; set; }
      public string Date { get; set; }
      public string Via { get; set; }
      public int FollowCount { get; set; }
      public int FavCount { get; set; }
      public int TweetCount { get; set; }
      public int FollowerCount { get; set; }
      // public string ProfileImageUrl { get; set; }
    }
    List<UserInfo> user;
    List<UserInfo> userPro;

    public string userId { get; set; }
    public string ScreenName { get; set; }
    //UserResponse showedUser;

    public Home()
    {
      InitializeComponent();

      if (!string.IsNullOrEmpty(Properties.Settings.Default.AccessToken)

&& !string.IsNullOrEmpty(Properties.Settings.Default.AccessTokenSecret))

      {

        tokens = Tokens.Create(

            Properties.Settings.Default.ApiKey

            , Properties.Settings.Default.ApiSecret

            , Properties.Settings.Default.AccessToken

            , Properties.Settings.Default.AccessTokenSecret);
      }

      tweetLoad();
    }

    private void tweetLoad()
    {
      if (tokens != null)

      {

        // var tweet = new Tweet();

        tweet = new List<Tweet>();

        //viewTextBox.Clear();
        //var stream = tokens.Streaming.StartStream(CoreTweet.Streaming.StreamingType.User,
        //        new StreamingParameters(replies => "all"));

        try

        {

          //  foreach (var message in stream)
          foreach (var status in tokens.Statuses.HomeTimeline(count => 200))

          {
            // http://qiita.com/lambdalice/items/55b1a3d8403ecc603b47#2-4 例1: REST API 

            // {ユーザー名}: {投稿内容}{改行}



            if (status.RetweetedStatus != null)
            {
              Regex http = new Regex(@"http://(?<domain>[\w\.]*)/(?<path>[\w\./]*)");
              Match m = http.Match(status.Text);

              tweet.Add(new Tweet
              {


                UserName = status.RetweetedStatus.User.Name + " ",
                UserId = status.RetweetedStatus.User.Id,
                ScreenName = "@" + status.RetweetedStatus.User.ScreenName,
                ProfileImageUrl = status.RetweetedStatus.User.ProfileImageUrlHttps.OriginalString,
                Text = status.RetweetedStatus.Text,
                Date = status.RetweetedStatus.CreatedAt.ToString(),
                Id = status.RetweetedStatus.Id,
                //retUser = status.RetweetedStatus,
                Url = m.Value.ToString(),
                Via = status.RetweetedStatus.Source,
                RetweetUser = "Retweeted by @" + status.User.Name

              }
                );


            }
            else
            {
              Regex http = new Regex(@"http://(?<domain>[\w\.]*)/(?<path>[\w\./]*)");
              Match m = http.Match(status.Text);

              //  viewTextBox.AppendText(string.Format("{0}: {1}{2}"
              tweet.Add(new Tweet
              {

                UserName = status.User.Name + " ",
                UserId = status.User.Id,
                ScreenName = "@" + status.User.ScreenName,
                ProfileImageUrl = status.User.ProfileImageUrlHttps.OriginalString,
                Text = status.Text,
                Date = status.CreatedAt.ToString(),
                Id = status.Id,
                //retUser = status.RetweetedStatus,
                Url = m.Value.ToString(),
                Via = status.Source

              }
              );
              //  tweet.ScreenName = status.User.ScreenName;

              //    tweet.Text = status.Text;


            }







            //    , Environment.NewLine));

            /*
                        // http://qiita.com/lambdalice/items/55b1a3d8403ecc603b47#2-4 例1: REST API 

                        // {ユーザー名}: {投稿内容}{改行}
                        var status = (message as StatusMessage).Status;
                        viewTextBox.AppendText(string.Format("{0}:{1}", status.User.ScreenName, status.Text));


                             (string.Format("{0}: {1}{2}"

                               , status.User.ScreenName

                               , status.Text

                               , Environment.NewLine));
                               */
          }
          listView.ItemsSource = tweet;
          //this.tweetGrid.ItemsSource = tweet;



        }



        catch (Exception ex)

        {

          //viewTextBox.AppendText(ex.Message);

        }

      }

    }

    private void getTl_Click(object sender, RoutedEventArgs e)
    {

    }
  }
}
