# Prism Quickstart Templates

These templates are a prototype for the official Prism Templates to be released later this year. These templates will be released separately and in parallel. These templates have several major changes from the traditional templates you are used to from Xamarin or Prism.

The core project uses netstandard1.4 by default, and can be overridden to use netstandard1.5, netstandard1.6 or netstandard2.0. Localization is supported and ready to go out of the box with this template.

## Additional Features

- Uses [MvvmHelpers](https://github.com/jamesmontemagno/mvvm-helpers) from James Montemagno by default.
- Integrate with [Mobile Center](https://mobile.azure.com).
- Includes Fody Property Changed to keep your code clean and concise.
- Supports Data Services using the [AzureMobileClient.Helpers](https://github.com/dansiegel/AzureMobileClient.Helpers) and [Realm](https://github.com/realm/realm-dotnet) (Defaults to no data services)
- Works Cross Platform with `dotnet new`
- Includes an iOS project by default
- Includes an Android project by default
- Includes a UWP project
- Includes a UI Test project by default
- Includes a `Mock` Build configuration great for swapping services for testing and development.

## Item Templates

In addition to all of the great things you get with the project template, you also get item templates for Views and Services.

### View Item Templates

The View Item Templates will create a MasterDetailPage, TabbedPage, ContentPage or PopupPage along with a ViewModel. Optional flags allow you to create the ViewModel as `INavigationAware`, `INavigatingAware`, `INavigatedAware`, `IActiveAware`, or `IDestructible`, and to choose whether to use Prism's `BindableBase` or the MvvmHelpers `BaseViewModel`.

### Service Item Templates

The Service Item Templates will create an interface an implementing class, and a Mock implementing class.

## Installing

[![Shield]][MyGetPage]

```bash
nuget sources Add -Name "Prism-Beta-Templates" -Source https://www.myget.org/F/dansiegel-templates/api/v3/index.json
dotnet new -i Prism.Forms.Templates::*
```

```bash
# Support for installing directly from a Git Repo has not been introduced and as such it must be done locally
git clone https://github.com/dansiegel/Prism-Templates.git
dotnet new --install Prism-Templates/content/MobileApp-CSharp/
dotnet new --install Prism-Templates/content/XamarinItems/
```

## Getting Started

Currently the Visual Studio team is working on integration with dotnet templates for use within both Visual Studio 2017 and Visual Studio for Mac. Until that support is released the use of these templates will be limited to the cli.

To create a new Project using the Prism Template you must specify an App Id which should follow the format of `com.somecompany.yourappname`.

### Basic App Creation

Note that basic app creation will give you a solution with projects for iOS, Android, a Shared .NET Standard 1.4 library and a UI Test project.

```bash
# To create an app inside of an empty directory or an app that will take the name of the parent directory
dotnet new prismforms -id "com.contoso.awesomeapp"

# To create an app in a new directory
dotnet new prismforms -id "com.contoso.awesomeapp" -o AwesomeApp

# To create an app with a specific name in the current directory
dotnet new prismforms -id "com.contoso.awesomeapp" -n "Contoso.AwesomeApp"
```

### Advanced App Creation

```bash
dotnet new prismforms -id "com.contoso.awesomeapp" --use-mobile-center true -ios-secret "{your iOS secret}" -android-secret "{your Android secret}"
dotnet new prismforms -id "com.contoso.awesomeapp" -data "AzureMobileClient" -framework "netstandard1.5" -client-id "your Authentication Client Id"
```

### Creating Prism Items

Note that you should execute these commands from the Project root and not the Solution root. Also Visual Studio for Mac has a known issue that it will not observe changes to the directory which will require you to reload the project in order to see the new classes in the IDE.

```bash
$namespace = "AwesomeApp"
# Create a basic Content Page & the View Model
dotnet new prismitem -n "ViewA" -namespace $namespace

# Create a Tabbed Page
dotnet new prismitem -n "TabbedRoot" -namespace $namespace -page TabbedPage
dotnet new prismitem -n "TabA" -namespace $namespace -child
# Add the IDestructible interface
dotnet new prismitem -n "TabB" -namespace $namespace -child -destructible

# Override the default behavior to make the ViewModel INavigatingAware instead of INavigationAware
dotnet new prismitem -n "ViewB" -namespace $namespace -navigating

# Make a ViewModel IActiveAware and not INavigationAware
dotnet new prismitem -n "ViewC" -namespace $namespace -aa true -navigation false

# Use BindableBase instead of MvvmHelpers BaseViewModel
dotnet new prismitem -n "ViewD" -namespace $namespace -umh false
```

### Known Issues

- The Fody Weavers file is not being properly renamed and as such will need to be manually renamed. You simply need to remove either the `Normal` or `Realm` part of the file name.
- When creating a Service item from the templates, an error occurs that prevents the creation of the empty service class though the Mock class is created.
- When creating a UWP project there is currently no way to update the project with a new temporary signing certificate.

[Shield]: https://img.shields.io/myget/dansiegel-templates/vpre/Prism.Forms.Templates.svg
[MyGetPage]: https://www.myget.org/feed/dansiegel-templates/package/nuget/Prism.Forms.Templates