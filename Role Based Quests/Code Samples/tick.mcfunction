#tellraw @p {"text":"DEBUG: In tick function", "color": "blue"}

#runs every tick and gives a quest if the player doesn't have one
execute if score @p quest_mode matches 0 if score @p has_quest matches 0 run function role_quests:baseline/pick

#runs every tick and does a nested check if the player is in dynamic mode so it can both update role scores and give out a quest
execute if score @p quest_mode matches 1 run function role_quests:dynamic_nested_check

execute if score @p selected_quest matches 1 if score @p b1quest_complete matches 0 run function role_quests:builder/q1_track
execute if score @p selected_quest matches 2 if score @p b2quest_complete matches 0 run function role_quests:builder/q2_track
execute if score @p selected_quest matches 3 if score @p b3quest_complete matches 0 run function role_quests:builder/q3_track
execute if score @p selected_quest matches 4 if score @p e1quest_complete matches 0 run function role_quests:explorer/q1_track
execute if score @p selected_quest matches 5 if score @p e2quest_complete matches 0 run function role_quests:explorer/q2_track
execute if score @p selected_quest matches 6 if score @p e3quest_complete matches 0 run function role_quests:explorer/q3_track
execute if score @p selected_quest matches 7 if score @p f1quest_complete matches 0 run function role_quests:fighter/q1_track
execute if score @p selected_quest matches 8 if score @p f2quest_complete matches 0 run function role_quests:fighter/q2_track
execute if score @p selected_quest matches 9 if score @p f3quest_complete matches 0 run function role_quests:fighter/q3_track
