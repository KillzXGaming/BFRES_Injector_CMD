using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syroot.NintenTools.Bfres;
using Syroot.NintenTools.Bfres.GX2;

namespace BFRES_Injector_CMD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public ResFile TargetBFRES = null;

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "bfres Files (BFRES)|*.BFRES";


            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    TargetBFRES = new ResFile(ofd.FileName);

                    comboBox1.Items.Clear();
                    comboBox2.Items.Clear();

                    textBox1.Text = TargetBFRES.Name;

                    foreach (Model mdl in TargetBFRES.Models.Values)
                    {
                        comboBox1.Items.Add(mdl.Name);
                    }
                    comboBox1.SelectedIndex = 0;

                }
                catch
                {
                    MessageBox.Show("Could not read BFRES file");
                }
            }
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog obj = new OpenFileDialog();
            obj.Filter = "obj Files (OBJ)|*.OBJ";
            if (obj.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    BFRES.MeshObj test = new BFRES.MeshObj();

                    test.ReadObj(obj.FileName);

                    PolygonControl poly = new PolygonControl();
                    poly.CullBack = true;
                    poly.CullFront = true;
                    poly.PolygonModeEnabled = true;
                    TargetBFRES.Models[0].Materials[0].RenderState.PolygonControl = poly;
                    test.InjectMesh(TargetBFRES.Models[0], TargetBFRES.ByteOrder);


                    SaveFileDialog SvBFRES = new SaveFileDialog();
                    SvBFRES.Filter = "bfres Files (BFRES)|*.BFRES";
                    if (SvBFRES.ShowDialog() == DialogResult.OK)
                    {
                        TargetBFRES.Save(SvBFRES.FileName);
                        MessageBox.Show("Saved bfres file successfully!");
                    }
                }
                catch
                {
                    MessageBox.Show("Could not read OBJ file");
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BFRES.FMDLIndex = comboBox1.SelectedIndex;

            comboBox2.Items.Clear();
            comboBox2.ResetText();
            foreach (Shape shp in TargetBFRES.Models[comboBox1.SelectedIndex].Shapes.Values)
            {
                comboBox2.Items.Add(shp.Name);
            }
            comboBox2.SelectedIndex = 0;

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BFRES.ShapeIndex = comboBox2.SelectedIndex;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TargetBFRES.Name = textBox1.Text;
        }
    }
}
