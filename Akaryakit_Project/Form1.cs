using System.Data.SqlClient;

namespace Akaryakit_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=krsDbAkaryakit;Integrated Security=True");

        private void FiyatListesi()
        {
            con.Open();
            string command = "select sale_price from tbl_petrol";
            SqlCommand cmd = new SqlCommand(command, con);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                int labelIndex = 0;

                while (dr.Read())
                {
                    if (labelIndex == 0)
                    {
                        lblKursunsuz95.Text = dr[0].ToString();
                    }
                    else if (labelIndex == 1)
                    {
                        lblMaxDiesel.Text = dr[0].ToString();
                    }
                    else if (labelIndex == 2)
                    {
                        lblProDiesel.Text = dr[0].ToString();
                    }
                    else if (labelIndex == 3)
                    {
                        lblOtogaz.Text = dr[0].ToString();
                    }

                    labelIndex++;
                }
            }
            dr.Close();
            con.Close();

        }

        private void DolulukOrani()
        {
            con.Open();
            string command = "select stock from tbl_petrol";
            SqlCommand cmd = new SqlCommand(command, con);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                int index = 0;

                while (dr.Read())
                {
                    if (index == 0)
                    {
                        lblKursunsuz95d.Text = dr[0].ToString();
                        pbKursunsuz95.Value = Convert.ToInt16(lblKursunsuz95d.Text);
                    }
                    else if (index == 1)
                    {
                        lblMaxDieseld.Text = dr[0].ToString();
                        pbMaxDiesel.Value = Convert.ToInt16(lblMaxDieseld.Text);
                    }
                    else if (index == 2)
                    {
                        lblProDieseld.Text = dr[0].ToString();
                        pbProDiesel.Value = Convert.ToInt16(lblProDieseld.Text);
                    }
                    else if (index == 3)
                    {
                        lblOtogazd.Text = dr[0].ToString();
                        pbOtogaz.Value = Convert.ToInt16(lblOtogazd.Text);
                    }

                    index++;
                }
            }
            dr.Close();
            con.Close();

        }

        private double FiyatHesaplama(string x)
        {
            double petrol, liter, amount;

            if (x == "Kursunsuz95")
            {
                petrol = Convert.ToDouble(lblKursunsuz95.Text);
                liter = Convert.ToDouble(numericUpDown1.Value);
                amount = petrol * liter;
            }
            else if (x == "MaxDiesel")
            {
                petrol = Convert.ToDouble(lblMaxDiesel.Text);
                liter = Convert.ToDouble(numericUpDown2.Value);
                amount = petrol * liter;
            }
            else if (x == "ProDiesel")
            {
                petrol = Convert.ToDouble(lblProDiesel.Text);
                liter = Convert.ToDouble(numericUpDown3.Value);
                amount = petrol * liter;
            }
            else if (x == "Otogaz")
            {
                petrol = Convert.ToDouble(lblOtogaz.Text);
                liter = Convert.ToDouble(numericUpDown4.Value);
                amount = petrol * liter;
            }
            else
            {
                return 0;
            }

            return amount;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FiyatListesi();
            DolulukOrani();
        }

        //Noktadan sonraki 3 rakamý görmek için '.ToString("N3")' kullanýldý
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            txtKursunsuz95p.Text = FiyatHesaplama("Kursunsuz95").ToString("N3");
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            txtMaxDieselp.Text = FiyatHesaplama("MaxDiesel").ToString("N3");
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            txtProDieselp.Text = FiyatHesaplama("ProDiesel").ToString("N3");
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            txtOtogazp.Text = FiyatHesaplama("Otogaz").ToString("N3");
        }
    }
}