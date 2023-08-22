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

        private void Form1_Load(object sender, EventArgs e)
        {
            FiyatListesi();
        }
    }
}