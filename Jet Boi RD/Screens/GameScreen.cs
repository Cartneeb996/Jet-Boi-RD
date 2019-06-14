/*
 * Carter Neeb, Berkley Fair
 * June 14 2019
 * jetpack joyride esc game
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Drawing.Imaging;
using System.Media;
using System.IO;


namespace Jet_Boi_RD.Screens
{
    public partial class GameScreen : UserControl
    {
        #region globals
        public bool up = false;
        public bool grounded = true; // for gravity
        public bool jumping = false;

        Classes.Player player = new Classes.Player(50, 0); //player
        Pen playerPen = new Pen(Color.White);

        SolidBrush coinBrush = new SolidBrush(Color.Yellow); // for coins

        public const int pheight = 120;
        public const int pwidth = 80;
        const int longLaser = 200; //height width const
        const int midLaser = 150;
        const int smallLaser = 100;
        const int laserWidth = 50;

        int airDownFrames = 0; // for gravity

        int timeBtwnLasers = 240; 
        int tick = 0;
        Random r = new Random();
        
        public static int backgroundMoveSpd = 8; // scroll spd

        //things to remove
        Classes.laser laserToRemove;
        Classes.mechToken MTokenToRemove;
        Classes.rocket rocketToRemove;

        public static long dist; // pixel count
        public static long maxDist; 
        public static float actualDist; //actual distance

        public static int coinScore = 0; 
        int coinChance = 30;
        bool endGame = false; // when game is slowing down and stopping

        int bounce; // on death, adds "bounce"
        bool bounceUp = true; 

        bool started = false; // game started

        int cloudX = 0; // for scrolling clouds

        bool spawnBarrage = false; // for rockets
        int barrageSpawns = 0;
        

        public bool toClearLsers = false; 

        public bool keydown = false; // used to check if the key was pushed, mainly for teleporter

        int spdStorage = 0; // used when the player dies and chooses to reveive - it restores speed

        public static string curntMech = "none"; 

        public static bool abort = false; // if you don't have a upgrade purchased, it will stop the creation of a token

        public bool gravDown = true; //for gravity suit

        public bool teleporterUp = false; // for teleporter
        public int teleporterMarkerY = 0; // 

        public int upFrames = 0; // for super jump

        //image animations
        Image gifImage = Properties.Resources.character;
        FrameDimension dimension;
        Image gImage = Properties.Resources.gsuit;
        FrameDimension gdimension;   
        Image spImage = Properties.Resources.superJumper;
        FrameDimension spdimension;
        int frameCount;
        int frame = 0;

        //all the music and sounds
        System.Windows.Media.MediaPlayer death = new System.Windows.Media.MediaPlayer();
        System.Windows.Media.MediaPlayer alert = new System.Windows.Media.MediaPlayer();
        System.Windows.Media.MediaPlayer broken  = new System.Windows.Media.MediaPlayer();
        System.Windows.Media.MediaPlayer music = new System.Windows.Media.MediaPlayer();

        //all the object lists, and shop lists
        public static Dictionary<string, bool> mechs = new Dictionary<string, bool>();
        public static Dictionary<string, bool> upgrades = new Dictionary<string, bool>();
        public static Dictionary<string, bool> upgradesActv = new Dictionary<string, bool>();
        public static Dictionary<string, Dictionary<string, bool>> mechUpgs = new Dictionary<string, Dictionary<string, bool>>();
        static List<Classes.mechToken> MTokens = new List<Classes.mechToken>();
        static List<Classes.rocket> rockets = new List<Classes.rocket>();
        List<Classes.laser> lasers = new List<Classes.laser>();
        List<Classes.coin> coins = new List<Classes.coin>();
        #endregion

        public GameScreen()
        {
            InitializeComponent();
            cloudX = this.Width;

            //animations & music
            dimension = new FrameDimension(gifImage.FrameDimensionsList[0]);
            gdimension = new FrameDimension(gImage.FrameDimensionsList[0]);
            spdimension = new FrameDimension(spImage.FrameDimensionsList[0]);
            frameCount = gifImage.GetFrameCount(dimension);
            death.Open(new Uri(Application.StartupPath + "/death.mp3"));
            death.Volume = 1;
            alert.Open(new Uri(Application.StartupPath + "/alert.mp3"));
            broken.Open(new Uri(Application.StartupPath + "/broken.mp3"));
            broken.Volume = 0.1d;
            music.Open(new Uri(Application.StartupPath + "/music.mp3"));
            music.Volume = 0.25;

            backgroundMoveSpd = 8;

            lasers.Clear();
            rockets.Clear(); //clears objects
            MTokens.Clear();

            coinChance = 30;

            if (!Form1.start) // adds to shop lists for xmlload, only onstart
            {
                mechs.Add("superJump", false);
                mechs.Add("teleporter", false);
                mechs.Add("gravity", false);
                upgrades.Add("jumpBoost", false);
                upgradesActv.Add("jumpBoost", false); //unimplemented
                upgrades.Add("ironBoi", false);
                upgradesActv.Add("ironBoi", false); //unimplemented
                upgrades.Add("xray", false);
                upgradesActv.Add("xray", false); //unimplemented
                upgrades.Add("jammer", false);
                upgradesActv.Add("jammer", false); //unimplemented
                Dictionary<string, bool> a = new Dictionary<string, bool>(); //unimplemented
                a.Add("magnet", false); //unimplemented
                a.Add("golden", false); //unimplemented
                Dictionary<string, bool> b = new Dictionary<string, bool>(); //unimplemented
                b.Add("magnet", false); //unimplemented
                b.Add("golden", false); //unimplemented
                Dictionary<string, bool> c = new Dictionary<string, bool>(); //unimplemented
                c.Add("magnet", false); //unimplemented
                c.Add("golden", false); //unimplemented
                mechUpgs.Add("teleporter", a); //unimplemented
                mechUpgs.Add("superJump", b); //unimplemented
                mechUpgs.Add("gravity", c); //unimplemented

                Form1.start = true;
            }
            
             xmlLoad();
    
            Refresh(); 

            gameTimer.Enabled = false; //pauses until space is pressed
        }

        public static void xmlLoad()
        {

            //creates variables and xml reader needed
            XmlReader reader = XmlReader.Create("player1.xml");
            reader.ReadToFollowing("coin");
            coinScore = Convert.ToInt16(reader.ReadString());
            reader.ReadToFollowing("maxDist");
            maxDist = Convert.ToInt64(reader.ReadString());        
            Dictionary<string, bool> upgd = new Dictionary<string, bool>();

            foreach (KeyValuePair<string, bool> b in upgrades) // reads upgrades
            {
                reader.ReadToFollowing("upgrade");
                string key = reader.GetAttribute("name");
                upgd[key] = XmlConvert.ToBoolean(reader.GetAttribute("value").ToLower());
            }

            upgrades = upgd;
            Dictionary<string, bool> upgdA = new Dictionary<string, bool>();

            foreach (KeyValuePair<string, bool> b in upgradesActv)
            {
                reader.ReadToFollowing("upgrade");
                string key = reader.GetAttribute("name");
                upgdA[key] = XmlConvert.ToBoolean(reader.GetAttribute("value").ToLower());
            }

            upgradesActv = upgdA;
            reader.ReadToFollowing("mechs");
            Dictionary<string, bool> mch = new Dictionary<string, bool>();

            foreach (KeyValuePair<string, bool> b in mechs) // reads mechs
            {
                reader.ReadToFollowing(b.Key);
                mch[b.Key] = XmlConvert.ToBoolean(reader.ReadString().ToLower());
            }

            mechs = mch;
            reader.ReadToFollowing("mechUpgs");
            Dictionary<string, Dictionary<string, bool>> mchUpgd = new Dictionary<string, Dictionary<string, bool>>();

            foreach (KeyValuePair<string, Dictionary<string, bool>> c in mechUpgs) //obsolete
            {

                reader.ReadToFollowing(c.Key);
                Dictionary<string, bool> x = new Dictionary<string, bool>();
                foreach (KeyValuePair<string, bool> b in c.Value)
                {

                    reader.ReadToFollowing(b.Key);
                    x.Add(b.Key, XmlConvert.ToBoolean(reader.ReadString().ToLower()));

                }

                mchUpgd[c.Key] = x;
            }

            mechUpgs = mchUpgd;
            reader.Close();

            //death.Stop();
        }

        public static void xmlSave()
        {
            //writes all things to xml
            XmlWriter writer = XmlWriter.Create("player1.xml", null);
            writer.WriteStartElement("player");
            writer.WriteString("\n");
            writer.WriteElementString("coin", "" + coinScore);
            writer.WriteString("\n");
            writer.WriteElementString("maxDist", "" + maxDist);
            writer.WriteString("\n");
            writer.WriteStartElement("upgradesPurch");
            writer.WriteString("\n");

            foreach (KeyValuePair<string, bool> b in upgrades)
            {
                writer.WriteStartElement("upgrade");
                writer.WriteAttributeString("name", b.Key);
                writer.WriteAttributeString("value", "" + b.Value);
                writer.WriteEndElement();
                writer.WriteString("\n");
            }

            writer.WriteEndElement();
            writer.WriteString("\n");
            writer.WriteStartElement("upgradesActv");
            writer.WriteString("\n");

            foreach (KeyValuePair<string, bool> b in upgradesActv)
            {
                writer.WriteStartElement("upgrade");
                writer.WriteAttributeString("name", b.Key);
                writer.WriteAttributeString("value", "" + b.Value);
                writer.WriteEndElement();
                writer.WriteString("\n");
            }

            writer.WriteEndElement();
            writer.WriteString("\n");
            writer.WriteStartElement("mechs");
            writer.WriteString("\n");

            foreach (KeyValuePair<string, bool> b in mechs)
            {
                writer.WriteStartElement(b.Key);
                writer.WriteString("" + b.Value);
                writer.WriteEndElement();
                writer.WriteString("\n");
            }

            writer.WriteEndElement();
            writer.WriteString("\n");
            writer.WriteStartElement("mechUpgs");
            writer.WriteString("\n");

            foreach (KeyValuePair<string, Dictionary<string, bool>> c in mechUpgs)
            {
                writer.WriteStartElement(c.Key);
                writer.WriteString("\n");

                foreach (KeyValuePair<string, bool> b in c.Value)
                {
                    writer.WriteStartElement(b.Key);
                    writer.WriteString("" + b.Value);
                    writer.WriteEndElement();
                    writer.WriteString("\n");
                }

                writer.WriteEndElement();
                writer.WriteString("\n");
            }

            writer.WriteEndElement();
            writer.WriteString("\n");
            writer.Close();

        }

        public void GameOver()
        {
            if (dist > maxDist) maxDist = dist; // if you broke record, replace it

            xmlSave();
            gameTimer.Stop(); // stop doing things

            if (coinScore >= 0) 
            {
                revivePopup.dist = (int)(dist / 100); //sets the dist you travelled
                revivePopup rp = new revivePopup();   //then opens a new popup    
                rp.Location = this.FindForm().Location; // places in proper loc
                DialogResult result = rp.ShowDialog(); //show
               
                if (result == DialogResult.Yes) // if revive button pushed,
                {
                    gameTimer.Enabled = true; // renable timer
                    backgroundMoveSpd = spdStorage; // restore spd
                    timeBtwnLasers = 120; // reset this
                    coinScore -= 250; // remove coins
                    endGame = false; // reset
                    lasers.Clear(); //reset
                }
                else if (result == DialogResult.No) // if shop button
                {
                    Form1.switchScreen(this, "shop"); //switch to shop
                    actualDist = 0; //reset dist
                    dist = 0;
                }
                else if (result == DialogResult.OK) // obselete
                {
                    ShopScreen.switchS = true;
                    Form1.switchScreen(this, "shop");
                    
                    dist = 0;
                    actualDist = 0;
                }
                else if (result == DialogResult.Abort)
                {
                    FindForm().Close(); // close game
                }
            }
            else //obselete
            {
                Form1.switchScreen(this, "shop");
                
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            tick++; // tick
            cloudX -= backgroundMoveSpd; // move cloud
            dist += backgroundMoveSpd; // add to dist
            actualDist = dist / 100; // actual distance travelled

            if (endGame && tick % 15 == 0) // for bounces and slowing down after being killed
            {
                if (backgroundMoveSpd > 0) backgroundMoveSpd--;
                else if (bounce < 10 && backgroundMoveSpd == 0) GameOver(); // if fully stopped, gameover
            }

            if (endGame && player.hb.Bottom >= this.Height && !bounceUp) // if player hits bottom, bounce up
            {
                bounceUp = true;
                bounce /= 2;
                if (bounce < 15) bounce = 0;
            }

            if (player.hb.Bottom > this.Height - bounce && bounceUp && endGame) // if less than bounce height, keep moving up
            {
                player.move(-10);
                airDownFrames = 0;
            }
            else
            {
                bounceUp = false; // turn around and fall down

            }

            if (r.Next(0, 101) < coinChance && tick % 120 == 0 && !endGame) // if game isnt over, and chance is met, generate a coin pattern
            {
                generateCoin(r.Next(0, 3), r.Next(100, this.Height - 100));
            }

            if (r.Next(0, 2) == 0 && tick % 1200 == 0 && !endGame && curntMech == "none") // every 1200 ticks, have a chance to generate a mech token
            {
                Classes.mechToken m = new Classes.mechToken(this.Width, r.Next(0, this.Height - 50));

                if (!abort) // if the player doesn't have the generated mech, stop the generation
                {
                    MTokens.Add(m);
                }
                else abort = false;
            }

            if (tick == 180) // generates startup laser
            {
                lasers.Add(new Classes.laser(this.Width, 10, midLaser, laserWidth, Properties.Resources.laserV));
            }
            else if (tick % timeBtwnLasers == 0 && !endGame) //from here on out, generate a laser every timebtwnlaser ticks
            {
                generateLaser(player.y);
            }

            if (tick % 1200 == 0 && coinChance < 35 && !endGame) coinChance += 5; // every 1200 ticks increase chance to get coins

            if (tick % 240 == 0) // move the cloud to front of screen
            {
                cloudX = this.Width;
            }

            if (tick % 550 == 0 && !endGame) // movespeed increase every 550 ticks
            {

                if (backgroundMoveSpd < 20) // caps speed at 20
                {
                    backgroundMoveSpd += 2;
                }
            }

            if (tick % 240 == 0) //every 240 ticks, decrease spawn timeout for lasers
            {
                if (timeBtwnLasers > 20) timeBtwnLasers -= 15;
            }

            if(tick % 600 == 0 && r.Next(0,3) == 0 && !upgrades["jammer"]) // spawn rockets on 1/3 chance, every 600 ticks, and if you haven't bought jammer
            {
                if (r.Next(0, 5) == 0) // 1/5 chance to spawn a 5 rocket barrage
                {
                    spawnBarrage = true;
                    barrageSpawns = 0;
                }

                Classes.rocket rc = new Classes.rocket(this.Width - 80, player.y + 25); // create a rocket that follows the player

                alert.Stop(); //play alert
                alert.Play();
                rockets.Add(rc);
            }

            if(spawnBarrage && tick % 30 == 0 && barrageSpawns < 5) // spawn barrage rockets every 30 ticks until 5 are spawned
            {
                Classes.rocket rc = new Classes.rocket(this.Width - 80, player.y + 25);
                rockets.Add(rc);
                barrageSpawns++;
            }
            
            switch (curntMech) //based on mech, do different physics
            {
                case "none":
                    standardGrounded();
                    standardUp();
                    standardGav();
                    standardJump();
                    break;
                case "teleporter":
                    grounded = true;
                    break;
                case "superJump":
                    superJumpUp();
                    superJumpGav();
                    break;
                case "gravity":
                    GsuitGav();
                    break;
            }

            if (endGame) up = false; // stop the player from flying if they got hit
            Refresh();
        }

        private void GameScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !endGame && curntMech != "gravity") // if space, jump on ground, or fly in air
            {     
                if (grounded) // boost
                {
                    grounded = false;
                    jumping = true;
                    up = true;
                }
                else if (!endGame)
                {
                    up = true;
                }
            }
            else if (e.KeyCode == Keys.Escape && !endGame) //if pause button is pressed
            {
                pausePopup rp = new pausePopup();
                rp.Location = this.FindForm().Location;
                gameTimer.Enabled = false;
                DialogResult result = rp.ShowDialog();



                if (result == DialogResult.Yes) //if continuing
                {
                    gameTimer.Enabled = true;
                    backgroundMoveSpd = spdStorage;
                    timeBtwnLasers = 120;
                    coinScore -= 250;
                    endGame = false;
                    lasers.Clear();
                }
                else if (result == DialogResult.No)// if quitting to shop
                {
                    dist = 0;
                    actualDist = 0;
                    Refresh();
                    ShopScreen.switchS = true;
                    Form1.switchScreen(this, "shop");
                }
            }
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //draw sky
            SolidBrush g = new SolidBrush(Color.Green);
            SolidBrush b = new SolidBrush(Color.SkyBlue);
            e.Graphics.FillRectangle(g, 0, this.Height - 100, this.Width, 100);
            e.Graphics.FillRectangle(b, 0, 0, this.Width, this.Height - 100);
            e.Graphics.DrawImage(Properties.Resources.cloud, cloudX, 50, 100, 50); //clouds

            if(!started) // display startup message
            {
                Font s = new Font(Font.FontFamily, 20);

                e.Graphics.DrawString("Press Green to Play!", s, new SolidBrush(Color.Black), (this.Width - 300) / 2, 100);
            }
            //decides animation frames
            if (!up && !grounded) frame = 9;
            if (up && curntMech != "superJump") frame = 8;
            if (grounded && !up && tick % 4 == 0) frame++;
            if (curntMech == "superJump" && tick % 4 == 0) frame++;
            if (endGame) frame = 10;

            switch (curntMech) //draws image based on mech/character
            {           
                case "none":
                    frameCount = gifImage.GetFrameCount(dimension);
                    if (frame >= 8 && grounded && !up && !endGame) frame = 0;
                    e.Graphics.DrawImage(gifImage, player.hb);
                    gifImage.SelectActiveFrame(dimension, frame);           
                    break;
                case "teleporter":
                    e.Graphics.DrawImage(Properties.Resources.teleporter, player.hb.X, player.hb.Y);
                    break;
                case "superJump":
                    frameCount = spImage.GetFrameCount(dimension);
                    if (frame >= 8 && !endGame) frame = 0;
                    e.Graphics.DrawImage(spImage, player.x, player.y);
                    spImage.SelectActiveFrame(spdimension, frame);
                    break;
                case "gravity":
                    frameCount = gImage.GetFrameCount(dimension);
                    if (frame >= 8 && grounded && !up && !endGame) frame = 0;
                    e.Graphics.DrawImage(gImage, player.x, player.y);
                    gImage.SelectActiveFrame(gdimension, frame);
                    break;
            }
          
            if (curntMech == "teleporter") teleporterDraw(e.Graphics, teleporterMarkerY); // draws teleporter cursor

            foreach (Classes.rocket l in rockets)
            {
                if (l.launchingRocket && l.launchingTicks < 180) // displays warning, and tracks player
                {
                    l.launchingTicks++;
                    l.hb.Y = player.y + 25;
                    l.y = player.y + 25;
                    e.Graphics.DrawImage(l.i, l.hb);
                }
                if(l.launchingTicks == 180 && l.launchingRocket) // sets rocket to player location
                {
                    l.hb.Y = player.y + 25;
                    l.y = player.y + 25;
                    l.launchingRocket = false;
                }
                if (!l.launchingRocket) // shoots rocket at player
                {                        
                    l.move();
                    e.Graphics.DrawImage(l.i, l.hb);

                    if (l.hb.Right <= 0) rocketToRemove = l; // out of bounds, delete

                    if (player.hb.IntersectsWith(l.hb)) // if hits player, kill player and delete
                    {
                        if (!endGame && curntMech == "none") // kill
                        {
                            endGame = true;
                            death.Stop();
                            death.Play();
                            spdStorage = backgroundMoveSpd;
                            bounce = (this.Height - player.y) / 2;
                        }
                        else if (curntMech != "none") // destroy mech instead of killing
                        {
                            broken.Stop();
                            broken.Play();
                            toClearLsers = true; // clears all objects
                            grounded = false;
                            curntMech = "none";
                        }
                    }
                }
            }

            foreach (Classes.laser l in lasers) 
            {
                l.move();
                e.Graphics.DrawImage(l.i, l.hb); //display laser

                if (l.hb.Right <= 0) laserToRemove = l; // remove if out of bounds

                if (player.hb.IntersectsWith(l.hb)) //if hits player
                {
                    if (!endGame && curntMech == "none")// kill player
                    {
                        death.Stop();
                        death.Play();
                        spdStorage = backgroundMoveSpd;                      
                        endGame = true;
                        bounce = (this.Height - player.y) / 2;
                    }
                    else if (curntMech != "none") //destroy mech
                    {
                        broken.Stop();
                        broken.Play();
                        toClearLsers = true;
                        grounded = false;
                        curntMech = "none";
                    }
                }
            }

            if (toClearLsers)
            {
                lasers.Clear();
                rockets.Clear();
                toClearLsers = false;
            }

            List<Classes.coin> delete = new List<Classes.coin>(); // coins to delete

            foreach (Classes.coin c in coins)
            {
                if (c.hb.IntersectsWith(player.hb)) // if player hits coins
                {
                    coinScore++;
                    //coinToRemove = c;
                    delete.Add(c);
                }
                else
                {
                    c.move(); // move and draw
                    e.Graphics.FillEllipse(coinBrush, c.hb);

                    if (c.hb.Right <= 0) // if ooB, delete
                    {
                        delete.Add(c);
                    }
                }
            }

            foreach (Classes.mechToken m in MTokens)
            {   
                if (m.hb.IntersectsWith(player.hb)) //if player hits mech token
                {
                    MTokenToRemove = m;
                    curntMech = m.type; // set player mech to token type
                    lasers.Clear();
                    rockets.Clear();
                    broken.Play();
                }
                else
                {
                    m.move();
                    e.Graphics.FillRectangle(coinBrush, m.hb); //move and draw

                    if (m.hb.Right <= 0) // if ooB, delete
                    {
                        MTokenToRemove = m;

                    }
                }

                if (upgrades["xray"]) // if xray is bought, you can see the type of mech token
                {
                    string s = "";
                    Font f = new Font(Font.FontFamily, 20);

                    if (m.type == "teleporter")
                    {
                        s = "T";
                    }
                    else if (m.type == "superJump")
                    {
                        s = "S";
                    }
                    else if (m.type == "gravity")
                    {
                        s = "G";
                    }

                    e.Graphics.DrawString(s, f, new SolidBrush(Color.Black), m.hb.X + 10, m.hb.Y + 10);
                }
            }
            //draws coin and distance
            Font fr = new Font("Arial", 16);          
            e.Graphics.DrawString("Coins " + coinScore, fr, new SolidBrush(Color.Black), 0, 10);
            e.Graphics.DrawString("Distance " + actualDist +"m", fr, new SolidBrush(Color.Black), 0, 30);
            
            //removes objects
            RemoveLaser(laserToRemove);
            RemoveCoin(delete);
            RemoveMToken(MTokenToRemove);
            MTokenToRemove = null;
            coinToRemove = null;
            laserToRemove = null;
        }

        //removing func
        public void RemoveLaser(Classes.laser l)
        {
            lasers.Remove(l);
        }

        public void RemoveCoin(Classes.coin l)
        {
            coins.Remove(l);
        }

        public void RemoveCoin(List<Classes.coin> l)
        {
            foreach (Classes.coin c in l)
            {
                coins.Remove(c);
            }
        }

        public void RemoveMToken(Classes.mechToken m)
        {
            MTokens.Remove(m);
        }
        public void RemoveRocket(Classes.rocket r)
        {
            rockets.Remove(r);
        }

        private void GameScreen_Load(object sender, EventArgs e) // when gamescreen is loaded, reset player and play music
        {
            player.y = this.Height - pheight;
            player.hb.Y = this.Height - pheight;
            music.Stop();
            music.Play();
            music.MediaEnded += music_Loop;

        }

        private void music_Loop(object sender, EventArgs e) // loops music
        {
            music.Stop();
            music.Play();
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e) // mainly used for teleporter
        {
            up = false;
            keydown = false;
        }

        private void generateCoin(int pattern, int pos) // generates coins based on pattern and pos
        {
            List<Classes.coin> c = new List<Classes.coin>();

            switch (pattern)
            {
                case 0: //creates block of coins
                    for (int x = 0; x < 5; x++)
                    {
                        for (int y = pos; y < pos + 70; y += 10)
                        {
                            c.Add(new Classes.coin(this.Width + x * 10, y)); 
                        }
                    }

                    break;
                case 1: //creates check mark
                    c.Add(new Classes.coin(this.Width, pos - 12));
                    for (int x = 0; x > -5; x--)
                    {

                        c.Add(new Classes.coin(this.Width - (x - 1) * 10, pos + x * 10));

                    }
                    break;
                case 2: // creates sin wave
                    int n;
                    n = r.Next(20, 100);
                    for (int x = 0; x < 18; x++)
                    {
                        c.Add(new Classes.coin(this.Width + x * n, pos + n * (float)Math.Sin(45 * x * (Math.PI / 180))));
                    }
                    break;
                case 3:

                    break;
                case 4:
                    break;
            } 

            foreach (Classes.coin coin in c) // adds coins to actual list
            {
                coins.Add(coin);
            }
        }

        private void generateLaser(int py) //takes player y, and spawns laser
        {
            int w = 0;
            int l = 0;
            int y = 0;

            Image i = Properties.Resources.laserV;

            switch (r.Next(0, 2))
            {
                case 0: // vert laser
                    i = Properties.Resources.laserV;

                    switch (r.Next(0, 3))
                    {
                        case 0:
                            l = smallLaser;         
                            break;
                        case 1:
                            l = midLaser;
                            break;
                        case 2:
                            l = longLaser;
                            break;
                    }

                    if (py <= l / 2) y = 0; // if spawn above screen, place at top instead of ooB
                    else y = py - l / 2;

                    w = laserWidth;
                    break;
                case 1: // horiz laser
                    i = Properties.Resources.laserH;

                    switch (r.Next(0, 3))
                    {
                        case 0:
                            w = smallLaser;
                            break;
                        case 1:
                            w = midLaser;
                            break;
                        case 2:
                            w = longLaser;
                            break;
                    }

                    if (py <= w / 2) y = 0; 
                    else y = py - w / 2;

                    l = laserWidth;
                    break;
            }
            if (py <= l) y = 0; // if spawn above screen, place at top instead of ooB
            else y = py - l / 2;

            lasers.Add(new Classes.laser(this.Width, y, l, w, i));
        }

        public void teleporterMove(int y) //teleporter move func
        {
            player.moveTo(y);
        }

        public void teleporterDraw(Graphics g, int y) // draws cursor
        {
            if (y >= this.Height - pheight) // goes up,
            {
                teleporterUp = true;
            }
            if (y < 0) //then down
            {
                teleporterUp = false;
            }


            if (teleporterUp) y -= 10; // up
            else y += 10; //down

            g.DrawRectangle(new Pen(Color.Black), player.x, y, pwidth, pheight); //draw cursor
            teleporterMarkerY = y;
        }

        private void GameScreen_KeyPress(object sender, KeyPressEventArgs e) //used for teleporter and gravity so they can't cheat
        {
            if (e.KeyChar == (char)Keys.Space && !keydown)
            {
                switch (curntMech)
                {
                    case "none":
                        break;
                    case "teleporter":
                        teleporterMove(teleporterMarkerY);
                        break;
                    case "superJump":
                        break;
                    case "gravity":
                        gravDown = !gravDown;
                        gImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        airDownFrames = 0;
                        break;
                }

                keydown = true; // ensures you can't press and hold space
            }
            if (e.KeyChar == (char)Keys.Space && !started) // for when the game is waiting for user to press space to start
            {
                started = true;
                gameTimer.Enabled = true;
            }

        }

        #region gravities
        public void standardGav() // the standard gravity
        {
            //down: lower means stronger pull down
            //airDownFrames: no frames the player isn't going up (for gravity affect)
            if (player.hb.Bottom < this.Height) // if player is not touching bottom of screen
            {
                if (!up)
                {
                    int down = 20;
                    if (upgrades["ironBoi"]) down = 10;
                   
                    if (player.hb.Bottom < this.Height) //if not touching bottom, gravity
                    {
                        player.move(2 + (int)Math.Floor(Math.Pow(airDownFrames, 2) / down));
                    }

                    if (airDownFrames < 15) airDownFrames++;
                }
            }
        }

        public void GsuitGav() //same gravity as normal, it just flips the direction of pull when space is pressed
        {
            if (gravDown)
            {
                if (player.hb.Bottom < this.Height) // if player is not touching bottom of screen (grav = down)
                {
                    if (player.hb.Bottom < this.Height)
                    {
                        player.move(2 + (int)Math.Floor(Math.Pow(airDownFrames, 2) / 30));
                    }

                    if (airDownFrames < 20) airDownFrames++;
                }
                else airDownFrames = 0;
            }
            else
            {
                if (player.hb.Top > 0) // if player is not touching bottom of screen (grav = up)
                {
                    if (player.hb.Top > 0)
                    {
                        player.move(-(2 + (int)Math.Floor(Math.Pow(airDownFrames, 2) / 30)));
                    }

                    if (airDownFrames < 20) airDownFrames++;
                }
                else airDownFrames = 0;
            }
        }

        public void superJumpGav() // same as normal, but the player "floats" more on downfall
        {
            if (player.hb.Bottom < this.Height) // if player is not touching bottom of screen
            {
                if (player.hb.Bottom < this.Height)
                {
                    player.move(1 + (int)Math.Floor(Math.Pow(airDownFrames, 2) / 20));
                }

                if (upFrames > 0 && !jumping) upFrames--;

                if (airDownFrames < 12 && !grounded) airDownFrames++;
            }
            else
            {
                grounded = true;
                airDownFrames = 0;
            }

        }
        #endregion

        #region ups
        public void standardUp() // controlls the players up movement
        {
            if (up) // if player is pressing space, go up 
            {
                if (player.hb.Y > 0)
                {                 
                    player.move(-8);
                }
                if (airDownFrames > 0) airDownFrames -= 2; // decrease down frames to simulate inertia
            }
        }

        public void superJumpUp() //makes the jump more powerful, and gradual instead of instantanous
        {
            if (jumping && upFrames < 20)
            {
                if (player.hb.Y > 0)
                {
                    player.move(-15);
                    upFrames++;
                }

            }
            else jumping = false;
        }
        #endregion

        #region jumps
        public void standardJump() // player jumps if on ground
        {
            if (jumping)
            {
                int m = -30;

                if (upgrades["jumpBoost"]) // if upgrade purch, jump higher
                {
                    m = -40;
                }

                player.move(m);
                jumping = false;
            }
        }


        #endregion
        public void standardGrounded() // this is used to tell if player touches the bottom (aka ground)
        {
            if (player.hb.Bottom >= this.Height) // if player is touching bottom of screen
            {
                grounded = true;
            }
        }
    }
}
