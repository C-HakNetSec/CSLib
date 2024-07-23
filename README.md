# CSLib

CSLib is a versatile C# library that provides a range of utilities for common Windows application tasks, including user interactions, file management, system checks, and configuration.

This library was created by [Mommy](https://github.com/l-mommy-l).

## Features

- [**MessageBox**](#messagebox): Create and manage message boxes with various options and behaviors.
- [**Shortcut**](#shortcut): Create and manage application shortcuts with customizable options.
- [**AdminRights**](#adminrights): Check and request administrative rights.
- [**StartupApps**](#startupapps): Add applications to the startup folder to run them automatically on system boot.

## Installation

You can use CSLib in your project in one of two ways:

### 1. Using the Compiled DLL

1. **Download the Compiled DLL:**
   - Go to the [Releases](https://github.com/l-mommy-l/CSLib/releases) page of the repository.
   - Download the latest release zip file.
   - Extract the `CSLib.dll` file from the zip.

2. **Add Reference:**
   - **Visual Studio:** 
     - Right-click on **References** in the Solution Explorer.
     - Select **Add Reference**.
     - Browse to the location of `CSLib.dll` and add it to your project.
   - **Other IDEs:**
     - Open your project's properties or settings.
     - Look for an option to add references or assemblies.
     - Browse to the `CSLib.dll` file and add it to your project.

### 2. Using the Source Code

1. **Download the Source Code:**
   - Clone or download the repository from [GitHub](https://github.com/l-mommy-l/CSLib).

2. **Add Files to Your Project:**
   - Add the `CSLib.cs` file to your project.

3. **Compile Your Project:**
   - Build your project to integrate CSLib into your application.

## Usage

### MessageBox

The `MessageBox` class provides methods to create and display message boxes with customizable buttons, icons, and behaviors.

#### Example

```csharp
using CSLib;

class Program
{
    static void Main()
    {
        // Create an instance of the MessageBox class
        MessageBox msgBox = new MessageBox(
            WindowHandle: IntPtr.Zero, // Handle of the parent window (or IntPtr.Zero if no parent)
            Text: "Hello, World!", // Text to display in the message box (default is null)
            Title: "My MessageBox", // Title of the message box (default is null)
            Behavior: (ulong)MessageBox.MessageBoxButtons.MB_OK | // Button options (e.g., OK button)
                      (ulong)MessageBox.MessageBoxIcon.MB_ICONINFORMATION // Icon options (e.g., Information icon)
        );

        // Show the message box and get the user's response
        int result = msgBox.ShowMessageBox();

        // Convert the result to a readable button name
        string buttonClicked = MessageBox.GetButtonName(result);

        // Print the name of the clicked button
        Console.WriteLine($"Button clicked: {buttonClicked}");
    }
}
```

- **Constructor Parameters:**
  - `WindowHandle` (optional): The handle of the parent window. Use `IntPtr.Zero` if no parent window is needed.
  - `Text` (optional, default is `null`): The message text displayed in the message box.
  - `Title` (optional, default is `null`): The title of the message box window.
  - `Behavior`: A combination of options that define the buttons, icon, default button, modality, and other features of the message box. No default value.

- **Methods:**
  - `ShowMessageBox()`: Displays the message box and returns the ID of the button clicked by the user.
  - `GetClickedButton()`: Retrieves the ID of the button that was clicked. Throws an `InvalidDataException` if the message box has not been interacted with yet.
  - `GetButtonName(int ButtonId)`: Converts the button ID into a human-readable string representation.

### Shortcut

The `Shortcut` class allows you to create shortcuts to applications, specifying target paths, icons, arguments, and descriptions.

#### Example

```csharp
using CSLib;

class Program
{
    static void Main()
    {
        // Define the path for the shortcut (e.g., Desktop)
        string shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        // Path to the application executable
        string appPath = @"C:\Path\To\Application.exe";

        // Name of the shortcut
        string name = "MyApp";

        // Path to the icon file (optional, default is null)
        string iconPath = @"C:\Path\To\Icon.ico";

        // Command-line arguments for the application (optional, default is null)
        string args = "-someArgument";

        // Description for the shortcut (optional, default is null)
        string description = "My Application Shortcut";

        // Create the shortcut
        Shortcut shortcut = new Shortcut(shortcutPath, appPath, name, iconPath, args, description);
    }
}
```

- **Constructor Parameters:**
  - `shortcutPath`: The folder where the shortcut will be created (e.g., Desktop).
  - `appPath`: The path to the application executable that the shortcut will launch.
  - `name`: The name of the shortcut.
  - `iconPath` (optional, default is `null`): The path to the icon file for the shortcut.
  - `args` (optional, default is `null`): Command-line arguments for the application.
  - `description` (optional, default is `null`): Description text for the shortcut.

### AdminRights

The `AdminRights` class provides methods to check for and request administrative rights for the current user.

#### Example

```csharp
using CSLib;

class Program
{
    static void Main()
    {
        // Check if the current user has administrative rights
        if (!AdminRights.CheckIfHasAdminRights())
        {
            // Request administrative rights if not already granted
            AdminRights.GetAdminRights();
        }
        else
        {
            // Notify the user if administrative rights are already granted
            Console.WriteLine("You already have administrative rights.");
        }
    }
}
```

- **Methods:**
  - `CheckIfHasAdminRights()`: Checks if the current user has administrative rights and returns `true` or `false`.
  - `GetAdminRights()`: Attempts to elevate the current process to run with administrative rights if the user does not already have them. Prompts the user for elevation if needed.

### StartupApps

The `StartupApps` class helps add applications to the startup folder, so they automatically run when the system boots.

#### Example

```csharp
using CSLib;

class Program
{
    static void Main()
    {
        // Path to the application executable
        string appPath = @"C:\Path\To\Application.exe";

        // Command-line arguments for the application (optional, default is null)
        string args = "-someArgument";

        // If true, install for all users; otherwise, install only for the current user (optional, default is false)
        bool installForAllUsers = true;

        // Add the application to the startup folder
        StartupApps.AddToStartup(appPath, args, installForAllUsers);
    }
}
```

- **Methods:**
  - `AddToStartup(string path, string args = "", bool installForAllUsers = false)`: Adds the specified application to the startup folder.
    - `path`: The path to the application executable.
    - `args` (optional, default is `""`): Command-line arguments for the application.
    - `installForAllUsers` (optional, default is `false`): If `true`, the application is added to the common startup folder for all users; if `false`, it is added to the startup folder for the current user only.

## License

This project is licensed under the MIT License. See the LICENSE file for details.

## Author

CSLib is created by [Mommy](https://github.com/l-mommy-l).
