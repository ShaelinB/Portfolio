tellraw @p {"text":"Quest Complete", "color": "green"}

give @p arrow 64

scoreboard players set @p f2quest_complete 1
scoreboard players set @p selected_quest -1
scoreboard players set @p has_quest 0