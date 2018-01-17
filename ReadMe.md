# Prism Quickstart Templates

These templates are far from the traditional templates you are used to from Xamarin or Prism. These templates are designed to get you from a new project to a real working app as fast as possible. The QuickStart Template provides you options to hit the ground running with a number of services and even includes my "DevOps in a Box" [Mobile.BuildTools](https://github.com/dansiegel/Mobile.BuildTools).

## Support

If this project helped you reduce time to develop and made your app better, please help support this project.

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.me/dansiegel)

## Additional Features

- Uses [MvvmHelpers](https://github.com/jamesmontemagno/mvvm-helpers) from James Montemagno by default.
- Integrate with [App Center](https://appcenter.ms).
- Includes Fody Property Changed to keep your code clean and concise.
- Supports Data Services using the [AzureMobileClient.Helpers](https://github.com/dansiegel/AzureMobileClient.Helpers) and [Realm](https://github.com/realm/realm-dotnet) (Defaults to no data services)
- Works Cross Platform with `dotnet new`
- Includes an iOS project by default
- Includes an Android project by default
- Includes a UWP project
- Includes a UI Test project by default
- Includes a `Mock` Build configuration great for swapping services for testing and development.
- Includes [Prism.Forms.Extensions](https://www.nuget.org/packages/Prism.Forms.Extensions/)
- Includes updated [Prism.Plugin.Popups](https://github.com/dansiegel/Prism.Plugin.Popups) with fully integrated support for Popup Pages in Prism's Navigation Service.
- NuGet based configuration for MFractor
- NuGet based DevOps tooling [Mobile.BuildTools](https://github.com/dansiegel/Mobile.BuildTools)

## Item Templates

In addition to all of the great things you get with the project template, you also get item templates for Views and Services.

### View Item Templates

The View Item Templates will create a MasterDetailPage, TabbedPage, ContentPage or PopupPage along with a ViewModel. Optional flags allow you to create the ViewModel as `INavigationAware`, `INavigatingAware`, `INavigatedAware`, `IActiveAware`, or `IDestructible`, and to choose whether to use Prism's `BindableBase` or the MvvmHelpers `BaseViewModel`.

### Service Item Templates

The Service Item Templates will create an interface an implementing class, and a Mock implementing class.

## Installing

[![NuGetShield]][NuGetPage]

Currently it is recommended that you have at least at least the [DotNet CLI 2.0 preview 3](https://github.com/dotnet/cli/tree/release/2.0.0) or later.

```bash
dotnet new -i Prism.Forms.QuickstartTemplates::*
```

```bash
# Support for installing directly from a Git Repo has not been introduced and as such it must be done locally
git clone https://github.com/dansiegel/Prism-Templates.git
dotnet new --install Prism-Templates/content/MobileAppEmpty-CSharp/
dotnet new --install Prism-Templates/content/MobileAppQuickStart-CSharp/
dotnet new --install Prism-Templates/content/PrismModule-CSharp/
dotnet new --install Prism-Templates/content/XamarinItems/
```

## Getting Started

Currently the Visual Studio team is working on integration with dotnet templates for use within both Visual Studio 2017 and Visual Studio for Mac. Until that support is released the use of these templates will be limited to the cli.

To create a new Project using the Prism Template you must specify an App Id which should follow the format of `com.somecompany.yourappname`.

There are two project templates and one item template that is installed with this package. Note that the two project templates may be grouped and as such you will only see one listed in the installed templates even though they are both successfully installed.

| Template Name | Short Name |
|---------------|:----------:|
| Prism Forms Mobile App - Quickstart | prismforms |
| Prism Forms Mobile App - Empty | prismformsempty |
| Prism Forms Module | prismmodule |
| Prism Forms Item Templates | prismitem |

### Listing Template Options

```bash
dotnet new prismitem -h
dotnet new prismforms -h
dotnet new prismformsempty -h
dotnet new prismmodule -h
```

### Basic App Creation

Note that basic app creation will give you a solution with projects for iOS, Android, a Shared .NET Standard 1.4 library and a UI Test project. This will work the exact same for either the Empty or QuickStart template.

```bash
# To create an app inside of an empty directory or an app that will take the name of the parent directory
dotnet new prismformsempty -id "com.contoso.awesomeapp"

# To create an app in a new directory
dotnet new prismformsempty -id "com.contoso.awesomeapp" -o AwesomeApp

# To create an app with a specific name in the current directory
dotnet new prismformsempty -id "com.contoso.awesomeapp" -n "Contoso.AwesomeApp"
```

### Advanced App Creation

```bash
dotnet new prismforms -id "com.contoso.awesomeapp" --use-mobile-center true -ios-secret "{your iOS secret}" -android-secret "{your Android secret}"
dotnet new prismforms -id "com.contoso.awesomeapp" -data "AzureMobileClient" -fr "netstandard1.5" -client-id "{Your Authentication Client Id}"
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
```

### Creating a Module

```bash
dotnet new prismforms -id com.contoso.awesomeapp -o Contoso.AwesomeApp
cd Contoso.AwesomeApp/src
dotnet new prismmodule -o Contoso.AwesomeApp.AwesomeModule
cd ..
dotnet sln add src/Contoso.AwesomeApp.AwesomeModule/Contoso.AwesomeApp.AwesomeModule.csproj
```

### Known Issues

- When creating a Service item from the templates, an error occurs that prevents the creation of the empty service class though the Mock class is created.

[MyGetShield]: https://img.shields.io/myget/dansiegel-templates/vpre/Prism.Forms.Templates.svg
[MyGetPage]: https://www.myget.org/feed/dansiegel-templates/package/nuget/Prism.Forms.Templates
[NuGetShield]: https://img.shields.io/nuget/vpre/Prism.Forms.QuickstartTemplates.svg
[NuGetPage]: https://www.nuget.org/packages/Prism.Forms.QuickstartTemplates