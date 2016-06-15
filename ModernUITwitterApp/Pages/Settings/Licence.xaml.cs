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

namespace ModernUITwitterApp.Pages.Settings
{
  /// <summary>
  /// Interaction logic for Licence.xaml
  /// </summary>
  public partial class Licence : UserControl
  {
    public Licence()
    {
      InitializeComponent();


      versionBlock.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();



    }


  }
}
