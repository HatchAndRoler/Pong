using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pong
{
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        GraphicsDevice device;
        SpriteBatch spriteBatch;

        Texture2D backGround;
        Texture2D Player1;
        Texture2D Player2;
        Texture2D Ball;

        int screenWidth;
        int screenHeight;
        int count = 0;

        float BallX = 400;
        float BallY = 250;
        float m;

        Vector2 Player1Position = new Vector2(5, 225);
        Vector2 Player2Position = new Vector2(785, 225);
        Vector2 BallPosition;
        Vector2 LastBallPosition;

        KeyboardState keyboard;

        Random GenerateRandomX = new Random();
        Random GenerateRandomY = new Random();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 500;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Yonatan Broder's Pong";
            BallPosition = new Vector2(400, 250);

            base.Initialize();
        }

        protected override void LoadContent()
        {  
            device = graphics.GraphicsDevice;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            backGround = Content.Load<Texture2D>("BG");
            screenWidth = device.PresentationParameters.BackBufferWidth;
            screenHeight = device.PresentationParameters.BackBufferHeight;
            Ball = Content.Load<Texture2D>("Ball");
            Player1 = Content.Load<Texture2D>("Player1");
            Player2 = Content.Load<Texture2D>("player2");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            PlayerMovment();
            RandomBall();
            BounceOfWalls();
            BounceOfPlayer();

            base.Update(gameTime);
        }
        public void PlayerMovment()
        {
            keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Right))
            {
                if (Player1Position.Y < 440)
                    Player1Position.Y += 5;
            }

            if (keyboard.IsKeyDown(Keys.Left))
            {
                if (Player1Position.Y > 5)
                    Player1Position.Y -= 5;
            }

            if (keyboard.IsKeyDown(Keys.D))
            {
                if (Player2Position.Y < 440)
                    Player2Position.Y += 5;
            }

            if (keyboard.IsKeyDown(Keys.A))
            {
                if (Player2Position.Y > 5)
                    Player2Position.Y -= 5;
            }
        }
        public void RandomBall()
        {
            if (count == 0)
            {
                BallX = -1;
                BallY = -2;
                count++;
            }
            BallPosition.X += BallX;
            BallPosition.Y += BallY;
        }
        public void BounceOfWalls()
        {
            m = ((LastBallPosition.Y - BallPosition.Y) / (LastBallPosition.X - BallPosition.X));
            
                if ((BallPosition.Y < 10) || (BallPosition.Y > 490))
                {
                    if(BallY>0)
                    BallY = 0 - m;
                    if (BallY <= 0)
                        BallY = BallY * -1;
                    if (BallPosition.Y>400)
                    {
                        if (BallY > 485)
                            BallY = 0 - m;
                        if (BallY <= 485)
                            BallY = BallY * -1;
                    }
                }
            
            LastBallPosition = BallPosition;
        }
        public void BounceOfPlayer()
        {
            for(int i=0;i<50;i++)
            {
                if ((Player2Position.Y+i == BallPosition.Y) && (Player2Position.X-10 == BallPosition.X))
                {
                    if (i < 25)
                    {
                        BallX = 0 - BallX;
                        BallY = m *2;
                    }
                    else
                    {
                        BallX = 0 - BallX;
                        BallY = m*BallX;
                    }
                }
                if ((Player1Position.Y+i == BallPosition.Y) && (Player1Position.X+10 == BallPosition.X))
                {
                    if (i < 25)
                    {
                        BallX = 0 - BallX;
                        BallY = 0 - m ;
                    }
                    else
                    {
                        BallX = 0 - BallX;
                        BallY =  (0-m)*BallX;
                    }
                }
            }
            }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            Rectangle screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Draw(backGround, screenRectangle, Color.White);
            spriteBatch.Draw(Player1, Player1Position, Color.White);
            spriteBatch.Draw(Player2, Player2Position, Color.White);
            spriteBatch.Draw(Ball, BallPosition, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
