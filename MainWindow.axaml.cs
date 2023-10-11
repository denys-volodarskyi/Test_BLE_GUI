#define BLE

using System;

using Avalonia.Controls;
using Avalonia.Threading;

#if BLE
using InTheHand.Bluetooth;
#else
using System.Threading.Tasks;
#endif

namespace Test_BLE_GUI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Btn_Find_BLE_Devices.Click += Btn_Find_BLE_Devices_Click;
    }

    public void LogLine(string message = "") => Dispatcher.UIThread.Invoke(delegate
    {
        TB_Log.Text += message + Environment.NewLine;
        TB_Log.CaretIndex = TB_Log.Text.Length;
    });

    private async void Btn_Find_BLE_Devices_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
#if BLE
        LogLine("Searching the devices...");

        if (await Bluetooth.RequestDeviceAsync() is BluetoothDevice device)
        {
            LogLine
                (
                $"Bluetooth device:\r\n" +
                $"  Name: {device.Name}\r\n" +
                $"  ID:   {device.Id}\r\n"
                );
        }
        else
            LogLine("Nothing found or selected.");
#else
        await Task.CompletedTask;
#endif
    }
}