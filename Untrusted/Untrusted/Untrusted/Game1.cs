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
        public static Texture2D menu;
        public static Texture2D end;
        public static bool? pause = true;
        public static int currentMap = 1, currentStory = 0;
        public static List<List<RoomObjects>> all = new List<List<RoomObjects>>();
        public static List<Police> n = new List<Police>();
        public int z = 0;
        /**/
        public static TimeSpan previousUpdateTime;
        public static TimeSpan updateTime;

        public static int score;
        public static Random random = new Random();
        public static bool houseb = false, libraryb = false, gasb = false, cafeb = false, hotelb = false, bankb = false, endinfob = false;

        public bool Active;
        public bool Pressed;
        public KeyboardState last;
        public static KeyboardState keyboard = Keyboard.GetState();
        /**/
        public static TextInterface bottomtext;
        TextInterface sidetext;
        TextInterface scoretext;
        public static List<List<string>> story = new List<List<string>>();

        public struct Device
        {
            public Device(int aValue, string aName)
            {
                income = aValue;
                name = aName;
            }
            public int income;
            public string name;
        }

        public static Device device = new Device(-1, "device");
        public static bool changed = false;

        public static List<string> usernames = new List<string>(new string[] { "Sam678", "Alice567", "Larry659", "Mike324", "FlabbyBird34", "kelly334", "google", "admin23", "redpen3", "john308", "AmazingME", "CheeseSandwich" });
        public static List<string> passwords = new List<string>(new string[] { "#google", "pass", "eggsnham", "redcoat", "hunter2", "foodx!", "whales43", "skyie8", "whatpass", "mcdonalds%", "123456", "password", "God", "monkey", "george", "HashSet", "alan1234" });
   

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
            temp.Add(new RoomObjects (back, new Vector2(0,0)));
            Texture2D table = Content.Load<Texture2D> (@"Sprites/TableChairs");
            temp.Add(new RoomObjects(table, new Vector2(600, 100)));
            Texture2D mat = Content.Load<Texture2D>(@"Sprites/mat");
            temp.Add(new RoomObjects(mat, new Vector2(350, 700)));

            Texture2D comp = Content.Load<Texture2D>(@"Sprites/roomComp");
            temp.Add(new RoomObjects(comp, new Vector2(700, 700),MathHelper.Pi));
            int j = 20;
            for (int i = 0; i < 4; i++)
            {
                temp.Add(new RoomObjects(table, new Vector2(j, 100), MathHelper.PiOver2 ));
                temp.Add(new RoomObjects(table, new Vector2(j, 400), MathHelper.PiOver2 ));
                j += 100;
            }
            all.Add(temp);
        }

        public void loadHotel()
        {
            List<RoomObjects> temp = new List<RoomObjects>();
            Texture2D back = Content.Load<Texture2D>(@"Sprites/hotel");
            temp.Add(new RoomObjects (back, new Vector2(0,0)));
            Texture2D upEl = Content.Load<Texture2D> (@"Sprites/UpEl");
            temp.Add(new RoomObjects(upEl, new Vector2(300, 80), MathHelper.PiOver2 *3));
            Texture2D downEl = Content.Load<Texture2D>(@"Sprites/DownEl");
            temp.Add(new RoomObjects(downEl, new Vector2(500, 80), MathHelper.PiOver2*3));
            Texture2D desk = Content.Load<Texture2D>(@"Sprites/hoteldesk");
            temp.Add(new RoomObjects(desk, new Vector2(550, 540),MathHelper.PiOver2));
            Texture2D comp = Content.Load<Texture2D>(@"Sprites/roomComp");
            temp.Add(new RoomObjects(comp, new Vector2(700, 700),MathHelper.Pi));
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
            /**/
            score = 0;
            random = new Random();
            previousUpdateTime = TimeSpan.Zero;
            updateTime = TimeSpan.FromSeconds(1.0f);
            /**/
            
            List<string> gameover = new List<string>();
            List<string> intro = new List<string>();
            List<string> library = new List<string>();
            List<string> gas = new List<string>();
            List<string> gas2 = new List<string>();
            List<string> cafe = new List<string>();
            List<string> hotel = new List<string>();
            List<string> bank = new List<string>();
            List<string> endinfo = new List<string>();

            gameover.Add("The libarian has caught you in the act of installing a keylogger -GAME OVER-");
            gameover.Add("You have been caught trying to attach the Skimmer to the gas stations pump. -GAME OVER-");
            gameover.Add("You have been caught by a hotel employee while trying to compromise multiple VIP accounts. -GAME OVER-");
            gameover.Add("A cafe employee has noticed your suspicious behavior and discovered you in the act of installing the packer Sniffer -GAME OVER-");

            intro.Add("You received a text!");
            intro.Add("I have heard great stories about you! Lets Work together.");
            intro.Add("Further instructions on your first job have been sent.");
            intro.Add("You've got mail!");
            intro.Add("Central: Your first Job is to install this keylogger onto the");
            intro.Add("computer(s) at the library.");
            //intro.Add("Key Logger: A piece of software that captures keystrokes");
            intro.Add("Go to the Library");
            
            library.Add("Install keylogger onto one of the computers");
            library.Add("Do NOT get caught by the patrolling security guard");
            library.Add("Installing KeyLogger...");

            gas.Add("Central: Congratulations on installing the key logger!");
            gas.Add("Central: Money will be desposited based on the number of");
            gas.Add("facebook accounts compromised");
            gas.Add("Central: Your next job will being installing a skimmer at a gas station.");
            //gas.Add("A skimmer is a portable device attached onto or behind a legitimate scanner. It passively records data as you swipe/insert your card.");
            gas.Add("Go to the Gas Station ");
            gas.Add("Install the skimmer behind a gas pump");
            gas.Add("Installing Skimmer...");
            
            gas2.Add("Nicely done, Money will be desposited based on the number of");
            gas2.Add("Credit Cards compromised");
            gas2.Add("Central: Your next Job is to install a packet sniffer at the cafe.");
            gas2.Add("Go to the cafe");
            
            //cafe.Add("The Packet sniffer will intercept and log traffic passing over the network it is installed on and relay them to Central.");
            cafe.Add("Install the packet sniffer on the cafe's computer ");
            cafe.Add("Installing packet Sniffer...");
            
            hotel.Add("Nicely done, Money will be desposited based on the number of");
            hotel.Add("bank accounts compromised");
            hotel.Add("Good job, Your last job is to infiltrate the VIP hotel and install");
            hotel.Add("the trojan horse on the hotel's central computer.");
            //hotel.Add("The Trojan horse is a malicious piece of software that qill carry out specific tasks when executed.");
            hotel.Add("Go to VIP Hotel ");
            hotel.Add("Download the trojan horse onto the Hotel's Computer");
            hotel.Add("Downloading the trojan horse...");
            
            bank.Add("Central: Well Done, as reward, we have transferred");
            bank.Add("**$1,000,000** to your account.");
            
            endinfo.Add("New Text Message!");
            endinfo.Add("Your current bank account is over the limit,");
            endinfo.Add("please transfer the money into different accounts");
            endinfo.Add("Banker: Hello! How may we help you today?");
            endinfo.Add("You: I would like to transfer money from my account...");
            endinfo.Add("Banker: The account you are currently trying to access is empty.");
            endinfo.Add("Your account seems to have been compromised by a Sudanite.");
            endinfo.Add("Upon further investigation, you discovered a keylogger on your computer.");
            endinfo.Add("Key Logger: Software that captures keystokes");
            endinfo.Add("Packet Sniffer: Software that can intercept packets/info over the internet/wifi");
            endinfo.Add("Skimmer: Is a portable device that attaches to a legitimate scanner");
            endinfo.Add("and passively records data as you insert/swipe your card");
            endinfo.Add("Trojan Horse: a malicious piece of software that carries out actions when executed");
            story.Add(intro);
            story.Add(library);
            story.Add(gas);
            story.Add(gas2);
            story.Add(cafe);
            story.Add(hotel);
            story.Add(bank);
            story.Add(endinfo);
            story.Add(gameover);
            
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
            a.Add(new RoomObjects(map, new Vector2(370, 365),0,.39f));
            Texture2D pump = Content.Load<Texture2D>(@"Sprites\Pumps");
            a.Add(new RoomObjects(pump, new Vector2(400, 700), MathHelper.PiOver2,.25f));
            all.Add(a);
            loadRoom();
            loadLibrary();
            loadCafe();
            loadHotel();
            font = Content.Load<SpriteFont>("Courier New");
            bottomtext = new TextInterface();
            sidetext = new TextInterface();
            scoretext = new TextInterface();
            scoretext.Initialize(inst, new Vector2(750, 10), new Vector2(100, 75), font, "Score: 0", 300f, .75f, Color.Red);
            sidetext.Initialize(inst, new Vector2(750, 250), new Vector2(100, 180), font, "View Message", 300f, .75f, Color.White);
            bottomtext.Initialize(inst, new Vector2(0, 752), new Vector2(750, 17), font, "New Message", 1450f, 0.65f, Color.White);
            Texture2D police = Content.Load<Texture2D>(@"Sprites\POLICE");
            List<Vector2> temp = new List<Vector2>();
            temp.Add(new Vector2((750 * .75f)-60, 0));
            temp.Add(new Vector2((750 * .75f)-60, 550));
            n.Add(new Police(police, new Vector2((750 * .75f)-60, 0), temp));
            menu = Content.Load<Texture2D>(@"Sprites\MENU");
            last = Keyboard.GetState();
            Texture2D end = Content.Load<Texture2D>(@"Sprites\endScreen");
            List<RoomObjects> b = new List<RoomObjects>();
            Texture2D end2 = Content.Load<Texture2D>(@"Sprites\endScreen2");
            b.Add(new RoomObjects(end2, new Vector2(475, 400)));
            b.Add(new RoomObjects(end, new Vector2(475, 400)));
            all.Add(b);
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
            if(Keyboard.GetState().IsKeyDown(Keys.Q)) Exit();

            // TODO: Add your update logic here
            if (pause == true) { 
                if(Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    pause = false;
                }
            }
            else
            {
                p.update(gameTime);
                for (int i = 0; i < n.Count(); i++)
                {
                    n[i].update(gameTime);
                }
                /**/
                int userOne = 0;
                int passOne = 0;

                if (gameTime.TotalGameTime - previousUpdateTime > updateTime)
                {
                    previousUpdateTime = gameTime.TotalGameTime;

                    if (device.income >= 2)
                    {
                        if (random.Next(100) < 80)
                        {
                            score += 100 + device.income * 20;
                            userOne = random.Next(usernames.Count()) - 1;
                            passOne = random.Next(passwords.Count()) - 1;
                            changed = true;
                            scoretext.Update("score: " + score +
                                  ((changed) ? "\n\nUsername: " + usernames[userOne +1] + "\nPassword: " + passwords[passOne+1]
                                             : " "));
                        }
                    }
                }

                KeyboardState keyboard = Keyboard.GetState();
                if (keyboard.IsKeyDown(Keys.Space) && last.IsKeyUp(Keys.Space))
                {

                    if (bottomtext.counter < Game1.story[currentStory].Count())
                    {
                        bottomtext.Update(Game1.story[Game1.currentStory][bottomtext.counter]);
                        bottomtext.counter++;
                    }
                }

                /**/
                //if (bottomtext.keyboard.IsKeyDown(Keys.Space) && bottomtext.last.IsKeyUp(Keys.Space))
                //{
                //    if (bottomtext.counter < Game1.all[Game1.currentStory].Count)
                //    {
                //        bottomtext.counter++;
                //        bottomtext.Update(Game1.story[Game1.currentStory][bottomtext.counter]);
                //    }
                //}
                //sidetext.Update();
                //scoretext.Update();
                bottomtext.Update();
                if (device.income >= 0)
                {
                    sidetext.Update("Tools:\n -Keylogger");
                }
                if (device.income > 2)
                {
                    sidetext.Update("Tools:\n -Keylogger\n -Skimmer");
                }
                if (device.income > 5)
                {
                    sidetext.Update("Tools:\n -Keylogger\n -Skimmer\n -Sniffer");
                }
                if (device.income > 9)
                {
                    sidetext.Update("Tools:\n -Keylogger\n -Skimmer\n -Sniffer\n\n -Trojan Horse");
                }
                if (device.income > 11)
                {
                    sidetext.Update("Tools:\n -Keylogger\n -Skimmer\n -Sniffer\n\n -Trojan Horse\n -$1,000,000");
                }
                changed = false;
                last = keyboard;
            }
            if (pause == null)
            {
                keyboard = Keyboard.GetState();
                if (keyboard.IsKeyDown(Keys.Enter))
                {
                    z = 1;
                }
            }
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

            if (pause == true) { spriteBatch.Draw(menu, new Rectangle(0, 0, 1000, 800), Color.White); }
            else if (pause == false)
            {
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
            }
            if (pause == null)
            {
                int temp = all.Count -1;
                all[temp][z].draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
