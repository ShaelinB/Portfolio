tellraw @p {"text":"Quest Complete", "color": "green"}

give @p diamond_sword

scoreboard players set @p f3quest_complete 1
scoreboard players set @p selected_quest -1
scoreboard players set @p has_quest 0