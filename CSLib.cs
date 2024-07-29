using IWshRuntimeLibrary;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;

namespace CSLib
{
    /// <summary>
    /// Represents a message box with various options and behaviors.
    /// </summary>
    public class MessageBox
    {
        /// <summary>
        /// Enum for message box buttons.
        /// </summary>
        public enum MessageBoxButtons : UInt64
        {
            /// <summary>
            /// Abort, Retry, Ignore buttons.
            /// </summary>
            MB_ABORTRETRYIGNORE = 0x00000002L,
            /// <summary>
            /// Cancel, Try Again, Continue buttons.
            /// </summary>
            MB_CANCELTRYCONTINUE = 0x00000006L,
            /// <summary>
            /// Help button.
            /// </summary>
            MB_HELP = 0x00004000L,
            /// <summary>
            /// OK button.
            /// </summary>
            MB_OK = 0x00000000L,
            /// <summary>
            /// OK, Cancel buttons.
            /// </summary>
            MB_OKCANCEL = 0x00000001L,
            /// <summary>
            /// Retry, Cancel buttons.
            /// </summary>
            MB_RETRYCANCEL = 0x00000005L,
            /// <summary>
            /// Yes, No buttons.
            /// </summary>
            MB_YESNO = 0x00000004L,
            /// <summary>
            /// Yes, No, Cancel buttons.
            /// </summary>
            MB_YESNOCANCEL = 0x00000003L
        }

        /// <summary>
        /// Enum for message box icons.
        /// </summary>
        public enum MessageBoxIcon : UInt64
        {
            /// <summary>
            /// Warning icon.
            /// </summary>
            MB_ICONEXCLAMATION = 0x00000030L,
            /// <summary>
            /// Warning icon.
            /// </summary>
            MB_ICONWARNING = 0x00000030L,
            /// <summary>
            /// Information icon.
            /// </summary>
            MB_ICONINFORMATION = 0x00000040L,
            /// <summary>
            /// Information icon.
            /// </summary>
            MB_ICONASTERISK = 0x00000040L,
            /// <summary>
            /// Question mark icon.
            /// </summary>
            MB_ICONQUESTION = 0x00000020L,
            /// <summary>
            /// Error icon.
            /// </summary>
            MB_ICONSTOP = 0x00000010L,
            /// <summary>
            /// Error icon.
            /// </summary>
            MB_ICONERROR = 0x00000010L,
            /// <summary>
            /// Error icon.
            /// </summary>
            MB_ICONHAND = 0x00000010L
        }

        /// <summary>
        /// Enum for default button.
        /// </summary>
        public enum MessageBoxDefaultButton : UInt64
        {
            /// <summary>
            /// First button is default.
            /// </summary>
            MB_DEFBUTTON1 = 0x00000000L,
            /// <summary>
            /// Second button is default.
            /// </summary>
            MB_DEFBUTTON2 = 0x00000100L,
            /// <summary>
            /// Third button is default.
            /// </summary>
            MB_DEFBUTTON3 = 0x00000200L,
            /// <summary>
            /// Fourth button is default.
            /// </summary>
            MB_DEFBUTTON4 = 0x00000300L
        }

        /// <summary>
        /// Enum for message box modality.
        /// </summary>
        public enum MessageBoxModality : UInt64
        {
            /// <summary>
            /// Application modal.
            /// </summary>
            MB_APPLMODAL = 0x00000000L,
            /// <summary>
            /// System modal.
            /// </summary>
            MB_SYSTEMMODAL = 0x00001000L,
            /// <summary>
            /// Task modal.
            /// </summary>
            MB_TASKMODAL = 0x00002000L
        }

        /// <summary>
        /// Enum for other message box options.
        /// </summary>
        public enum MessageBoxOtherOptions : UInt64
        {
            /// <summary>
            /// Default desktop only.
            /// </summary>
            MB_DEFAULT_DESKTOP_ONLY = 0x00020000L,
            /// <summary>
            /// Right-aligned text.
            /// </summary>
            MB_RIGHT = 0x00080000L,
            /// <summary>
            /// Right-to-left reading.
            /// </summary>
            MB_RTLREADING = 0x00100000L,
            /// <summary>
            /// Set foreground.
            /// </summary>
            MB_SETFOREGROUND = 0x00010000L,
            /// <summary>
            /// Topmost window.
            /// </summary>
            MB_TOPMOST = 0x00040000L,
            /// <summary>
            /// Service notification.
            /// </summary>
            MB_SERVICE_NOTIFICATION = 0x00200000L
        }

        /// <summary>
        /// Enum for button clicked.
        /// </summary>
        public enum MessageBoxButtonClicked
        {
            /// <summary>
            /// Abort button clicked.
            /// </summary>
            IDABORT = 3,
            /// <summary>
            /// Cancel button clicked.
            /// </summary>
            IDCANCEL = 2,
            /// <summary>
            /// Continue button clicked.
            /// </summary>
            IDCONTINUE = 11,
            /// <summary>
            /// Ignore button clicked.
            /// </summary>
            IDIGNORE = 5,
            /// <summary>
            /// No button clicked.
            /// </summary>
            IDNO = 7,
            /// <summary>
            /// OK button clicked.
            /// </summary>
            IDOK = 1,
            /// <summary>
            /// Retry button clicked.
            /// </summary>
            IDRETRY = 4,
            /// <summary>
            /// Try Again button clicked.
            /// </summary>
            IDTRYAGAIN = 10,
            /// <summary>
            /// Yes button clicked.
            /// </summary>
            IDYES = 6
        }

        /// <summary>
        /// Import the MessageBox function from User32.dll.
        /// </summary>
        [DllImport("User32.dll", SetLastError = true, EntryPoint = "MessageBox")]
        static private extern int MessageBoxImported(IntPtr hWnd, string lpText, string lpCaption, UInt64 uType);

        /// <summary>
        /// Private fields for the message box.
        /// </summary>
        private IntPtr WINDOWHANDLE = IntPtr.Zero;
        private string TEXT;
        private string TITLE;
        private UInt64 BEHAVIOR;
        private int RETURNVALUE;

        /// <summary>
        /// Constructor for the message box.
        /// </summary>
        /// <param name="WindowHandle">The handle of the parent window (optional).</param>
        /// <param name="Text">The text to display in the message box (optional).</param>
        /// <param name="Title">The title of the message box (optional).</param>
        /// <param name="Behavior">The behavior of the message box (optional).</param>
        public MessageBox(IntPtr WindowHandle = default, string Text = null, string Title = null, UInt64 Behavior = default)
        {
            WINDOWHANDLE = WindowHandle;
            TEXT = Text;
            TITLE = Title;
            BEHAVIOR = Behavior;
        }

        /// <summary>
        /// Show the message box.
        /// </summary>
        /// <returns>The return value of the message box.</returns>
        public int ShowMessageBox()
        {

            // Call the imported MessageBox function
            int Return = MessageBoxImported(WINDOWHANDLE, TEXT, TITLE, BEHAVIOR);

            RETURNVALUE = Return;

            return Return;
        }

        /// <summary>
        /// Retrieves the ID of the button clicked by the user.
        /// </summary>
        /// <returns>The ID of the clicked button.</returns>
        public int GetClickedButton()
        {
            // Wait for the user to click a button, polling every 100ms
            while (RETURNVALUE == 0)
            {
                // Pause execution for 100ms to avoid busy-waiting
                Thread.Sleep(100);
            }

            // Return the ID of the button clicked by the user
            return RETURNVALUE;
        }

        /// <summary>
        /// Get the name of the button clicked based on its ID.
        /// </summary>
        /// <param name="ButtonId">The ID of the clickedbutton.</param>
        /// <returns>The name of the clicked button.</returns>
        public static string GetButtonName(int ButtonId)
        {
            // Use a switch statement to map the button ID to its corresponding name
            switch (ButtonId)
            {
                case (int)MessageBox.MessageBoxButtonClicked.IDOK:
                    // Return "OK" for the OK button
                    return "OK";
                case (int)MessageBox.MessageBoxButtonClicked.IDTRYAGAIN:
                    // Return "Try Again" for the Try Again button
                    return "Try Again";
                case (int)MessageBox.MessageBoxButtonClicked.IDRETRY:
                    // Return "Retry" for the Retry button
                    return "Retry";
                case (int)MessageBox.MessageBoxButtonClicked.IDABORT:
                    // Return "Abort" for the Abort button
                    return "Abort";
                case (int)MessageBox.MessageBoxButtonClicked.IDNO:
                    // Return "NO" for the No button
                    return "NO";
                case (int)MessageBox.MessageBoxButtonClicked.IDCANCEL:
                    // Return "Cancel" for the Cancel button
                    return "Cancel";
                case (int)MessageBox.MessageBoxButtonClicked.IDCONTINUE:
                    // Return "Continue" for the Continue button
                    return "Continue";
                case (int)MessageBox.MessageBoxButtonClicked.IDIGNORE:
                    // Return "Ignore" for the Ignore button
                    return "Ignore";
                case (int)MessageBox.MessageBoxButtonClicked.IDYES:
                    // Return "Yes" for the Yes button
                    return "Yes";
                default:
                    // If the button ID is not recognized, throw an exception
                    throw new InvalidDataException("Couldn't find button");
            }
        }
    }

    /// <summary>
    /// Represents a shortcut.
    /// </summary>
    public class Shortcut
    {
        /// <summary>
        /// Constructor for creating a new shortcut.
        /// </summary>
        /// <param name="shortcutPath">The path where the shortcut will be created.</param>
        /// <param name="appPath">The path of the application that the shortcut will launch.</param>
        /// <param name="name">The name of the shortcut.</param>
        /// <param name="iconPath">The path of the icon for the shortcut.</param>
        /// <param name="args">The arguments for the shortcut (optional).</param>
        /// <param name="description">The description of the shortcut (optional).</param>
        public Shortcut(string shortcutPath, string appPath, string name, string iconPath = null, string args = null, string description = null)
        {
            // Create a new instance of the WshShell object
            WshShell wshShell = new WshShell();

            // Create a new shortcut object at the specified path with the given name
            IWshShortcut shortcut = wshShell.CreateShortcut($"{shortcutPath}\\{name}.lnk");

            // Set the target path of the shortcut (i.e. the application it will launch)
            shortcut.TargetPath = appPath;

            // Set the icon location for the shortcut
            if (iconPath != null && iconPath != "")
            {
                shortcut.IconLocation = iconPath;
            }

            // Set the arguments for the shortcut (optional)
            shortcut.Arguments = args;

            // Set the description for the shortcut (optional)
            shortcut.Description = description;

            // Save the shortcut to disk
            shortcut.Save();
        }
    }

    public static class AdminRights
    {
        /// <summary>
        /// Checks if the current user has administrator rights.
        /// </summary>
        /// <returns>true if the user has administrator rights, false otherwise.</returns>
        public static bool CheckIfHasAdminRights()
        {
            // Get the current Windows identity
            WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();

            // Create a Windows principal from the identity
            WindowsPrincipal windowsPrincipal = new WindowsPrincipal(windowsIdentity);

            // Check if the user is in the administrator role
            return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        /// <summary>
        /// Elevates the current process to run with administrator rights if necessary.
        /// </summary>
        public static void GetAdminRights()
        {
            // Check if the user already has administrator rights
            if (!CheckIfHasAdminRights())
            {
                // Get the current process
                Process CurrentProcess = Process.GetCurrentProcess();

                // Get the path of the current process
                string ProcessPath = CurrentProcess.MainModule.FileName;

                // Create a new process start info
                ProcessStartInfo startinfo = new ProcessStartInfo(ProcessPath);

                // Set the "runas" verb to elevate the process
                startinfo.UseShellExecute = true;
                startinfo.Verb = "runas";

                // Start the new process
                Process.Start(startinfo);

                // Kill the current process
                CurrentProcess.Kill();
            }
        }

        /// <summary>
        /// Ensures that the application has administrator rights before proceeding.
        /// If the application does not have admin rights, it will prompt the user to retry or cancel.
        /// </summary>
        /// <param name="Errortext">Optional text to include in the error message when the user cancels.</param>
        /// <param name="MessageBoxText">Optional text to include in the message box displayed to the user.</param>
        public static void EnsureAdminRights(string Errortext = "", string MessageBoxText = "")
        {
            // Loop until admin rights are confirmed or the user cancels
            while (true)
            {
                // Check if the current process has administrator rights
                if (!AdminRights.CheckIfHasAdminRights())
                {
                    // Display a message box to inform the user of missing admin permissions
                    // Create a message box with an error icon and retry/cancel buttons
                    MessageBox messageBox = new MessageBox(
                        IntPtr.Zero,
                        $"Missing admin permissions. {MessageBoxText}",
                        "Missing permissions",
                        (ulong)MessageBox.MessageBoxIcon.MB_ICONERROR | (ulong)MessageBox.MessageBoxButtons.MB_RETRYCANCEL
                    );

                    // Show the message box to the user
                    messageBox.ShowMessageBox();

                    // Get the button that the user clicked
                    int clickedButton = messageBox.GetClickedButton();

                    // Handle the user's response
                    if (clickedButton == (int)MessageBox.MessageBoxButtonClicked.IDCANCEL)
                    {
                        // Abort the operation if the user chooses to cancel
                        throw new OperationCanceledException($"User aborted {Errortext}");
                    }
                    else if (clickedButton == (int)MessageBox.MessageBoxButtonClicked.IDRETRY)
                    {
                        // Retry checking for admin rights if the user chooses to retry
                        continue;
                    }
                }
                else
                {
                    // Break out of the loop if admin rights are confirmed
                    break;
                }
            }
        }
    }

    /// <summary>
    /// A static class for managing startup applications.
    /// </summary>
    public static class StartupApps
    {
        /// <summary>
        /// Adds an application to the startup folder.
        /// </summary>
        /// <param name="path">The path to the application executable.</param>
        /// <param name="args">Optional command-line arguments for the application.</param>
        /// <param name="installForAllUsers">If true, installs the application for all users on the system.</param>
        public static void AddToStartup(string path, string args = "", bool installForAllUsers = false)
        {
            // Get the startup folder path for the current user
            string startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

            // Extract the program name from the executable path
            string programName = Path.GetFileName(path);

            // If installing for all users, ensure admin rights and use the common startup folder
            if (installForAllUsers)
            {
                // Ensure that user has admin rights
                AdminRights.EnsureAdminRights($"adding {programName} to startup");

                // Use the common startup folder for all users if admin rights are available
                startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup);
            }

            // Remove the file extension from the program name
            programName = programName.Substring(0, programName.IndexOf("."));

            // Create a new shortcut in the startup folder, including the command-line arguments
            new Shortcut(startupFolderPath, path, programName, args: args);
        }
    }

    /// <summary>
    /// A class that provides functionality to check if a process is critical.
    /// </summary>
    public class CriticalProcess
    {
        // Import the IsProcessCritical function from kernel32.dll
        [DllImport("kernel32.dll", SetLastError = true, EntryPoint = "IsProcessCritical")]
        private static extern bool IsProcessCriticalImported(IntPtr hProcess, out bool Critical);

        // Check if a process is critical
        public static bool IsCritical(Process process)
        {
            // Ensure that user has admin rights
            AdminRights.EnsureAdminRights("checking if process is critical");

            // Check if the process is null
            if (process == null)
            {
                // Display an error message if the process is null
                MessageBox messageBox = new MessageBox(IntPtr.Zero, "Process doesn't exist", "Error", (ulong)MessageBox.MessageBoxIcon.MB_ICONERROR | (ulong)MessageBox.MessageBoxButtons.MB_OK);
                messageBox.ShowMessageBox();

                return false;
            }

            // Call the IsProcessCriticalImported function to check if the process is critical
            bool result;
            bool error = IsProcessCriticalImported(process.Handle, out result);

            // Check if an error occurred when calling IsProcessCriticalImported
            if (error == false)
            {
                // Get the error code from the last Win32 error
                int errorCode = Marshal.GetLastWin32Error();

                // Display an error message with the error code
                MessageBox messageBox = new MessageBox(IntPtr.Zero, $"Error {errorCode} occured when trying to get info about process", "Error", (ulong)MessageBox.MessageBoxIcon.MB_ICONERROR | (ulong)MessageBox.MessageBoxButtons.MB_OK);
                messageBox.ShowMessageBox();

                // Throw a Win32Exception with the error code
                throw new Win32Exception($"Error {errorCode} occured when trying to get info about process");
            }

            // Return the result of the IsProcessCriticalImported function
            return result;
        }
    }
}