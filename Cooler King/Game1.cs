using Microsoft.Xna.Framework;
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
        GameOver
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        GameState CurrentGameState;

        KeyboardState currKeyb, oldKeyb;

        Paddle paddle;
        Ball ball;

        Rectangle screenSize;

        SpriteFont ScoreFont;
        float frustration;
        int score;

        int bricksWide = 10;
        int bricksHigh = 5;
        Brick[,] bricks;
        Texture2D brickImage;

        public static int level = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

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
            score = 0 ;
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //loads the paddle texture and sets it position
            paddle = new Paddle(Content.Load <Texture2D>("paddle (2)"), new Vector2 (screenSize.Width/2 - 35 , 450));

            //loads the ball texture and sets its position
            ball = new Ball(Content.Load <Texture2D>("ball"),new Vector2(200, 200));

            //loads the bricks texture
            brickImage = Content.Load<Texture2D>("Brick");

            //loads the score font
            ScoreFont = Content.Load<SpriteFont>("ScoreText");

            
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
                    //sets each brick with the brick image, a collition rectangle and a color
                    bricks[x, y] = new Brick(brickImage, new Rectangle(x * brickImage.Width, y * brickImage.Height, brickImage.Width, brickImage.Height), tint);
                }
            }

            //loops through each brick
            for (int i = 0; i < bricks.GetLength(0); i++)
            {
                for (int j = 0; j < bricks.GetLength(1); j++)
                {
                    var brick = bricks[i, j];

                    if (ball.Rect.Intersects(brick.Rect) && (brick.alive == true))
                    {
                        // get the overlap rectangle
                        var overlap = Rectangle.Intersect(ball.Rect, brick.Rect);
                        brick.alive = false;
                        frustration--;
                        score++;

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
                        // moves the ball back one pixel so it doesn't get stuck in a brick
                        ball.MoveBack();


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
                            
                            bricks[x, y].alive = true;
                            if (x % 2 == 0)
                            {
                                bricks[x, y].alive = false;
                                
                            }
                            break;

                        //sets level three
                        case 2:
                            bricks[x, y].alive = false;
                            if (x > 0 && y > 0 && x < bricksWide - 1 && y < bricksHigh - 1)
                            {
                                bricks[x, y].alive = true;

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

            currKeyb = Keyboard.GetState();

            //gives the conditions for if the state needs to me changed
            void AttractUpdate(KeyboardState cKB, KeyboardState oKB)
            {
                if (cKB.IsKeyDown(Keys.Space) && oKB.IsKeyUp(Keys.Space))
                {
                    CurrentGameState = GameState.Playing;
                    //initializes the start game function
                    StartGame();
                }
            }

            //Check to see if the game states need to be updated
            switch (CurrentGameState)
            {
                case GameState.Attract:
                    AttractUpdate(currKeyb, oldKeyb);
                    break;
                //case GameState.Playing:
                //    PlayingUpdate(currKeyb, oldKeyb);
                //    break;
                //case GameState.GameOver:
                //    GameOverUpdate(currKeyb, oldKeyb);
                //    break;
            }

            oldKeyb = currKeyb;

            //updates both the paddle and ball class and gives them screensizes
            paddle.UpdateMe(screenSize.Width);
            ball.UpdateMe(screenSize.Height, screenSize.Width);

            //adds a point of frustraiton when the ball hits the bottom of the screen
            if (ball.pos.Y >= 603 - ball.Rect.Height)
            {
                frustration++;
            }


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

            ////loops through each brick
            //for (int i = 0; i < bricks.GetLength(0); i++)
            //{
            //    for (int j = 0; j < bricks.GetLength(1); j++)
            //    {
            //        var brick = bricks[i, j];

            //        if (ball.Rect.Intersects(brick.Rect) && (brick.alive == true))
            //        {
            //            // get the overlap rectangle
            //            var overlap = Rectangle.Intersect(ball.Rect, brick.Rect);
            //            brick.alive = false;
            //            frustration--;
            //            score++;

            //            // if the overlap rect width > height
            //            if (overlap.Width > overlap.Height)
            //            {
            //                // flip the y velocity
            //                ball.BounceY();

            //            }
            //            else
            //            {
            //                // flip the x velocity
            //                ball.BounceX();

            //            }
            //            // moves the ball back one pixel so it doesn't get stuck in a brick
            //            ball.MoveBack();


            //        }
            //    }
            //}

            //add frustration every second that the game does on
            frustration += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //changes level if you have destroyed every brick in the level
            if (level == 0 && score > 50)
            {
                level++;
                ProceedLevel();
            }
            if (level == 1 && score > 75)
            {
                level++;
                ProceedLevel();
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
                _spriteBatch.DrawString(ScoreFont, "I am Attract Mode", Vector2.Zero, Color.White);
                _spriteBatch.End();
            }

            //Sets what is shown in the Playing state
            void PlayingDraw()
            {
                GraphicsDevice.Clear(Color.Yellow);
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

            switch (CurrentGameState)
            {
                case GameState.Attract:
                    AttractDraw();
                    break;
                case GameState.Playing:
                    PlayingDraw();
                    break;
                //case GameState.GameOver:
                //    GameOverDraw();
                //    break;
            }

            _spriteBatch.Begin();

           

            base.Draw(gameTime);
        }
    }
}