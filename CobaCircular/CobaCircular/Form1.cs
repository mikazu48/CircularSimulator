using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CobaCircular
{
    public partial class Form1 : Form
    {
        DataTable dt = new DataTable();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            p_GenerateCircular();
        }
        private void p_GenerateCircular()
        {
            try
            {
                int iTotalCirc = int.Parse(textBox2.Text);

                decimal decSumEnd = 0;

                decimal decSumRest = 0;
                decimal decSum = 0;
                decimal decFix = 0;
                decimal decFinalRes = 0;
                decimal decRestFix = 0;
                bool bCirc = false;
                for (int i = 0; i < iTotalCirc; i++)
                {
                    if(!bCirc)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            decFix += decimal.Parse(dr[0].ToString());
                        }
                        decSum = decFix;
                        bCirc = true;
                    }
                    if(decSumRest == 0)
                    {
                        decSumRest = (decSum * 5) / 100; // ini karena 5%
                        decSumEnd = decFix + decSumRest;
                    }
                    else
                    {
                        decSum = decSumEnd;

                        decSumRest = (decSumRest * 5) / 100;
                        decSumEnd += decSumRest;

                        //For perline
                        decRestFix += decSumRest;

                        decFinalRes = (decimal)decSumEnd - (decimal)decSum;
                        if (decFinalRes <= 0.001m)
                        {
                            break;
                        }
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr[1] = decimal.Parse(dr[0].ToString()) + decRestFix;
                    }

                }

                txtCirc1.Text = decSum.ToString();
                txtCircResult.Text = (decSumEnd).ToString();
                string szMsg = "The difference of circ " + decFinalRes.ToString();
                MessageBox.Show(szMsg);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            dt.Columns.Add("Number");
            dt.Columns.Add("Number 2");

            DataRow dr = dt.NewRow();
            dr[0] = 10;
            dr[1] = 0;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 20;
            dr[1] = 0;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 30;
            dr[1] = 0;
            dt.Rows.Add(dr);
            
            dataGridView1.DataSource = dt;

        }
    }
}
