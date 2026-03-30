scoreboard players operation @p b3quest_progress = @p stat_mine_oak
scoreboard players operation @p b3quest_progress -= @p b3quest_initial

execute if score @p b3quest_progress matches 64.. run function role_quests:builder/q3_complete