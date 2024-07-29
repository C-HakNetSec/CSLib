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

- **[MessageBox Class](#messagebox-class)**

#### Enums

- [MessageBoxButtons](#messageboxbuttons)
- [MessageBoxIcon](#messageboxicon)
- [MessageBoxDefaultButton](#messageboxdefaultbutton)
- [MessageBoxModality](#messageboxmodality)
- [MessageBoxOtherOptions](#messageboxotheroptions)
- [MessageBoxButtonClicked](#messageboxbuttonclicked)

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

- **[Shortcut Class](#shortcut-class)**

#### Constructor

- [Shortcut Constructor](#shortcut-constructor)

#### Example

```csharp
using CSLib;

// Create a shortcut on the desktop
Shortcut shortcut = new Shortcut(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), @"C:\Path\To\Application.exe", "MyApp", @"C:\Path\To\Icon.ico");
```

### AdminRights

The `AdminRights` class provides methods to check and obtain administrator rights for the current process.

- **[AdminRights Class](#adminrights-class)**

#### CheckIfHasAdminRights

- [CheckIfHasAdminRights Method](#checkifhasadminrights)

#### GetAdminRights

- [GetAdminRights Method](#getadminrights)

#### EnsureAdminRights

- [EnsureAdminRights Method](#ensureadminrights)

#### Example

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

- **[StartupApps Class](#startupapps-class)**

#### AddToStartup

- [AddToStartup Method](#addtostartup)

#### Example

```csharp
using CSLib;

// Add an application to the startup folder
StartupApps.AddToStartup(@"C:\Path\To\Application.exe", args: "--silent", installForAllUsers: true);
```

### CriticalProcess

The `CriticalProcess` class provides functionality to check if a specific process is critical.

- **[CriticalProcess Class](#criticalprocess-class)**

#### IsCritical

- [IsCritical Method](#iscritical)

#### Example

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

- [MessageBox Example](#messagebox)
- [Shortcut Example](#shortcut)
- [AdminRights Example](#adminrights)
- [StartupApps Example](#startupapps)
- [CriticalProcess Example](#criticalprocess)

## Links

- [My GitHub Profile](https://github.com/l-mommy-l)

## License

This project is licensed under the MIT License.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request with your changes.

---
