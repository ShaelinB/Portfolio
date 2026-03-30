scoreboard players operation @p b2quest_progress = @p stat_place_stone
scoreboard players operation @p b2quest_progress -= @p b2quest_initial

execute if score @p b2quest_progress matches 32.. run function role_quests:builder/q2_complete