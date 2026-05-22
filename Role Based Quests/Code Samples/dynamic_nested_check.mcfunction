#tellraw @p {"text":"DEBUG: In dynamic nested check function", "color": "blue"}

#updates the role scores
scoreboard players operation @p builder_score = @p wood_pickaxe
scoreboard players operation @p builder_score += @p stone_pickaxe
scoreboard players operation @p builder_score += @p copper_pickaxe
scoreboard players operation @p builder_score += @p gold_pickaxe
scoreboard players operation @p builder_score += @p iron_pickaxe
scoreboard players operation @p builder_score += @p diamond_pickaxe
scoreboard players operation @p builder_score += @p netherite_pickaxe


scoreboard players operation @p explorer_score = @p sprinted_distance
scoreboard players operation @p explorer_score /= @p explorer_scale

scoreboard players operation @p fighter_score = @p mobs_killed
scoreboard players operation @p fighter_score *= @p fighter_scale


#tellraw @p [{"text":"DEBUG: Builder: ","color":"blue"},{"score":{"name":"@p","objective":"builder_score"}},{"text":" Explorer: ","color":"blue"},{"score":{"name":"@p","objective":"explorer_score"}},{"text":" Fighter: ","color":"blue"},{"score":{"name":"@p","objective":"fighter_score"}}]


#executes in dynamic mode if the player doesn't have a quest
execute if score @p has_quest matches 0 run function role_quests:dynamic/pick