using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ShapeUtils;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Threading;

namespace JohnnyRocket
{
    public enum GameStates
    {
        MainMenu,
        LevelSelect,
        Game,
        Pause,
        GameOver,
        Win
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //FSM fields
        private GameStates currentState;
        private KeyboardState kb;
        private KeyboardState prevKb;
        private bool debugState;

        //mouse
        private MouseState prevMouseState;

        //manages overlapping buttons
        private List<Button> buttons;

        //fonts
        private SpriteFont debugText;
        private SpriteFont hudText;

        //HUD
        private bool promptVisible;

        //screen textures
        private List<Texture2D> screenTextures;

        //buttons
        private Button playButton;
        private Texture2D playButtonTexture;

        private Button levelSelectButton;
        private Texture2D levelSelectButtonTexture;

        private Button exitButton;
        private Texture2D exitButtonTexture;

        private Button resumeButton;
        private Texture2D resumeButtonTexture;

        private Button quitToTitleButton;
        private Button quitToTitleButton2;
        private Texture2D quitToTitleButtonTexture;

        private Button retryButton;
        private Texture2D retryButtonTexture;

        private Button backButton;
        private Texture2D backButtonTexture;

        private Button level1Button;
        private Texture2D level1ButtonTexture;

        private Button level2Button;
        private Texture2D level2ButtonTexture;

        private Button level3Button;
        private Texture2D level3ButtonTexture;

        private Button level4Button;
        private Texture2D level4ButtonTexture;

        //player
        private Player player;

        private Texture2D missileFingersTexture;
        private Texture2D knifeArmTexture;
        private Texture2D missileFriendlyTexture;
        private int maxHealth;

        private int levelNum;
        private List<GameObject> gameObjectsForLevel;
        private List<GeneralTile> tilesForLevel;
        private LevelManager manager;
        private Level level;

 
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 64*12;
            _graphics.PreferredBackBufferHeight = 64*9;
            _graphics.ApplyChanges();
            promptVisible = false;
            screenTextures = new List<Texture2D>();
            currentState = GameStates.MainMenu;
            levelNum = -1;
            gameObjectsForLevel = new List<GameObject>();
            tilesForLevel = new List<GeneralTile>();
            manager = new LevelManager(Content);
            
            
            debugState = false;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            //text---------------------------------------------------------
            debugText = Content.Load<SpriteFont>("DebugText");
            hudText = Content.Load<SpriteFont>("Consolas-24");

            //screens------------------------------------------------------
            screenTextures.Add(Content.Load<Texture2D>("blank_screen"));
            screenTextures.Add(Content.Load<Texture2D>("bonnie_title_screen"));
            screenTextures.Add(Content.Load<Texture2D>("johnny_rocket_pause"));
            screenTextures.Add(Content.Load<Texture2D>("gameover_screen"));
            screenTextures.Add(Content.Load<Texture2D>("win_screen"));

            //buttons------------------------------------------------------
            playButtonTexture = Content.Load<Texture2D>("button_play");
            playButton = new Button(
                new Rectangle(
                    75,
                    300,
                    playButtonTexture.Width/2,
                    playButtonTexture.Height/2)
                ,playButtonTexture,"PLAY!");

            levelSelectButtonTexture = Content.Load<Texture2D>("button_levelselect");
            levelSelectButton = new Button(
                new Rectangle(
                    75,
                    390,
                    levelSelectButtonTexture.Width/2,
                    levelSelectButtonTexture.Height/2)
                , levelSelectButtonTexture, "LEVEL SELECT");

            exitButtonTexture = Content.Load<Texture2D>("button_exit");
            exitButton = new Button(
                new Rectangle(
                    75,
                    480,
                    exitButtonTexture.Width/2,
                    exitButtonTexture.Height / 2)
                , exitButtonTexture, "EXIT");

            resumeButtonTexture = Content.Load<Texture2D>("button_resume");
            resumeButton = new Button(
                new Rectangle(
                    160,
                    420,
                    resumeButtonTexture.Width / 2,
                    resumeButtonTexture.Height / 2)
                , resumeButtonTexture, "RESUME");

            quitToTitleButtonTexture = Content.Load<Texture2D>("button_quit");
            quitToTitleButton = new Button(
                new Rectangle(
                    460,
                    420,
                    quitToTitleButtonTexture.Width / 2,
                    quitToTitleButtonTexture.Height / 2)
                , quitToTitleButtonTexture, "QUIT TO TITLE");
            quitToTitleButton2 = new Button(
                new Rectangle(
                    155,
                    340,
                    quitToTitleButtonTexture.Width / 2,
                    quitToTitleButtonTexture.Height / 2)
                , quitToTitleButtonTexture, "QUIT TO TITLE");

            retryButtonTexture = Content.Load<Texture2D>("button_retry");
            retryButton = new Button(
                new Rectangle(
                    160,
                    420,
                    retryButtonTexture.Width / 2,
                    retryButtonTexture.Height / 2)
                , retryButtonTexture, "RETRY");

            backButtonTexture = Content.Load<Texture2D>("button_back");
            backButton = new Button(
                new Rectangle(
                    75,
                    345,
                    backButtonTexture.Width / 2,
                    backButtonTexture.Height / 2)
                , backButtonTexture, "BACK");

            level1ButtonTexture = Content.Load<Texture2D>("button_level_1");
            level1Button = new Button(
                new Rectangle(
                    70,
                    150,
                    level1ButtonTexture.Width / 2,
                    level1ButtonTexture.Height / 2)
                , level1ButtonTexture, "1");

            level2ButtonTexture = Content.Load<Texture2D>("button_level_2");
            level2Button = new Button(
                new Rectangle(
                    170,
                    150,
                    level2ButtonTexture.Width / 2,
                    level2ButtonTexture.Height / 2)
                , level2ButtonTexture, "2");

            level3ButtonTexture = Content.Load<Texture2D>("button_level_3");
            level3Button = new Button(
                new Rectangle(
                    270,
                    150,
                    level3ButtonTexture.Width / 2,
                    level3ButtonTexture.Height / 2)
                , level3ButtonTexture, "3");

            level4ButtonTexture = Content.Load<Texture2D>("button_level_4");
            level4Button = new Button(
                new Rectangle(
                    170,
                    240,
                    level4ButtonTexture.Width / 2,
                    level4ButtonTexture.Height / 2)
                , level4ButtonTexture, "4");

            buttons = new List<Button>
            {
                playButton,
                levelSelectButton,
                exitButton,
                resumeButton,
                quitToTitleButton,
                quitToTitleButton2,
                retryButton,
                backButton,
                level1Button,
                level2Button,
                level3Button
            };

            // load levels
            manager.MakeLevel("level1");
            manager.MakeLevel("level2");
            manager.MakeLevel("level-1");
            manager.MakeLevel("maze");

            /* stretch goal ammo pick up
            ammoPickUpTexture = Content.Load<Texture2D>("ammoPickUp");
            ammoPickUpList = new List<GameObject>
            {
                new AmmoPickUp(
                    new Vector2(50,50),
                    ammoPickUpTexture),
                new AmmoPickUp(
                    new Vector2(100,75),
                    ammoPickUpTexture),
                new AmmoPickUp(
                    new Vector2(30,300),
                    ammoPickUpTexture),
            };*/

            

            //Texture2D placeholderBullet = Content.Load<Texture2D>("placeholder-bullet");

            // Load player
            /*playerTexture = Content.Load<Texture2D>("placeholder-player");
            player = new Player(5, 30, new Vector2(50, 50), playerTexture, placeholderBullet,
            10, 0.5, 1, 15); */

            /*// Load testing level
            Vector2 screenPos = new Vector2((_graphics.PreferredBackBufferWidth - playerTexture.Width) / 2,
                (_graphics.PreferredBackBufferHeight - playerTexture.Height) / 2);
            testLevel = new Level(screenPos, new List<GeneralTile>(), new List<GameObject>());

            // Load testing tiles
            testLevel.AddTile(new Wall(new Rectangle(20, 92, 32, 32),
                Content.Load<Texture2D>("placeholder-tile")));
            testLevel.AddTile(new Wall(new Rectangle(20, 124, 32, 32),
                Content.Load<Texture2D>("placeholder-tile")));
            testLevel.AddTile(new Wall(new Rectangle(20, 156, 32, 32), 
                Content.Load<Texture2D>("placeholder-tile")));
            maxHealth = player.Health;
            testLevel.AddObject(player);*/
            missileFingersTexture = Content.Load<Texture2D>("icon_missile");
            knifeArmTexture = Content.Load<Texture2D>("icon_knife");
            missileFriendlyTexture = Content.Load<Texture2D>("friendly_missile");
            /*
            // Load testing tiles
            testLevel.AddTile(new Wall(new Rectangle(400, 60, 32, 32),
                Content.Load<Texture2D>("placeholder-tile")));
            testLevel.AddTile(new Wall(new Rectangle(400, 92, 32, 32),
                Content.Load<Texture2D>("placeholder-tile")));
            testLevel.AddTile(new Wall(new Rectangle(400, 124, 32, 32),
                Content.Load<Texture2D>("placeholder-tile")));

            Texture2D enemyTexture = Content.Load<Texture2D>("robot-placeholder");
            // Load testing enemies
            testLevel.AddObject(new Enemy(new Vector2(200, 200), enemyTexture,
                placeholderBullet, 5, 1, 1, new Vector2(2,0), 2.5));
            testLevel.AddObject(new Enemy(new Vector2(200, 500), enemyTexture,
                placeholderBullet, 5, 1, 1, new Vector2(0, 2), 1));
            testLevel.AddObject(new Enemy(new Vector2(300, 300), enemyTexture,
                placeholderBullet, 1, 3, 1));
            testLevel.AddObject(new Enemy(new Vector2(200, 100), enemyTexture,
                placeholderBullet, 5, 5, 1));*/
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            kb = Keyboard.GetState();

            switch(currentState)
            {
                case GameStates.MainMenu:
                    if (playButton.Click())
                    {
                        if (SingleMouseClick())
                        {
                            levelNum = 0;
                            level = manager.Levels[levelNum];
                            gameObjectsForLevel = manager.GetLevelGameObjects(levelNum);
                            tilesForLevel = manager.GetLevelTiles(levelNum);
                            player = level.PlayerStart;
                            maxHealth = player.Health;
                            currentState = GameStates.Game;
                            promptVisible = true;
                        }
                    }
                    else if (levelSelectButton.Click())
                    {
                        if (SingleMouseClick())
                        {
                            currentState = GameStates.LevelSelect;
                        }
                    }
                    else if (exitButton.Click())
                    {
                        if (ButtonManager(exitButton))
                        {
                            Exit();
                        }

                    }
                    break;

                case GameStates.LevelSelect:
                    if (backButton.Click())
                    {
                        if (SingleMouseClick())
                        {
                            currentState = GameStates.MainMenu;
                        }
                    }
                    if (level1Button.Click())
                    {
                        if (SingleMouseClick())
                        {
                            levelNum = 0;
                            level = manager.Levels[levelNum];
                            gameObjectsForLevel = manager.GetLevelGameObjects(levelNum);
                            tilesForLevel = manager.GetLevelTiles(levelNum);
                            player = level.PlayerStart;
                            maxHealth = player.Health;
                            currentState = GameStates.Game;
                            promptVisible = true;
                        }
                    }
                    if (level2Button.Click())
                    {
                        if (SingleMouseClick())
                        {
                            levelNum = 1;
                            level = manager.Levels[levelNum];
                            gameObjectsForLevel = manager.GetLevelGameObjects(levelNum);
                            tilesForLevel = manager.GetLevelTiles(levelNum);
                            player = level.PlayerStart;
                            maxHealth = player.Health;
                            currentState = GameStates.Game;
                            promptVisible = false;

                        }
                    }
                    if (level3Button.Click())
                    {
                        if (SingleMouseClick())
                        {
                            levelNum = 2;
                            level = manager.Levels[levelNum];
                            gameObjectsForLevel = manager.GetLevelGameObjects(levelNum);
                            tilesForLevel = manager.GetLevelTiles(levelNum);
                            player = level.PlayerStart;
                            maxHealth = player.Health;
                            currentState = GameStates.Game;
                            promptVisible = false;

                        }
                    }
                    if (level4Button.Click())
                    {
                        if (SingleMouseClick())
                        {
                            levelNum = 3;
                            level = manager.Levels[levelNum];
                            gameObjectsForLevel = manager.GetLevelGameObjects(levelNum);
                            tilesForLevel = manager.GetLevelTiles(levelNum);
                            player = level.PlayerStart;
                            maxHealth = player.Health;
                            currentState = GameStates.Game;
                            promptVisible = false;

                        }
                    }


                    break;

                case GameStates.Game:
                    
                    // Transitions
                    if (kb.IsKeyDown(Keys.Escape) && prevKb.IsKeyUp(Keys.Escape))
                    {
                        currentState = GameStates.Pause;
                        break;
                    }
                    if (kb.IsKeyDown(Keys.F1) && prevKb.IsKeyUp(Keys.F1) && debugState == false)
                    {
                        debugState = true;
                        break;
                    }
                    if (kb.IsKeyDown(Keys.F1) && prevKb.IsKeyUp(Keys.F1) && debugState == true)
                    {
                        debugState = false;
                        break;
                    }

                    // cam fryer
                    // handle exit logic
                    if (level.CheckExit(player))
                    {
                        // win
                        if(levelNum == manager.Levels.Count - 1)
                        {
                            currentState = GameStates.Win;
                            levelNum = 0;
                            level = manager.Levels[levelNum];
                            player = level.PlayerStart;
                            player.NumBullets = 3;
                            break;
                        }
                        // move on to next level
                        if(levelNum < manager.Levels.Count - 1)
                        {
                            levelNum++;
                            level = manager.Levels[levelNum];
                            player = level.PlayerStart;
                            player.NumBullets = 3;
                        }
                    }
                    if (player.CurrentPlayerState == PlayerState.Dead)
                    {
                        currentState = GameStates.GameOver;
                        manager.ReloadLevel(levelNum);
                        level = manager.Levels[levelNum];
                        player = level.PlayerStart;

                        break;
                    }
                    else if (player.CurrentPlayerState == PlayerState.Knifearm || player.CurrentPlayerState == PlayerState.Missilefingers)
                    {
                        // lock controls if prompt is visible
                        if (promptVisible)
                        {
                            // disappear prompt
                            if (kb.IsKeyDown(Keys.Space))
                            {
                                promptVisible = false;
                            }
                        }
                        else
                        {
                            level.UpdateAll(player, gameTime);
                            level.UpdateAttacks(player);
                        }                        
                        break;
                    }
                    break;

                case GameStates.Pause:
                    if (resumeButton.Click())
                    {
                        if (SingleMouseClick())
                        {
                            currentState = GameStates.Game;
                        }
                    }
                    if (quitToTitleButton.Click())
                    {
                        if (SingleMouseClick())
                        {
                            currentState = GameStates.MainMenu;
                            manager.ReloadLevel(levelNum);
                            level = manager.Levels[levelNum];
                            player = level.PlayerStart;
                        }
                    }
                    break;

                case GameStates.GameOver:
                    

                    if (retryButton.Click())
                    {
                        if (SingleMouseClick())
                        {

                            currentState = GameStates.Game;
                            manager.ReloadLevel(levelNum);
                            level = manager.Levels[levelNum];
                            player = level.PlayerStart;
                        }

                    }
                    if (quitToTitleButton.Click())
                    {
                        if (SingleMouseClick())
                        {
                            currentState = GameStates.MainMenu;

                            manager.ReloadLevel(levelNum);
                            level = manager.Levels[levelNum];
                            player = level.PlayerStart;
                        }
                    }
                    break;

                case GameStates.Win:
                    if (quitToTitleButton2.Click())
                    {
                        if (SingleMouseClick())
                        {
                            currentState = GameStates.MainMenu;

                            for(int i = 0; i<manager.Levels.Count; i++)
                            {

                                manager.ReloadLevel(i);
                            }

                            level = manager.Levels[levelNum];
                            player = level.PlayerStart;
                        }
                    }
                    break;
            }

            prevKb = kb;
            prevMouseState = Mouse.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Rectangle screenRectangle = new Rectangle(0, 0,
                    _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            MouseState mouseState = Mouse.GetState();

            _spriteBatch.Begin();

            
            switch (currentState)
            {
                case GameStates.MainMenu:
                    //main menu draw stuff
                    _spriteBatch.Draw(screenTextures[1], screenRectangle, Color.White);
                    DrawButton(playButton, mouseState, _spriteBatch);
                    DrawButton(levelSelectButton, mouseState, _spriteBatch);
                    DrawButton(exitButton, mouseState, _spriteBatch);
                    break;

                case GameStates.LevelSelect:
                    //level select draw stuff
                    _spriteBatch.Draw(screenTextures[0], screenRectangle, Color.White);
                    DrawButton(level1Button, mouseState, _spriteBatch);
                    DrawButton(level2Button, mouseState, _spriteBatch);
                    DrawButton(level3Button, mouseState, _spriteBatch);
                    DrawButton(level4Button, mouseState, _spriteBatch);
                    DrawButton(backButton, mouseState, _spriteBatch);
                    break;

                case GameStates.Game:
                    //game draw stuff                  
                    level.DrawAll(_spriteBatch);
                    player.Draw(_spriteBatch);
                    _spriteBatch.End();
                    ShapeBatch.Begin(GraphicsDevice);
                    ShapeBatch.Box(new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, 96), Color.Black);
                    ShapeBatch.End();
                    _spriteBatch.Begin();
                    //draws player state icon and name
                    if (player.CurrentPlayerState==PlayerState.Missilefingers)
                    {
                        _spriteBatch.Draw(
                            missileFingersTexture,
                            new Rectangle(
                                16, 16, 
                                64,
                                64),
                            Color.White);

                        _spriteBatch.DrawString(hudText, "[I] Bonnie Misslefingers", new Vector2(96,20), Color.White);
                        _spriteBatch.DrawString(hudText, "[P] Switch", new Vector2(616, 50), Color.White);
                    }
                    else
                    {
                        _spriteBatch.Draw(
                            knifeArmTexture,
                            new Rectangle(
                                16, 16,
                                64,
                                64),
                            Color.White);

                        _spriteBatch.DrawString(hudText, "[P] Bonnie Knifearm", new Vector2(96, 20), Color.White);
                        _spriteBatch.DrawString(hudText, "[I] Switch", new Vector2(616, 50), Color.White);
                    }
                    //draws pause prompt
                    _spriteBatch.DrawString(hudText, "[Esc] Pause", new Vector2(616, 20), Color.White);
                    //hud prompt for level 1
                    if (promptVisible)
                    {
                        _spriteBatch.End();
                        string line1 = "Bonnie has been turned into a cyborg!";
                        string line2 = "Escape Johnny Rocket's robot lab.";
                        string line3 = "Press [Space] to continue";
                        ShapeBatch.Begin(GraphicsDevice);
                        ShapeBatch.Box(
                            new Rectangle(((_graphics.PreferredBackBufferWidth - (line1.Length * 12)) / 2) - 10, 260, (line1.Length * 12) + 10, 110),
                            Color.Black);
                        ShapeBatch.End();
                        _spriteBatch.Begin();
                        _spriteBatch.DrawString(
                            hudText, line1, 
                            new Vector2((_graphics.PreferredBackBufferWidth - (line1.Length * 12))/2, 270), 
                            Color.White);
                        _spriteBatch.DrawString(
                            hudText, line2, 
                            new Vector2((_graphics.PreferredBackBufferWidth - (line2.Length * 12)) / 2, 300), 
                            Color.White);
                        _spriteBatch.DrawString(
                            hudText, line3,
                            new Vector2((_graphics.PreferredBackBufferWidth - (line3.Length * 12)) / 2, 330),
                            Color.White);
                    }
                    //draws amount of ammo
                    for (int i = 0; i < player.NumBullets; i++)
                    {
                        _spriteBatch.Draw(missileFriendlyTexture,
                            new Rectangle(300 + 32 * i,48,
                            32, 32),
                            Color.LightGray);
                    }
                    _spriteBatch.End();
                    //makes outline around player state icon and health bar
                    ShapeBatch.Begin(GraphicsDevice);
                    ShapeBatch.BoxOutline(16, 16, 64, 64, Color.White);                    
                    //health bar
                    if(((float)player.Health / (float)maxHealth) * 100 > 25)
                    {
                        ShapeBatch.Box(96, 48, ((float)player.Health / (float)maxHealth) * 200, 32, Color.Green);
                    }
                    else
                    {
                        ShapeBatch.Box(96, 48, ((float)player.Health / (float)maxHealth) * 200, 32, Color.Red);
                    }
                    ShapeBatch.BoxOutline(96,48,200,32,Color.White); 
                    ShapeBatch.End();
                    _spriteBatch.Begin();                    

                    //drawing debug stuff
                    if (debugState)
                    {
                        // Commented because it doesn't work
                        /*
                        _spriteBatch.End();
                        ShapeBatch.Begin(GraphicsDevice);
                        for (int i = 0; i < gameObjectsForLevel.Count; i++)
                        {
                            ShapeBatch.BoxOutline(gameObjectsForLevel[i].Rectangle, Color.Red);
                        }
                        for (int i = 0; i < tilesForLevel.Count; i++)
                        {
                            if (tilesForLevel[i] is Wall)
                            {
                                ShapeBatch.BoxOutline(tilesForLevel[i].Rectangle, Color.Blue);
                            }
                        }
                        
                        ShapeBatch.End();                        
                        _spriteBatch.Begin();
                        
                        for (int i = 0; i < level.gameObjects.Count; i++)
                        {
                            if (level.gameObjects[i] is Player)
                            {
                                _spriteBatch.DrawString(
                                    debugText,
                                    $"Health: {player.Health}\n" +
                                    $"Ammo Left: {player.NumBullets}\n" +
                                    $"Pos: ({player.X},{player.Y})",
                                    new Vector2(player.X + player.Width + 5, player.Y),
                                    Color.Black);
                            }
                            else
                            {
                                _spriteBatch.DrawString(
                                    debugText,
                                    $"Health: {((Entity)(level.gameObjects[i])).Health}\n" +
                                    $"Pos: ({level.gameObjects[i].X},{level.gameObjects[i].Y})",
                                    new Vector2(level.gameObjects[i].X + level.gameObjects[i].Width + 5, level.gameObjects[i].Y),
                                    Color.Black);
                            }
                        }*/

                    }
                    break;

                case GameStates.Pause:
                    //pause menu draw stuff
                    _spriteBatch.Draw(screenTextures[2], screenRectangle, Color.White);
                    DrawButton(resumeButton, mouseState, _spriteBatch);
                    DrawButton(quitToTitleButton, mouseState, _spriteBatch);
                    break;

                case GameStates.GameOver:
                    //game over screen draw stuff
                    _spriteBatch.Draw(screenTextures[3], screenRectangle, Color.White);
                    DrawButton(retryButton, mouseState, _spriteBatch);
                    DrawButton(quitToTitleButton, mouseState, _spriteBatch);
                    break;

                case GameStates.Win:
                    // put win screen here
                    _spriteBatch.Draw(screenTextures[4], screenRectangle, Color.White);
                    DrawButton(quitToTitleButton2, mouseState, _spriteBatch);
                    break;
            }

            // debug, shows worldPos, must set worldPos in level to public
            //_spriteBatch.DrawString(debugText, $"{testLevel.worldPos}", new Vector2(100, 100), Color.Green);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Checks if this was the first frame the left mouse button was clicked
        /// </summary>
        /// <returns>True if this was the first frame, false otehrwise</returns>
        private bool SingleMouseClick()
        {
            return ((Mouse.GetState().LeftButton == ButtonState.Pressed) 
                && (prevMouseState.LeftButton == ButtonState.Released));
        }
        
        /// <summary>
        /// Draws a button. Lightens the button and pops it out if mouse is 
        /// hovering over it
        /// </summary>
        /// <param name="button">Button to draw</param>
        /// <param name="ms">Mousestate to track mouse position</param>
        /// <param name="sb">Active spritebatch</param>
        private void DrawButton(Button button, MouseState ms, SpriteBatch sb)
        {
            if (button.ButtonRect.Contains(new Point(ms.X, ms.Y)))
            {
                Rectangle adjustedRect = button.ButtonRect;
                adjustedRect.X -= 6;
                adjustedRect.Y -= 4;
                sb.Draw(button.ButtonTexture, adjustedRect, Color.LightSalmon);
            }
            else
            {
                sb.Draw(button.ButtonTexture, button.ButtonRect, Color.White);
            }
        }
        /// <summary>
        /// returns a bool depending on if thats the only button being clicked
        /// </summary>
        /// <param name="buttonClicked">the button that is supposed to be clicked</param>
        /// <returns>true if thats the only button being clicked false otherwise</returns>
        private bool ButtonManager(Button buttonClicked)
        {
            int buttonsNotClicked = 0;
            int indexOfButtonClicked = buttons.IndexOf(buttonClicked);
            for (int i = 0; i < buttons.Count; i++)
            {
                if (i == indexOfButtonClicked)
                {
                    continue;
                }
                else
                {
                    if (!buttons[i].Click())
                    {
                        buttonsNotClicked++;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            if (buttonsNotClicked == buttons.Count - 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}