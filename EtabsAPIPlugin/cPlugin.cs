
using EtabsAPIPlugin.Infrastructure.Bootstrap;
using EtabsAPIPlugin.UI.Screens;
using ETABSv1;
using System.Windows;

namespace EtabsAPIPlugin
{
    public class cPlugin : cPluginContract
    {
        private int errorCode = 0;//Default return code is no error
        private static string _modality = "Non-Modal";
        public void Main(ref ETABSv1.cSapModel SapModel, ref ETABSv1.cPluginCallback pluginCallback)
        {
            AssemblyResolver.Register(); 
            MainWindow mainWindow = new MainWindow();
            try
            {
                mainWindow.SetSapModel(ref SapModel, ref pluginCallback);
                if(string.Compare(_modality,"Non-Modal",true)==0)
                {
                    mainWindow.Show();
                }
                else
                {
                    mainWindow.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                errorCode = -1;
                MessageBox.Show("The follow error terminate the plugin: " + ex.Message);

                // call Finish to inform the CSI program that the plugin has terminated
                try
                {
                    pluginCallback.Finish(errorCode); // error code -1 will be visible to plugin end-user for debugging purposes
                    
                }
                catch (Exception)
                {

                }
            }
        }
        public int Info(ref string Text)
        {
            Text = "My Etabs API plugin template\n";
            return 0;
        }
    }

}
