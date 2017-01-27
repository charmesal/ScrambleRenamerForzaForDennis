using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace ScrambleRenamerForza
{
    public partial class Form1 : Form
    {
        List<string> dirs;
        public Form1()
        {
            InitializeComponent();
            dirs = new List<string>();
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);
                    renameFilesRecursive(fbd.SelectedPath);
                    MessageBox.Show("kappa", "done");
                }
            }
        }

        private void renameFilesRecursive(string selectedPath)
        {
            List<string> dirs = new List<string>();
            List<string> files = new List<string>();

            dirs = Directory.GetDirectories(selectedPath).ToList();
            files = Directory.GetFiles(selectedPath).ToList();

            foreach (String s in dirs)
            {
                string renamedFolder = "";
                DirectoryInfo di = new DirectoryInfo(s);
                renameFilesRecursive(s);
                try
                {
                    renamedFolder = '\\' + rename(di.Name);
                    Directory.Move(di.FullName, selectedPath + renamedFolder);

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + "\nquitting");
                    Application.Exit();
                }
                listBox1.Items.Add(di.Name + "\t->\t" + renamedFolder);
                listBox1.Items.Add("");

                listBox1.Refresh();

            }
            listBox1.Items.Add(selectedPath + ":");
            listBox1.Refresh();

            foreach (String fs in files)
            {
                if (File.Exists(fs))
                {
                    string renamedFile = "";
                    FileInfo fi = new FileInfo(fs);
                    try
                    {
                        renamedFile = '\\' + rename(fi.Name);
                        File.Move(fi.FullName, fi.DirectoryName +renamedFile);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message + "\nquitting");
                        Application.Exit();
                    }
                    listBox1.Items.Add("\t" + fi.Name + "\t->\t" + renamedFile);
                    listBox1.Refresh();
                }
            }
        }

        private string rename( string fs)
        {
            string newString = "";
            for(int i = 0; i< fs.Length; i++)
            {
                newString += checkTable(fs[i]);
            }
            return newString;
        }

        private char checkTable(char c)
        {
            char returnval = '\0';
            char[] scrabmled =
            {
                'l', '`', '^', '6', 'q', 'v', '{', '@', '$', '7', 's', 'b', 'g', '8', 'h', 'u', 'f', '4', '~', '1', '=', '\'', 'm', ']', '!', ',', 'y', '_', '[', '0', 'w', 'k', '(', '2', 'j', '}', ';', '+', '.'
            };

            char[] unscrambled =
            {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '_', '-', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.'
            };

            for(int i = 0; i < scrabmled.Length; i++)
            {
                if(scrabmled[i] == c)
                {
                    returnval = unscrambled[i];
                    break;
                }
            }

            return returnval;
        }
    }
}
