using Microsoft.Xna.Framework.Graphics;
﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Content;
using static System.Net.WebRequestMethods;

namespace JohnnyRocket
{
    internal class LevelManager 
    {
        //fields------------------

        private List<Level> levels;
        private List<string> levelNames;
        private List<List<GameObject>> gameObjects;
        private List<List<GeneralTile>> tiles;
        private List<Texture2D> textureList;
        private ContentManager content;

        private Point recentExit;

        //constant enemies--------
        //NOTE: these require textures be passed in as variables,
        //and positions must be overridden by the load method

        private static Texture2D enemyTexture;
        private static Texture2D bulletTexture;

        

        //properties-------------

        /// <summary>
        /// gets the list of levels
        /// </summary>
        public List<Level> Levels { get => levels; }

        public Player Player
        {
            get
            {
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    for (int j = 0; j < gameObjects[i].Count; j++)
                    {
                        if (gameObjects[i][j] is Player)
                        {
                            return (Player)gameObjects[i][j];
                        }
                    }
                }
                return null;
            }
        }

        //constructor-------------

        public LevelManager(ContentManager content)
        {
            this.content = content;
            LoadContent();

            levels = new List<Level>();
            levelNames = new List<string>();
            gameObjects = new List<List<GameObject>>();
            tiles = new List<List<GeneralTile>>();
        }

        //methods-----------------

        /// <summary>
        /// adds level to level list
        /// </summary>
        /// <param name="level">the level getting added</param>
        public void Add(Level level)
        {
            levels.Add(level);
        }

        public void Save(string filename)
        {

        }


        /// <summary>
        /// loads the background tiles
        /// </summary>
        /// <param name="filename">location of file</param>
        /// <returns>list of tiles</returns>
        public string[,] LoadBackground(string filename) 
        {
            StreamReader input = null;
            string[,] data = new string[0,0];
            int lineCount = 0;

            try
            {
                input = new StreamReader("Content/Levels/" + filename + ".bg");

                // assign width and height
                data = new string[int.Parse(input.ReadLine()), int.Parse(input.ReadLine())];

                string line = null;

                for (int y = 0; y < data.GetLength(1); y++)
                {
                    line = input.ReadLine();
                    for (int x = 0; x < data.GetLength(0); x++)
                    {
                        data[x, y] = line.Substring(x * 2, 2);
                    }
                }
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("File doesn't exist");
            }
            finally
            {
                if (input != null)
                {
                    input.Close();
                }
            }
            return data;

        }

        /// <summary>
        /// laods the game objects
        /// </summary>
        /// <param name="filename">location of file</param>
        /// <returns>list of game objects</returns>
        public string[,] LoadGameObjects(string filename)
        {
            StreamReader input = null;
            string[,] data = new string[0, 0];
            int lineCount = 0;

            try
            {
                input = new StreamReader("Content/Levels/" + filename + ".et");

                // assign width and height
                data = new string[int.Parse(input.ReadLine()), int.Parse(input.ReadLine())];

                string line = null;

                for (int y = 0; y < data.GetLength(1); y++)
                {
                    line = input.ReadLine();
                    for (int x = 0; x < data.GetLength(0); x++)
                    {
                        data[x, y] = line.Substring(x * 2, 2);
                    }
                }
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("File doesn't exist");
            }
            finally
            {
                if (input != null)
                {
                    input.Close();
                }
            }
            return data;

        }

        public List<GameObject> MakeGameObjects(string filename)
        {
            //list of all string codes for tx2D
            string[,] data = LoadGameObjects(filename);
            //determines width and height of array
            int width = data.GetLength(0);
            int height = data.GetLength(1);

            //this list will be returned 
            //contains all entity objects and their pos in level
            List<GameObject> entities = new List<GameObject>();
            Texture2D[] playerTextures = new Texture2D[4];
            playerTextures = new Texture2D[] {  textureList[19], textureList[10], textureList[20], textureList[21] };
            Player player = new Player(5, 30, new Vector2(50, 50), playerTextures, textureList[11],
            textureList[17], 5, 0.5, 1, 1);

            Texture2D[] enemyTextures = new Texture2D[4];
            enemyTextures = new Texture2D[] {  textureList[22], textureList[12], textureList[23], textureList[24] };

            //switch statement to determine which object belongs to which textcode
            for (int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    switch (data[x, y])
                    {
                        //this is null 
                        case "..":
                            break;
                        //player object
                        case "Pd":
                            player.X = x * 64;
                            player.Y = y * 64;
                            entities.Add(player);
                            break;
                        //htank enemy
                        case "Ht":
                            entities.Add(new Enemy(new Vector2(x * 64, y * 64), enemyTextures, textureList[11], 3, 1, 1, new Vector2(1, 0), 2));
                            break;
                        //vtank enemy
                        case "Vt":
                            entities.Add(new Enemy(new Vector2(x * 64, y * 64), enemyTextures, textureList[11], 3, 1, 1, new Vector2(0, -1), 2));
                            break;
                        //turret enemy
                        case "Tt":
                            entities.Add(new Enemy(new Vector2(x * 64, y * 64), enemyTextures, textureList[11], 3, 1, 1));
                            break;

                    }
                }
            }

            //array w/ all entities 
            return entities;
        }

        /// <summary>
        /// loads in all  tiles
        /// </summary>
        protected void LoadContent()
        {
            textureList = new List<Texture2D>
            {
                content.Load<Texture2D>("Tiles/Void"), //0
                content.Load<Texture2D>("Tiles/tile_center"), //1
                content.Load<Texture2D>("Tiles/tile_side_wall_01"), //2
                content.Load<Texture2D>("Tiles/tile_corner_wall_04"), //3
                content.Load<Texture2D>("Tiles/tile_corner_wall_01"), //4
                content.Load<Texture2D>("Tiles/tile_side_wall_02"), //5
                content.Load<Texture2D>("Tiles/tile_side_wall_04"), //6
                content.Load<Texture2D>("Tiles/tile_side_wall_03"), //7
                content.Load<Texture2D>("Tiles/tile_corner_wall_02"), //8
                content.Load<Texture2D>("Tiles/tile_corner_wall_03"), //9
                content.Load<Texture2D>("bonnie_rocket"), //10
                content.Load<Texture2D>("placeholder-bullet"), //11
                content.Load<Texture2D>("evil_robot"), //12
                content.Load<Texture2D>("Tiles/tile_inside_corner_01"), //13
                content.Load<Texture2D>("Tiles/tile_inside_corner_02"), //14
                content.Load<Texture2D>("Tiles/tile_inside_corner_03"), //15
                content.Load<Texture2D>("Tiles/tile_inside_corner_04"), //16
                content.Load<Texture2D>("slash"), //17
                content.Load<Texture2D>("Tiles/exit_up"), //18
                content.Load<Texture2D>("bonnie_rocket_up"), //19
                content.Load<Texture2D>("bonnie_rocket_left"), //20
                content.Load<Texture2D>("bonnie_rocket_right"), //21
                content.Load<Texture2D>("evil_robot_up"), //22
                content.Load<Texture2D>("evil_robot_left"), //23
                content.Load<Texture2D>("evil_robot_right"), //24
            };
        }

        /// <summary>
        /// draws background of current level
        /// </summary>
        /// <param name="filename">file to load from</param>
        public GeneralTile[,] MakeBackground(string filename)
        {
            string[,] data = LoadBackground(filename);
            int width = data.GetLength(0);
            int height = data.GetLength(1);


            Texture2D tileTexture;

            GeneralTile[,] background = new GeneralTile[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    switch (data[x, y])
                    {
                        //assigns each spot to the corresponding string code
                        //black tile
                        case (".."):


                            tileTexture = textureList[0];
                            background[x, y] = new Floor(
                                new Rectangle(x * 64, y * 64, 64, 64),
                                tileTexture);
                            break;

                        //floor tile
                        case ("Fl"):
                            tileTexture = textureList[1];
                            background[x, y] = new Floor(
                                new Rectangle(x * 64, y * 64, 64, 64),
                                tileTexture);
                            break;
                        //wall 1
                        case ("W1"):
                            tileTexture = textureList[2];
                            background[x, y] = new Wall(
                                new Rectangle(x * 64, y * 64, 64, 64),
                                tileTexture);
                            break;
                        //wall 2
                        case ("W2"):
                            tileTexture = textureList[3];
                            background[x, y] = new Wall(
                                new Rectangle(x * 64, y * 64, 64, 64),
                                tileTexture);
                            break;
                        //wall 3
                        case ("W3"):
                            tileTexture = textureList[4];
                            background[x, y] = new Wall(
                                new Rectangle(x * 64, y * 64, 64, 64),
                                tileTexture);
                            break;
                        //wall 4
                        case ("W4"):
                            tileTexture = textureList[5];
                            background[x, y] = new Wall(
                                new Rectangle(x * 64, y * 64, 64, 64),
                                tileTexture);
                            break;
                        //wall 5
                        case ("W5"):
                            tileTexture = textureList[6];
                            background[x, y] = new Wall(
                                new Rectangle(x * 64, y * 64, 64, 64),
                                tileTexture);
                            break;
                        //wall 6
                        case ("W6"):
                            tileTexture = textureList[7];
                            background[x, y] = new Wall(
                                new Rectangle(x * 64, y * 64, 64, 64),
                                tileTexture);
                            break;
                        //wall 7
                        case ("W7"):
                            tileTexture = textureList[8];
                            background[x, y] = new Wall(
                                new Rectangle(x * 64, y * 64, 64, 64),
                                tileTexture);
                            break;
                        //wall 8
                        case ("W8"):
                            tileTexture = textureList[9];
                            background[x, y] = new Wall(
                                new Rectangle(x * 64, y * 64, 64, 64),
                                tileTexture);
                            break;
                        case ("C1"):
                            tileTexture = textureList[13];
                            background[x, y] = new Wall(
                                new Rectangle(x * 64, y * 64, 64, 64),
                                tileTexture);
                            break;
                        case ("C2"):
                            tileTexture = textureList[14];
                            background[x, y] = new Wall(
                                new Rectangle(x * 64, y * 64, 64, 64),
                                tileTexture);
                            break;
                        case ("C3"):
                            tileTexture = textureList[15];
                            background[x, y] = new Wall(
                                new Rectangle(x * 64, y * 64, 64, 64),
                                tileTexture);
                            break;
                        case ("C4"):
                            tileTexture = textureList[16];
                            background[x, y] = new Wall(
                                new Rectangle(x * 64, y * 64, 64, 64),
                                tileTexture);
                            break;
                        case ("Ex"):
                            tileTexture = textureList[18];
                            background[x, y] = new Floor(
                                new Rectangle(x * 64, y * 64, 64, 64),
                                tileTexture);
                            recentExit = new Point(x, y);
                            break;
                    }
                }
            }

            return background;

           
            
        }
        public void MakeLevel(string filename)        
        {
            recentExit = new Point(0, 0);

            Level num = new Level(new Vector2(384 - 32, 288 - 32),
                MakeBackground(filename), MakeGameObjects(filename), recentExit);
            levels.Add(num);
            MakeGameObjectList(MakeGameObjects(filename));
            MakeTilesObjectList(MakeBackground(filename));

            levelNames.Add(filename);
        }

        /// <summary>
        /// Rewrites data to an existing level
        /// </summary>
        /// <param name="filename"></param>
        public void ReloadLevel(int index)
        {
            recentExit = new Point(0, 0);
            Level one = new Level(new Vector2(384 - 32, 288 - 32),
                MakeBackground(levelNames[index]), MakeGameObjects(levelNames[index]), recentExit);
            levels[index] = one;
            MakeGameObjectList(MakeGameObjects(levelNames[index]));
            MakeTilesObjectList(MakeBackground(levelNames[index]));
        }

        public void MakeGameObjectList(List<GameObject> listOfGameObjects)
        {
            List<GameObject> levelObj = new List<GameObject>();
            foreach (GameObject obj in listOfGameObjects)
            {
                if(obj != null)
                {
                    levelObj.Add(obj);
                }
            }
            gameObjects.Add(levelObj);
        }

        public List<GameObject> GetLevelGameObjects(int levelNum)
        {
            return gameObjects[levelNum];
        }

        public void MakeTilesObjectList(GeneralTile[,] listOfTiles)
        {
            List<GeneralTile> levelObj = new List<GeneralTile>();
            foreach (GeneralTile obj in listOfTiles)
            {
                levelObj.Add(obj);
            }
            tiles.Add(levelObj);
        }

        public List<GeneralTile> GetLevelTiles(int levelNum)
        {
            return tiles[levelNum];
        }


    }
}
