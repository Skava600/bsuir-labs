using System;
using System.Management;
using System.Windows.Forms;

namespace Lab8
{
    public partial class DeviceInfoWindow : Form
    {
        private static string GetDeviceInfo(string queryString, string name)
        {
            var str = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT * FROM {queryString}");

            foreach (var i in searcher.Get())
            {
                var obj = (ManagementObject)i;

                foreach (var property in obj.Properties)
                {
                    if (property.Name == name)
                    {
                        str = property.Value.ToString();
                    }
                }
            }

            if (str == string.Empty)
            {
                return "Unknown";
            }

            return str;
        }

        public DeviceInfoWindow()
        {
            InitializeComponent();

            this.Cursor = Cursors.WaitCursor;
            motherboardLabel.Text = GetDeviceInfo("Win32_BaseBoard", "Product") + " from " + GetDeviceInfo("Win32_BaseBoard", "Manufacturer");
            processorLabel.Text = GetDeviceInfo("Win32_Processor", "Name");
            ramLabel.Text = Convert.ToInt64(GetDeviceInfo("Win32_PhysicalMemory", "Capacity")) / 1_073_741_824 + " GB";
            driveLabel.Text = GetDeviceInfo("Win32_DiskDrive", "Model") + " " + Convert.ToInt64(GetDeviceInfo("Win32_DiskDrive", "Size")) / 1_073_741_824 + " GB";
            cdromLabel.Text = GetDeviceInfo("Win32_VideoController", "Name");
            monitorLabel.Text = GetDeviceInfo("Win32_DesktopMonitor", "Caption");
            keyboardLabel.Text = GetDeviceInfo("Win32_Keyboard", "Description");
            networkLabel.Text = GetDeviceInfo("Win32_NetworkAdapter", "Description");
            this.Cursor = Cursors.Default;

        }

        private void showDeviceInfoButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            motherboardLabel.Text = GetDeviceInfo("Win32_BaseBoard", "Product") + " from " + GetDeviceInfo("Win32_BaseBoard", "Manufacturer");
            processorLabel.Text = GetDeviceInfo("Win32_Processor", "Name");
            ramLabel.Text = Convert.ToInt64(GetDeviceInfo("Win32_PhysicalMemory", "Capacity")) / 1_073_741_824 + " Gb capacity";
            driveLabel.Text = GetDeviceInfo("Win32_DiskDrive", "Model") + " of " + Convert.ToInt64(GetDeviceInfo("Win32_DiskDrive", "Size")) / 1_073_741_824 + " Gb size";
            cdromLabel.Text = GetDeviceInfo("Win32_CDROMDrive", "Drive") + " from " + GetDeviceInfo("Win32_CDROMDrive", "Manufacturer");
            monitorLabel.Text = GetDeviceInfo("Win32_DesktopMonitor", "Caption");
            keyboardLabel.Text = GetDeviceInfo("Win32_Keyboard", "Description");
            networkLabel.Text = GetDeviceInfo("Win32_NetworkAdapter", "Description");
            this.Cursor = Cursors.Default;
        }

        private void DeviceInfoWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
