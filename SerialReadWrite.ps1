Write-Host "Liquid Level and Temperature:"
$port = new-Object System.IO.Ports.SerialPort COM3,9600,None,8,one

# Use Following to Read Device Address:
# [byte[]] $sendcmd = 0x18,0x08,0xAB,0x00,0x00,0x77,0x82,0x0D

# Use Following to Read Data Output Register:
[byte[]] $sendcmd = 0x18,0x08,0x6A,0xFF,0xFF,0x27,0xCE,0x0D

$port.open()  

$port.Write($sendcmd, 0, $sendcmd.Count)

Start-Sleep -milliseconds 100

$BytesToRead = $port.BytesToRead

$PortReturn = [System.Byte[]]::CreateInstance([System.Byte], $BytesToRead)

$NumberOfBytes = $port.Read($PortReturn, 0, $BytesToRead)

$HexArray = $PortReturn | ForEach-Object {"{0:X2}" -f $_}

$DistanceHex = $HexArray[6] + $HexArray[5]

[Convert]::ToInt32($DistanceHex, 16)

$TemperatureHex = $HexArray[10]

[Convert]::ToInt32($TemperatureHex, 16)

$port.Close()