tellraw @p {"text":"DEBUG: In setup function", "color": "blue"}

#need to make sure they exist
scoreboard objectives add quest_mode dummy

scoreboard objectives add builder_score dummy
scoreboard objectives add wood_pickaxe minecraft.used:minecraft.wooden_pickaxe
scoreboard objectives add stone_pickaxe minecraft.used:minecraft.stone_pickaxe
scoreboard objectives add copper_pickaxe minecraft.used:minecraft.copper_pickaxe
scoreboard objectives add gold_pickaxe minecraft.used:minecraft.golden_pickaxe
scoreboard objectives add iron_pickaxe minecraft.used:minecraft.iron_pickaxe
scoreboard objectives add diamond_pickaxe minecraft.used:minecraft.diamond_pickaxe
scoreboard objectives add netherite_pickaxe minecraft.used:minecraft.netherite_pickaxe

scoreboard objectives add explorer_score dummy
scoreboard objectives add sprinted_distance minecraft.custom:minecraft.sprint_one_cm
scoreboard objectives add explorer_scale dummy

scoreboard objectives add fighter_score dummy
scoreboard objectives add mobs_killed minecraft.custom:minecraft.mob_kills
scoreboard objectives add fighter_scale dummy


scoreboard objectives add has_quest dummy
scoreboard objectives add quest_roll dummy
scoreboard objectives add selected_quest dummy

scoreboard objectives add b1quest_complete dummy
scoreboard objectives add stat_crafting_table minecraft.custom:minecraft.interact_with_crafting_table
scoreboard objectives add b1quest_initial dummy
scoreboard objectives add b1quest_progress dummy


scoreboard objectives add b2quest_complete dummy
scoreboard objectives add stat_place_stone minecraft.used:minecraft.stone
scoreboard objectives add b2quest_initial dummy
scoreboard objectives add b2quest_progress dummy

scoreboard objectives add b3quest_complete dummy
scoreboard objectives add stat_mine_oak minecraft.mined:minecraft.oak_log
scoreboard objectives add b3quest_initial dummy
scoreboard objectives add b3quest_progress dummy

scoreboard objectives add e1quest_complete dummy
scoreboard objectives add stat_craft_map minecraft.crafted:minecraft.map
scoreboard objectives add e1quest_initial dummy
scoreboard objectives add e1quest_progress dummy


scoreboard objectives add e2quest_complete dummy
scoreboard objectives add stat_pig_travel minecraft.custom:minecraft.pig_one_cm
scoreboard objectives add e2quest_initial dummy
scoreboard objectives add e2quest_progress dummy

scoreboard objectives add e3quest_complete dummy
scoreboard objectives add stat_boat_travel minecraft.custom:minecraft.boat_one_cm
scoreboard objectives add e3quest_initial dummy
scoreboard objectives add e3quest_progress dummy


scoreboard objectives add f1quest_complete dummy
scoreboard objectives add stat_kill_zombie minecraft.killed:minecraft.zombie
scoreboard objectives add f1quest_initial dummy
scoreboard objectives add f1quest_progress dummy

scoreboard objectives add f2quest_complete dummy
scoreboard objectives add stat_kill_creeper minecraft.killed:minecraft.creeper
scoreboard objectives add f2quest_initial dummy
scoreboard objectives add f2quest_progress dummy

scoreboard objectives add f3quest_complete dummy
scoreboard objectives add stat_kill_enderman minecraft.killed:minecraft.enderman
scoreboard objectives add f3quest_initial dummy
scoreboard objectives add f3quest_progress dummy


#used for either random(0) or dynamic(1)
scoreboard players set @p quest_mode -1
#tellraw @p {"score": {"name": "@p", "objective": "quest_mode"}}

#used for tracking the roles so it can determine which role the player is most suited for
scoreboard players set @p builder_score 0
scoreboard players set @p explorer_score 0
scoreboard players set @p fighter_score 0

scoreboard players set @p explorer_scale 1000
scoreboard players set @p fighter_scale 10


#used for tracking quest progress MIGHT NOT NEED
#scoreboard players set @p bq1_progression 0

#used for tracking if a quest is accepted, has quest = 1, doesn't have quest = 0
scoreboard players set @p has_quest 0
scoreboard players set @p selected_quest -1

#used for individual quest tracking
scoreboard players set @p b1quest_complete 0
scoreboard players set @p b2quest_complete 0
scoreboard players set @p b3quest_complete 0
scoreboard players set @p e1quest_complete 0
scoreboard players set @p e2quest_complete 0
scoreboard players set @p e3quest_complete 0
scoreboard players set @p f1quest_complete 0
scoreboard players set @p f2quest_complete 0
scoreboard players set @p f3quest_complete 0

scoreboard players set @p b1quest_progress 0
scoreboard players set @p b2quest_progress 0
scoreboard players set @p b3quest_progress 0
scoreboard players set @p e1quest_progress 0
scoreboard players set @p e2quest_progress 0
scoreboard players set @p e3quest_progress 0
scoreboard players set @p f1quest_progress 0
scoreboard players set @p f2quest_progress 0
scoreboard players set @p f3quest_progress 0

function role_quests:mode_menu