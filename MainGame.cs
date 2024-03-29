using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TopDownRPGEngine
{
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
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
