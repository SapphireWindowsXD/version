using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace version
{
    internal class program
    {
        [STAThread]
        static void Main(string[] arguments)
        {
            Console.WriteLine("machine name: " + Environment.MachineName);
            Console.WriteLine("windows version: " + Environment.OSVersion);
            Console.WriteLine("username: " + Environment.UserName);
            Console.WriteLine("is 64-bit: " + Environment.Is64BitOperatingSystem);

            // get cpu information
            var cpu = new ManagementObjectSearcher("select name from Win32_Processor");
            foreach (var obj in cpu.Get())
            {
                Console.WriteLine("CPU name: " + obj["name"]);
            }

            // get ram information
            var ram = new ManagementObjectSearcher("select TotalPhysicalMemory from Win32_ComputerSystem");
            foreach (var obj in ram.Get())
            {
                ulong bytes = (ulong)obj["TotalPhysicalMemory"];
                Console.WriteLine("total ram: " + (bytes / 1024 / 1024) + " megabytes");
            }

            // get gpu information
            var gpu = new ManagementObjectSearcher("select name from Win32_VideoController");
            foreach (var obj in gpu.Get())
            {
                Console.WriteLine("GPU name: " + obj["name"]);
            }

            // get drive information
            foreach (var drives in DriveInfo.GetDrives())
            {
                if (drives.IsReady)
                {
                    Console.WriteLine($"drive {drives.Name} - {drives.TotalSize / 1024 / 1024 / 1024} gigabytes total, " +
                        $"{drives.AvailableFreeSpace / 1024 / 1024 / 1024} gigabytes free");
                }
            }

            Console.ReadLine();
        }
    }
}