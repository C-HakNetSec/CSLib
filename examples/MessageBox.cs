using System;
using CSLib;

namespace MessageBoxExample
{
    public class MessageBoxEX
    {
        public static void install()
        {

            //initialize Message
            MessageBox msgbox = new MessageBox(
                // window handle
                IntPtr.Zero,
                //text
                "test text",
                //title
                "test title",
                //buttons displayed in Message
                (UInt64) MessageBox.MessageBoxButtons.MB_OKCANCEL
                //Message icon
                | (UInt64) MessageBox.MessageBoxIcon.MB_ICONERROR
                //Modality
                | (UInt64) MessageBox.MessageBoxModality.MB_SYSTEMMODAL
                );

            //show Message (this function also return id of button that user clicked)
            msgbox.ShowMessageBox();

            //get return value
            int ReturnValue = msgbox.GetClickedButton();

            //get button name from return value
            string ClickedButton = MessageBox.GetButtonName(ReturnValue);

            //show clicked button
            msgbox = new MessageBox(IntPtr.Zero, ClickedButton, "Clicked Button");
            msgbox.ShowMessageBox();

            //check if user clicked "OK" button with button id's
            if (ReturnValue == (uint) MessageBox.MessageBoxButtonClicked.IDOK)
            {
                msgbox = new MessageBox(IntPtr.Zero, "User clicked OK button as we wanted", "YAY");
                msgbox.ShowMessageBox();
            }

            else
            {
                msgbox = new MessageBox(IntPtr.Zero, "User didnt click OK button as we wanted :c", ":c");
                msgbox.ShowMessageBox();
            }
        }
    }
}
