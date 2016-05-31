using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using IWshRuntimeLibrary; //For creating shortcuts (Add References > COM > Windows Script Host Object)

namespace Windows_Add_To_Start
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Setting default form visibility to hidden on load.
        /// Form is only set to be visible when user input is required.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;
            base.OnLoad(e);
        }

        public Form1()
        {
            InitializeComponent();
            string[] args = Environment.GetCommandLineArgs();
            //MessageBox.Show(args[1]);
            CopyToStartMenu(args[1]);
            //close the form
            Close();
        }

        void CopyToStartMenu(string path)
        {
            //Get win root
            string windowsDrive = Path.GetPathRoot(Environment.SystemDirectory);
            //path to start menu
            string startMenuPath = "ProgramData\\Microsoft\\Windows\\Start Menu\\Programs";
            //Get filename from path
            string fileName = Path.GetFileNameWithoutExtension(path);
            //Get extension
            string extension = Path.GetExtension(path);

            //If the path is not a shortcut
            if (extension != ".lnk" && extension != ".url" && extension != ".appref-ms")
            {
                CreateShortcut(fileName, windowsDrive + startMenuPath, path);
            }
            else //If the path is a shortcut
            {
                try
                {
                    System.IO.File.Copy(path, windowsDrive + startMenuPath + Path.DirectorySeparatorChar + fileName + extension);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
            //return;
        }

        public static void CreateShortcut(string shortcutName, string shortcutPath, string targetFileLocation)
        {
            string shortcutLocation = System.IO.Path.Combine(shortcutPath, shortcutName + ".lnk");
            MessageBox.Show(shortcutLocation);
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);
            shortcut.WorkingDirectory = Application.StartupPath; //?
            //shortcut.IconLocation = @"c:\myicon.ico";           // The icon of the shortcut
            shortcut.TargetPath = targetFileLocation;            // The path of the file that will launch when the shortcut is run
            try
            {
                shortcut.Save();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

    }
}
