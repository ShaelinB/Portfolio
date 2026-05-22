tellraw @p {"text":"Quest Complete", "color": "green"}

give @p saddle

scoreboard players set @p e3quest_complete 1
scoreboard players set @p selected_quest -1
scoreboard players set @p has_quest 0