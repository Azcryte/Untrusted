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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static SpriteFont font;
        public static Player p;
        public static Texture2D terminal, inst;
        public static Texture2D castBar, castBar2;

        public static int currentMap = 1;
        public static List<List<RoomObjects>> all = new List<List<RoomObjects>>();
        public static List<Police> n = new List<Police>();

        /**/
        public static TimeSpan previousUpdateTime;
        public static TimeSpan updateTime;

        public static int score;
        public static Random random = new Random();
        /**/
        TextInterface bottomtext;
        TextInterface sidetext;
        TextInterface scoretext;



        public void loadRoom()
        {
            List<RoomObjects> home = new List<RoomObjects>();
            Texture2D room = Content.Load<Texture2D>(@"Sprites\room");
            home.Add(new RoomObjects(room, new Vector2(0, 0), 0, .75f));
            Texture2D roomback = Content.Load<Texture2D>(@"Sprites\roomback");
            home.Add(new RoomObjects(roomback, new Vector2(375, 50)));
            Texture2D bed = Content.Load<Texture2D>(@"Sprites\bed");
            home.Add(new RoomObjects(bed, new Vector2(93, 288), 0, 1.25f));
            Texture2D door = Content.Load<Texture2D>(@"Sprites\Door");
            home.Add(new RoomObjects(door, new Vector2(750 - door.Width, 750 - door.Height), -MathHelper.PiOver4, 2));
            Texture2D roomComp = Content.Load<Texture2D>(@"Sprites\roomComp");
            home.Add(new RoomObjects(roomComp, new Vector2(50, 765 - roomComp.Height / 2), MathHelper.Pi, .75f));
            Texture2D TV = Content.Load<Texture2D>(@"Sprites\TV");
            home.Add(new RoomObjects(TV, new Vector2(750 - TV.Height / 2, 315 - TV.Height), (float)MathHelper.PiOver2));
            all.Add(home);
        }
        public void loadLibrary()
        {
            List<RoomObjects> library = new List<RoomObjects>();
            Texture2D floor = Content.Load<Texture2D>(@"Sprites\library");
            library.Add(new RoomObjects(floor, new Vector2(0, 0)));
            Texture2D desk = Content.Load<Texture2D>(@"Sprites\libDesk");
            library.Add(new RoomObjects(desk, new Vector2(450 - desk.Width / 2, 60), (float)MathHelper.PiOver2, .7f));
            Texture2D books = Content.Load<Texture2D>(@"Sprites\bookshelf");
            int j = 0;
            for (int i = 0; i < 4; i++)
            {
                library.Add(new RoomObjects(books, new Vector2(j, 150), MathHelper.PiOver2, .5f));
                library.Add(new RoomObjects(books, new Vector2(j, 450), MathHelper.PiOver2, .5f));
                j += 80;
            }
            Texture2D libComp = Content.Load<Texture2D>(@"Sprites\libComp");
            Texture2D mat = Content.Load<Texture2D>(@"Sprites\mat");
            library.Add(new RoomObjects(mat, new Vector2(750 * .75f, 20)));
            library.Add(new RoomObjects(libComp, new Vector2(350, 750 - libComp.Height + 50), (float)MathHelper.Pi));
            all.Add(library);
        }

        public void loadCafe()
        {
            List<RoomObjects> temp = new List<RoomObjects>();
            Texture2D back = Content.Load<Texture2D>(@"Sprites/library");
            temp.Add(new RoomObjects(back, new Vector2(0, 0)));
            Texture2D table = Content.Load<Texture2D>(@"Sprites/TableChairs");
            temp.Add(new RoomObjects(table, new Vector2(600, 100)));
            Texture2D mat = Content.Load<Texture2D>(@"Sprites/mat");
            temp.Add(new RoomObjects(mat, new Vector2(350, 700)));

            Texture2D comp = Content.Load<Texture2D>(@"Sprites/roomComp");
            temp.Add(new RoomObjects(comp, new Vector2(700, 700), MathHelper.Pi));
            int j = 20;
            for (int i = 0; i < 4; i++)
            {
                temp.Add(new RoomObjects(table, new Vector2(j, 100), MathHelper.PiOver2));
                temp.Add(new RoomObjects(table, new Vector2(j, 400), MathHelper.PiOver2));
                j += 100;
            }
            all.Add(temp);
        }

        public void loadHotel()
        {
            List<RoomObjects> temp = new List<RoomObjects>();
            Texture2D back = Content.Load<Texture2D>(@"Sprites/hotel");
            temp.Add(new RoomObjects(back, new Vector2(0, 0)));
            Texture2D upEl = Content.Load<Texture2D>(@"Sprites/UpEl");
            temp.Add(new RoomObjects(upEl, new Vector2(300, 80), MathHelper.PiOver2 * 3));
            Texture2D downEl = Content.Load<Texture2D>(@"Sprites/DownEl");
            temp.Add(new RoomObjects(downEl, new Vector2(500, 80), MathHelper.PiOver2 * 3));
            Texture2D desk = Content.Load<Texture2D>(@"Sprites/hoteldesk");
            temp.Add(new RoomObjects(desk, new Vector2(550, 540), MathHelper.PiOver2));
            Texture2D comp = Content.Load<Texture2D>(@"Sprites/roomComp");
            temp.Add(new RoomObjects(comp, new Vector2(700, 700), MathHelper.Pi));
            Texture2D mat = Content.Load<Texture2D>(@"Sprites/mat");
            temp.Add(new RoomObjects(mat, new Vector2(350, 700)));

            all.Add(temp);
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1000;
            IsMouseVisible = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Texture2D text = Content.Load<Texture2D>(@"Sprites\Hacker");
            p = new Player(text);
            terminal = Content.Load<Texture2D>(@"Sprites\Terminal");
            inst = Content.Load<Texture2D>(@"Sprites\instbar");
            castBar = Content.Load<Texture2D>(@"Sprites\bars");
            castBar2 = Content.Load<Texture2D>(@"Sprites\bars2");
            Texture2D map = Content.Load<Texture2D>(@"Sprites\Map");
            List<RoomObjects> a = new List<RoomObjects>();
            a.Add(new RoomObjects(map, new Vector2(370, 365), 0, .39f));
            Texture2D pump = Content.Load<Texture2D>(@"Sprites\Pumps");
            a.Add(new RoomObjects(pump, new Vector2(400, 700), MathHelper.PiOver2, .25f));
            all.Add(a);
            loadRoom();
            loadLibrary();
            loadCafe();
            loadHotel();
            font = Content.Load<SpriteFont>("Courier New");
            bottomtext = new TextInterface();
            sidetext = new TextInterface();
            scoretext = new TextInterface();

            scoretext.Initialize(inst, new Vector2(750, 10), new Vector2(100, 75), font, "Score: 1000", 300f, .75f, Color.Red);
            sidetext.Initialize(inst, new Vector2(750, 250), new Vector2(100, 180), font, "Status: come to the office", 300f, .75f, Color.White);
            bottomtext.Initialize(inst, new Vector2(0, 752), new Vector2(750, 17), font, "New Message", 1450f, 0.75f, Color.White);
            Texture2D police = Content.Load<Texture2D>(@"Sprites\POLICE");
            List<Vector2> temp = new List<Vector2>();
            temp.Add(new Vector2((750 * .75f) - 60, 0));
            temp.Add(new Vector2((750 * .75f) - 60, 550));
            n.Add(new Police(police, new Vector2((750 * .75f) - 60, 0), temp));
            foreach (Police cop in n)
            {
                cop.LoadContent(text);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Q)) Exit();

            // TODO: Add your update logic here

            p.update(gameTime);
            for (int i = 0; i < n.Count(); i++)
            {
                if (n[i].update(gameTime, p.area))
                {
                    // Write Cop Captured Code Here.
                    Console.WriteLine("It works!!! ");
                    score += 100;
                }

            }
            /**/
            // how often hacks are providing results
            if (gameTime.TotalGameTime - previousUpdateTime > updateTime)
            {
                previousUpdateTime = gameTime.TotalGameTime;

                if (random.Next(100) < 50)
                {
                    //score += 100;
                }
                // 
            }
            /**/
            scoretext.Update("Score: " + score);
            bottomtext.Update();
            sidetext.Update();
            scoretext.Update();


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            for (int i = 0; i < all[currentMap].Count(); i++)
            {
                all[currentMap][i].draw(spriteBatch);
            }
            p.draw(spriteBatch);
            //for (int i = 0; i < n.Count(); i++)
            //{
            //    n[i].draw(spriteBatch);
            //}
            spriteBatch.Draw(terminal, new Rectangle(750, 0, 250, 750), Color.Black);
            spriteBatch.Draw(inst, new Rectangle(0, 750, 1000, 50), Color.Black);
            bottomtext.Draw(spriteBatch);
            sidetext.Draw(spriteBatch);
            scoretext.Draw(spriteBatch);
            if (currentMap == 2) n[0].draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
