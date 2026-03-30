tellraw @p {"text":"Quest Complete", "color": "green"}

give @p iron_axe

scoreboard players set @p b2quest_complete 1
scoreboard players set @p selected_quest -1
scoreboard players set @p has_quest 0