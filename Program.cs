using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Linq;

class Program
{
    [STAThread]
    static void Main()
    {
        string userName = Environment.UserName;
        string path = $@"C:\Users\{userName}\AppData\Local\Autodesk\Revit";

        // Check if Revit (excluding RevitAccelerator) is running
        if (Process.GetProcesses().Any(p => p.ProcessName.Contains("Revit") && !p.ProcessName.Contains("RevitAccelerator")))
        {
            MessageBox.Show("Revit is currently running. Please close it to continue.", "Failure", MessageBoxButtons.OK);
            return;  // Ends the script here
        }

        // Check if Revit Accelerator is running and kill it
        foreach (var process in Process.GetProcesses().Where(p => p.ProcessName.Contains("RevitAccelerator")))
        {
            try
            {
                process.Kill();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to kill Revit Accelerator: {ex.Message}", "Failure", MessageBoxButtons.OK);
                return;
            }
        }

        try
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                MessageBox.Show("Operation was successful.", "Success", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("The path does not exist.", "Failure", MessageBoxButtons.OK);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred: {ex.Message}", "Failure", MessageBoxButtons.OK);
        }
    }
}

