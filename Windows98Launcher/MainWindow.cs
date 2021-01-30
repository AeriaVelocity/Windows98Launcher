using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace Windows98Launcher
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            errorLog.Text = "No errors to report.";
            if (!Directory.Exists("qemu-98"))
            {
                MessageBox.Show("The directory 'qemu-98' does not exist. Program must close.", "Fatal error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Environment.Exit(0);
            }
            string Path = Directory.GetCurrentDirectory() + @"\qemu-98";
            if (File.Exists("windows98.iso") == false)
            {
                MessageBox.Show("The Windows 98 ISO file was not found.", "ISO file not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Setup.Enabled = false;
                errorLog.Text = "ISO image file not found." + Environment.NewLine + "Close the program, add an ISO file next to this program named \"windows98.iso\" and start the program again.";
            }
            if (File.Exists("winboot.img") == false)
            {
                MessageBox.Show("The Windows 98 boot floppy image was not found.", "Boot floppy image not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorLog.Text = "Boot floppy image file not found." + Environment.NewLine + "Close the program, add a Windows 98 boot floppy disk file next to this program named \"winboot.img\" and start the program again..";
                Setup.Enabled = false;
            }
            if (!File.Exists("win98.qcow2"))
            {
                DialogResult createHardDisk = MessageBox.Show("No virtual hard drive found for Windows 98. Create one?", "No hard drive found", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (createHardDisk == DialogResult.Yes)
                {
                    try
                    {
                        Process create = new Process();
                        create.StartInfo.UseShellExecute = true;
                        create.StartInfo.WorkingDirectory = Path;
                        create.StartInfo.FileName = "qemu-img.exe";
                        create.StartInfo.Arguments = @"create -f qcow2 ..\win98.qcow2 512M";
                        create.StartInfo.CreateNoWindow = true;
                        create.Start();
                    }
                    catch
                    {
                        MessageBox.Show("The QEMU installation is missing files. Program must close.", "Fatal error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        Environment.Exit(0);
                    }
                }
                if (createHardDisk == DialogResult.No)
                {
                    errorLog.Text = "No hard drive image found." + Environment.NewLine + "Restart the program and click \"Yes\" at the prompt to create one.";
                    Start.Enabled = false;
                    Setup.Enabled = false;
                    BootOptions.Enabled = false;
                    ClearDisk.Enabled = false;
                }
            }
            if (File.Exists("win98.qcow2"))
            {
                long HardDriveLength = new FileInfo("win98.qcow2").Length;
                if (HardDriveLength < 1048576)
                {
                    MessageBox.Show("The virtual hard drive image is smaller than 1 MB. Standard booting will be unavailable.", "Hard drive empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Start.Enabled = false;
                    errorLog.Text = "Hard drive image is empty." + Environment.NewLine + "Click \"Setup Windows 98\" to install the OS.";
                }
            }
        }

        static class Qemu
        {
            public static string Path = Directory.GetCurrentDirectory() + @"\qemu-98";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process win98 = new Process();
            win98.StartInfo.UseShellExecute = true;
            win98.StartInfo.WorkingDirectory = Qemu.Path;
            win98.StartInfo.FileName = "qemu-system-i386.exe";
            win98.StartInfo.Arguments = @"-serial stdio -drive file=..\win98.qcow2,media=disk";
            win98.StartInfo.CreateNoWindow = true;
            win98.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process win98 = new Process();
            win98.StartInfo.UseShellExecute = true;
            win98.StartInfo.WorkingDirectory = Qemu.Path;
            win98.StartInfo.FileName = "qemu-system-i386.exe";
            win98.StartInfo.Arguments = @"-serial stdio -drive file=..\win98.qcow2,media=disk -fda ..\winboot.img -cdrom ..\windows98.iso -boot d";
            win98.StartInfo.CreateNoWindow = true;
            win98.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var options = new BootOptions();
            options.Show();
        }

        private void ClearDisk_Click(object sender, EventArgs e)
        {
            DialogResult ClearHardDisk = MessageBox.Show("Are you sure you want to clear the hard disk image? You will lose EVERYTHING on it!", "Clear hard disk", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (ClearHardDisk == DialogResult.Yes)
            {
                try
                {
                    File.Delete(@"..\win98.qcow2");
                    Process create = new Process();
                    create.StartInfo.UseShellExecute = true;
                    create.StartInfo.WorkingDirectory = Qemu.Path;
                    create.StartInfo.FileName = "qemu-img.exe";
                    create.StartInfo.Arguments = @"create -f qcow2 ..\win98.qcow2 512M";
                    create.StartInfo.CreateNoWindow = true;
                    create.Start();
                }
                catch
                {
                    MessageBox.Show("The QEMU installation is missing files. Program must close.", "Fatal error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    Environment.Exit(0);
                }
                errorLog.Text = "Hard drive image is empty." + Environment.NewLine + "Click \"Setup Windows 98\" to install the OS.";
                Start.Enabled = false;

            }
            if (ClearHardDisk == DialogResult.No)
            {
                // Do nothing -- go back to the main window
            }
        }

        private void MainWindow_Activated(object sender, EventArgs e)
        {
            try
            {
                long HardDriveLength = new FileInfo("win98.qcow2").Length;
                if (HardDriveLength < 1048576)
                {
                    Start.Enabled = false;
                    errorLog.Text = "Hard drive image is empty." + Environment.NewLine + "Click \"Setup Windows 98\" to install the OS.";
                }
                else
                {
                    Start.Enabled = true;
                    errorLog.Text = "No errors to report.";
                }
            }
            catch
            {
                // nothing :D
            }

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.github.com/SpeedStriker243/Windows98Launcher");
        }

        private void Button3_Click_1(object sender, EventArgs e)
        {
            Process.Start("https://www.qemu.org/");
        }
    }
}
