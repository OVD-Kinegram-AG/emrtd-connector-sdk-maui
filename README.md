
# Kinegram eMRTD Connector MAUI
`Kinegram eMRTD Connector MAUI` is a project demonstrating the usage of both `Kinegram eMRTD Connector SDK Android` and `Kinegram eMRTD Connector SDK iOS` in a multi-platform .NET MAUI app.

See also: [Machine Readable Travel Documents](https://www.icao.int/publications/Documents/9303_p1_cons_en.pdf)

The entry point for iOS and Android is ```kinegram-emrtd-connector-maui/Pages/MainPage.xaml.cs```.

Additionally, on Android, there is an [EmrtdConnectorActivity.java](https://github.com/OVD-Kinegram-AG/emrtd-connector-sdk-android/blob/main/kinegram-emrtd-connector/src/main/java/com/kinegram/android/emrtdconnector/EmrtdConnectorActivity.java) you might also want to have a look at. This Activity presents the user a "Ready to Scan" sheet.

On iOS this sheet is presented automatically by the system.

## Setup Overview

You will need the following development software to get the demo app running from source. Any actions documented regarding macOS can be taken on a remote Mac as well.

>It is strongly recommended to install all software in the given order so Visual Studio Code can find the required Frameworks and SDKs right away.

- [Git](https://git-scm.com/downloads)
- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [XCode](https://developer.apple.com/xcode/)
- [Microsoft OpenJDK 17.0](https://learn.microsoft.com/en-us/java/openjdk/install)
- [Android Studio](https://developer.android.com/studio)
    - Android 14.0 ("UpsideDownCake") API Level 34
    - Android SDK Build-Tools 34
    - Android SDK Command-line Tools (latest)
- [Visual Studio Code](https://code.visualstudio.com/download)
    - [C# Dev Kit Extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)
    - [.NET MAUI Extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.dotnet-maui)

You might be successful with other IDEs like Rider, but Microsoft recommends Visual Studio Code for .NET MAUI development so this documentation will not consider any other IDEs.

See also: [What happened to Visual Studio for Mac?](https://learn.microsoft.com/en-us/visualstudio/releases/2022/what-happened-to-vs-for-mac)

Don't forget to add your Apple Developer Account to XCode and to set up your Certificate and Profile (more detail below).

## Visual Studio Code Setup

1. Install the above mentioned extensions for Visual Studio Code
2. Clone the [emrtd-connector-sdk-maui](https://github.com/OVD-Kinegram-AG/emrtd-connector-sdk-maui)
3. Open the cloned folder in Visual Studio Code
4. Ignore the NuGet error popping up fow now, the NuGet will be built later on
5. Confirm installation of the `.NET MAUI SDK` if asked
6. Install missing components and SDKs (if there are any left at this point):
   ```
   cd kinegram-emrtd-connector-maui
   dotnet workload restore
   ```
  
7. Again, in your terminal, trigger a build on the missing NuGet:
   ```
   cd kinegram-emrtd-connector-maui-android
   dotnet build kinegram-emrtd-connector-maui-android.csproj
   ```

This will create the `kinegram-emrtd-connector-maui-android.x.x.x.nupkg` which is the `Android eMRTD Connector SDK` wrapped in a NuGet for use in .NET MAUI. You can find further information regarding the SDKs and the steps required to apply changes made by yourself in the respective section below.

### Starting the Android app

1. Open a code file in the editor, for example `Pages/MainPage.xaml.cs`
2. Click the curly brackets in the Status Bar (bottom right), click `Debug Target` and pick your device from the list presented
3. If Visual Studio Code asks for an Android SDK location, pick the one suggested, this usually is `/Users/<username>/Library/Android/sdk` on macOS and `/Users/<username>/AppData/Local/Android/sdk` on Windows
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

## Customizing the eMRTD Android Connector SDK

1. Clone [emrtd-connector-sdk-android](https://github.com/OVD-Kinegram-AG/emrtd-connector-sdk-android.git)
2. Open the project in Android Studio
3. Apply your changes to the SDK library
4. Copy `emrtd-connector-sdk-android/kinegram-emrtd-connector/build/outputs/aar` to `kinegram-emrtd-connector-maui/kinegram-emrtd-connector-maui-android/libs` and recompile the MAUI app.

## Example 

## Customizing changes to the eMRTD iOS Connector SDK

1. Clone [emrtd-connector-sdk-ios](https://github.com/OVD-Kinegram-AG/emrtd-connector-sdk-ios)
2. Open the project in XCode
3. Apply your changes to the SDK library
4. Copy `emrtd-connector-sdk-ios/ObjCFramework/Distribution/KinegramEmrtdConnectorObjC.xcframework` to `kinegram-emrtd-connector-maui/kinegram-emrtd-connector-maui-ios/Native`
5. Make sure you adjust `ApiDefinitions.cs` and `StructsAndEnums.cs` in `kinegram-emrtd-connector-maui/kinegram-emrtd-connector-maui-ios` if you made modifications to the respective calls
