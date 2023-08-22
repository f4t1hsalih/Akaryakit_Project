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

        private int DegerKontrol()
        {
            int value = 0;
            if (numericUpDown1.Value != 0) value++;
            if (numericUpDown2.Value != 0) value++;
            if (numericUpDown3.Value != 0) value++;
            if (numericUpDown4.Value != 0) value++;

            return value;
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

        private void SatisYap(string petrol, decimal litre, decimal price)
        {
            con.Open();
            string command = "insert into tbl_transaction (license_plate, kind_of_petrol, liter, price) values (@p1, @p2, @p3, @p4)";
            SqlCommand cmd = new SqlCommand(command, con);
            cmd.Parameters.AddWithValue("@p1", txtPlaka.Text.ToUpper());
            cmd.Parameters.AddWithValue("@p2", petrol);
            cmd.Parameters.AddWithValue("@p3", litre);
            cmd.Parameters.AddWithValue("@p4", price);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Satýþ Yapýldý");

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

        private void btnDepoDoldur_Click(object sender, EventArgs e)
        {
            if (txtPlaka.Text != "")
            {
                //Ayný anda birkaç deðer girilip girilmediði kontrol ediliyor
                int value = DegerKontrol();

                if (value != 1) MessageBox.Show("Lütfen Sadece Bir Çeþit Petrol Alýnýz!");

                else if (numericUpDown1.Value != 0)
                {
                    SatisYap("Kurþunsuz 95", numericUpDown1.Value, decimal.Parse(txtKursunsuz95p.Text));
                }
                else if (numericUpDown2.Value != 0)
                {
                    SatisYap("Max Diesel", numericUpDown2.Value, decimal.Parse(txtMaxDieselp.Text));
                }
                else if (numericUpDown3.Value != 0)
                {
                    SatisYap("Pro Diesel", numericUpDown3.Value, decimal.Parse(txtProDieselp.Text));
                }
                else if (numericUpDown4.Value != 0)
                {
                    SatisYap("Gaz Otogaz", numericUpDown4.Value, decimal.Parse(txtOtogazp.Text));
                }
            }
            else
            {
                MessageBox.Show("Lütfen Plakayý Giriniz!");
            }
        }
    }
}