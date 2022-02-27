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

namespace GarticWordsTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GarticWordsTool.Net.Gartic gartic = new Net.Gartic("garticio=s%3Acc8568ea-3202-42df-966a-10488a795c52.0y101WJtUBAyDczgWzJMQ3vupdkviiN%2F6TS4p9yfCtc; __cf_bm=N28pL58R_eNSwTRMAnL9h9qNcr1pVKyOhVjRh7sLorI-1645927671-0-AbGMogNVrF0NnUTf6aItZqUuXpEY/yJ/tjrIAo26Lg9LNpjiQ5jlEOtp/GKXWyY8GRPwLeRXndVWn6b7nQwIUyQ=; _ga=GA1.2.1605748715.1645927675; _gid=GA1.2.71720014.1645927675; _gat_gtag_UA_3906902_31=1; _ga_VR1WBQ9P5N=GS1.1.1645927673.1.0.1645927677.0");
            gartic.GetLoginProfile();
        }
    }
}
