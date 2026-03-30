scoreboard players operation @p e3quest_progress = @p stat_boat_travel
scoreboard players operation @p e3quest_progress -= @p e3quest_initial

tellraw @p [{"text":"DEBUG: "},{"score":{"name":"@p","objective":"e3quest_progress"},"color":"blue"}]

execute if score @p e3quest_progress matches 50000.. run function role_quests:explorer/q3_complete
