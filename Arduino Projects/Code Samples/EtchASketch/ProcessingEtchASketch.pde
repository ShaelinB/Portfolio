import processing.serial.*;

Serial myPort;

//sets initial values to middle of the canvas
int xPos = 480;
int yPos = 270;
boolean buttonState = false;

void setup() {
  //sets up the canvas
  size(960, 540);
  frameRate(120);
  background(255);
  colorMode(RGB, 255);

//connects the arduino
  myPort = new Serial(this, "COM3", 9600);
}

void draw() {

  //reads the string
  String line = myPort.readStringUntil('\n');
  if (line == null) return;

  //splits the string into an array
  line = trim(line);
  String[] data = split(line, ',');

  //if data isn't length of 3 then it got formatted wrong
  if (data.length != 3) return;

  //sets the values from the arduino
  int newX = int(data[0]);
  int newY = int(data[1]);
  boolean newBtn = int(data[2]) == 1;

  //if the button is hit clear the screen
  if (newBtn) {
    background(255);
  } 
  //else draw a line from the previous position to the current position
  else {
    stroke(0);
    strokeWeight(2);
    line(xPos, yPos, newX, newY);
  }

  //save this state
  xPos = newX;
  yPos = newY;
  buttonState = newBtn;
}
