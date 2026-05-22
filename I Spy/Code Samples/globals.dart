import 'package:flutter/material.dart';
import 'package:camera/camera.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:audioplayers/audioplayers.dart';
import 'dart:convert';

//style stuff

// background color of main screens
Color colorPrimaryBackground = Color(0xFFF5F1E8);

// secondary UI background color (app bar, accents)
Color colorSecondaryBackground = Color(0xFF6EC1C8);

// primary text color used across app
Color colorPrimaryTextColor = Color(0xFF3A3A3A);

// large title text style
TextStyle textStyleBodyTitle = TextStyle(
  fontFamily: "Quicksand",
  fontSize: 28,
  color: colorPrimaryTextColor,
);

// normal body text style
TextStyle textStyleBodyMain = TextStyle(
  fontFamily: "Quicksand",
  fontSize: 20,
  color: colorPrimaryTextColor,
);

//camera stuff

// available device cameras (front/back)
late List<CameraDescription> cameras;

//leaderboard stuff

// reactive leaderboard list (updates UI automatically)
ValueNotifier<List<Map<String, dynamic>>> leaderboard = ValueNotifier([]);

// loads leaderboard from local storage (SharedPreferences)
Future<void> loadLeaderboard() async {
  final prefs = await SharedPreferences.getInstance();

  // get saved JSON string
  final data = prefs.getString('leaderboard');

  if (data != null) {
    // decode JSON into list
    List decoded = jsonDecode(data);

    // convert dynamic maps into proper Map<String, dynamic>
    final list = decoded.map((e) => Map<String, dynamic>.from(e)).toList();

    // sort by highest score first
    list.sort((a, b) => b['score'].compareTo(a['score']));

    leaderboard.value = list;
  }
}

// saves leaderboard to device storage
Future<void> saveLeaderboard() async {
  final prefs = await SharedPreferences.getInstance();

  // encode leaderboard list into JSON string
  await prefs.setString('leaderboard', jsonEncode(leaderboard.value));
}

// adds new score entry and updates leaderboard
Future<void> addScore(String name, int score) {
  final list = List<Map<String, dynamic>>.from(leaderboard.value);

  // add new entry
  list.add({"name": name, "score": score});

  // sort highest score first
  list.sort((a, b) => b['score'].compareTo(a['score']));

  leaderboard.value = list;

  // persist changes
  return saveLeaderboard();
}

// clears leaderboard completely
Future<void> clearLeaderboard() async {
  leaderboard.value = []; // clear memory

  final prefs = await SharedPreferences.getInstance();
  await prefs.remove('leaderboard'); // remove saved data
}

//sound stuff

// global audio player instance
final AudioPlayer audioPlayer = AudioPlayer();

// current volume level (0.0 to 1.0)
double audioVolume = 1.0;

// sets global volume for all sounds
void setVolume(double value) {
  audioVolume = value.clamp(0.0, 1.0);

  // apply volume immediately
  audioPlayer.setVolume(audioVolume);
}

// plays a sound from assets/sounds/
Future<void> playSound(String file) async {
  try {
    await audioPlayer.stop(); // stop any current sound
    await audioPlayer.setVolume(audioVolume); // apply volume
    await audioPlayer.play(AssetSource('sounds/$file')); // play new sound
  } catch (e) {
    print("Sound error: $e"); // debug error
  }
}