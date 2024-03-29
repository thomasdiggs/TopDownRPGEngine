using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Comora;
using System;

// TO DO
// Create my own camera class
// Create my own animation class
// Eight direction movement then normalize diagonal movement
// Menu system
// REFACTOR REFACTOR REFACTOR

namespace TopDownRPGEngine
{
    enum Dir
    {
        Up,
        Down,
        Left,
        Right
    }

    public class MainGame : Game
    {
        Texture2D playerSprite;
        Texture2D walkDown;
        Texture2D walkUp;
        Texture2D walkLeft;
        Texture2D walkRight;
        Texture2D background;
        Texture2D ball;
        Texture2D skull;
        Player player = new();
        Camera camera;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
            camera = new(_graphics.GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            playerSprite = Content.Load<Texture2D>("Player/player");
            walkDown = Content.Load<Texture2D>("Player/walkDown");
            walkUp = Content.Load<Texture2D>("Player/walkUp");
            walkLeft = Content.Load<Texture2D>("Player/walkLeft");
            walkRight = Content.Load<Texture2D>("Player/walkRight");
            background = Content.Load<Texture2D>("background");
            ball = Content.Load<Texture2D>("ball");
            skull = Content.Load<Texture2D>("skull");
            player.animations[0] = new SpriteAnimation(walkUp, 4, 8);
            player.animations[1] = new SpriteAnimation(walkDown, 4, 8);
            player.animations[2] = new SpriteAnimation(walkLeft, 4, 8);
            player.animations[3] = new SpriteAnimation(walkRight, 4, 8);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);
            if (!player.dead)
            {
                Controller.Update(gameTime, skull);
            }
            camera.Position = player.Position;
            camera.Update(gameTime);

            foreach (Projectile p in Projectile.projectiles)
            {
                p.Update(gameTime);
            }

            foreach (Enemy e in Enemy.enemies)
            {
                e.Update(gameTime, player.Position, player.dead);
                int sum = 32 + e.radius;
                if (Vector2.Distance(player.Position, e.Position) < sum)
                {
                    player.dead = true;
                }
            }

            foreach (Projectile p in Projectile.projectiles)
            {
                foreach (Enemy e in Enemy.enemies)
                {
                    int sum = e.radius + p.radius;
                    if (Vector2.Distance(p.Position, e.Position) < sum)
                    {
                        p.Collided = true;
                        e.Dead = true;
                    }
                }
            }

            Enemy.enemies.RemoveAll(e => e.Dead);
            Projectile.projectiles.RemoveAll(p => p.Collided);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(camera);
            _spriteBatch.Draw(background, new Vector2(-500, -500), Color.White);
            foreach (Enemy e in Enemy.enemies)
            {
                e.anim.Draw(_spriteBatch);
            }
            foreach (Projectile p in Projectile.projectiles)
            {
                _spriteBatch.Draw(ball, new Vector2(p.Position.X - 48, p.Position.Y - 48), Color.White);
            }
            if (!player.dead)
            {
                player.anim.Draw(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
