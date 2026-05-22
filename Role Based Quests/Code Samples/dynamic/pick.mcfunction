tellraw @p {"text":"DEBUG: In dynamic pick function", "color": "blue"}

scoreboard players set @p has_quest 1

# Reset quest_roll first
scoreboard players set @p quest_roll 0

# Builder has higher score
execute as @p if score @p builder_score >= @p explorer_score if score @p builder_score >= @p fighter_score run function role_quests:dynamic/builder_pick

# Explorer has higher score
execute as @p if score @p explorer_score >= @p builder_score if score @p explorer_score >= @p fighter_score run function role_quests:dynamic/explorer_pick

# Fighter has higher score
execute as @p if score @p fighter_score >= @p builder_score if score @p fighter_score >= @p explorer_score run function role_quests:dynamic/fighter_pick
