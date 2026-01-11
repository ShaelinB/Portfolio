const int trigPin = 10;
const int echoPin = 9;
const int motorPin = 6;  // the number of the motor pin

int highPin = 2;
int lowPin = 3;

bool gameOver = false;

float maxTimer = 2;
float curTimer = maxTimer;
int prevMillis = 0;

float duration, distance, gem1, gem2;

void setup() {
  pinMode(trigPin, OUTPUT);
  pinMode(echoPin, INPUT);
  pinMode(motorPin, OUTPUT);

  Serial.begin(9600);
}

void loop() {

  //checks if the game is over
  if (!gameOver) {

    int frameMillis = millis();

    float deltaTime = frameMillis - prevMillis;D
    curTimer -= (deltaTime / 1000);
    Serial.println(deltaTime);

    if (curTimer <= 0) {
      curTimer = maxTimer;
      SwapHighLow();
    }

    //shoot bursts from the ultra sonic sensor
    digitalWrite(trigPin, LOW);
    delayMicroseconds(2);
    digitalWrite(trigPin, HIGH);
    delayMicroseconds(10);
    digitalWrite(trigPin, LOW);

    duration = pulseIn(echoPin, HIGH);
    distance = (duration * .0343) / 2;
    if (curTimer == maxTimer) {
      Serial.println("Distance: ");
      Serial.println(distance);
    }
    delay(100);

    gem1 = analogRead(A0);
    gem2 = analogRead(A2);




    if (distance > 60) {
      analogWrite(motorPin, 80);
      digitalWrite(lowPin, LOW);
      digitalWrite(highPin, HIGH);
    } else {
      Lose();
      return;
    }


    //check win or lose
    if (gem1 > 20 && gem2 > 20) {
      Win();
      return;
    }

    prevMillis = frameMillis;
  }
}

//function that gets called when you win
void Win() {
  analogWrite(motorPin, 0);
  Serial.println("You Win");
  gameOver = true;
  return;
}

//function that gets called when you lose
void Lose() {
  analogWrite(motorPin, 0);
  Serial.println("You Lose");
  gameOver = true;
  return;
}

//function that gets called when the sensor flips direction
void SwapHighLow() {
  if (highPin == 2) {
    lowPin = 2;
    highPin = 3;
  } else {
    lowPin = 3;
    highPin = 2;
  }
}
