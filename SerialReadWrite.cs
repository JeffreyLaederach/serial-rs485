using System;
using System.IO.Ports;

class Program
{
    static void Main()
    {
        Console.WriteLine("Liquid Level and Temp:");

        SerialPort port = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);

        // Use Following to Read Device Address:
        // byte[] sendcmd = { 0x18, 0x08, 0xAB, 0x00, 0x00, 0x77, 0x82, 0x0D };

        // Use Following to Read Data Output Register:
        byte[] sendcmd = { 0x18, 0x08, 0x6A, 0xFF, 0xFF, 0x27, 0xCE, 0x0D };

        port.Open();
        port.Write(sendcmd, 0, sendcmd.Length);
        System.Threading.Thread.Sleep(100);

        int bytesToRead = port.BytesToRead;
        byte[] portReturn = new byte[bytesToRead];
        int numberOfBytes = port.Read(portReturn, 0, bytesToRead);

        string[] hexArray = new string[portReturn.Length];
        for (int i = 0; i < portReturn.Length; i++)
        {
            hexArray[i] = portReturn[i].ToString("X2");
        }

        string distanceHex = hexArray[6] + hexArray[5];
        int distance = Convert.ToInt32(distanceHex, 16);

        string temperatureHex = hexArray[10];
        int temperature = Convert.ToInt32(temperatureHex, 16);

        port.Close();

        Console.WriteLine($"Distance: {distance}"); // in millimeters 
        Console.WriteLine($"Temperature: {temperature}"); // in celsius 
    }
}