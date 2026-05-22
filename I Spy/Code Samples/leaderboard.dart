import 'package:flutter/material.dart';
import "globals.dart" as globals;

class Leaderboard extends StatefulWidget {
  const Leaderboard({super.key});

  @override
  State<Leaderboard> createState() => _LeaderboardState();
}

class _LeaderboardState extends State<Leaderboard> {
  @override
  void initState() {
    super.initState();

    // load saved leaderboard once when screen opens
    globals.loadLeaderboard();
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      width: double.infinity,
      height: double.infinity,
      color: globals.colorPrimaryBackground,
      padding: const EdgeInsets.all(10),

      // main column layout
      child: Column(
        children: [
          // title
          Text("Leaderboard", style: globals.textStyleBodyTitle),

          SizedBox(height: 10),

          // listens for leaderboard updates automatically
          Expanded(
            child: ValueListenableBuilder(
              valueListenable: globals.leaderboard,
              builder: (context, list, _) {
                return Column(
                  children: [
                    Expanded(
                      child: list.isEmpty
                          ? Center(
                              child: Text(
                                "No scores yet",
                                style: globals.textStyleBodyMain,
                              ),
                            )
                          : ListView.builder(
                              itemCount: list.length,
                              itemBuilder: (context, index) {
                                final item = list[index];

                                return Card(
                                  color: Colors.white,
                                  child: ListTile(
                                    title: Text(
                                      item['name'],
                                      style: globals.textStyleBodyMain,
                                    ),
                                    trailing: Text(
                                      item['score'].toString(),
                                      style: globals.textStyleBodyMain,
                                    ),
                                  ),
                                );
                              },
                            ),
                    ),

                    //appears when the list isn't empty
                    if (list.isNotEmpty) ...[
                      
                      SizedBox(height: 10),

                      //clear leaderboard button
                      ElevatedButton(
                        style: ElevatedButton.styleFrom(
                          backgroundColor: Colors.white,
                          shape: RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(5),
                          ),
                        ),
                        onPressed: () {
                          globals.clearLeaderboard();
                        },
                        child: Text(
                          "Clear Leaderboard",
                          style: globals.textStyleBodyMain,
                        ),
                      ),
                    ],
                  ],
                );
              },
            ),
          ),
        ],
      ),
    );
  }
}