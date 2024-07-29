---

# CSLib

CSLib is a C# library providing utilities for creating message boxes, managing shortcuts, checking for admin rights, adding applications to startup, and determining if a process is critical.

## Features

- **MessageBox**: Display custom message boxes with various options and behaviors.
- **Shortcut**: Create desktop shortcuts programmatically.
- **AdminRights**: Check and obtain administrator rights.
- **StartupApps**: Add applications to the startup folder.
- **CriticalProcess**: Check if a process is critical.

## Installation

To use CSLib in your project, clone the repository and include the source files in your solution.

```bash
git clone https://github.com/l-mommy-l/CSLib.git
```

## Usage

### MessageBox

The `MessageBox` class allows you to create and display message boxes with various button and icon configurations.

#### Enums

- `MessageBoxButtons`: Defines the buttons displayed in the message box.
  - `MB_ABORTRETRYIGNORE`: Abort, Retry, Ignore buttons.
  - `MB_CANCELTRYCONTINUE`: Cancel, Try Again, Continue buttons.
  - `MB_HELP`: Help button.
  - `MB_OK`: OK button.
  - `MB_OKCANCEL`: OK, Cancel buttons.
  - `MB_RETRYCANCEL`: Retry, Cancel buttons.
  - `MB_YESNO`: Yes, No buttons.
  - `MB_YESNOCANCEL`: Yes, No, Cancel buttons.

- `MessageBoxIcon`: Defines the icons displayed in the message box.
  - `MB_ICONEXCLAMATION`: Warning icon.
  - `MB_ICONWARNING`: Warning icon.
  - `MB_ICONINFORMATION`: Information icon.
  - `MB_ICONASTERISK`: Information icon.
  - `MB_ICONQUESTION`: Question mark icon.
  - `MB_ICONSTOP`: Error icon.
  - `MB_ICONERROR`: Error icon.
  - `MB_ICONHAND`: Error icon.

- `MessageBoxDefaultButton`: Defines the default button selected in the message box.
  - `MB_DEFBUTTON1`: First button is default.
  - `MB_DEFBUTTON2`: Second button is default.
  - `MB_DEFBUTTON3`: Third button is default.
  - `MB_DEFBUTTON4`: Fourth button is default.

- `MessageBoxModality`: Defines the modality of the message box.
  - `MB_APPLMODAL`: Application modal.
  - `MB_SYSTEMMODAL`: System modal.
  - `MB_TASKMODAL`: Task modal.

- `MessageBoxOtherOptions`: Defines other options for the message box.
  - `MB_DEFAULT_DESKTOP_ONLY`: Default desktop only.
  - `MB_RIGHT`: Right-aligned text.
  - `MB_RTLREADING`: Right-to-left reading.
  - `MB_SETFOREGROUND`: Set foreground.
  - `MB_TOPMOST`: Topmost window.
  - `MB_SERVICE_NOTIFICATION`: Service notification.

- `MessageBoxButtonClicked`: Defines the button clicked by the user.
  - `IDABORT`: Abort button clicked.
  - `IDCANCEL`: Cancel button clicked.
  - `IDCONTINUE`: Continue button clicked.
  - `IDIGNORE`: Ignore button clicked.
  - `IDNO`: No button clicked.
  - `IDOK`: OK button clicked.
  - `IDRETRY`: Retry button clicked.
  - `IDTRYAGAIN`: Try Again button clicked.
  - `IDYES`: Yes button clicked.

#### Example

```csharp
using CSLib;

MessageBox messageBox = new MessageBox(IntPtr.Zero, "Hello, World!", "Greeting", (ulong)MessageBox.MessageBoxIcon.MB_ICONINFORMATION | (ulong)MessageBox.MessageBoxButtons.MB_OK);
int result = messageBox.ShowMessageBox();
string buttonName = MessageBox.GetButtonName(result);
Console.WriteLine($"Clicked button: {buttonName}");
```

### Shortcut

The `Shortcut` class allows you to create shortcuts to applications with optional arguments and descriptions.

#### Constructor

- `Shortcut(string shortcutPath, string appPath, string name, string iconPath = null, string args = null, string description = null)`: Creates a new shortcut.
  - `shortcutPath`: The path where the shortcut will be created.
  - `appPath`: The path of the application that the shortcut will launch.
  - `name`: The name of the shortcut.
  - `iconPath`: The path of the icon for the shortcut (optional).
  - `args`: The arguments for the shortcut (optional).
  - `description`: The description of the shortcut (optional).

#### Example

```csharp
using CSLib;

// Create a shortcut on the desktop
Shortcut shortcut = new Shortcut(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), @"C:\Path\To\Application.exe", "MyApp", @"C:\Path\To\Icon.ico");
```

### AdminRights

The `AdminRights` class provides methods to check and obtain administrator rights for the current process.

#### CheckIfHasAdminRights

Check if the current user has administrator rights.

```csharp
using CSLib;

// Check if the current user has admin rights
bool isAdmin = AdminRights.CheckIfHasAdminRights();
Console.WriteLine($"Is admin: {isAdmin}");
```

#### GetAdminRights

Elevate the current process to run with administrator rights.

```csharp
using CSLib;

// Elevate the current process to run with admin rights
AdminRights.GetAdminRights();
```

#### EnsureAdminRights

Ensure that the application has administrator rights before proceeding. If the application does not have admin rights, it will prompt the user to retry or cancel.

```csharp
using CSLib;

try
{
    AdminRights.EnsureAdminRights("performing this operation", "Please grant admin rights to continue.");
}
catch (OperationCanceledException ex)
{
    Console.WriteLine(ex.Message);
}
```

### StartupApps

The `StartupApps` class provides a method to add applications to the startup folder, allowing them to run at system startup.

#### AddToStartup

Add an application to the startup folder.

```csharp
using CSLib;

// Add an application to the startup folder
StartupApps.AddToStartup(@"C:\Path\To\Application.exe", args: "--silent", installForAllUsers: true);
```

- `path`: The path to the application executable.
- `args`: Optional command-line arguments for the application.
- `installForAllUsers`: If true, installs the application for all users on the system.

### CriticalProcess

The `CriticalProcess` class provides functionality to check if a specific process is critical.

#### IsCritical

Check if a process is critical.

```csharp
using CSLib;
using System.Diagnostics;

// Check if a process is critical
Process process = Process.GetProcessesByName("explorer")[0];
bool isCritical = CriticalProcess.IsCritical(process);
Console.WriteLine($"Is critical: {isCritical}");
```

## Examples

Here are some example use cases demonstrating how to use CSLib in your applications.

- [MessageBox Example](examples/MessageBoxExample.cs)
- [Shortcut Example](examples/ShortcutExample.cs)
- [AdminRights Example](examples/AdminRightsExample.cs)
- [StartupApps Example](examples/StartupAppsExample.cs)
- [CriticalProcess Example](examples/CriticalProcessExample.cs)

## Links

- [My GitHub Profile](https://github.com/l-mommy-l)

## License

This project is licensed under the MIT License.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request with your changes.

---

This README now includes detailed explanations and examples for each class and method in your CSLib library.
