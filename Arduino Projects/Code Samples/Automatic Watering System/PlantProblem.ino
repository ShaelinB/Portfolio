int moisture; //the value that the moisture sensor is reading
int threshold = 190; //the value that determines whether the pump is on or off

//runs once at the beginning
void setup()
{
  pinMode(4, OUTPUT); //relay IN pin
  pinMode(A0, INPUT); //moisture sensor AUOT pin
  Serial.begin(9600);
}

void loop()
{
  moisture = analogRead(A0); //reads sensor
  Serial.println(moisture);

  if (moisture > threshold) //checks if soil is dry
  {
    digitalWrite(4, LOW); //turns pump on
  }
  else
  {
    digitalWrite(4, HIGH); //turns pump off
  }
}