#tellraw @p {"text":"DEBUG: In baseline pick function", "color": "blue"}

# baseline/pick.mcfunction NEED TO MAKE MORE QUESTS AND MAKE IT RUN ONCE
scoreboard players set @p has_quest 1

execute store result score @p quest_roll run random value 1..9

execute if score @p quest_roll matches 1 run function role_quests:builder/quest1
execute if score @p quest_roll matches 2 run function role_quests:builder/quest2
execute if score @p quest_roll matches 3 run function role_quests:builder/quest3
execute if score @p quest_roll matches 4 run function role_quests:explorer/quest1
execute if score @p quest_roll matches 5 run function role_quests:explorer/quest2
execute if score @p quest_roll matches 6 run function role_quests:explorer/quest3
execute if score @p quest_roll matches 7 run function role_quests:fighter/quest1
execute if score @p quest_roll matches 8 run function role_quests:fighter/quest2
execute if score @p quest_roll matches 9 run function role_quests:fighter/quest3