# GreenTechSee 2024 - Creative Development Unit 1

## IOT Lock
integrated with Azure IOT Hub. 

has 4 relays to simulate locks with ids 1-4

valid commands:
<ul>
	<li>lock:[lockId]</li>
	<li>unlock:[lockId]</li>
</ul>

components:
<ul>
	<li>LOLIN D1 mini</li>
	<li>4 relays</li>
</ul>

### setup
change defines in iot_configs.h

#define IOT_CONFIG_WIFI_SSID "[SSID]"

#define IOT_CONFIG_WIFI_PASSWORD "[PASSWORD]"

#define IOT_CONFIG_IOTHUB_FQDN "[IoTHubName].azure-devices.net"

#define IOT_CONFIG_DEVICE_ID "[IoTHubDeviceName]"

#define IOT_CONFIG_DEVICE_KEY "[Key from IoT Hub Device]"

#define TELEMETRY_FREQUENCY_MILLISECS 10000
