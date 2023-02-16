using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoreHub2._0.Models
{
    internal class About
    {
        public string Title => AppInfo.Name;
        public string Version => AppInfo.VersionString;
        public string MoreInforUrl => "https://aka.ms/maui";
        public string Message => "This app is writting in XAML and C# with .NET MAUI. This app will only get better from here!";
    }
}