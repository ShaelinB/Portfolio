scoreboard players operation @p f2quest_progress = @p stat_kill_creeper
scoreboard players operation @p f2quest_progress -= @p f2quest_initial

execute if score @p f2quest_progress matches 20.. run function role_quests:fighter/q2_complete