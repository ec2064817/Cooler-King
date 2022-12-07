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

        int bricksWide = 10;
        int bricksHigh = 5;
        Brick[,] bricks;
        Texture2D brickImage;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 750;
            _graphics.PreferredBackBufferHeight = 600;

        }

        protected override void Initialize()
        {
            screenSize = GraphicsDevice.Viewport.Bounds;


            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //loads the paddle texture
            paddle = new Paddle(Content.Load <Texture2D>("paddle (2)"), new Vector2 (screenSize.Width/2 - 35 , 450));
            ball = new Ball(Content.Load <Texture2D>("ball"),new Vector2(100, 100));
            brickImage = Content.Load<Texture2D>("Brick");

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
                //paddle.MoveBack();
                
            }

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
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}