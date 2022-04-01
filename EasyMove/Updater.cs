using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Security.Cryptography;

namespace EasyMove
{
    class Updater
    {
        public static void PerformUpdate()
        {

        }

        public static void GetUpdate()
        {
            GetUpdate(false);
        }

        public static void GetUpdate(bool AutoUpdate)
        {
            Version downloadedVersion = new Version("0.0.0.0");  // for version check file
            Version fileVersion = new Version("0.0.0.0"); // to validate the downloaded file is the expected version
            Version currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            string fileHash256 = string.Empty;
            string expectedHash = string.Empty;
            string expectedHash256 = string.Empty;
            DialogResult result;


            using (System.Net.WebClient updater = new System.Net.WebClient())
            {
                //download version check file
                try
                {
                    updater.DownloadFile("https://www.shelteringoak.com/dl/WindowsHealthCheck/version.txt", Environment.ExpandEnvironmentVariables("%Temp%").ToString() + @"\WindowsHealthCheckVersion.txt");
                }
                catch (System.Net.WebException ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Failed to check version", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // read file into memory and parse into Version type
                try
                {
                    using (StreamReader sr = new StreamReader(Environment.ExpandEnvironmentVariables("%Temp%").ToString() + @"\WindowsHealthCheckVersion.txt"))
                    {
                        downloadedVersion = System.Version.Parse(sr.ReadLine()); // version info is in first line of file
                        expectedHash = sr.ReadLine();  // MD5 hash is in second line of file
                        expectedHash256 = sr.ReadLine(); // hash256 is the third line of file
                    }
                }
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "IO Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // cleanup temp directory
                finally
                {
                    if (File.Exists(Environment.ExpandEnvironmentVariables("%Temp%").ToString() + @"\WindowsHealthCheckVersion.txt"))
                    {
                        try
                        {
                            File.Delete(Environment.ExpandEnvironmentVariables("%Temp%").ToString() + @"\WindowsHealthCheckVersion.txt");
                        }
                        catch
                        {
                            // do nothing
                        }
                    }
                }


                // Compare versions
                if (downloadedVersion > currentVersion)
                {
                    if (!AutoUpdate)
                    {
                        result = MessageBox.Show("New version " + downloadedVersion.ToString() + " available. Download now?", "Download available", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.No)
                        {
                            return;
                        }
                    }

                    // Validate file downloaded
                    do
                    {
                        // Download new executable
                        try
                        {
                            updater.DownloadFile("https://www.shelteringoak.com/dl/WindowsHealthCheck/WindowsHealthCheck.exe", @".\WindowsHealthCheck v" + downloadedVersion.ToString() + ".exe");
                        }
                        catch (FileNotFoundException ex)
                        {
                            MessageBox.Show(ex.Message.ToString(), "Failed to check version", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        catch (IOException ex)
                        {
                            MessageBox.Show(ex.Message.ToString(), "IO Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // get md5 hash of downloaded file
                        fileHash256 = GetFileHash256(Directory.GetCurrentDirectory() + @"\WindowsHealthCheck v" + downloadedVersion.ToString() + ".exe");

                        if (fileHash256 != expectedHash256 && !AutoUpdate)
                        {
                            result = MessageBox.Show("Error validating downloaded file. SHA256 checksum mismatch.", "File validation error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                            if (result == DialogResult.Cancel)
                            {
                                File.Delete(Directory.GetCurrentDirectory() + @"\WindowsHealthCheck v" + downloadedVersion.ToString() + ".exe");
                                return;
                            }
                        }
                        if (fileHash256 != expectedHash256 && !AutoUpdate)
                        {
                            return;
                        }

                    } while (fileHash256 != expectedHash256);

                    // Extract Updater.exe to perform update
                    try
                    {
                        DobsonUtilities.ExtractEmbededResource.Extract(Directory.GetCurrentDirectory().ToString(), "Resources", "Updater.exe");
                    }
                    catch
                    {
                        MessageBox.Show("Failed to extract Updater.", "File extraction error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (File.Exists(Directory.GetCurrentDirectory() + @"\WindowsHealthCheck v" + downloadedVersion.ToString() + ".exe"))
                            File.Delete(Directory.GetCurrentDirectory() + @"\WindowsHealthCheck v" + downloadedVersion.ToString() + ".exe");
                        return;
                    }

                    // begin update process
                    Process newver = new Process();
                    newver.StartInfo.FileName = "Updater.exe";
                    newver.StartInfo.Arguments = "\"WindowsHealthCheck v" + downloadedVersion.ToString() + ".exe\" WindowsHealthCheck.exe";
                    newver.StartInfo.CreateNoWindow = true;
                    newver.Start();
                }
                else
                {
                    if (!AutoUpdate)
                        MessageBox.Show("You are currently using the latest version.", "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private static string GetFileHash256(string filename)
        {
            SHA256 fileHash = SHA256.Create();
            byte[] hash = new byte[0];
            string result = string.Empty;

            using (FileStream stream = File.OpenRead(filename))
            {
                hash = fileHash.ComputeHash(stream);
            }

            foreach (byte b in hash) result += b.ToString("x2");
            return result;
        }
    }
}