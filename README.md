# UART / HID Communication Manager

<p align="center">
  <strong>Windows tool for serial-port and USB HID devices</strong><br>
  Pick a COM port, send serial data and watch incoming data from a WPF interface.
</p>

<p align="center">
  <img alt=".NET" src="https://img.shields.io/badge/.NET-8.0%20Windows-512BD4?logo=dotnet&logoColor=white">
  <img alt="UI" src="https://img.shields.io/badge/UI-WPF-0078D4?logo=windows&logoColor=white">
  <img alt="UART" src="https://img.shields.io/badge/Communication-UART%20%2F%20COM-008080">
  <img alt="HID" src="https://img.shields.io/badge/USB-HID-555555">
</p>

🇹🇷 [Türkçe README](README.tr.md)

---

## Overview

UartComManager is a C# WPF desktop application for working with serial-port and HID devices on Windows. It lists COM ports, lets you pick a baud rate, opens and closes the connection, sends serial data and shows received data live. A helper view lists connected USB HID devices.

It was built as a bench tool for testing embedded boards and industrial devices (MCU firmware, sensor modules, PLC-side serial links) without a terminal program.

## Features

- 🔌 Lists available COM ports
- ⚙️ Common baud rates from 9600 to 115200
- 📤 Sends text to the serial port
- 📥 Shows incoming serial data live
- 📜 Auto-scroll and clear for the receive view
- 🔎 Lists USB HID devices with VID/PID and description
- ⚠️ Warns when the serial device disconnects

## Tech stack

- C# / .NET 8 for Windows
- WPF
- System.IO.Ports
- HidLibrary.Unofficial
- System.Management

## Build

1. Install the .NET 8 SDK on Windows.
2. Open `UartComManager.sln` in Visual Studio.
3. Restore NuGet packages.
4. Build in Release.

Command line:

```powershell
dotnet restore .\UartComManager.sln
dotnet build .\UartComManager.sln -c Release
```

## Usage

1. Start the app and select the COM port.
2. Select the baud rate your device uses.
3. Click **Connect** to open the port.
4. Send test data from the send box; received data appears in the receive view.
5. For HID listing, make sure Windows detects the target device.

## Notes

- Parity, data bits, stop bits and line terminator are not configurable yet; add them if your device needs non-default framing.
- Serial data can arrive in fragments; the app currently appends raw text. For protocol work, add packet parsing and timeouts.
- HID devices can be listed, but full HID read/write is not wired into the main screen yet.

## License

No license file is included. Ask the repository owner before reusing the code.
