
# Kinegram eMRTD Connector MAUI
`Kinegram eMRTD Connector MAUI` is a project demonstrating the usage of both `Kinegram eMRTD Connector SDK Android` and `Kinegram eMRTD Connector SDK iOS` in a multi-platform .NET MAUI app.

See also: [Machine Readable Travel Documents](https://www.icao.int/publications/Documents/9303_p1_cons_en.pdf)

## Setup Overview
You will need the following development software to get the demo app running:

- [XCode](https://developer.apple.com/xcode/)
- [Visual Studio Code](https://code.visualstudio.com/download)
    - [C# Dev Kit Extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)
    - [.NET MAUI Extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.dotnet-maui)
- [Android Studio](https://developer.android.com/studio)
    - Android 14.0 ("UpsideDownCake") API Level 34
    - Android SDK Build-Tools 34
    - Android SDK Command-line Tools (latest)
- [Microsoft OpenJDK 17.0](https://learn.microsoft.com/en-us/java/openjdk/install)
    - Use the [Homebrew](https://brew.sh) approach if you don't want to set up Java manually. Follow the instructions [here](https://learn.microsoft.com/en-us/java/openjdk/install#install-on-macos)

You might be successful with other IDEs like Rider, but Microsoft recommends Visual Studio Code for .NET MAUI development so this documentation will not consider any other IDEs.

See also: [What happened to Visual Studio for Mac?](https://learn.microsoft.com/en-us/visualstudio/releases/2022/what-happened-to-vs-for-mac)

Also, this documentation will consider setting up the environment on Macs only.

>It is recommended to install **and** open XCode and Android Studio **before** you install Visual Studio Code so the latter can find the paths to the required Frameworks and SDKs right away.

Don't forget to add your Apple Developer Account to XCode and to set up your Certificate and Profile (more detail below).

## Visual Studio Code Setup

1. Install the above mentioned extensions for Visual Studio Code
2. Clone the [kinegram-emrtd-connector-maui](https://git.kurzdigital.com/kta/kinegram-emrtd-connector-maui.git)
3. Open the cloned folder in Visual Studio Code and choose to open `kinegram-emrtd-connector-maui.sln` if asked to choose a solution
4. Ignore the NuGet error popping up fow now
5. Open a terminal and run `dotnet workload restore` to install missing components and SDKs (this will take a while)
6. Again, in your terminal, run this command to create the missing NuGet: `dotnet build kinegram-emrtd-connector-maui-android/kinegram-emrtd-connector-maui-android.csproj`

> The last step will create the `kinegram-emrtd-connector-maui-android.x.x.x.nupkg` which is the `Android eMRTD Connector SDK` wrapped in a NuGet for use in .NET MAUI. You can find further information regarding the Android SDK and the steps required to apply changes by yourself in the respective section below.

### Starting the Android app

1. Open a code file in the editor, for example `Pages/MainPage.xaml.cs`
2. Click the curly brackets in the Status Bar, click `Debug Target` and pick your device from the list presented
3. If Visual Studio Code asks for an Android SDK location, pick the one suggested, this usually is `/Users/<username>/Library/Android/sdk`
4. Run the app
    - Click the `Run and Debug` icon on the left and then the `Run and Debug` button
    - Or click the `Run project associated with this file` (play icon) on the top right

### Starting the iOS app

1. Open `kinegram-emrtd-connector-maui.csproj`and adjust the values in `CodesignKey` and `CodesignProvision` for Debug and/or Release to your own certificate and profile names
2. Open `Platforms/Info.plist` and adjust `CFBundleIdentifier` to the bundle identifier you set in your Apple Developer Account for this app
3. Open a code file in the editor, for example `Pages/MainPage.xaml.cs`
4. Run the app
    - Click the `Run and Debug`icon on the left and then the `Run and Debug` button
    - Or click the `Run project associated with this file` (play icon) on the top right

## Committing changes to the eMRTD Android Connector SDK

1. Clone [emrtd-connector-sdk-android](https://github.com/OVD-Kinegram-AG/emrtd-connector-sdk-android.git)
2. Open the project in Android Studio
3. Apply your changes to the SDK library
4. Copy `emrtd-connector-sdk-android/kinegram-emrtd-connector/build/outputs/aar` to `kinegram-emrtd-connector-maui/kinegram-emrtd-connector-maui-android/libs` and recompile the MAUI app.

## Committing changes to the eMRTD iOS Connector SDK

1. Clone [emrtd-connector-sdk-ios](https://github.com/OVD-Kinegram-AG/emrtd-connector-sdk-ios)
2. Open the project in XCode
3. Apply your changes to the SDK library
4. Copy `emrtd-connector-sdk-ios/ObjCFramework/Distribution/KinegramEmrtdConnectorObjC.xcframework` to `kinegram-emrtd-connector-maui/kinegram-emrtd-connector-maui-ios/Native`
5. Make sure you adjust `ApiDefinitions.cs` and `StructsAndEnums.cs` in `kinegram-emrtd-connector-maui/kinegram-emrtd-connector-maui-ios` if you made modifications to the respective calls
