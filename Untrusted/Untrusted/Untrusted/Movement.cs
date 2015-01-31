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
    public class Player_Movement
    {
        public KeyboardState last, current;
        Keys RightMovement, LeftMovement, ForwardMovement, BackMovement;
        float Player_Speed = 5f;

        public Player_Movement()
        {
            RightMovement = Keys.D;
            LeftMovement = Keys.A;
            ForwardMovement = Keys.W;
            BackMovement = Keys.S;
        }

        public void updatePlayer(Player pl, GameTime gameTime)
        {
            if (last == null) { last = current; } // temp
            current = Keyboard.GetState();

            if (current.IsKeyDown(RightMovement))
            {
                pl.movement.X = Player_Speed;
                Game1.p.currentAngle = MathHelper.PiOver2;
            }
            if (current.IsKeyDown(LeftMovement))
            {
                pl.movement.X = -Player_Speed;
                Game1.p.currentAngle = -MathHelper.PiOver2;
            }
            if (current.IsKeyDown(ForwardMovement))
            {
                pl.movement.Y = -Player_Speed;
                Game1.p.currentAngle = 0;
            }
            if (current.IsKeyDown(BackMovement))
            {
                pl.movement.Y = Player_Speed;
                Game1.p.currentAngle = MathHelper.Pi;
            }
            if (current.IsKeyDown(RightMovement) && current.IsKeyDown(ForwardMovement))
            {
                    double val = (Math.Sin(45.00) * Player_Speed);
                    pl.movement.X = (int)val;
                    pl.movement.Y = (int)-val;
                    Game1.p.currentAngle = MathHelper.PiOver4;
            }
            if (current.IsKeyDown(RightMovement) && current.IsKeyDown(BackMovement))
            {
                double val = (Math.Sin(45.00) * Player_Speed);
                pl.movement.X = (int)val;
                pl.movement.Y = (int)val;
                Game1.p.currentAngle = 3*MathHelper.PiOver4;
            }
            if (current.IsKeyDown(LeftMovement) && current.IsKeyDown(BackMovement))
            {
                double val = (Math.Sin(45.00) * Player_Speed);
                pl.movement.X = -(int)val;
                pl.movement.Y = (int)val;
                Game1.p.currentAngle = -3*MathHelper.PiOver4;
            }
            if (current.IsKeyDown(LeftMovement) && current.IsKeyDown(ForwardMovement))
            {
                double val = (Math.Sin(45.00) * Player_Speed);
                pl.movement.X = -(int)val;
                pl.movement.Y = -(int)val;
                Game1.p.currentAngle = -MathHelper.PiOver4;
            }
            if (Game1.currentMap == 0) pl.movement *= .4f;
            pl.location += pl.movement;
            if (pl.location.X < 5) pl.location.X = 5;
            else if (pl.location.X + pl.texture.Width*.5f > 750) pl.location.X = 750 - pl.texture.Width/2;
            if (pl.location.Y < 0) pl.location.Y = 0;
            else if (pl.location.Y + pl.texture.Height > 750) pl.location.Y = 750 - pl.texture.Height;
            
            //for (int i = 2; i < Game1.home.Count(); i++)
            //{
            //    if(pl.area.Intersects(Game1.home[i].area))
            //    {
            //        if (pl.location.X <= Game1.home[i].location.X )
            //        {
            //            pl.location.X -= pl.area.Right - Game1.home[i].area.Left;
                                         
            //        }
            //        else if (pl.location.X >= Game1.home[i].location.X)
            //        {
            //            pl.location.X += Game1.home[i].area.Right - pl.area.Left;
            //        }
            //        if (pl.location.Y > Game1.home[i].location.Y)
            //        {
            //            pl.location.Y += pl.area.Bottom - Game1.home[i].area.Top;
            //        }
            //        else if (pl.location.Y < Game1.home[i].location.Y)
            //        {
            //            pl.location.Y -= Game1.home[i].area.Top - pl.area.Bottom;
            //        }       
            //    }
            //}
            pl.movement.X = 0;
            pl.movement.Y = 0;
            if (current.IsKeyDown(Keys.D1)) Game1.currentMap = 0;
            if(current.IsKeyDown(Keys.D2)) Game1.currentMap = 2;
            if (current.IsKeyDown(Keys.E))
            {
                if (Game1.currentMap == 0)
                {
                    if (pl.area.Intersects(new Rectangle(625, 625, 175, 175)) && last.IsKeyDown(Keys.E))
                    {
                        Game1.currentMap = 2;
                        pl.location = new Vector2(750 * .75f, 0);
                        if (Game1.currentStory < 1)
                        {
                            Game1.currentStory = 1;
                            Game1.bottomtext.counter = 0;
                        }
                    }
                    if(pl.area.Intersects(new Rectangle(0, 600,200,200)))
                    {
                        if (Game1.currentStory < 7) Game1.currentStory = 7;
                        Game1.bottomtext.counter = 0;
                        Game1.pause = null;
                    }
                }
                if (Game1.currentMap == 0)
                {
                    if(pl.area.Intersects(Game1.all[Game1.currentMap][1].area))
                    {
                        if (Game1.all[Game1.currentMap][1].hacked != true)
                            {
                            Game1.p.starthack();
                            Game1.p.castpercent += .005f;
                            if (Game1.p.castpercent >= 1)
                            {
                                Game1.all[Game1.currentMap][1].hacked = true;
                                Game1.device.income += 2;
                                Game1.p.castpercent = 0;
                                if (Game1.currentStory < 3) Game1.currentStory = 3;
                                Game1.bottomtext.counter = 0;
                            }
                        }
                    }
                    if (pl.area.Intersects(new Rectangle(100, 100, 100, 100)))
                    {
                        if (last.IsKeyUp(Keys.E))
                        {
                            Game1.currentMap = 3;
                            pl.location = new Vector2(350, 700);
                            if (Game1.currentStory < 4) Game1.currentStory = 4;
                            Game1.bottomtext.counter = 0;
                        }
                    }
                    if (pl.area.Intersects(new Rectangle(600, 75, 150, 150)))
                    {
                        if (last.IsKeyUp(Keys.E))
                        {
                            Game1.currentMap = 4;
                            pl.location = new Vector2(350, 700);
                        }
                    }
                }

                if (Game1.currentMap == 1 && pl.area.Intersects(Game1.all[Game1.currentMap][4].area))
                {
                    if (Game1.all[Game1.currentMap][4].hacked != true)
                    {
                        Game1.p.starthack();
                        Game1.p.castpercent += .005f;
                        if (Game1.p.castpercent >= 1)
                        {
                            Game1.all[Game1.currentMap][4].hacked = true;
                            Game1.device.income += 1;
                            Game1.p.castpercent = 0;
                        }
                    }
                    
                }
                if (Game1.currentMap == 1 && pl.area.Intersects(new Rectangle(750-100, 750-100,100,100)))
                    {
                        //MAP
                        Game1.currentMap = 0;
                        Game1.p.location = new Vector2((750 / 4) -21, (750 / 2) + 57);
                        Game1.p.currentAngle = MathHelper.Pi;
                    }
                if (Game1.currentMap == 2 && pl.area.Intersects(Game1.all[Game1.currentMap][11].area))
                {
                    if (Game1.all[Game1.currentMap][1].hacked != true)
                    {
                        Game1.p.starthack();
                        Game1.p.castpercent += .005f;
                        if (Game1.p.castpercent >= 1)
                        {
                            Game1.all[Game1.currentMap][1].hacked = true;
                            Game1.device.income += 4;
                            Game1.p.castpercent = 0;
                            if (Game1.currentStory < 2) Game1.currentStory = 2;
                            Game1.bottomtext.counter = 0;
                        }
                    }
                }
                if (Game1.currentMap == 2)
                {
                    if (pl.area.Intersects(new Rectangle((int)(750 * 0.7f), 0, 80, 40)))
                    {
                        if(last.IsKeyUp(Keys.E)){
                            Game1.currentMap = 0;
                            pl.location = new Vector2(520, 680);
                            pl.currentAngle = -MathHelper.PiOver2;
                        }
                    }
                }
                if (Game1.currentMap == 3)
                {
                    if (pl.area.Intersects(new Rectangle(680, 630, 50, 50)))
                    {
                        if (Game1.all[Game1.currentMap][Game1.all[Game1.currentMap].Count() - 1].hacked != true)
                        {
                            Game1.p.starthack();
                            Game1.p.castpercent += .005f;
                            if (Game1.p.castpercent >= 1)
                            {
                                Game1.all[Game1.currentMap][Game1.all[Game1.currentMap].Count() - 1].hacked = true;
                                Game1.device.income += 5;
                                Game1.p.castpercent = 0;
                                if (Game1.currentStory < 5) Game1.currentStory = 5;
                                Game1.bottomtext.counter = 0;
                            }
                        }
                    }
                    if(pl.area.Intersects(new Rectangle(335, 685, 50, 50)))
                    {
                        if (last.IsKeyUp(Keys.E))
                        {
                            Game1.currentMap = 0;
                            pl.location = new Vector2(175, 140);
                        }
                    }
                }
                if (Game1.currentMap == 4)
                {
                    if (pl.area.Intersects(new Rectangle(680, 630, 50, 50)))
                    {
                        if (Game1.all[Game1.currentMap][Game1.all[Game1.currentMap].Count() - 1].hacked != true)
                        {
                            Game1.p.starthack();
                            Game1.p.castpercent += .005f;
                            if (Game1.p.castpercent >= 1)
                            {
                                Game1.all[Game1.currentMap][Game1.all[Game1.currentMap].Count() - 1].hacked = true;
                                Game1.device.income += 6;
                                Game1.p.castpercent = 0;
                                if (Game1.currentStory < 6) Game1.currentStory = 6;
                                Game1.bottomtext.counter = 0;
                            }
                        }
                    }
                    if (pl.area.Intersects(new Rectangle(335, 685, 50, 50)))
                    {
                        if (last.IsKeyUp(Keys.E))
                        {
                            Game1.currentMap = 0;
                            pl.location = new Vector2(650, 195);
                        }
                    }
                }
            }
            last = current;
        }
    }
    #region NPC Movement Class

    public class NPC_Movement
    {
        float Player_Speed = 5f;
        const int defaultSpeed = 4;
        public Vector2 otherPosition; //for now just one target, no que list
        float ReactionDistance = 50f, SeparationDistance = 15f;

        public NPC_Movement()
        {
            //something needed?
        }
        public void update(GameTime gTime, Police sp)
        {
            for (int i = 0; i < Game1.n.Count(); i++)
            { //check to make sure they dont overlap
                Vector2 pushDirection = Vector2.Zero;
                float weight = .5f;

                if (Game1.n[i] != null && sp.area.Intersects(Game1.n[i].area) &&
                   sp.location != Game1.n[i].location)
                {
                    pushDirection = sp.location - Game1.n[i].location;
                    Vector2.Normalize(ref pushDirection, out pushDirection);

                    //push away
                    //weight *= (1 -(float)Animal.ReactionDistance / aiParams.SeparationDistance);
                    weight *= (1 - (float)ReactionDistance / SeparationDistance);
                    pushDirection *= weight;

                    //reacted = true;
                    sp.movement = pushDirection;
                }
                if (Game1.n[i] != null && sp.area.Intersects(Game1.p.area) &&
                   sp.location != Game1.p.location)
                {
                    pushDirection = sp.location - Game1.p.location;
                    Vector2.Normalize(ref pushDirection, out pushDirection);

                    //push away
                    //weight *= (1 -(float)Animal.ReactionDistance / aiParams.SeparationDistance);
                    weight *= (1 - (float)ReactionDistance / SeparationDistance);
                    pushDirection *= weight;

                    //reacted = true;
                    sp.movement = pushDirection;
                }
            }
        }
    }
    #endregion

    public class Police_Movement
    {
        float Player_Speed = 3f;
        public Vector2 otherPosition; //for now just one target, no que list
        public int count = 0;
        List<Vector2> Commands;
        Police p;

        
        public Police_Movement(List<Vector2> commands, Police p)
        {
            Commands = commands;
            this.p = p;
        }
        public void update(){
            if (count == Commands.Count()) { count = 0; }
            if (Commands[count].X != p.location.X)
            {
                if (p.location.X < Commands[count].X)
                {
                    p.location.X += Player_Speed;
                    p.currentAngle = -MathHelper.PiOver2;
                    if (p.location.X > Commands[count].X) p.location.X = Commands[count].X;
                }
                else if (p.location.X > Commands[count].X)
                {
                    p.location.X -= Player_Speed;
                   p.currentAngle = MathHelper.PiOver2;
                   if (p.location.X < Commands[count].X) p.location.X = Commands[count].X;
                }
            }
            if (Commands[count].Y != p.location.Y)
            {
                if (p.location.Y > Commands[count].Y)
                {
                    p.location.Y -= Player_Speed;
                    p.currentAngle = 0;
                    if (p.location.Y < Commands[count].Y) p.location.Y = Commands[count].Y;
                }
                if (p.location.Y < Commands[count].Y)
                {
                    p.location.Y += Player_Speed;
                    p.currentAngle = MathHelper.Pi;
                    if (p.location.Y > Commands[count].Y) p.location.Y = Commands[count].Y;
                }
            }
            if (Commands[count].X == p.location.X && Commands[count].Y == p.location.Y){ count++; }
            //something needed?
        }
    }
}
