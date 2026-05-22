import 'package:flutter/material.dart';
import 'package:camera/camera.dart';
import "bottom_nav.dart";
import "globals.dart" as globals;

// app entry point (runs before UI loads)
void main() async {

  // ensures camera list is loaded before app starts
  globals.cameras = await availableCameras();
  await globals.loadLeaderboard();

  // starts the Flutter app
  runApp(const MainApp());
}

// root widget of the application
class MainApp extends StatelessWidget {
  const MainApp({super.key});

  @override
  Widget build(BuildContext context) {

    return const MaterialApp(

      // removes debug banner in top right
      debugShowCheckedModeBanner: false,

      // starting screen of the app
      home: BottomNav(),
    );
  }
}