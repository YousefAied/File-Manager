using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OS_Folder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }





        //    "    Create Folder     "   //



        private void button1_Click(object sender, EventArgs e)
        {
            string folderPath = Path.Combine(textBox1.Text, textBox2.Text);
            try
            {
                Directory.CreateDirectory(folderPath);
                MessageBox.Show("Folder created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating folder: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





                                  //    "   Rename Folder    "   //




        private void button2_Click(object sender, EventArgs e)
        {
            string folderPath = Path.Combine(textBox1.Text, textBox2.Text);
            try
            {
                string newName = Microsoft.VisualBasic.Interaction.InputBox("Enter new folder name", "Rename folder", textBox2.Text);
                if (!string.IsNullOrWhiteSpace(newName))
                {
                    string newPath = Path.Combine(textBox1.Text, newName);
                    Directory.Move(folderPath, newPath);
                    MessageBox.Show("Folder renamed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox2.Text = newName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error renaming folder: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



                                     //    "    Delete Folder     "   //




        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the folder?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                string folderPath = Path.Combine(textBox1.Text, textBox2.Text);
                try
                {
                    Directory.Delete(folderPath, true);
                    MessageBox.Show("Folder deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox2.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting folder: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



                                            //    "  Move Folder    "   //



        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = "C:\\";
            saveFileDialog1.Title = "Save Folder";
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.CheckPathExists = false;
            saveFileDialog1.FileName = textBox2.Text;
            saveFileDialog1.Filter = "Folder|*.";
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string targetPath = Path.GetDirectoryName(saveFileDialog1.FileName);
                string folderPath = Path.Combine(textBox1.Text, textBox2.Text);
                try
                {
                    // Copy the folder to the target location
                    CopyDirectory(folderPath, Path.Combine(targetPath, textBox2.Text));
                    // Delete the original folder
                    Directory.Delete(folderPath, true);
                    MessageBox.Show("Folder moved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox2.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error moving folder: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private static void CopyDirectory(string sourceDir, string targetDir)
        {
            Directory.CreateDirectory(targetDir);

            // Copy each file in the source directory
            foreach (string file in Directory.GetFiles(sourceDir))
            {
                string targetFile = Path.Combine(targetDir, Path.GetFileName(file));
                File.Copy(file, targetFile);
            }

            // Copy each subdirectory in the source directory
            foreach (string sourceSubDir in Directory.GetDirectories(sourceDir))
            {
                string targetSubDir = Path.Combine(targetDir, Path.GetFileName(sourceSubDir));
                CopyDirectory(sourceSubDir, targetSubDir);
            }
        }




                                               //    ...   //



        private void button5_Click(object sender, EventArgs e)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            List<string> driveNames = new List<string>();
            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    driveNames.Add(drive.Name);
                }
            }
            if (driveNames.Count > 0)
            {
                string selectedDrive = Microsoft.VisualBasic.Interaction.InputBox("Select a drive:", "Select Drive", driveNames[0], -1, -1);
                if (selectedDrive != "")
                {
                    textBox1.Text = selectedDrive;
                }
            }
            else
            {
                MessageBox.Show("No drives available on this system.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
