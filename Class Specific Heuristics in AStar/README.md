## 📄 Research on Class Specific Heuristics in A*
**Created:** Aug 2025 - Dec 2025

**Skills Used:** 
- Unreal Engine 5
- Blueprints
- Scientific Method
- GitHub

**Genre:** Research

**Description:** By implementing and testing a solution in a custom game, Quit Dreaming, it will either prove or disprove the claim that by implementing class specific heuristics to A* it increases the strategic complexity by raising its overall difficulty.

**Where to view:**  <a href="https://www.youtube.com/watch?v=uzG55BKzncM">https://www.youtube.com/watch?v=uzG55BKzncM</a>

**Development:** 

Using the scientific method, I examined the problem that many turn-based strategy games rely on a single heuristic for A* pathfinding, which can result in predictable enemy behavior and reduced strategic depth. To test this, I developed two controlled versions of Quit Dreaming.

The first version used a single shared heuristic across all character classes, while the second version implemented class-specific heuristics designed to reflect each class’s intended behavior and movement priorities. I created a toggleable system using a boolean flag that allowed the game to switch between heuristic modes without changing other gameplay systems.

To evaluate the impact, I conducted a blind playtest in which participants played both versions of the game. Half of the participants played the single-heuristic version first, while the other half played the class-specific version first to reduce order bias. Player feedback and performance data were collected and analyzed to assess differences in perceived difficulty and strategic decision-making.

The results did not support the original claim. Analysis showed that because players could select a destination tile directly, characters would always reach the same end position regardless of the path taken, minimizing the impact of differing heuristics. As a result, changes in pathfinding behavior did not significantly affect gameplay complexity.

This outcome highlighted an important design limitation and informed potential future work. A possible extension would be to explore constrained or pattern-based movement systems, similar to chess, where path choice directly affects positioning and outcomes. Overall, the project provided valuable insight into the relationship between AI pathfinding, player agency, and perceived difficulty in strategy games.
