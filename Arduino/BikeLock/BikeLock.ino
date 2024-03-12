#include <ESP8266WiFi.h>

#define RELAY_1 15
#define RELAY_2 13
#define RELAY_3 12
#define RELAY_4 14

void setup() {
  Serial.begin(115200);
  Serial.println("Setup");

  pinMode(RELAY_1, OUTPUT);
  pinMode(RELAY_2, OUTPUT);
  pinMode(RELAY_3, OUTPUT);
  pinMode(RELAY_4, OUTPUT);

  connectToWiFi();
}

void loop() {
  lock(RELAY_1);
  delay(500);
  lock(RELAY_2);
  delay(500);
  lock(RELAY_3);
  delay(500);
  lock(RELAY_4);
  delay(500);
  unlock(RELAY_1);
  delay(500);
  unlock(RELAY_2);
  delay(500);
  unlock(RELAY_3);
  delay(500);
  unlock(RELAY_4);
  delay(500);
}

void lock(int pin) {
  Serial.println("Lock");
  digitalWrite(pin, HIGH);
}

void unlock(int pin) {
  Serial.println("Unlock");
  digitalWrite(pin, LOW);
}

void setLED(int r, int g, int b) {
  r = constrain(r, 0, 255);
  g = constrain(g, 0, 255);
  b = constrain(b, 0, 255);
}

void connectToWiFi(){
  const char* ssid = "Kulturfabrikken-Gjest";
  const char* pass = "Kfgjest!";

  Serial.print("Connecting to WIFI SSID ");
  Serial.println(ssid);

  WiFi.mode(WIFI_STA);
  WiFi.begin(ssid, pass);
  while (WiFi.status() != WL_CONNECTED)
  {
    delay(500);
    Serial.print(".");
  }

  Serial.print("WiFi connected, IP address: ");
  Serial.println(WiFi.localIP());
}
