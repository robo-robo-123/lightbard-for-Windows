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
using System.Diagnostics;
using ModernUITwitterApp.Class;

namespace ModernUITwitterApp.Pages
{
  /// <summary>
  /// Interaction logic for BasicPage1.xaml
  /// </summary>
  public partial class BasicPage1 : UserControl
  {

    iTunesLib.iTunesApp app;

    ModernUITwitterApp.Class.Tweets data = new ModernUITwitterApp.Class.Tweets();

    // iTunesLib.iTunesApp app;

    public BasicPage1()
    {
      InitializeComponent();
      data.getToken();
    }

    private void openButton_Click(object sender, RoutedEventArgs e)
    {
      //     var player = new NowPlayingLib.iTunes();
      try
      {
        app = new iTunesLib.iTunesApp();
      }
      catch (Exception)
      {
        MessageBox.Show("オープンに失敗しました");
      }


    }

    private void musicButton_Click(object sender, RoutedEventArgs e)
    {
      try
      {
        app = new iTunesLib.iTunesApp();
      }
      catch (Exception)
      {
        MessageBox.Show("オープンに失敗しました");
      }

      if (app != null)
      {
        try
        { 
        //何も再生していないと例外発生するぞ！（ここでは面倒なので何もしていない）
        string trackName = app.CurrentTrack.Name;
        string trackArtist = app.CurrentTrack.Artist;
        var dialog = new ModernDialog1("アーティスト:" + trackArtist + "\n曲名:" + trackName);
        dialog.ShowDialog();
          this.tweetInputBox.Text = "#nowplaying"+"\nアーティスト:" + trackArtist + "\n曲名:" + trackName;
          //あとDisposeか何かしてやらないと、これを閉じた後もiTunesがまだ繋がってると認識するっぽい。
        }
        catch(Exception ex)
        {
          var dialog = new ModernDialog1("何も再生していません");
          dialog.ShowDialog();
        }

      }

    }

    private void tweetButton_Click(object sender, RoutedEventArgs e)
    {
     // ModernUITwitterApp.Pages.BasicPage1 timeline = this;
     // FileInfo fileinfo = null;

      data.tweetMethod(this.tweetInputBox.Text);
      this.tweetInputBox.Text = null;
    }


    /*
     public async void SelectPlayer(string parameter)
          { 
            //  this.IsMenuShown = false; 
              MediaPlayerBase player; 


              try 
              { 
             //     this.PlayerName = "PLEASE WAIT..."; 
             //     this.ErrorMessage = null; 
             //     this.MediaItem = null; 


                  switch (parameter) 
                  { 
                      case "WINDOWS MEDIA PLAYER": 
                          //player = await Task.Run(() => new WindowsMediaPlayer()); 
                          player = new WindowsMediaPlayer();
                          break; 
                      case "ITUNES": 
                          //player = await Task.Run(() => new iTunes()); 
                          player = new iTunes();
                          break; 
                      default: 
                          return; 
                  } 


                  var p = player as INotifyPlayerStateChanged; 
                  if (p != null) 
                  { 
                      p.CurrentMediaChanged += SetCurrentMedia; 
                      p.Closed += ResetPlayer; 
                  } 
                  SetCurrentMedia(player, new CurrentMediaChangedEventArgs(await player.GetCurrentMedia())); 


                  this.Player = player; 
                  this.PlayerName = parameter; 
              } 
              catch (Exception ex) 
              { 
                  this.PlayerName = "ERROR"; 
                  this.MediaItem = null; 
                  this.ErrorMessage = ex.GetType().FullName + "\n" + ex.Message; 
              } 
          }

     private void SetCurrentMedia(object sender, CurrentMediaChangedEventArgs e)
          { 
              this.MediaItem = e.CurrentMedia; 
          } 

          private void ResetPlayer(object sender, EventArgs e)
          { 
              this.MediaItem = null; 
              this.PlayerName = null; 
          }
          */
  }
}
