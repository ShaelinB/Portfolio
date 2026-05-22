import 'package:flutter/material.dart';
import "globals.dart" as globals;

class About extends StatefulWidget {
  const About({super.key});

  @override
  State<About> createState() => _AboutState();
}

class _AboutState extends State<About> {
  @override
  Widget build(BuildContext context) {
    return Container(
      width: double.infinity,
      height: double.infinity,
      color: globals.colorPrimaryBackground,

      // outer padding for entire page
      padding: EdgeInsets.all(10),

      // allows scrolling if content overflows
      child: SingleChildScrollView(
        child: Column(
          children: [

            // title: how to play
            Text(
              "How to Play",
              style: globals.textStyleBodyTitle,
            ),

            SizedBox(height: 10),

            // instructions container
            Container(
              width: double.infinity,
              padding: EdgeInsets.fromLTRB(10, 0, 10, 0),

              child: Text(
                "Once you enter your name and select your difficulty hit “Start” to begin.\n\n"
                "The game will prompt you to find something of a specific color. "
                "Take a picture of that object before the time runs out.\n\n"
                "The game ends when the timer runs out or the object is the incorrect color.",
                style: globals.textStyleBodyMain,
              ),
            ),

            SizedBox(height: 10),

            // credits title
            Text(
              "Credits",
              style: globals.textStyleBodyTitle,
            ),

            SizedBox(height: 10),

            // credits text container
            Container(
              width: double.infinity,
              padding: EdgeInsets.fromLTRB(10, 0, 10, 0),

              child: Text(
                "Designer/Developer: Shaelin Brown\n\n"
                "Resources:\n\t"
                "- https://pub.dev/packages/camera\n\t"
                "- https://pub.dev/packages/shared_preferences\n\t"
                "- https://pub.dev/packages/image\n\t"
                "- https://pub.dev/packages/audioplayers\n\t"
                "- https://docs.flutter.dev/\n\t"
                "- https://dart.dev/docs\n\t"
                "- https://github.com/jptweb/IGME-340-Shared/blob/main/reference/README.md\n\t"
                "- https://chatgpt.com/"
                "- https://fonts.google.com/specimen/Quicksand?query=quicksan&preview.script=Latn\n\t"
                "- https://freesound.org/people/HughGuiney/sounds/352832/\n\t"
                "- https://freesound.org/people/Rudmer_Rotteveel/sounds/536421/\n\t"
                "- https://freesound.org/people/LittleRainySeasons/sounds/335908/\n\t",
                style: globals.textStyleBodyMain,
              ),
            ),
          ],
        ),
      ),
    );
  }
}