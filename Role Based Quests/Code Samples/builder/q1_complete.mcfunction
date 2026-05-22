tellraw @p {"text":"Quest Complete", "color": "green"}

give @p oak_planks 64

scoreboard players set @p b1quest_complete 1
scoreboard players set @p selected_quest -1
scoreboard players set @p has_quest 0