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
using System.Reflection;

namespace ModernUITwitterApp.Pages.Settings
{
  /// <summary>
  /// Interaction logic for About.xaml
  /// </summary>
  public partial class About : UserControl
  {
    public About()
    {
      InitializeComponent();

      System.Diagnostics.FileVersionInfo ver =
    System.Diagnostics.FileVersionInfo.GetVersionInfo(
    System.Reflection.Assembly.GetExecutingAssembly().Location);

      //verBlock.Text = ver.ToString();
      //Assembly.
      var assm = Assembly.GetExecutingAssembly();
      var name = assm.GetName();

      verBlock.Text = name.Version.ToString();


    }
  }
}
