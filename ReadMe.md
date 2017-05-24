# Prism Quickstart Templates

These templates are a prototype for the official Prism Templates to be released later this year. These templates will be released separately and in parallel. These templates have several major changes from the traditional templates you are used to from Xamarin or Prism.

The core project uses netstandard1.4 by default, and can be overridden to use netstandard2.0. Localization is supported and ready to go out of the box with this template.

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