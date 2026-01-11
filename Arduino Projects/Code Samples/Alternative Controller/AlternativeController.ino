int xPin = A0;

bool flipped = false;

int flipThreshold = 800;    
int resetThreshold = 750;

unsigned long lastFlipTime = 0;
unsigned long flipCooldown = 500;

void setup() {
  Serial.begin(9600);
}

void loop() {
  //gets the voltage from the X pin
  int xVal = analogRead(xPin);
  unsigned long currentTime = millis();

  //checks if a flip occurs when the x value goes above and amount and if some amount of time passes
  if (xVal > flipThreshold && !flipped && (currentTime - lastFlipTime > flipCooldown)) {
    flipped = true;
    lastFlipTime = currentTime;
  }

  //resets if the x value goes below the reset threshold
  if (xVal < resetThreshold && flipped) {
    flipped = false;
  }

  //sends bool and x value to Unity
  Serial.print(flipped);
  Serial.print(",");
  Serial.println(xVal);

  delay(50);
}
