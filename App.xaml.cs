﻿using System.Configuration;
using System.Data;
using System.Globalization;
using System.Windows;

namespace SAE2._01_Loxam
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            CultureInfo culture = new CultureInfo("fr-FR");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            this.Exit += App_Exit;
        }
        private void App_Exit(object sender, ExitEventArgs e)
        {
            Environment.Exit(0);
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Connexion connexionWindow = new Connexion();
            connexionWindow.Show();
        }


    }

}
