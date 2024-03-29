﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace Cooler_King
{
    //makes an enum used for making different game states
    enum GameState
    {
        Attract,
        Playing,
        GameOver,
        Win
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        GameState CurrentGameState;

        KeyboardState currKeyb, oldKeyb;

        Paddle paddle;
        Ball ball;

        SoundEffect wallHit;
        SoundEffect brickHit;

        bool gameOver;
        bool gameWin;

        public readonly static Random RNG = new Random();

        Rectangle screenSize;

        SpriteFont ScoreFont;
        float frustration;
        int score;

        int bricksWide = 10;
        int bricksHigh = 5;
        Brick[,] bricks;
        Texture2D brickImage;
        Texture2D brickImage2;
        Texture2D brickImage3;
        Texture2D brickImage4;
        

        public static int level = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            //sets the screen size to 730 by 600
            _graphics.PreferredBackBufferWidth = 730;
            _graphics.PreferredBackBufferHeight = 600;

        }

        protected override void Initialize()
        {
            //sets the first game state to the main menu
            CurrentGameState = GameState.Attract;

            //Sets screenSize var as the screen bounds
            screenSize = GraphicsDevice.Viewport.Bounds;

            //Sets both frustration and score to zero
            frustration = 0;
            score = 0;
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //loads the paddle texture and sets it position
            paddle = new Paddle(Content.Load <Texture2D>("paddle (2)"), new Vector2 (screenSize.Width/2 - 35 , 450));

            //loads the ball texture and sets its position
            ball = new Ball(Content.Load <Texture2D>("ball"),new Vector2(200, 200));

            //loads the standard brick texture
            brickImage = Content.Load<Texture2D>("Brick");
            //loads the reinforced brick texture
            brickImage2 = Content.Load<Texture2D>("BrickR");
            //loads the reinforced brick texture
            brickImage3 = Content.Load<Texture2D>("BrickG");
            //loads the reinforced brick texture
            brickImage4 = Content.Load<Texture2D>("Brick - Copy");

            //loads the score font
            ScoreFont = Content.Load<SpriteFont>("ScoreText");

            //loads the wall hit sound effect
            wallHit = Content.Load<SoundEffect>("wallHitb1");

            //loads the brick hit sound effect
            brickHit = Content.Load<SoundEffect>("correct");

            //Begins the start game function
            StartGame();
        }

        protected void StartGame()
        {
            //sets the brick var as each brick
            bricks = new Brick[bricksWide, bricksHigh];

            //loops through each brick in the brickHigh var
            for (int y = 0; y < bricksHigh; y++)
            {
                //makes each bricks tint white
                Color tint = Color.White;

                switch (y)
                {
                    //makes the first line of bricks blue
                    case 0:
                        tint = Color.Blue;
                        break;
                    //makes the second line of bricks green
                    case 1:
                        tint = Color.Green;
                        break;
                    //makes the third line of bricks red
                    case 2:
                        tint = Color.Red;
                        break;
                    //makes the forth line of bricks yellow
                    case 3:
                        tint = Color.Yellow;
                        break;
                    //makes the fifth line of bricks purple
                    case 4:
                        tint = Color.Purple;
                        break;
                }

                //loops through brick in the brickWide var
                for (int x = 0; x < bricksWide; x++)
                {
                    //sets randint toa random number between 0 to 3
                    int randint = RNG.Next(0, 4);

                    
                    if (randint == 0)
                    {
                        //sets each normal brick with the brick image, a collition rectangle and a color
                        bricks[x, y] = new Brick(brickImage, new Rectangle(x * brickImage.Width, y * brickImage.Height, brickImage.Width, brickImage.Height), tint, 1);
                    }
                    else if (randint == 1)
                    {
                        
                        //sets each strong brick with the brick image, a collition rectangle and a color
                        bricks[x, y] = new Brick(brickImage2, new Rectangle(x * brickImage2.Width, y * brickImage2.Height, brickImage2.Width, brickImage2.Height), tint, 2);
                        bricks[x, y].isStrong = true;
                    }
                    else if (randint == 2)
                    {
                        //sets each glass brick with the brick image, a collition rectangle and a color
                        bricks[x, y] = new Brick(brickImage3, new Rectangle(x * brickImage3.Width, y * brickImage3.Height, brickImage3.Width, brickImage3.Height), tint, 1);
                        bricks[x, y].isGlass = true;
                    }
                    else
                    {
                        //sets each bomb brick with the brick image, a collition rectangle and a color
                        bricks[x, y] = new Brick(brickImage4, new Rectangle(x * brickImage4.Width, y * brickImage4.Height, brickImage4.Width, brickImage4.Height), tint, 1);
                        bricks[x, y].isBomb = true;
                    }
                    

                    
                }
            }

           
        }

        public void ProceedLevel()
        {
            //halfs your frustration
            frustration = frustration / 2.0f;
            
            //loops through each brick in the bricksHigh var
            for (int y = 0; y < bricksHigh; y++)
            {
                //loops through each brick in the bricksWide var
                for (int x = 0; x < bricksWide; x++)
                {
                    switch (level)
                    {
                        //sets level two
                        case 1:

                            if (x % 2 == 0)
                            {
                                bricks[x, y].alive = false;
                            }
                            else
                            {
                                bricks[x, y].alive = true;

                                if (bricks[x, y].isStrong == true)
                                {
                                    bricks[x, y].hitpoints = 2;
                                }
                                else
                                {
                                    bricks[x, y].hitpoints = 1;
                                }
                            }

                            break;

                        //sets level three
                        case 2:
                            
                            if (x > 0 && y > 0 && x < bricksWide - 1 && y < bricksHigh - 1)
                            {
                                bricks[x, y].alive = true;
                                if (bricks[x, y].isStrong == true)
                                {
                                    bricks[x, y].hitpoints = 2;
                                }
                                else
                                {
                                    bricks[x, y].hitpoints = 1;
                                }
                                
                            }
                            else
                            {
                                bricks[x, y].alive = false;

                            }
                            break;
                    }
                }
            }
        }
        protected override void Update(GameTime gameTime)
        {
            //Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //sets the currKeyb var as the function keyboard.getstate
            currKeyb = Keyboard.GetState();

            //gives the conditions for if the state needs to me changed
            void AttractUpdate(KeyboardState cKB, KeyboardState oKB)
            {
                if (cKB.IsKeyDown(Keys.Space) && oKB.IsKeyUp(Keys.Space))
                {
                    //sets the alive bool as false
                    ball.alive = false;

                    //sets the current game state as playing
                    CurrentGameState = GameState.Playing;

                    //add frustration every second that the game goes on
                    frustration += (float)gameTime.ElapsedGameTime.TotalSeconds * (level+1);
                }
            }

            //if the frustration is greater than or equal to 200 then the gaveOver bool is true
            if (frustration >= 200)
            {
                gameOver = true;
            }

            if (score >= 99)
            {
                gameWin = true;
            }

            void PlayingUpdate(KeyboardState cKB, KeyboardState oKB)
            {
                //sets the alive bool as true
                ball.alive = true;

                //add frustration every second that the game does on
                frustration += (float)gameTime.ElapsedGameTime.TotalSeconds;

                //if game over is true or space is hit
                if (gameOver == true || cKB.IsKeyDown(Keys.Space) && oKB.IsKeyUp(Keys.Space))
                {
                    //change the current state to game over
                    CurrentGameState = GameState.GameOver;
                }

                if (gameWin == true || cKB.IsKeyDown(Keys.B) && oKB.IsKeyUp(Keys.B))
                {
                    //change the current state to game over
                    CurrentGameState = GameState.Win;
                }

            }

            //An update function that gets the keyboardstate and inputs it as the current and old keyboard var
            void GameOverUpdate(KeyboardState cKB, KeyboardState oKB)
            {
                //sets the alive bool as false
                ball.alive = false;

            }

            //An update function that gets the keyboardstate and inputs it as the current and old keyboard var
            void WinUpdate(KeyboardState cKB, KeyboardState oKB)
            {
                //sets the alive bool as false
                ball.alive = false;

            }

            //Check to see if the game states need to be updated
            switch (CurrentGameState)
            {
                case GameState.Attract:
                    AttractUpdate(currKeyb, oldKeyb);
                    break;
                case GameState.Playing:
                    PlayingUpdate(currKeyb, oldKeyb);
                    break;
                case GameState.GameOver:
                    GameOverUpdate(currKeyb, oldKeyb);
                    break;
                case GameState.Win:
                    WinUpdate(currKeyb, oldKeyb);
                    break;
            }

            //sets the oldKeyb var as the currkeyb var
            oldKeyb = currKeyb;

            //updates both the paddle and ball class and gives them screensizes
            paddle.UpdateMe(screenSize.Width);

            //checks if the ball if 'alive' and if it is then it lets the game update the ball class
            if(ball.alive)
                ball.UpdateMe(screenSize.Height, screenSize.Width, wallHit, brickHit);

            //adds a point of frustraiton when the ball hits the bottom of the screen
            if (ball.pos.Y >= 603 - ball.Rect.Height)
            {
                frustration++;
            }

            //if the ball hits the paddle
            if (ball.Rect.Intersects(paddle.Rect))
            {
                // get the overlap rectangle
                var overlap = Rectangle.Intersect(ball.Rect, paddle.Rect);
                

                // if the overlap rect width > height
                if (overlap.Width > overlap.Height)
                {
                  // flip the y velocity
                  ball.BounceY();

                }
                else
                {
                  // flip the x velocity
                  ball.BounceX();

                }
               
                   

            }

            //debug, kills bricks when k is clicked
            bool killbrick = false;

            if (currKeyb.IsKeyDown(Keys.K))
                killbrick = true;

            //loops through each brick and checks if it needs to kill one
            for (int i = 0; i < bricks.GetLength(0); i++)
            {
                for (int j = 0; j < bricks.GetLength(1); j++)
                {
                    var brick = bricks[i, j];

                    if (brick.hitpoints <= 0 && brick.alive)
                    {
                        brick.alive = false;
                        score++;
                    }

                    if ((ball.Rect.Intersects(brick.Rect) || killbrick) && brick.alive == true)
                    {
                        killbrick = false;
                        // get the overlap rectangle
                        var overlap = Rectangle.Intersect(ball.Rect, brick.Rect);
                        //takes away one from the bricks hit points
                        brick.hitpoints--;
                        //takes away one frustration
                        frustration--;

                        //checks if the brick is a bomb
                        if (brick.isBomb)
                        {
                            //Kills the brick beside it
                            int x = i + 1;
                            if (x < bricks.GetLength(0))
                                bricks[x, j].hitpoints--;
                        }

                        //checks if the brick is a bomb
                        if (brick.isBomb)
                        {
                            //Kills the other brick beside it
                            int x = i - 1;
                            if (x > 0)
                                bricks[x, j].hitpoints--;
                        }

                        //if the overlap rect width > height
                        if (overlap.Width > overlap.Height)
                        {
                            //checks if the brick is not a glass brick
                            if (!brick.isGlass)
                            {
                                // flip the y velocity
                                ball.BounceY();
                            }
                            //plays the brickhit sound effect
                            brickHit.Play();

                        }
                        else
                        {
                            //checks if the brick is not a glass brick
                            if (!brick.isGlass)
                            {
                                // flip the x velocity
                                ball.BounceX();
                            }
                            //plays the brickhit sound effect
                            brickHit.Play();

                        }
                        // moves the ball back one pixel so it doesn't get stuck in a brick
                        ball.MoveBack();
                    }
                }
            }

            

            //changes level if you have destroyed every brick in each level
            if (level == 0 && score >= 50)
            {
                level++;
                //speeds up the ball
                ball.Speed = new Vector2(4, 4);
                ProceedLevel();
                //sets score to 50
                score = 50;
            }
            if (level == 1 && score >= 75)
            {
                level++;
                //speeds up the ball
                ball.Speed = new Vector2(5, 5);
                ProceedLevel();
                //sets score to 75
                score = 75;
            }
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Sets what is shown in the Attract state
            void AttractDraw()
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);

                
                _spriteBatch.Begin();
                _spriteBatch.DrawString(ScoreFont, "Welcome to TOTALLY NOT BREAKOUT", new Vector2(100, 100), Color.White);
                _spriteBatch.DrawString(ScoreFont, "Make sure not to get to 200 frustration!", new Vector2(100, 200), Color.White);
                _spriteBatch.DrawString(ScoreFont, "Press Space to start!", new Vector2(225, 300), Color.White);
                _spriteBatch.End();
            }

            //Sets what is shown in the Playing state
            void PlayingDraw()
            {
                GraphicsDevice.Clear(Color.Blue);
                _spriteBatch.Begin();

                //draws each brick
                foreach (Brick brick in bricks)
                {
                    brick.DrawMe(_spriteBatch);
                }

                //draws the paddle and ball
                paddle.DrawMe(_spriteBatch);
                ball.DrawMe(_spriteBatch);

                //draws the font with the given text and the position
                _spriteBatch.DrawString(ScoreFont, "Frustration: " + frustration, new Vector2(0, 560), Color.White);
                
                _spriteBatch.End();
            }

            void GameOverDraw()
            {
                GraphicsDevice.Clear(Color.Blue);
                _spriteBatch.Begin();

                //draws the font with the given text and the position
                _spriteBatch.DrawString(ScoreFont, "You DIED! Your score was: " + score, new Vector2(0, 560), Color.White);

                _spriteBatch.End();
            }

            void WinDraw()
            {
                GraphicsDevice.Clear(Color.Blue);
                _spriteBatch.Begin();

                //draws the font with the given text and the position
                _spriteBatch.DrawString(ScoreFont, "YOU WON! Your score was: " + score, new Vector2(0, 560), Color.White);

                _spriteBatch.End();
            }

            switch (CurrentGameState)
            {
                case GameState.Attract:
                    AttractDraw();
                    break;
                case GameState.Playing:
                    PlayingDraw();
                    break;
                case GameState.GameOver:
                    GameOverDraw();
                    break;
                case GameState.Win:
                    WinDraw();
                    break;
            }

            _spriteBatch.Begin();


           

            
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}