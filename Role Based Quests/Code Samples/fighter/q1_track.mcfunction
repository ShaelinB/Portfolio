scoreboard players operation @p f1quest_progress = @p stat_kill_zombie
scoreboard players operation @p f1quest_progress -= @p f1quest_initial

execute if score @p f1quest_progress matches 10.. run function role_quests:fighter/q1_complete