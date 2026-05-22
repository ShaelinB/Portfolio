scoreboard players operation @p e1quest_progress = @p stat_craft_map
scoreboard players operation @p e1quest_progress -= @p e1quest_initial

execute if score @p e1quest_progress matches 1.. run function role_quests:explorer/q1_complete
