tellraw @p {"text":"DEBUG: In mode menu function", "color": "blue"}

#Has player select their mode then setup is complete
tellraw @p {"text":"Pick a Quest Mode", "color": "white", "bold": true}
tellraw @p [{"text":"[Random]","color":"yellow","bold":true,click_event:{"action":"run_command",command:"/scoreboard players set @p quest_mode 0"}},{"text":"  "},{"text":"[Dynamic]","color":"green","bold":true,click_event:{"action":"run_command",command:"/scoreboard players set @p quest_mode 1"}}]