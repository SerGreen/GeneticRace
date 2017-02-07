using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Collections;
using GeneticRace.Surface.GrassSurface;
using GeneticRace.Surface.RoadSurface;

namespace GeneticRace
{
    public partial class TestForm : Form
    {
        private bool running;
        private int FPS = 45;
        private int estimatedFrameTime;

        float grassFriction = 0.76f;
        float roadFriction = 0.97f;

        private World world;

        public TestForm()
        {
            InitializeComponent();
            scanMaps();
            saveList.SelectedIndex = 0;
            estimatedFrameTime = 1000 / FPS;
            Vector2F camera = new Vector2F(screen.Width, screen.Height);
            world = new World(loadMap(mapsList.SelectedIndex), camera);
            running = true;
            Thread thread = new Thread(new ThreadStart(tick));
            thread.Start();
        }

        private void tick()
        {
            Stopwatch sw = new Stopwatch();

            while (running)
            {
                sw.Reset();
                sw.Start();
                Bitmap bmp = new Bitmap(screen.Width, screen.Height);
                Graphics g = Graphics.FromImage(bmp);

                world.tick();
                world.render(g);

                try
                {
                    screen.Image = bmp;
                }
                catch (InvalidOperationException)
                { /* Объект используется другим потоком */ }

                g.Dispose();
                sw.Stop();
                if (sw.ElapsedMilliseconds < estimatedFrameTime)
                    Thread.Sleep((int)(estimatedFrameTime - sw.ElapsedMilliseconds));
            }
        }

        private void updateGenerationInfo()
        {
            List<Bot> bots = world.getBots();
            var activeBots = from bot in bots
                             where !bot.GoalAchieved && !bot.Kicked
                             select bot;

            valActive.Text = activeBots.Count().ToString() + "/" + bots.Count.ToString();
            valTime.Text = world.generationTime.ToString();
            valGen.Text = world.generationNumber.ToString();
        }

        private void updateBotsInfo()
        {
            if (!world.lockBots)
            {
                int populationSize = world.populationSize;

                if (listBots.Items.Count != populationSize)
                    createEmptyListBots(populationSize);

                List<Bot> bots = world.getBots();
                List<int> cp = world.getCP();

                int selectedID = -1;
                if (listBots.SelectedIndices.Count > 0)
                    selectedID = int.Parse(listBots.Items[listBots.SelectedIndices[0]].SubItems[1].Text);

                for (int i = 0; i < bots.Count; i++)
                {
                    try
                    {
                        int id = bots[i].Car.ID;

                        listBots.Items[i].Text = (i + 1).ToString();
                        listBots.Items[i].SubItems[1].Text = id.ToString();
                        listBots.Items[i].SubItems[2].Text = bots[i].Happiness.ToString();
                        listBots.Items[i].SubItems[3].Text = cp[i].ToString();
                        listBots.Items[i].SubItems[4].Text = bots[i].IdleTime.ToString();
                        listBots.Items[i].SubItems[5].Text = bots[i].GoalAchieved.ToString();
                        listBots.Items[i].SubItems[6].Text = bots[i].Kicked.ToString();

                        if (listBots.SelectedIndices.Count > 0)
                        {
                            if (i == listBots.SelectedIndices[0])
                            {
                                if (id != selectedID)
                                    updateRulesList();
                            }
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    { }
                }
            }
        }

        private void createEmptyListBots(int size)
        {
            listBots.Items.Clear();
            for (int i = 0; i < size; i++)
            {
                listBots.Items.Add("");
                listBots.Items[i].SubItems.Add("");
                listBots.Items[i].SubItems.Add("");
                listBots.Items[i].SubItems.Add("");
                listBots.Items[i].SubItems.Add("");
                listBots.Items[i].SubItems.Add("");
                listBots.Items[i].SubItems.Add("");
            }
        }

        private void updateRulesList()
        {
            if (listBots.SelectedIndices.Count > 0)
            {
                List<Bot> bots = world.getBots();
                int index = listBots.SelectedIndices[0];

                List<BotClasses.Rules.Rule> rules = bots[index].rules;
                if (rules.Count != listBots.Items.Count)
                    createEmptyRulesList(rules.Count);

                for (int i = 0; i < rules.Count; i++)
                {
                    listRules.Items[i].Text = (i + 1).ToString();
                    listRules.Items[i].SubItems[1].Text = rules[i].ToString();
                }
            }
        }

        private void createEmptyRulesList(int size)
        {
            listRules.Items.Clear();
            for (int i = 0; i < size; i++)
            {
                listRules.Items.Add("");
                listRules.Items[i].SubItems.Add("");
            }
        }

        private void TestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            running = false;
        }

        private void scanMaps()
        {
            int currentMapToCheck = 0;

            while (File.Exists("./map_" + currentMapToCheck + ".grm"))
            {
                mapsList.Items.Add("Map " + currentMapToCheck);
                currentMapToCheck++;
            }

            mapsList.SelectedIndex = 0;
        }

        private Map loadMap(int index)
        {
            Map map = new Map();

            if (File.Exists("./map_" + index +".grm"))
            {
                StreamReader sr = new StreamReader("./map_" + index + ".grm");
                string file = sr.ReadToEnd();
                sr.Close();

                string[] mapParts = file.Split('#');

                string[] startPointParts = mapParts[0].Split(':');
                map.StartPoint = new Vector2F(float.Parse(startPointParts[0]), float.Parse(startPointParts[1]));

                string[] checkPointsParts = mapParts[1].Split('\n');

                map.CheckPoints = new List<Polygon>();
                foreach (string checkPoint in checkPointsParts)
                {
                    string[] cpParts = checkPoint.Split('$');
                    ArrayList goalPoints = new ArrayList();
                    foreach (string goalPoint in cpParts)
                    {
                        string[] goalPointParts = goalPoint.Split(':');
                        goalPoints.Add(new Vector2F(float.Parse(goalPointParts[0]), float.Parse(goalPointParts[1])));
                    }
                    map.CheckPoints.Add(new Polygon(goalPoints));
                }

                string[] shapes = mapParts[2].Split('\n');
                foreach (string shape in shapes)
                {
                    string[] shapeParts = shape.Split('$');

                    if (int.Parse(shapeParts[1]) == 0)  //circle
                    {
                        string[] circleParts = shapeParts[2].Split(':');
                        Circle cir = new Circle(new Vector2F(float.Parse(circleParts[0]), float.Parse(circleParts[1])), float.Parse(circleParts[2]));

                        if (int.Parse(shapeParts[0]) == 0)   //grass
                            map.addSurfaceObject(new GrassCircle(cir, grassFriction));
                        else if (int.Parse(shapeParts[0]) == 1)   //road
                            map.addSurfaceObject(new RoadCircle(cir, roadFriction));
                    }
                    else if (int.Parse(shapeParts[1]) == 1)  //polygon
                    {
                        ArrayList pointsForSO = new ArrayList();

                        for (int i = 2; i < shapeParts.Length; i++)
                        {
                            string[] pointParts = shapeParts[i].Split(':');
                            pointsForSO.Add(new Vector2F(float.Parse(pointParts[0]), float.Parse(pointParts[1])));
                        }
                        Polygon pol = new Polygon(pointsForSO);

                        if (int.Parse(shapeParts[0]) == 0)   //grass
                            map.addSurfaceObject(new GrassPolygon(pol, grassFriction));
                        else if (int.Parse(shapeParts[0]) == 1)   //road
                            map.addSurfaceObject(new RoadPolygon(pol, roadFriction));
                    }
                }
            }

            return map;
        }

        private void saveGeneration_Click(object sender, EventArgs e)
        {
            world.saveGeneration(saveList.SelectedIndex);
        }

        private void loadGeneration_Click(object sender, EventArgs e)
        {
            world.loadGeneration(saveList.SelectedIndex);
        }

        private void screen_SizeChanged(object sender, EventArgs e)
        {
            world.setNewCameraSize(screen.Width, screen.Height);
        }

        private void uiUpdateTimer_Tick(object sender, EventArgs e)
        {
            updateBotsInfo();
            updateGenerationInfo();
        }

        private void listBots_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateRulesList();
            try
            {
                world.carToFollow = listBots.SelectedIndices[0];
            }
            catch (ArgumentOutOfRangeException)
            { }
        }

        private void resetGeneration_Click(object sender, EventArgs e)
        {
            world.setNewMap(loadMap(mapsList.SelectedIndex));
        }
    }
}
