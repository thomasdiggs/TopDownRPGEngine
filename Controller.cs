using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Comora;
using System;

namespace TopDownRPGEngine
{
    internal class Controller
    {
        public static double timer = 2D;
        public static double maxTime = 2D;
        static Random rand = new();

        public static void Update(GameTime gameTime, Texture2D spriteSheet)
        {
            timer -= gameTime.ElapsedGameTime.TotalSeconds;
            if (timer <= 0)
            {
                int side = rand.Next(4);
                if (side == 0)
                    Enemy.enemies.Add(new(new Vector2(-500, rand.Next(-500, 2000)), spriteSheet));
                else if (side == 1)
                    Enemy.enemies.Add(new(new Vector2(rand.Next(-500, 2000), -500), spriteSheet));
                else if (side == 2)
                    Enemy.enemies.Add(new(new Vector2(2000, rand.Next(-500, 2000)), spriteSheet));
                else if (side == 3)
                    Enemy.enemies.Add(new(new Vector2(rand.Next(-500, 2000), 2000), spriteSheet));
                timer = maxTime;
                if (maxTime > 0.5)
                {
                    maxTime -= 0.1D;
                }
            }
        }
    }
}
