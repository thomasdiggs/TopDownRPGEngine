using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TopDownRPGEngine
{
    internal class Player
    {
        private Vector2 position = new(500, 300);
        private int speed = 300;
        private Dir direction = Dir.Down;
        private bool isMoving = false;
        public SpriteAnimation anim;
        public SpriteAnimation[] animations = new SpriteAnimation[4];
        private KeyboardState kStateOld = Keyboard.GetState();
        public bool dead = false;

        public Vector2 Position { get { return position; } }

        public void SetX(float x) { position.X = x; }

        public void SetY(float y) { position.Y = y; }

        public void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            isMoving = false;

            if (kState.IsKeyDown(Keys.W) || kState.IsKeyDown(Keys.Up))
            {
                direction = Dir.Up;
                isMoving = true;
            }
            if (kState.IsKeyDown(Keys.S) || kState.IsKeyDown(Keys.Down))
            {
                direction = Dir.Down;
                isMoving = true;
            }
            if (kState.IsKeyDown(Keys.A) || kState.IsKeyDown(Keys.Left))
            {
                direction = Dir.Left;
                isMoving = true;
            }
            if (kState.IsKeyDown(Keys.D) || kState.IsKeyDown(Keys.Right))
            {
                direction = Dir.Right;
                isMoving = true;
            }

            if (kState.IsKeyDown(Keys.Space))
            {
                isMoving = false;
            }

            if (dead)
            {
                isMoving = false;
            }

            if (isMoving)
            {
                switch (direction)
                {
                    case Dir.Up:
                        if (position.Y > 200)
                        position.Y -= speed * dt;
                        break;
                    case Dir.Down:
                        if (position.Y < 1250)
                        position.Y += speed * dt;
                        break;
                    case Dir.Left:
                        if (position.X > 225)
                        position.X -= speed * dt;
                        break;
                    case Dir.Right:
                        if (position.X < 1280)
                        position.X += speed * dt;
                        break;
                    default:
                        break;
                }
            }

            anim = animations[(int)direction];
            anim.Position = new Vector2(position.X - 48, position.Y - 48);
            if (kState.IsKeyDown(Keys.Space))
            {
                anim.setFrame(0);
            }
            else if (isMoving)
            {
                anim.Update(gameTime);
            } else
            {
                anim.setFrame(1);
            }

            if (kState.IsKeyDown(Keys.Space) && kStateOld.IsKeyUp(Keys.Space))
            {
                Projectile.projectiles.Add(new Projectile(position, direction));
                Sounds.projectileSound.Play(1f, 0.5f, 0f);
            }

            kStateOld = kState;
        }
    }
}
