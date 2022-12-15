using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace Cooler_King
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

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
            screenSize = GraphicsDevice.Viewport.Bounds;

            frustration = 0;
            score = 74;
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //loads the paddle texture
            paddle = new Paddle(Content.Load <Texture2D>("paddle (2)"), new Vector2 (screenSize.Width/2 - 35 , 450));
            ball = new Ball(Content.Load <Texture2D>("ball"),new Vector2(200, 200));
            brickImage = Content.Load<Texture2D>("Brick");
            ScoreFont = Content.Load<SpriteFont>("ScoreText");

            StartGame();
        }

        protected void StartGame()
        {
            bricks = new Brick[bricksWide, bricksHigh];

            for (int y = 0; y < bricksHigh; y++)
            {
                Color tint = Color.White;

                switch (y)
                {
                    case 0:
                        tint = Color.Blue;
                        break;
                    case 1:
                        tint = Color.Green;
                        break;
                    case 2:
                        tint = Color.Red;
                        break;
                    case 3:
                        tint = Color.Yellow;
                        break;
                    case 4:
                        tint = Color.Purple;
                        break;
                }
                for (int x = 0; x < bricksWide; x++)
                {

                    bricks[x, y] = new Brick(brickImage, new Rectangle(x * brickImage.Width, y * brickImage.Height, brickImage.Width, brickImage.Height), tint);

                    //bricks[x, y] = new Brick(brickImage, new Rectangle(x * brickImage.Width, (int)(MathF.Sin(x * 0.05f) * y * brickImage.Height), brickImage.Width, brickImage.Height), tint);





                }
            }
            
        }

        public void ProceedLevel()
        {
            frustration = frustration / 2.0f;
            for (int y = 0; y < bricksHigh; y++)
            {
                for (int x = 0; x < bricksWide; x++)
                {
                    switch (level)
                    {
                        case 1:
                            
                            bricks[x, y].alive = true;
                            if (x % 2 == 0)
                            {
                                bricks[x, y].alive = false;
                                
                            }
                            
                            break;

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

            paddle.UpdateMe(screenSize.Width);
            ball.UpdateMe(screenSize.Height, screenSize.Width);

            // Check for a collision between the logos

            //THIS DOESN'T WORK
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
                    // endif
                    ball.MoveBack();
                   

                }

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
                        // endif
                        ball.MoveBack();


                    }
                }
            }

            frustration += (float)gameTime.ElapsedGameTime.TotalSeconds;


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
            //if (score >= 50)
            //{
            //    Exit();
            //}

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            foreach (Brick brick in bricks)
            {
                brick.DrawMe(_spriteBatch);
            }

            paddle.DrawMe(_spriteBatch);
            ball.DrawMe(_spriteBatch);
            _spriteBatch.DrawString(ScoreFont, "Frustration: " + frustration, new Vector2(0, 560), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}