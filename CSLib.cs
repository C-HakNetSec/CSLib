using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CSLib
{
    public class MessageBox
    {
        // Enum for message box buttons
        public enum MessageBoxButtons : UInt64
        {
            // Abort, Retry, Ignore buttons
            MB_ABORTRETRYIGNORE = 0x00000002L,
            // Cancel, Try Again, Continue buttons
            MB_CANCELTRYCONTINUE = 0x00000006L,
            // Help button
            MB_HELP = 0x00004000L,
            // OK button
            MB_OK = 0x00000000L,
            // OK, Cancel buttons
            MB_OKCANCEL = 0x00000001L,
            // Retry, Cancel buttons
            MB_RETRYCANCEL = 0x00000005L,
            // Yes, No buttons
            MB_YESNO = 0x00000004L,
            // Yes, No, Cancel buttons
            MB_YESNOCANCEL = 0x00000003L
        }

        // Enum for message box icons
        public enum MessageBoxIcon : UInt64
        {
            // Warning icon
            MB_ICONEXCLAMATION = 0x00000030L,
            // Warning icon
            MB_ICONWARNING = 0x00000030L,
            // Information icon
            MB_ICONINFORMATION = 0x00000040L,
            // Information icon
            MB_ICONASTERISK = 0x00000040L,
            // Question mark icon
            MB_ICONQUESTION = 0x00000020L,
            // Error icon
            MB_ICONSTOP = 0x00000010L,
            // Error icon
            MB_ICONERROR = 0x00000010L,
            // Error icon
            MB_ICONHAND = 0x00000010L
        }

        // Enum for default button
        public enum MessageBoxDefaultButton : UInt64
        {
            // First button is default
            MB_DEFBUTTON1 = 0x00000000L,
            // Second button is default
            MB_DEFBUTTON2 = 0x00000100L,
            // Third button is default
            MB_DEFBUTTON3 = 0x00000200L,
            // Fourth button is default
            MB_DEFBUTTON4 = 0x00000300L
        }

        // Enum for message box modality
        public enum MessageBoxModality : UInt64
        {
            // Application modal
            MB_APPLMODAL = 0x00000000L,
            // System modal
            MB_SYSTEMMODAL = 0x00001000L,
            // Task modal
            MB_TASKMODAL = 0x00002000L
        }

        // Enum for other message box options
        public enum MessageBoxOtherOptions : UInt64
        {
            // Default desktop only
            MB_DEFAULT_DESKTOP_ONLY = 0x00020000L,
            // Right-aligned text
            MB_RIGHT = 0x00080000L,
            // Right-to-left reading
            MB_RTLREADING = 0x00100000L,
            // Set foreground
            MB_SETFOREGROUND = 0x00010000L,
            // Topmost window
            MB_TOPMOST = 0x00040000L,
            // Service notification
            MB_SERVICE_NOTIFICATION = 0x00200000L
        }

        // Enum for button clicked
        public enum MessageBoxButtonClicked
        {
            // Abort button clicked
            IDABORT = 3,
            // Cancel button clicked
            IDCANCEL = 2,
            // Continue button clicked
            IDCONTINUE = 11,
            // Ignore button clicked
            IDIGNORE = 5,
            // No button clicked
            IDNO = 7,
            // OK button clicked
            IDOK = 1,
            // Retry button clicked
            IDRETRY = 4,
            // Try Again button clicked
            IDTRYAGAIN = 10,
            // Yes button clicked
            IDYES = 6
        }

        // Import the MessageBox function from User32.dll
        [DllImport("User32.dll", SetLastError = true, EntryPoint = "MessageBox")]
        static private extern int MessageBoxImported(IntPtr hWnd, string lpText, string lpCaption, UInt64 uType);

        // Private fields for the message box
        private IntPtr WINDOWHANDLE = IntPtr.Zero;
        private string TEXT;
        private string TITLE;
        private UInt64 BEHAVIOR;
        private int RETURNVALUE;

        // Constructor for the message box
        public MessageBox(IntPtr WindowHandle = default, string Text = null, string Title = null, UInt64 Behavior = default)
        {
            WINDOWHANDLE = WindowHandle;
            TEXT = Text;
            TITLE = Title;
            BEHAVIOR = Behavior;
        }

        // Show themessage box
        public int ShowMessageBox()
        {

            // Call the imported MessageBox function
            int Return = MessageBoxImported(WINDOWHANDLE, TEXT, TITLE, BEHAVIOR);

            RETURNVALUE = Return;

            return Return;
        }

        // Get the button clicked
        public int GetClickedButton()
        {
            if (RETURNVALUE == 0)
            {
                throw new InvalidDataException("User not interacted yet");
            } 
            
            else
            {
                return RETURNVALUE;
            }
        }

        public static string GetButtonName(int ButtonId)
        {
            switch (ButtonId)
            {
                case (int)MessageBox.MessageBoxButtonClicked.IDOK:
                    return "OK";
                case (int)MessageBox.MessageBoxButtonClicked.IDTRYAGAIN:
                    return "Try Again";
                case (int)MessageBox.MessageBoxButtonClicked.IDRETRY:
                    return "Retry";
                case (int)MessageBox.MessageBoxButtonClicked.IDABORT:
                    return "Abort";
                case (int)MessageBox.MessageBoxButtonClicked.IDNO:
                    return "NO";
                case (int)MessageBox.MessageBoxButtonClicked.IDCANCEL:
                    return "Cancel";
                case (int)MessageBox.MessageBoxButtonClicked.IDCONTINUE:
                    return "Continue";
                case (int)MessageBox.MessageBoxButtonClicked.IDIGNORE:
                    return "Ignore";
                case (int)MessageBox.MessageBoxButtonClicked.IDYES:
                    return "Yes";
                default:
                    throw new InvalidDataException("Couldn't find button");
            }
        }
    }
}