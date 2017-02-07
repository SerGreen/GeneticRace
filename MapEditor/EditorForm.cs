using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GeneticRace;
using System.Collections;
using GeneticRace.BaseEntities;
using GeneticRace.Surface.RoadSurface;
using GeneticRace.Surface.GrassSurface;
using System.IO;

namespace MapEditor
{
    public partial class EditorForm : Form
    {
        int material;       //0=grass, 1=road, 2=mud, 3=wall
        int tool;           //0=circle, 1=polygon, 2=startPoint, 3=endPolygon
        ArrayList pointsForPolygon;
        Vector2F pointForCircle;
        Vector2F startPoint;
        List<Polygon> checkPoints;
        Vector2F point;
        bool adjustPoint;
        List<SurfaceObject> surfaceObjects;
        //List<Circle> circles;
        //List<Polygon> polygons;

        float grassFriction = 0.81f;
        float roadFriction = 0.97f;
        //float mudFriction = 0.64f;

        Bitmap bmp;
        Graphics g;

        Brush bRe = new SolidBrush(Color.Red);
        Brush bOr = new SolidBrush(Color.Orange);
        Pen pRe = new Pen(Color.Red);
        Pen pYe = new Pen(Color.Yellow);

        public EditorForm()
        {
            InitializeComponent();
            
            bmp = new Bitmap(screen.Width, screen.Height);
            g = Graphics.FromImage(bmp);
            renderTimer.Enabled = true;

            materialBox.SelectedIndex = 0;
            bPolygon.Enabled = false;
            tool = 1;
            adjustPoint = false;

            checkPoints = new List<Polygon>();
            pointsForPolygon = new ArrayList();
            //circles = new List<Circle>();
            //polygons = new List<Polygon>();
            surfaceObjects = new List<SurfaceObject>();
            pointForCircle = null;
            point = null;
        }

        private void materialBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            material = materialBox.SelectedIndex;
        }

        private void bCircle_Click(object sender, EventArgs e)
        {
            tool = 0;
            enableAll();
            bPolyAccept.Enabled = false;
            bDeleteLast.Enabled = false;
            bCircle.Enabled = false;
        }

        private void bPolygon_Click(object sender, EventArgs e)
        {
            tool = 1;
            enableAll();
            bPolygon.Enabled = false;
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            tool = 2;
            enableAll();
            bStart.Enabled = false;
            bPolyAccept.Enabled = false;
            bDeleteLast.Enabled = false;
        }

        private void bGoal_Click(object sender, EventArgs e)
        {
            tool = 3;
            enableAll();
            bGoal.Enabled = false;
        }

        private void bPolyAccept_Click(object sender, EventArgs e)
        {
            if (pointsForPolygon.Count > 2)
            {
                Polygon poly = new Polygon(pointsForPolygon);

                if (tool == 1)
                {
                    if (material == 0)
                        surfaceObjects.Add(new GrassPolygon(poly, grassFriction));
                    else if (material == 1)
                        surfaceObjects.Add(new RoadPolygon(poly, roadFriction));    
                }
                else if (tool == 3)
                {
                    checkPoints.Add(poly);
                }
                
                pointsForPolygon = new ArrayList();
            }
        }

        private void bDeleteLast_Click(object sender, EventArgs e)
        {
            if (pointsForPolygon.Count > 0)
                pointsForPolygon.RemoveAt(pointsForPolygon.Count - 1);
        }

        private void screen_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Vector2F(e.X, e.Y);

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (tool == 0)
                {
                    pointForCircle = point;
                }
                else if (tool == 1)
                {
                    adjustPoint = true;
                    pointsForPolygon.Add(point);
                }
                else if (tool == 2)
                {
                    startPoint = point;
                }
                else if (tool == 3)
                {
                    adjustPoint = true;
                    pointsForPolygon.Add(point);
                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                bool checkPointDeleted = true;

                for (int i = checkPoints.Count - 1; i >= 0; i--)
                {
                    if (checkPoints[i].isPointInside(point))
                    {
                        checkPoints.RemoveAt(i);
                        checkPointDeleted = false;
                        break;
                    }
                }

                if (checkPointDeleted)
                {
                    for (int i = surfaceObjects.Count - 1; i >= 0; i--)
                    {
                        if (surfaceObjects[i].Shape.isPointInside(point))
                        {
                            surfaceObjects.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
        }

        private void screen_MouseUp(object sender, MouseEventArgs e)
        {
            point = new Vector2F(e.X, e.Y);

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (tool == 0)
                {
                    Circle cir = new Circle(pointForCircle, pointForCircle.getDistanceTo(point));

                    if (material == 0)
                        surfaceObjects.Add(new GrassCircle(cir, grassFriction));
                    else if (material == 1)
                        surfaceObjects.Add(new RoadCircle(cir, roadFriction));

                    pointForCircle = null;
                }
                else if (tool == 1)
                {
                    adjustPoint = false;
                    pointsForPolygon[pointsForPolygon.Count - 1] = point;
                }
                else if (tool == 3)
                {
                    adjustPoint = false;
                    pointsForPolygon[pointsForPolygon.Count - 1] = point;
                }
            }
        }

        private void renderTimer_Tick(object sender, EventArgs e)
        {
            //Bitmap bmp = new Bitmap(screen.Width, screen.Height);
            //Graphics g = Graphics.FromImage(bmp);

            foreach (SurfaceObject so in surfaceObjects)
                so.paint(g);

            if (pointForCircle != null)
            {
                float dist = pointForCircle.getDistanceTo(point);
                g.FillEllipse(bRe, pointForCircle.X - 3, pointForCircle.Y - 3, 6, 6);
                g.DrawEllipse(pRe, pointForCircle.X - dist, pointForCircle.Y - dist, dist * 2, dist * 2);
            }

            if (pointsForPolygon.Count > 0)
            {
                for (int i = 0; i < pointsForPolygon.Count; i++)
                {
                    Vector2F a = (Vector2F)pointsForPolygon[i];
                    Vector2F b = (Vector2F)pointsForPolygon[(i + 1) % pointsForPolygon.Count];

                    if (i == pointsForPolygon.Count - 1)
                        g.FillEllipse(bRe, a.X - 3, a.Y - 3, 6, 6);
                    else
                        g.FillEllipse(bOr, a.X - 3, a.Y - 3, 6, 6);

                    if (pointsForPolygon.Count > 1)
                    {
                        g.DrawLine(pRe, a, b);
                    }
                }
            }

            if (startPoint != null)
            {
                g.DrawEllipse(pYe, startPoint.X - 10, startPoint.Y - 10, 20, 20);
            }

            if (checkPoints != null)
            {
                for (int i = 0; i < checkPoints.Count; i++)
                {
                    for (int j = 0; j < checkPoints[i].Points.Count; j++)
                    {
                        Vector2F a = (Vector2F)checkPoints[i].Points[j];
                        Vector2F b = (Vector2F)checkPoints[i].Points[(j + 1) % checkPoints[i].Points.Count];

                        g.DrawLine(pYe, a, b);
                    }
                }
            }

            //g.Dispose();
            screen.Image = bmp;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            if (startPoint == null)
                MessageBox.Show("Please add Start Point first", "Fail", MessageBoxButtons.OK);
            else if (checkPoints.Count == 0)
                MessageBox.Show("Please add some Check Points first", "Fail", MessageBoxButtons.OK);
            else
            {
                if (File.Exists("./map.grm"))
                    File.Delete("./map.grm");

                StreamWriter sr = null;
                string file = "";

                file = file + startPoint.X + ":" + startPoint.Y;
                file = file + "#";

                for (int j = 0; j < checkPoints.Count; j++)
                {
                    if (j > 0)
                        file = file + "\n";
                    for (int i = 0; i < checkPoints[j].Points.Count; i++)
                    {
                        if (i > 0)
                            file = file + "$";

                        Vector2F p = (Vector2F)checkPoints[j].Points[i];
                        file = file + p.X + ":" + p.Y;
                    }
                }
                file = file + "#";

                for (int j = 0; j < surfaceObjects.Count; j++)
                {
                    SurfaceObject so = surfaceObjects[j];

                    if (j > 0)
                        file = file + "\n";

                    if (so is Grass)
                        file = file + "0$";
                    else if (so is Road)
                        file = file + "1$";

                    if (so.Shape is Circle)
                    {
                        Circle cir = (Circle)so.Shape;
                        file = file + "0$";
                        file = file + cir.Center.X + ":" + cir.Center.Y + ":" + cir.Radius;
                    }
                    else if (so.Shape is Polygon)
                    {
                        file = file + "1$";

                        for (int i = 0; i < ((Polygon)so.Shape).Points.Count; i++)
                        //foreach (Vector2F p in ((Polygon)so.Shape).Points)
                        {
                            if (i > 0)
                                file = file + "$";

                            Vector2F p = (Vector2F)((Polygon)so.Shape).Points[i];
                            file = file + p.X + ":" + p.Y;
                        }
                    }
                }

                sr = new StreamWriter("./map.grm");
                sr.Write(file);
                sr.Close();
            }
        }

        private void bLoad_Click(object sender, EventArgs e)
        {
            if (File.Exists("./map.grm"))
            {
                StreamReader sr = new StreamReader("./map.grm");
                string file = sr.ReadToEnd();
                sr.Close();

                string[] mapParts = file.Split('#');

                string[] startPointParts = mapParts[0].Split(':');
                startPoint = new Vector2F(float.Parse(startPointParts[0]), float.Parse(startPointParts[1]));

                string[] checkPointsParts = mapParts[1].Split('\n');
                
                checkPoints = new List<Polygon>();
                foreach (string checkPoint in checkPointsParts)
                {
                    string[] cpParts = checkPoint.Split('$');
                    ArrayList goalPoints = new ArrayList();
                    foreach (string goalPoint in cpParts)
                    {
                        string[] goalPointParts = goalPoint.Split(':');
                        goalPoints.Add(new Vector2F(float.Parse(goalPointParts[0]), float.Parse(goalPointParts[1])));
                    }
                    checkPoints.Add(new Polygon(goalPoints));
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
                            surfaceObjects.Add(new GrassCircle(cir, grassFriction));
                        else if (int.Parse(shapeParts[0]) == 1)   //road
                            surfaceObjects.Add(new RoadCircle(cir, roadFriction));
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
                            surfaceObjects.Add(new GrassPolygon(pol, grassFriction));
                        else if (int.Parse(shapeParts[0]) == 1)   //road
                            surfaceObjects.Add(new RoadPolygon(pol, roadFriction));
                    }
                }
            }
        }

        private void screen_MouseMove(object sender, MouseEventArgs e)
        {
            point = new Vector2F(e.X, e.Y);

            if (adjustPoint)
                pointsForPolygon[pointsForPolygon.Count - 1] = point;
        }

        private void enableAll()
        {
            bCircle.Enabled = true;
            bPolygon.Enabled = true;
            bPolyAccept.Enabled = true;
            bDeleteLast.Enabled = true;
            bStart.Enabled = true;
            bGoal.Enabled = true;
        }

        private void screen_SizeChanged(object sender, EventArgs e)
        {
            bmp = new Bitmap(screen.Width, screen.Height);
            g = Graphics.FromImage(bmp);
        }
    }
}
