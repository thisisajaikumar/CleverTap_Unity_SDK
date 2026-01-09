# CleverTap Unity SDK (UPM)

## Overview
- Unity Package Manager (UPM) based SDK
- Provides a reusable GameObject and public API to show native messages
- Platform behavior:
  - Android: Native Toast
  - iOS: Native Alert (Snackbar equivalent)
- Includes a Weather sample application to demonstrate usage

## What the SDK Does
- Exposes a simple public API to show messages
- Provides a MonoBehaviour that can be attached to any GameObject
- Handles platform-specific logic internally
- Works across Android and iOS

## Package Structure
    com.clevertap.sdk
    ├── Runtime
    ├── Plugins
    ├── Samples~
    └── README.md

## Installation
- Copy the folder `com.clevertap.sdk` into:
  <UnityProject>/Packages/
- Unity automatically detects the package via `package.json`

## Usage

### Public API
    using CleverTap;
    CleverTapToast.Show("Hello from CleverTap SDK");

### GameObject Usage
- Add `CleverTapToastBehaviour` to a GameObject
- Set the message in the Inspector
- Call `OnClick()` from a UI Button or event

## Platform Implementation
- Android uses JNI to display a native Toast
- iOS uses a native Objective-C plugin to display an alert
- Editor logs the message using `Debug.Log`

## Sample Application (Weather Sample)
- Demonstrates SDK usage in a real scenario
- Calls a public Weather API
- Displays weather information using `CleverTapToast.Show()`

### Importing the Sample
- Open Window → Package Manager
- Select CleverTap Unity SDK
- Click Import under Weather Sample
- Open the scene from:
  Assets/Samples/CleverTap Unity SDK/1.0.0/WeatherSample

## Architecture
- Runtime assembly contains all build-time code
- Editor assembly contains editor-only utilities
- Platform-specific code is isolated per platform
- Assembly Definitions enforce clean separation

## Testing
- Compatible with Unity Test Framework
- Supports EditMode and PlayMode tests

## Conclusion
This SDK demonstrates a clean Unity UPM package with platform-specific integrations and a working sample application.
