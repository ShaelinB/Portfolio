scoreboard players operation @p e2quest_progress = @p stat_pig_travel
scoreboard players operation @p e2quest_progress -= @p e2quest_initial


tellraw @p [{"text":"DEBUG: "},{"score":{"name":"@p","objective":"e2quest_progress"},"color":"blue"}]

execute if score @p e2quest_progress matches 10000.. run function role_quests:explorer/q2_complete