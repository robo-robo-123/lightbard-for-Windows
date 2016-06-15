using System;
using System.IO;
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
using ModernUITwitterApp.Class;
using Microsoft.Win32;
using System.Globalization;
using System.Collections.Specialized;

namespace ModernUITwitterApp.Pages
{
  /// <summary>
  /// Interaction logic for TimeLine.xaml
  /// </summary>
  /// 
  /*
    public class VisibilityConverter : IValueConverter
    {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
        var text = value is string ? (string)value : string.Empty;
        return string.IsNullOrEmpty(text) ? Visibility.Collapsed : Visibility.Visible;
      }



      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
        throw new System.NotImplementedException();
      }
    }
    */

  public partial class TimeLine : UserControl
  {

    internal Tokens tokens;

    List<TweetClass.TweetInfo> tweet;
    List<TweetClass.TweetInfo> userTweet;

    List<TweetClass.UserInfo> user;
    List<TweetClass.UserInfo> userPro;
    public long? ReplyId { get; set; }

    public string userId { get; set; }
    public string ScreenName { get; set; }
    public string filename { get; set; }
    public FileInfo fileinfo { get; set; }

    Tweets data = new Tweets();
    //public Tweets Tweet = new Tweets();
    // public Tweets data;
    string authenticationHeaderValue;
    StringCollection nameList = (StringCollection)Properties.Settings.Default.mutewords;

    public class ImageList
    {
      BitmapImage imageSource1 { get; set; }
      BitmapImage imageSource2 { get; set; }
      BitmapImage imageSource3 { get; set; }
      BitmapImage imageSource4 { get; set; }
    }


    public TimeLine()
    {
      InitializeComponent();

     // var data = new Tweets();

      tokens = data.getToken();

      tweetLoad();

      AdmAccessToken admToken;


      var admAuth = new AdmAuthentication("roob_twi", "0OGK8MPcfIGFX6BtYhCbBI5V+EBp//2E3BF95HOu4Vs=");

      try
      {
        admToken = admAuth.GetAccessToken();
        //      textAfter.Text = admToken.ToString();
        authenticationHeaderValue = "Bearer " + admToken.access_token;
      }
      catch (Exception e)
      {

      }

    }






    private void tweetLoad()
    {
      if (tokens != null)
      {
        tweet = new List<TweetClass.TweetInfo>();
        //var data = new Tweets();
        try
        {
          tweet = data.tweetload();
          listView.ItemsSource = tweet;
        }
        catch (Exception ex)
        {
          //viewTextBox.AppendText(ex.Message);
        }
      }
    }

    private void tweetMethod(string test, FileInfo filename)
    {
      try
      {
        if (filename == null)
        {
          tokens.Statuses.Update(status => test);
          var dialog = new ModernDialog1("ツイートしました");
          dialog.ShowDialog();
        }
        else
        {
          tokens.Statuses.UpdateWithMedia(status => test, media => filename);
          //tokens.Media.Upload(media => fileinfo);
          var dialog = new ModernDialog1("ツイートしました");
          dialog.ShowDialog();
        }
      }
      catch (Exception ex)
      {
        var dialog = new ModernDialog1("空白です");
        dialog.ShowDialog();
      }
    }

    private void tweetButtom_Click(object sender, RoutedEventArgs e)
    {
      //ModernUITwitterApp.Pages.TimeLine timeline = this;
      data.tweetMethod(tweetInputBox.Text, fileinfo);
/*
      try
      {
        if (filename == null)
        {
          tokens.Statuses.Update(status => tweetInputBox.Text);
          var dialog = new ModernDialog1("ツイートしました");
          dialog.ShowDialog();
        }          
         else
        {
          tokens.Statuses.UpdateWithMedia(status => tweetInputBox.Text, media => fileinfo);
          //tokens.Media.Upload(media => fileinfo);
          var dialog = new ModernDialog1("ツイートしました");
          dialog.ShowDialog();
        }
      }
      catch (Exception ex)
      {
        var dialog = new ModernDialog1("空白です");
        dialog.ShowDialog();
      }
*/

      this.tweetInputBox.Text = null;
      this.sendImage1.Source = null;
      this.sendImage2.Source = null;
      this.sendImage3.Source = null;
      this.sendImage4.Source = null;
      //tweetInputBox.Text = "";
      this.tweetStackPanel.Visibility = Visibility.Hidden;

      tweetLoad();
      //tweetStackPanel.Visibility = Visibility.Visible;
      
    }

    private void loadButtom_Click(object sender, RoutedEventArgs e)
    {
      tweetLoad();
    }

    private void replyButtom_Click(object sender, RoutedEventArgs e)
    {
      tweetImage1.Source = null;
      tweetImage2.Source = null;
      tweetImage3.Source = null;
      tweetImage4.Source = null;
      tweetStackPanel.Visibility = Visibility.Hidden;

      var item = this.listView.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
        return;
      }
      else
      {
        tweet = new List<TweetClass.TweetInfo>();
        //var data = new Tweets();

        ReplyId = item.Id;
        replyBox.Text = item.ScreenName + " ";
        replyStackPanel.Visibility = Visibility.Visible;



        tweet = data.replytweetinfo(item);

        replyView.ItemsSource = tweet;

        if(item.urls != null)
        {
          urlView.ItemsSource = item.urls;
        }


        if (item.media != null)
        {
          
        int media_num = item.media.Length;
          //var media = item.media;
          //int test = media.Length;
          //replyBox.Text = item.media_number.ToString();
          for (int n=0;n<media_num;n++)
          {
            BitmapImage imageSource = new BitmapImage(item.media[n].MediaUrl);

            switch(n)
            { 
              case 0:
              tweetImage1.Source = imageSource;

                break;
              case 1:
                tweetImage2.Source = imageSource;
                break;
              case 2:
                tweetImage3.Source = imageSource;
                break;
              case 3:
                tweetImage4.Source = imageSource;
                break;

            }

          }
          
        }
        
        replyBlock.Text = "Reply to " + item.ScreenName;
      }
    }

    private void replyTweetButtom_Click(object sender, RoutedEventArgs e)
    {
      fileinfo = null;
      data.tweetMethod(replyBox.Text, fileinfo, ReplyId);
     // tokens.Statuses.Update(status => replyBox.Text, in_reply_to_status_id => ReplyId);
     // var dialog = new ModernDialog1("リプライを送りました");
     // dialog.ShowDialog();
      ReplyId = 0;
      replyBox.Text = null;
      replyStackPanel.Visibility = Visibility.Hidden;
      tweetLoad();
    }

    private void userbutton_Click(object sender, RoutedEventArgs e)
    {
      //ModernUITwitterApp.Pages.TimeLine timeline = new ModernUITwitterApp.Pages.TimeLine();
      ModernUITwitterApp.Pages.TimeLine timeline = this;
      timeline.tweetStackPanel.Visibility = Visibility.Collapsed;

      // var dialog = new ModernDialog1("testoasij");

      // dialog.ShowDialog();
     

    }

    private void favbutton_Click(object sender, RoutedEventArgs e)
    {
      var item = this.listView.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
        return;
      }
      else
      {
        try
        {
          tokens.Favorites.Create(id => item.Id);
          var dialog = new ModernDialog1("favしました！");
          dialog.ShowDialog();
        }
        catch (Exception ex)
        {
          var dialog = new ModernDialog1("既にfavされています");
          dialog.ShowDialog();
        }
      }
    }

    private void retbutton_Click(object sender, RoutedEventArgs e)
    {
      var item = this.listView.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
        return;
      }
      else
      {
        try
        {
          tokens.Statuses.Retweet(id => item.Id);
          var dialog = new ModernDialog1("リツイートしました！");
          dialog.ShowDialog();
        }
        catch (Exception ex)
        {
          var dialog = new ModernDialog1("既にリツイートしています");
          dialog.ShowDialog();
        }
      }
    }

    private void linkbutton_Click(object sender, RoutedEventArgs e)
    {
      var item = this.listView.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
        return;
      }
      else

      {
        try

        {
          System.Diagnostics.Process.Start(item.Url);

        }
        catch (Exception ex)
        { }
      };

    }

    private void retFavItem_Click(object sender, RoutedEventArgs e)
    {
      var item = this.listView.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
        return;
      }
      else
      {
        try
        {
          tokens.Statuses.Retweet(id => item.Id);
          tokens.Favorites.Create(id => item.Id);
          var dialog = new ModernDialog1("リツイート+お気に入りに登録しました！");
          dialog.ShowDialog();
        }
        catch (Exception ex)
        {
          var dialog = new ModernDialog1("既にリツイートもしくはお気に入りに登録をしています");
          dialog.ShowDialog();
        }


      }

    }

    private void tweetboxButtom_Click(object sender, RoutedEventArgs e)
    {
      replyStackPanel.Visibility = Visibility.Hidden;
      tweetStackPanel.Visibility = Visibility.Visible;
    }

    private void photoButtom_Click(object sender, RoutedEventArgs e)
    {
      // ファイルを開くダイアログ
      OpenFileDialog dlg = new OpenFileDialog();
      dlg.DefaultExt = ".jpg";
   //   dlg.Filter = "JPEG|*.jpg|BMP|*.bmp";

      Nullable<bool> result = dlg.ShowDialog();

      if (result == true)
      {
        filename = dlg.FileName;
        Uri path = new Uri(filename);
        fileinfo = new FileInfo(filename);
        tweetInputBox.Text = fileinfo.ToString();
        BitmapImage imageSource = new BitmapImage(new Uri(filename));

        sendImage1.Source = imageSource;
      }
    }

    private void closeButtom_Click(object sender, RoutedEventArgs e)
    {
      tweetStackPanel.Visibility = Visibility.Hidden;
      sendImage1.Source = null;
    }

    private void replyCloseButtom_Click(object sender, RoutedEventArgs e)
    {
      replyStackPanel.Visibility = Visibility.Hidden;
    }

    private void OpenPageRequestNavigate(object sender, RequestNavigateEventArgs e)
    {
      System.Diagnostics.Process.Start(e.Uri.ToString());
    }

    private void transbutton_Click(object sender, RoutedEventArgs e)
    {
      var item = this.listView.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
        return;
      }
      else
      {
        
        Translator.LanguageServiceClient client = new Translator.LanguageServiceClient();

        var result = client.Translate(authenticationHeaderValue, item.Text, "ja", "en", null, null);
        translateBox.Text = result;


      }

    }

    private void muteItem_Click(object sender, RoutedEventArgs e)
    {
      var item = this.listView.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
        return;
      }
      else
      {
        try
        {
          tokens.Mutes.Users.Create(id => item.UserId);
        }
        catch(Exception ex)
        {
          var dialog = new ModernDialog1("sippai");
          dialog.ShowDialog();
        }
      }
    }
  }
}
