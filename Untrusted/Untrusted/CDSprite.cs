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

namespace Untrusted
{
    #region Sprite base class
    public class Sprite
    {
        public Vector2 location, movement;
        public Texture2D texture; //will be a 'sprite sheet' later, for now just one
        public Player_Movement move;
        public float currentAngle;
        public Rectangle area;
        public Vector2 otherLocation;
        //other things basic sprites need

        public Sprite(Texture2D text)
        {
            texture = text;
            location = new Vector2(384, 512);
        }

        public Vector2 getLocation() { return location; }

        public void draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, new Rectangle((int)location.X, (int)location.Y, texture.Width, texture.Height), Color.White);
        }
    }
    #endregion
    #region Player
    public class Player : Sprite
    {
        public float castpercent = 0;
        bool hacking = false;

        public Player(Texture2D text)
            : base(text)
        {
            this.move = new Player_Movement();
        }
        public void starthack()
        {
            hacking = true;
        }
        public void update(GameTime gtime)
        {
            area = new Rectangle((int)location.X, (int)location.Y, texture.Width, texture.Height);
            move.updatePlayer(this, gtime);
        }
        public void draw(SpriteBatch spritebatch)
        {
            float temp = 1;
            if (Game1.currentMap != 0) { temp = 1; }
            else { temp = .25f; }
            spritebatch.Draw(
                texture,
                this.location,
                null,
                Color.White,
                //move.turnPlayerAngle(this) - (float)(((float)Math.PI) / (float)2),
                currentAngle,
                new Vector2((texture.Width / 2), texture.Height / 2),
                temp,
                SpriteEffects.None,
                1.0f);

            if (!hacking) { castpercent = 0; }
            else if (hacking)
            {
                spritebatch.Draw(Game1.castBar2, new Rectangle((int)location.X - 2, (int)location.Y - 5, 44, 19), Color.Gray);
                spritebatch.Draw(Game1.castBar, new Rectangle((int)location.X, (int)location.Y - 5, (int)(40 * castpercent), 15), Color.Yellow);
                hacking = false;
            }
        }
    }
    #endregion
    #region NPC
    public class Police : Sprite
    {
        public Police_Movement moves;
        public Color[] PoliceTextureData;
        public Color[] HackerTextureData;

        //other things basic sprites need

        public Police(Texture2D text, Vector2 local, List<Vector2> lst)
            : base(text)
        {
            texture = text;
            location = local;
            moves = new Police_Movement(lst, this);
        }

        public void LoadContent(Texture2D hackerTexture)
        {
            PoliceTextureData = new Color[texture.Width * texture.Height];
            texture.GetData(PoliceTextureData);

            HackerTextureData = new Color[hackerTexture.Width * hackerTexture.Height];
            hackerTexture.GetData(HackerTextureData);
        }

        public bool update(GameTime gtime, Rectangle playerRect)
        {
            moves.update();
            area = new Rectangle((int)location.X, (int)location.Y, texture.Width, texture.Height);
            return IntersectPixels(area, PoliceTextureData,
                                playerRect, HackerTextureData);
        }
        public void draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(
                texture,
                this.location,
                null,
                Color.White,
                //moves.turnToOther(this, Game1.p),// - (float)(((float)Math.PI) / (float)2),
                currentAngle,
                new Vector2((texture.Width / 2), texture.Height / 2),
                1.0f,
                SpriteEffects.None,
                1.0f);
        }
        static bool IntersectPixels(Rectangle rectangleA, Color[] dataA,
                                    Rectangle rectangleB, Color[] dataB)
        {
            // Find the bounds of the rectangle intersection
            int top = Math.Max(rectangleA.Top, rectangleB.Top);
            int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
            int left = Math.Max(rectangleA.Left, rectangleB.Left);
            int right = Math.Min(rectangleA.Right, rectangleB.Right);

            // Check every point within the intersection bounds
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Get the color of both pixels at this point
                    Color colorA = dataA[(x - rectangleA.Left) +
                                         (y - rectangleA.Top) * rectangleA.Width];
                    Color colorB = dataB[(x - rectangleB.Left) +
                                         (y - rectangleB.Top) * rectangleB.Width];

                    // If both pixels are not completely transparent,
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        // then an intersection has been found
                        return true;
                    }
                }
            }

            // No intersection found
            return false;
        }

    }
    #endregion

    public class RoomObjects : Sprite
    {
        float scale;
        public bool hacked;

        public RoomObjects(Texture2D text, Vector2 local, float angles = 0, float scales = 1)
            : base(text)
        {
            location = local;
            currentAngle = angles;
            this.scale = scales;
            hacked = false;
            area = new Rectangle((int)location.X, (int)location.Y, (int)(texture.Width * scale), (int)(texture.Height * scale));
        }

        public void draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(
                texture,
                this.location,
                null,
                Color.White,
                currentAngle,
                new Vector2((texture.Width / 2), texture.Height / 2),
                scale,
                SpriteEffects.None,
                1.0f);
        }
    }
}
