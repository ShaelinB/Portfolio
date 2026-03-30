scoreboard players operation @p b1quest_progress = @p stat_crafting_table
scoreboard players operation @p b1quest_progress -= @p b1quest_initial

execute if score @p b1quest_progress matches 10.. run function role_quests:builder/q1_complete