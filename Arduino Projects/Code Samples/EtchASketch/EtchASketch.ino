bool pastState = false;
bool buttonState = false;

void setup() {
  Serial.begin(9600);
  pinMode(A0, INPUT);
  pinMode(A1, INPUT);
  pinMode(2, INPUT);
}

void loop() {

  //maps the potentiometer values to the canvas width and height
  //rotating it to the left makes it go either left or down
  //rotating it to the right makes it go either right or up
  //0,0 in processing is top left
  //+x is right, +y is down
  int xPos = map(analogRead(A1), 0, 1023, 960, 0);
  int yPos = map(analogRead(A0), 0, 1023, 0, 540);

  //reads the current button state
  bool current = digitalRead(2) == LOW;

  //if the button is down and it was previously up then it is clicked
  if (current && !pastState) {
    buttonState = true;
  }
  //otherwise it's not a valid click
  else {
    buttonState = false;
  }

  //sets past state to current
  pastState = current;

  //sends data to processing
  Serial.print(xPos);
  Serial.print(",");
  Serial.print(yPos);
  Serial.print(",");
  Serial.println(buttonState);

  delay(10);
}
