using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Comora;
using System;
using System.Collections.Generic;

namespace TopDownRPGEngine
{
    internal class Enemy
    {
        private Vector2 position = new(0, 0);
        private int speed = 150;
        public SpriteAnimation anim;
        public static List<Enemy> enemies = new();
        public int radius = 30;
        private bool dead = false;

        public Enemy(Vector2 newPos, Texture2D spriteSheet)
        {
            position = newPos;
            anim = new(spriteSheet, 10, 6);
        }

        public Vector2 Position { get { return position; } }

        public bool Dead
        {
            get { return dead; }
            set { dead = value; }
        }

        public void Update(GameTime gameTime, Vector2 playerPos, bool isPlayerDead)
        {
            anim.Position = new Vector2(position.X - 48, position.Y - 66);
            anim.Update(gameTime);

            if (!isPlayerDead)
            {
                Vector2 moveDir = playerPos - anim.Position;
                moveDir.Normalize(); // Normalize the vector so that the enemy moves at a constant speed of 1
                position += moveDir * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
