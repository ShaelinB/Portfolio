import 'package:flutter/material.dart';
import "game.dart";
import "about.dart";
import "leaderboard.dart";
import "settings.dart";
import "globals.dart" as globals;

class BottomNav extends StatefulWidget {
  const BottomNav({super.key});

  @override
  State<BottomNav> createState() => _BottomNavState();
}

class _BottomNavState extends State<BottomNav> {
  // tracks which bottom nav tab is currently selected
  int currentBottomTab = 0;

  // list of screens used
  final List<Widget> bottomNavScreens = [
    Game(),
    About(),
    Leaderboard(),
    Settings(),
  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      // top app bar
      appBar: AppBar(
        title: Text(
          "I Spy",
          style: TextStyle(
            color: Colors.white,
            fontFamily: "Quicksand",
            fontSize: 36,
          ),
        ),
        leading: Image.asset("assets/images/app_icon.png"),
        // app bar background color
        backgroundColor: globals.colorSecondaryBackground,
      ),

      // shows selected screen while keeping others alive
      body: IndexedStack(index: currentBottomTab, children: bottomNavScreens),

      // bottom navigation bar
      bottomNavigationBar: BottomNavigationBar(
        // shifting animation between tabs
        type: BottomNavigationBarType.shifting,

        // label styling
        selectedLabelStyle: TextStyle(
          color: Colors.white,
          fontFamily: "Quicksand",
        ),

        // currently selected tab
        currentIndex: currentBottomTab,

        // bottom nav items
        items: [
          // game tab
          BottomNavigationBarItem(
            icon: Icon(Icons.videogame_asset),
            label: "Game",
            backgroundColor: globals.colorSecondaryBackground,
          ),

          // about tab
          BottomNavigationBarItem(
            icon: Icon(Icons.info),
            label: "About",
            backgroundColor: globals.colorSecondaryBackground,
          ),

          // leaderboard tab
          BottomNavigationBarItem(
            icon: Icon(Icons.leaderboard),
            label: "Leaderboard",
            backgroundColor: globals.colorSecondaryBackground,
          ),

          // settings tab
          BottomNavigationBarItem(
            icon: Icon(Icons.settings),
            label: "Settings",
            backgroundColor: globals.colorSecondaryBackground,
          ),
        ],

        // handles tab switching
        onTap: (value) {
          setState(() {
            currentBottomTab = value;
          });
        },
      ),
    );
  }
}