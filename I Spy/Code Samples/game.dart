import 'dart:typed_data';
import 'package:audioplayers/audioplayers.dart';
import 'package:flutter/material.dart';
import 'package:camera/camera.dart';
import 'package:image/image.dart' as img;
import "globals.dart" as globals;

class Game extends StatefulWidget {
  const Game({super.key});

  @override
  State<Game> createState() => _GameState();
}

class _GameState extends State<Game> {
  //camera fields
  late CameraController cameraController; // controls camera input
  bool isInitialized = false; // checks if camera is ready

  //game state fields
  bool gameStarted = false; // determines if game is running
  int score = 0; // player score
  String targetColor = ""; // current color player must find

  //player name text field
  final formKey = GlobalKey<FormState>(); // used for validation
  final TextEditingController nameController =
      TextEditingController(); // controls text field
  String name = ""; // stores player name

  //difficulty stuff
  String difficulty = "easy"; // current difficulty

  //timer stuff
  int timeLeft = 0; // time remaining
  int maxTime = 0; // max time based on difficulty
  bool timerRunning = false; // controls timer loop

  //color list
  final colors = ["red", "orange", "yellow", "green", "blue", "purple"];

  //overrides init state
  @override
  void initState() {
    super.initState();

    // listen for changes in the text field and update name
    nameController.addListener(() {
      setState(() => name = nameController.text);
    });

    // initialize camera on start
    _initCamera();
  }

  //cleans up memory
  @override
  void dispose() {
    cameraController.dispose(); // free camera memory
    nameController.dispose(); // free text controller memory
    super.dispose();
  }

  //initializes camera
  Future<void> _initCamera() async {
    cameraController = CameraController(
      globals.cameras[0], // use first available camera
      ResolutionPreset.medium,
    );

    try {
      await cameraController.initialize(); // initialize camera
      if (!mounted) return;
      setState(() => isInitialized = true); // update UI when ready
    } catch (e) {
      debugPrint("Camera error: $e"); // error if camera fails
    }
  }

  //start game logic
  void startGame() {
    if (!(formKey.currentState?.validate() ?? false)) return;

    setState(() {
      score = 0;
      gameStarted = true;
      timerRunning = true;

      // ensure difficulty is applied
      setDifficulty(difficulty);

      targetColor = _randomColor();
      timeLeft = maxTime;
    });

    _runTimer();
  }

  //game over logic
  void gameOver() async {
    await globals.playSound("incorrect.mp3");
    setState(() {
      gameStarted = false;
      timerRunning = false;
      globals.addScore(name, score);
    });

    // show popup with final score
    showDialog(
      context: context,
      builder: (_) => AlertDialog(
        title: Text(
          "Game Over",
          style: globals.textStyleBodyTitle,
          textAlign: TextAlign.center,
        ),
        content: Text(
          "$name's Score: $score",
          style: globals.textStyleBodyMain,
          textAlign: TextAlign.center,
        ),
        backgroundColor: globals.colorSecondaryBackground,
        actions: [
          Center(
            child: ElevatedButton(
              style: ElevatedButton.styleFrom(
                backgroundColor: Colors.white,
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadiusGeometry.circular(5),
                ),
              ),
              onPressed: () => Navigator.pop(context),
              child: Text("OK", style: globals.textStyleBodyMain),
            ),
          ),
        ],
      ),
    );
  }

  //sets timer
  void _runTimer() async {
    // loop while game is active
    while (timerRunning && gameStarted) {
      await Future.delayed(const Duration(seconds: 1)); // wait 1 second

      if (!mounted) return;

      setState(() => timeLeft--); // decrease time

      if (timeLeft <= 5) {
        await globals.playSound("timer.mp3");
      }

      // if time runs out → game over
      if (timeLeft <= 0) {
        gameOver();
        break;
      }
    }
  }

  //sets difficulty
  void setDifficulty(String value) {
    setState(() {
      difficulty = value;

      switch (value) {
        case "easy":
          maxTime = 20;
          break;
        case "medium":
          maxTime = 15;
          break;
        case "hard":
          maxTime = 10;
          break;
      }

      timeLeft = maxTime;
    });
  }

  //takes picture and checks color
  Future<void> takePicture() async {
    if (!gameStarted || !isInitialized) return;

    await globals.playSound("camera.mp3");

    final image = await cameraController.takePicture(); // capture image
    final bytes = await image.readAsBytes(); // convert to bytes

    // check detected color
    if (_detectColor(bytes) == targetColor) {
      await globals.playSound("correct.mp3");
      setState(() {
        score++; // increase score
        targetColor = _randomColor(); // new target
        timeLeft = maxTime; // reset timer
      });
    } else {
      gameOver(); // wrong color ends game
    }
  }

  //generates random color
  String _randomColor() {
    colors.shuffle(); // randomize list
    return colors.first; // return first item
  }

  //color detection
  String _detectColor(Uint8List bytes) {
    final image = img.decodeImage(bytes);
    if (image == null) return "unknown";

    int r = 0, g = 0, b = 0, count = 0;

    //sample pixels (every 5th pixel for performance)
    int startX = (image.width * 0.3).toInt();
    int endX = (image.width * 0.7).toInt();
    int startY = (image.height * 0.3).toInt();
    int endY = (image.height * 0.7).toInt();

    for (int y = startY; y < endY; y += 5) {
      for (int x = startX; x < endX; x += 5) {
        final p = image.getPixel(x, y);

        r += p.r.toInt();
        g += p.g.toInt();
        b += p.b.toInt();
        count++;
      }
    }

    // calculate average color
    r ~/= count;
    g ~/= count;
    b ~/= count;

    // classify based on RGB ranges
    if (r > g && r > b) {
      if (g > 100) return "yellow"; // red + green
      if (b > 100) return "purple"; // red + blue
      if (g > 50) return "orange";
      return "red";
    }

    if (g > r && g > b) return "green";

    if (b > r && b > g) return "blue";

    return "unknown";
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      width: double.infinity,
      height: double.infinity,
      color: globals.colorPrimaryBackground,
      padding: const EdgeInsets.all(10),
      child: SingleChildScrollView(
        child: Column(
          children: [
            //camera
            Container(
              width: 300,
              height: 300,
              decoration: BoxDecoration(
                borderRadius: BorderRadius.circular(16),
                border: Border.all(color: Colors.white, width: 5),
              ),
              child: isInitialized
                  ? ClipRRect(
                      borderRadius: BorderRadius.circular(16),
                      child: CameraPreview(cameraController),
                    )
                  : const Center(child: CircularProgressIndicator()),
            ),

            const SizedBox(height: 10),

            //game ui
            if (gameStarted) ...[
              //target color
              Text(
                "I spy something $targetColor",
                style: globals.textStyleBodyMain,
              ),

              const SizedBox(height: 10),

              //time left
              Text("Time Left: $timeLeft", style: globals.textStyleBodyMain),

              const SizedBox(height: 10),

              //score
              Text("Score: $score", style: globals.textStyleBodyMain),

              const SizedBox(height: 10),

              //take picture button
              ElevatedButton(
                style: ElevatedButton.styleFrom(
                  backgroundColor: Colors.white,
                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadiusGeometry.circular(5),
                  ),
                ),
                onPressed: takePicture,
                child: Text("Take Picture", style: globals.textStyleBodyMain),
              ),
            ],

            //start screen ui
            if (!gameStarted) ...[
              //start button
              ElevatedButton(
                style: ElevatedButton.styleFrom(
                  backgroundColor: Colors.white,
                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadiusGeometry.circular(5),
                  ),
                ),
                onPressed: startGame,
                child: Text("Start Game", style: globals.textStyleBodyMain),
              ),

              const SizedBox(height: 10),

              //name text field
              Form(
                key: formKey,
                child: TextFormField(
                  controller: nameController,
                  autovalidateMode: AutovalidateMode.onUserInteraction,
                  decoration: InputDecoration(
                    labelText: "Player Name",
                    labelStyle: globals.textStyleBodyMain,
                    fillColor: Colors.white,
                    filled: true,
                    border: OutlineInputBorder(
                      borderRadius: BorderRadius.circular(5),
                      borderSide: BorderSide.none,
                    ),
                  ),
                  validator: (value) => (value == null || value.trim().isEmpty)
                      ? "Enter a name"
                      : null,
                ),
              ),

              const SizedBox(height: 10),

              //difficulty dropdown
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Text("Difficulty", style: globals.textStyleBodyMain),

                  SizedBox(width: 20),

                  Container(
                    decoration: BoxDecoration(
                      color: Colors.white,
                      borderRadius: BorderRadius.circular(5),
                    ),
                    padding: EdgeInsets.all(5),
                    child: DropdownButton<String>(
                      value: difficulty,
                      items: const [
                        DropdownMenuItem(
                          value: "easy",
                          child: Text("Easy (20s)"),
                        ),
                        DropdownMenuItem(
                          value: "medium",
                          child: Text("Medium (15s)"),
                        ),
                        DropdownMenuItem(
                          value: "hard",
                          child: Text("Hard (10s)"),
                        ),
                      ],
                      style: globals.textStyleBodyMain,
                      onChanged: (value) {
                        if (value != null) setDifficulty(value);
                      },
                    ),
                  ),
                ],
              ),
            ],
          ],
        ),
      ),
    );
  }
}