import 'package:flutter/material.dart';
import "globals.dart" as globals;

class Settings extends StatefulWidget {
  const Settings({super.key});

  @override
  State<Settings> createState() => _SettingsState();
}

class _SettingsState extends State<Settings> {
  // local UI volume value
  double volume = globals.audioVolume;

  @override
  Widget build(BuildContext context) {
    return Container(
      width: double.infinity,
      height: double.infinity,
      color: globals.colorPrimaryBackground,

      // padding around entire settings page
      padding: const EdgeInsets.all(10),

      child: Column(
        children: [
          // settings title
          Text("Settings", style: globals.textStyleBodyTitle),

          const SizedBox(height: 20),

          // displays current volume as percentage
          Text(
            "Volume: ${(volume * 100).round()}%",
            style: globals.textStyleBodyMain,
          ),

          const SizedBox(height: 10),

          // volume row
          Row(
            children: [

              Icon(Icons.volume_down, color: globals.colorPrimaryTextColor),

              //volume slider
              Expanded(
                child: Slider(
                  value: volume,
                  min: 0,
                  max: 1,
                  divisions: 10,
                  label: (volume * 100).round().toString(),
                  activeColor: globals.colorSecondaryBackground,
                  inactiveColor: Colors.white,
                  onChanged: (value) {
                    setState(() {
                      volume = value; // update UI state
                      globals.setVolume(value); // update global volume
                    });
                  },
                ),
              ),

              Icon(Icons.volume_up, color: globals.colorPrimaryTextColor),
            ],
          ),
        ],
      ),
    );
  }
}