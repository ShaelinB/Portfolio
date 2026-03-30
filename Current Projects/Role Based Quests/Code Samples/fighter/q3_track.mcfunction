scoreboard players operation @p f3quest_progress = @p stat_kill_enderman
scoreboard players operation @p f3quest_progress -= @p f3quest_initial

execute if score @p f3quest_progress matches 5.. run function role_quests:fighter/q3_complete