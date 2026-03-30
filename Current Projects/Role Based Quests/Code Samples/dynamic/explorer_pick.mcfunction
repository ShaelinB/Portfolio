tellraw @p {"text":"DEBUG: In explorer pick function", "color": "blue"}

# Pick random explorer quest
execute store result score @s quest_roll run random value 1..3

execute as @p if score @p quest_roll matches 1 run function role_quests:explorer/quest1
execute as @p if score @p quest_roll matches 2 run function role_quests:explorer/quest2
execute as @p if score @p quest_roll matches 3 run function role_quests:explorer/quest3
