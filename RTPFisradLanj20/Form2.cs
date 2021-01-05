using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dicom;
using Dicom.Imaging;
using Dicom.Imaging.LUT;
using Dicom.Imaging.Codec;
using Dicom.IO;
using Dicom.Media;

namespace RTPFisradLanj20
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            groupBox8.Enabled = true;
            groupBox9.Enabled = false;
            groupBox10.Enabled = false;
            groupBox11.Enabled = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
                var img = new DicomImage(ofd.FileName);
                ImageManager.SetImplementation(WinFormsImageManager.Instance);
                pictureBox1.Image = img.RenderImage().AsClonedBitmap();

                var file = DicomFile.Open(textBox1.Text);             // Alt 1

                try
                {
                    var getInfoDicom = file.Dataset.GetSingleValue<string>(DicomTag.PatientName);
                    textBox2.Text = getInfoDicom.ToString();
                }
                catch (Exception)
                {
                    textBox2.Text = "NA";
                }

                try
                {
                    var getInfoDicom = file.Dataset.GetSingleValue<string>(DicomTag.PatientSex);
                    textBox3.Text = getInfoDicom.ToString();
                }
                catch (Exception)
                {
                    textBox3.Text = "NA";
                }

                try
                {
                    var getInfoDicom = file.Dataset.GetSingleValue<string>(DicomTag.SliceThickness);
                    textBox4.Text = getInfoDicom.ToString();
                }
                catch (Exception)
                {
                    textBox4.Text = "NA";
                }

                try
                {
                    var getInfoDicom = file.Dataset.GetSingleValue<string>(DicomTag.ImagePositionPatient);
                    textBox5.Text = getInfoDicom.ToString();
                }
                catch (Exception)
                {
                    textBox5.Text = "NA";
                }

                try
                {
                    var getInfoDicom = file.Dataset.GetSingleValue<string>(DicomTag.ImageOrientationPatient);
                    textBox6.Text = getInfoDicom.ToString();
                }
                catch (Exception)
                {
                    textBox6.Text = "NA";
                }

                try
                {
                    var getInfoDicom = file.Dataset.GetSingleValue<string>(DicomTag.SliceLocation);
                    textBox7.Text = getInfoDicom.ToString();
                }
                catch (Exception)
                {
                    textBox7.Text = "NA";
                }

                try
                {
                    var getInfoDicom = file.Dataset.GetSingleValue<string>(DicomTag.PixelSpacing);
                    textBox8.Text = getInfoDicom.ToString();
                }
                catch (Exception)
                {
                    textBox8.Text = "1"; // Default
                }

                try
                {
                    var getInfoDicom = file.Dataset.GetSingleValue<short>(DicomTag.Rows);
                    textBox13.Text = getInfoDicom.ToString();
                }
                catch (Exception)
                {
                    textBox13.Text = "NA";
                }

                try
                {
                    var getInfoDicom = file.Dataset.GetSingleValue<short>(DicomTag.Columns);
                    textBox12.Text = getInfoDicom.ToString();
                }
                catch (Exception)
                {
                    textBox12.Text = "NA";
                }

                try
                {
                    var getInfoDicom = file.Dataset.GetSingleValue<short>(DicomTag.BitsAllocated);
                    BitAllocated.Text = getInfoDicom.ToString();
                }
                catch (Exception)
                {
                    BitAllocated.Text = "NA";
                }

                try
                {
                    var getInfoDicom = file.Dataset.GetSingleValue<short>(DicomTag.BitsStored);
                    BitStored.Text = getInfoDicom.ToString();
                }
                catch (Exception)
                {
                    BitStored.Text = "NA";
                }

                try
                {
                    var getInfoDicom = file.Dataset.GetSingleValue<string>(DicomTag.RescaleIntercept);
                    HUIntercept.Text = getInfoDicom.ToString();
                }
                catch (Exception)
                {
                    HUIntercept.Text = "NA";
                }

                try
                {
                    var getInfoDicom = file.Dataset.GetSingleValue<string>(DicomTag.RescaleSlope);
                    HUSlope.Text = getInfoDicom.ToString();
                }
                catch (Exception)
                {
                    HUSlope.Text = "NA";
                }

                try
                {
                    var getInfoDicom = file.Dataset.GetSingleValue<string>(DicomTag.RescaleType);
                    textBox15.Text = getInfoDicom.ToString();
                }
                catch (Exception)
                {
                    textBox15.Text = "NA";
                }

                try
                {
                    var getInfoDicom = file.Dataset.GetSingleValue<string>(DicomTag.KVP);
                    textBox17.Text = getInfoDicom.ToString();
                    kvp1.Text = getInfoDicom.ToString();
                    kvp2.Text = getInfoDicom.ToString();
                    kvp3.Text = getInfoDicom.ToString();
                    kvp4.Text = getInfoDicom.ToString();
                }
                catch (Exception)
                {
                    textBox17.Text = "NA";
                }

                try
                {
                    var getInfoDicom = file.Dataset.GetSingleValue<string>(DicomTag.DistanceSourceToDetector);
                    textBox16.Text = getInfoDicom.ToString();
                }
                catch (Exception)
                {
                    textBox16.Text = "NA";
                }

                try
                {
                    var getInfoDicom = file.Dataset.GetSingleValue<string>(DicomTag.DistanceSourceToPatient);
                    textBox18.Text = getInfoDicom.ToString();
                }
                catch (Exception)
                {
                    textBox18.Text = "NA";
                }
            }
        }

        List<Point> CTVpoints = new List<Point>();
        List<Point> OARpoints = new List<Point>();

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if(checkBox1.Checked)
            {
                CTVpoints.Add(e.Location);
                textBox40.Text += "(" + e.X.ToString() + ", " + e.Y.ToString() + ")\r\n";

                if (CTVpoints.Count > 1)
                {
                    Pen p = new Pen(Color.Yellow, 2);
                    Graphics g = pictureBox1.CreateGraphics();
                    g.DrawLine(p, e.X, e.Y, CTVpoints[CTVpoints.Count - 2].X, CTVpoints[CTVpoints.Count - 2].Y);
                    g.Dispose();
                }
            }

            if (checkBox2.Checked)
            {
                OARpoints.Add(e.Location);
                textBox21.Text += "(" + e.X.ToString() + ", " + e.Y.ToString() + ")\r\n";

                if (OARpoints.Count > 1)
                {
                    Pen p = new Pen(Color.Orange, 2);
                    Graphics g = pictureBox1.CreateGraphics();
                    g.DrawLine(p, e.X, e.Y, OARpoints[OARpoints.Count - 2].X, OARpoints[OARpoints.Count - 2].Y);
                    g.Dispose();
                }
            }
        }

        int CTVX = 0, CTVY = 0;
        bool isCTVGet = false;
        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                if (!isCTVGet)
                {
                    CTVpoints.Clear();
                    textBox40.Text = "";
                    isCTVGet = true;
                }
            }
            else
            {
                if (MessageBox.Show("Selesain Menandai CTV?", "Konfirmasi", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    isCTVGet = false;
                    Pen p = new Pen(Color.Yellow, 2);
                    Graphics g = pictureBox1.CreateGraphics();
                    g.DrawLine(p, CTVpoints[0].X, CTVpoints[0].Y, CTVpoints[CTVpoints.Count - 1].X, CTVpoints[CTVpoints.Count - 1].Y);

                    float centroidX = 0, centroidY = 0;
                    float det = 0, tempDet = 0;
                    int j = 0;
                    int nVertices = CTVpoints.Count;

                    for (int i = 0; i < nVertices; i++)
                    {
                        // closed polygon
                        if (i + 1 == nVertices)
                            j = 0;
                        else
                            j = i + 1;

                        // compute the determinant
                        tempDet = CTVpoints[i].X * CTVpoints[j].Y - CTVpoints[j].X * CTVpoints[i].Y;
                        det += tempDet;

                        centroidX += (CTVpoints[i].X + CTVpoints[j].X) * tempDet;
                        centroidY += (CTVpoints[i].Y + CTVpoints[j].Y) * tempDet;
                    }

                    // divide by the total mass of the polygon
                    centroidX /= 3 * det;
                    centroidY /= 3 * det;

                    CTVX = (int)centroidX;
                    CTVY = (int)centroidY;

                    int maxWidth = 0;
                    for (int i = 0; i < nVertices; i++)
                    {
                        int radius = (int)(Math.Sqrt(Math.Pow(CTVpoints[i].X - CTVX, 2) + Math.Pow(CTVpoints[i].Y - CTVY, 2)));
                        if (radius > maxWidth) maxWidth = radius;
                    }

                    collimationWidth1.Text = (2 * maxWidth).ToString();
                    collimationWidth2.Text = (2 * maxWidth).ToString();
                    collimationWidth3.Text = (2 * maxWidth).ToString();
                    collimationWidth4.Text = (2 * maxWidth).ToString();

                    g.DrawLine(p, (int)centroidX - 10, (int)centroidY, (int)centroidX + 10, (int)centroidY);
                    g.DrawLine(p, (int)centroidX, (int)centroidY - 10, (int)centroidX, (int)centroidY + 10);
                    g.Dispose();
                }
                else checkBox1.Checked = true;
            }
        }

        int OARX = 0, OARY = 0;
        bool isOARGet = false;

        double[,] dose;
        List<PointSource> pointSources;

        private void Button3_Click(object sender, EventArgs e)
        {            
            dose = new double[pictureBox1.Width, pictureBox1.Height];
            // null kan
            for(int i=0; i<pictureBox1.Width; i++)
            {
                for(int j=0; j<pictureBox1.Height; j++)
                {
                    dose[i, j] = 0;
                }
            }

            pointSources = new List<PointSource>();

            // Source 1
            if(groupBox8.Enabled)
            {
                double ptsource = double.Parse(sourceToPatient1.Text);
                double sorient = (double)sOrientation1.Value;
                double xcenter = -ptsource * Math.Cos(Math.PI * sorient / 180);
                double ycenter = ptsource * Math.Sin(Math.PI * sorient / 180);
                double xdir = Math.Cos(Math.PI * sorient / 180);
                double ydir = -Math.Sin(Math.PI * sorient / 180);

                textBox39.Text = xcenter.ToString() + "\t" + ycenter.ToString() + "\r\n";
                textBox39.Text += xdir.ToString() + "\t" + ydir.ToString() + "\r\n";

                double pxtomm = 1.0;
                double collimationWidthH = double.Parse(collimationWidth1.Text) / 2;
                double collimationWidthL = double.Parse(collimationWidth1.Text) / 2;

                int numColH = (int)(collimationWidthH / pxtomm);
                int numColL = (int)(collimationWidthL / pxtomm);                

                for(int i = 0; i <= numColH; i++)
                {
                    double pointX = xcenter + i * pxtomm * Math.Sin(Math.PI * sorient / 180);
                    double pointY = ycenter + i * pxtomm * Math.Cos(Math.PI * sorient / 180);

                    PointSource ps = new PointSource();
                    ps.posX = pointX;
                    ps.posY = pointY;
                    ps.dirX = xdir;
                    ps.dirY = ydir;
                    ps.intensity = 100;
                    pointSources.Add(ps);
                }
                for (int i = 1; i <= numColL; i++)
                {
                    double pointX = xcenter - i * pxtomm * Math.Sin(Math.PI * sorient / 180);
                    double pointY = ycenter - i * pxtomm * Math.Cos(Math.PI * sorient / 180);
                    PointSource ps = new PointSource();
                    ps.posX = pointX;
                    ps.posY = pointY;
                    ps.dirX = xdir;
                    ps.dirY = ydir;
                    ps.intensity = 100;
                    pointSources.Add(ps);                    
                }
            }

            // Source 2
            if (groupBox9.Enabled)
            {
                double ptsource = double.Parse(sourceToPatient2.Text);
                double sorient = (double)sOrientation2.Value;
                double xcenter = -ptsource * Math.Cos(Math.PI * sorient / 180);
                double ycenter = ptsource * Math.Sin(Math.PI * sorient / 180);
                double xdir = Math.Cos(Math.PI * sorient / 180);
                double ydir = -Math.Sin(Math.PI * sorient / 180);

                textBox39.Text = xcenter.ToString() + "\t" + ycenter.ToString() + "\r\n";
                textBox39.Text += xdir.ToString() + "\t" + ydir.ToString() + "\r\n";

                double pxtomm = 1.0;
                double collimationWidthH = double.Parse(collimationWidth2.Text) / 2;
                double collimationWidthL = double.Parse(collimationWidth2.Text) / 2;

                int numColH = (int)(collimationWidthH / pxtomm);
                int numColL = (int)(collimationWidthL / pxtomm);

                for (int i = 0; i <= numColH; i++)
                {
                    double pointX = xcenter + i * pxtomm * Math.Sin(Math.PI * sorient / 180);
                    double pointY = ycenter + i * pxtomm * Math.Cos(Math.PI * sorient / 180);

                    PointSource ps = new PointSource();
                    ps.posX = pointX;
                    ps.posY = pointY;
                    ps.dirX = xdir;
                    ps.dirY = ydir;
                    ps.intensity = 100;
                    pointSources.Add(ps);
                }
                for (int i = 1; i <= numColL; i++)
                {
                    double pointX = xcenter - i * pxtomm * Math.Sin(Math.PI * sorient / 180);
                    double pointY = ycenter - i * pxtomm * Math.Cos(Math.PI * sorient / 180);
                    PointSource ps = new PointSource();
                    ps.posX = pointX;
                    ps.posY = pointY;
                    ps.dirX = xdir;
                    ps.dirY = ydir;
                    ps.intensity = 100;
                    pointSources.Add(ps);
                }
            }


            // Source 3
            if (groupBox10.Enabled)
            {
                double ptsource = double.Parse(sourceToPatient3.Text);
                double sorient = (double)sOrientation3.Value;
                double xcenter = -ptsource * Math.Cos(Math.PI * sorient / 180);
                double ycenter = ptsource * Math.Sin(Math.PI * sorient / 180);
                double xdir = Math.Cos(Math.PI * sorient / 180);
                double ydir = -Math.Sin(Math.PI * sorient / 180);

                textBox39.Text = xcenter.ToString() + "\t" + ycenter.ToString() + "\r\n";
                textBox39.Text += xdir.ToString() + "\t" + ydir.ToString() + "\r\n";

                double pxtomm = 1.0;
                double collimationWidthH = double.Parse(collimationWidth3.Text) / 2;
                double collimationWidthL = double.Parse(collimationWidth3.Text) / 2;

                int numColH = (int)(collimationWidthH / pxtomm);
                int numColL = (int)(collimationWidthL / pxtomm);

                for (int i = 0; i <= numColH; i++)
                {
                    double pointX = xcenter + i * pxtomm * Math.Sin(Math.PI * sorient / 180);
                    double pointY = ycenter + i * pxtomm * Math.Cos(Math.PI * sorient / 180);

                    PointSource ps = new PointSource();
                    ps.posX = pointX;
                    ps.posY = pointY;
                    ps.dirX = xdir;
                    ps.dirY = ydir;
                    ps.intensity = 100;
                    pointSources.Add(ps);
                }
                for (int i = 1; i <= numColL; i++)
                {
                    double pointX = xcenter - i * pxtomm * Math.Sin(Math.PI * sorient / 180);
                    double pointY = ycenter - i * pxtomm * Math.Cos(Math.PI * sorient / 180);
                    PointSource ps = new PointSource();
                    ps.posX = pointX;
                    ps.posY = pointY;
                    ps.dirX = xdir;
                    ps.dirY = ydir;
                    ps.intensity = 100;
                    pointSources.Add(ps);
                }
            }
            // Source 4
            if (groupBox11.Enabled)
            {
                double ptsource = double.Parse(sourceToPatient4.Text);
                double sorient = (double)sOrientation4.Value;
                double xcenter = -ptsource * Math.Cos(Math.PI * sorient / 180);
                double ycenter = ptsource * Math.Sin(Math.PI * sorient / 180);
                double xdir = Math.Cos(Math.PI * sorient / 180);
                double ydir = -Math.Sin(Math.PI * sorient / 180);

                textBox39.Text = xcenter.ToString() + "\t" + ycenter.ToString() + "\r\n";
                textBox39.Text += xdir.ToString() + "\t" + ydir.ToString() + "\r\n";

                double pxtomm = 1.0;
                double collimationWidthH = double.Parse(collimationWidth4.Text) / 2;
                double collimationWidthL = double.Parse(collimationWidth4.Text) / 2;

                int numColH = (int)(collimationWidthH / pxtomm);
                int numColL = (int)(collimationWidthL / pxtomm);

                for (int i = 0; i <= numColH; i++)
                {
                    double pointX = xcenter + i * pxtomm * Math.Sin(Math.PI * sorient / 180);
                    double pointY = ycenter + i * pxtomm * Math.Cos(Math.PI * sorient / 180);

                    PointSource ps = new PointSource();
                    ps.posX = pointX;
                    ps.posY = pointY;
                    ps.dirX = xdir;
                    ps.dirY = ydir;
                    ps.intensity = 100;
                    pointSources.Add(ps);
                }
                for (int i = 1; i <= numColL; i++)
                {
                    double pointX = xcenter - i * pxtomm * Math.Sin(Math.PI * sorient / 180);
                    double pointY = ycenter - i * pxtomm * Math.Cos(Math.PI * sorient / 180);
                    PointSource ps = new PointSource();
                    ps.posX = pointX;
                    ps.posY = pointY;
                    ps.dirX = xdir;
                    ps.dirY = ydir;
                    ps.intensity = 100;
                    pointSources.Add(ps);
                }
            }
            if (CalculateDoses.IsBusy) { MessageBox.Show("Still running"); return; }
            this.CalculateDoses.RunWorkerAsync(100);
        }

        private void addDose(double[,] doseBase, int ctrX, int ctrY, int posX, int posY)
        {

        }

        private void CalculateDoses_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;
            int arg = (int)e.Argument;
            e.Result = CalculateDoses_Operation(bw, arg);
            if (bw.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private bool isInside(Point pp)
        {
            if(pp.X >= 0 && pp.X < pictureBox1.Width)
            {
                if (pp.Y >= 0 && pp.Y < pictureBox1.Height) return true;
            }
            return false;
        }

        private int CalculateDoses_Operation(BackgroundWorker bw, int arg)
        {
            try
            {
                if(pointSources.Count > 1)
                {
                    Bitmap bitMap = (Bitmap)pictureBox1.Image;
                    for (int loop = 0; loop < 2000; loop++) {
                        for (int i = 0; i < pointSources.Count; i++)
                        {
                            pointSources[i].getPixel(CTVX, CTVY, 1);
                            double mu = 0;
                            double muperro = 0;

                            if (isInside(pointSources[i].p[0]) && isInside(pointSources[i].p[1]) && isInside(pointSources[i].p[2]) && isInside(pointSources[i].p[3]))
                            {
                                Color gray1 = bitMap.GetPixel(pointSources[i].p[0].X, pointSources[i].p[0].Y);
                                Color gray2 = bitMap.GetPixel(pointSources[i].p[1].X, pointSources[i].p[1].Y);
                                Color gray3 = bitMap.GetPixel(pointSources[i].p[2].X, pointSources[i].p[2].Y);
                                Color gray4 = bitMap.GetPixel(pointSources[i].p[3].X, pointSources[i].p[3].Y);

                                double grayFRGB1 = (0.3 * gray1.R) + (0.59 * gray1.G) + (0.11 * gray1.B);
                                double grayFRGB2 = (0.3 * gray2.R) + (0.59 * gray2.G) + (0.11 * gray2.B);
                                double grayFRGB3 = (0.3 * gray3.R) + (0.59 * gray3.G) + (0.11 * gray3.B);
                                double grayFRGB4 = (0.3 * gray4.R) + (0.59 * gray4.G) + (0.11 * gray4.B);

                                double gray = grayFRGB1 * pointSources[i].A[0] + grayFRGB2 * pointSources[i].A[1] + grayFRGB3 * pointSources[i].A[2] + grayFRGB4 * pointSources[i].A[3];
                                // Gray to HU
                                gray = gray * Math.Pow(2, double.Parse(BitStored.Text)) / 256.0;
                                double HU = double.Parse(HUSlope.Text) * gray + double.Parse(HUIntercept.Text);
                                double muWater = double.Parse(muData.Text);
                                mu = muWater + HU * muWater / 1000;
                                // per cm to per mm
                                mu /= 10;
                                
                            }
                            pointSources[i].Update(mu, 1);

                            pointSources[i].getPixel(CTVX, CTVY, 1);
                            if (isInside(pointSources[i].p[0]))
                            {
                                dose[pointSources[i].p[0].X, pointSources[i].p[0].Y] += pointSources[i].intensity * pointSources[i].A[0];
                            }
                            if (isInside(pointSources[i].p[1]))
                            {
                                dose[pointSources[i].p[1].X, pointSources[i].p[1].Y] += pointSources[i].intensity * pointSources[i].A[1];
                            }
                            if (isInside(pointSources[i].p[2]))
                            {
                                dose[pointSources[i].p[2].X, pointSources[i].p[2].Y] += pointSources[i].intensity * pointSources[i].A[2];
                            }
                            if (isInside(pointSources[i].p[3]))
                            {
                                dose[pointSources[i].p[3].X, pointSources[i].p[3].Y] += pointSources[i].intensity * pointSources[i].A[3];
                            }
                        }
                    }
                }
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private void CalculateDoses_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void CalculateDoses_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Bitmap bitMap = (Bitmap)pictureBox1.Image;
            double max = 0;
            for (int i = 0; i < pictureBox1.Width; i++)
            {
                for (int j = 0; j < pictureBox1.Height; j++)
                {
                    if (Math.Log(1+dose[i, j]) > max) max = Math.Log(1+dose[i, j]);
                }
            }

            for (int i = 0; i < pictureBox1.Width; i++)
            {
                for (int j = 0; j < pictureBox1.Height; j++)
                {
                    Color initCol = bitMap.GetPixel(i, j);
                    int redGrad = (int)(255.0 * Math.Log(1 + dose[i, j]) / max) + initCol.R;
                    if (redGrad > 255) redGrad = 255;
                    if (redGrad < 0) redGrad = 0;
                    bitMap.SetPixel(i, j, Color.FromArgb(redGrad, initCol.G, initCol.B));
                }
            }
            pictureBox1.Image = bitMap;
            GC.Collect();
            MessageBox.Show("done " + max.ToString());
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                if (!isOARGet)
                {
                    OARpoints.Clear();
                    textBox21.Text = "";
                    isOARGet = true;
                }
            }
            else
            {
                if (MessageBox.Show("Selesain Menandai OAR?", "Konfirmasi", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    isOARGet = false;
                    Pen p = new Pen(Color.Orange, 2);
                    Graphics g = pictureBox1.CreateGraphics();
                    g.DrawLine(p, OARpoints[0].X, OARpoints[0].Y, OARpoints[OARpoints.Count - 1].X, OARpoints[OARpoints.Count - 1].Y);
                    g.Dispose();

                    float centroidX = 0, centroidY = 0;
                    float det = 0, tempDet = 0;
                    int j = 0;
                    int nVertices = OARpoints.Count;

                    for (int i = 0; i < nVertices; i++)
                    {
                        // closed polygon
                        if (i + 1 == nVertices)
                            j = 0;
                        else
                            j = i + 1;

                        // compute the determinant
                        tempDet = OARpoints[i].X * OARpoints[j].Y - OARpoints[j].X * OARpoints[i].Y;
                        det += tempDet;

                        centroidX += (OARpoints[i].X + OARpoints[j].X) * tempDet;
                        centroidY += (OARpoints[i].Y + OARpoints[j].Y) * tempDet;
                    }

                    // divide by the total mass of the polygon
                    centroidX /= 3 * det;
                    centroidY /= 3 * det;

                    OARX = (int)centroidX;
                    OARY = (int)centroidY;
                    
                }
                else checkBox2.Checked = true;
            }
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            groupBox8.Enabled = true;
            if (numericUpDown1.Value > 1) groupBox9.Enabled = true;
            else groupBox9.Enabled = false;
            if (numericUpDown1.Value > 2) groupBox10.Enabled = true;
            else groupBox10.Enabled = false;
            if (numericUpDown1.Value > 3) groupBox11.Enabled = true;
            else groupBox11.Enabled = false;
        }
    }
}
