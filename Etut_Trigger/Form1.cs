using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Etut_Trigger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection bgl = new SqlConnection(@"Data Source=LENOVO\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True");

        void Listele()
        {

            SqlDataAdapter da = new SqlDataAdapter("select*from TBLKITAPLAR",bgl);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
                
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Listele();
            sayac();

        }

        void sayac()
        {
            bgl.Open();
            SqlCommand komut = new SqlCommand("select ADET from TBLSAYAC", bgl);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {

                LblSayac.Text = dr[0].ToString();
            }


            bgl.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            bgl.Open();
            SqlCommand komutEkle = new SqlCommand("insert into TBLKITAPLAR (AD,YAZAR,SAYFA,YAYINEVİ,TUR) values(@p1,@p2,@p3,@p4,@p5)", bgl);
            komutEkle.Parameters.AddWithValue("@p1", TxtAd.Text);
            komutEkle.Parameters.AddWithValue("@p2", TxtYazar.Text);
            komutEkle.Parameters.AddWithValue("@p3", TxtSayfa.Text);
            komutEkle.Parameters.AddWithValue("@p4", TxtYayınEvi.Text);
            komutEkle.Parameters.AddWithValue("@p5", TxtTur.Text);
            komutEkle.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("Kitap İşlemi Tamamlandı","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            sayac();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxTId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtYazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            TxtSayfa.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtYayınEvi.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtTur.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            bgl.Open();
            SqlCommand komutSil = new SqlCommand("delete from TBLKITAPLAR where ID=@p1", bgl);
            komutSil.Parameters.AddWithValue("@p1", TxTId.Text);
            komutSil.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("Kitap Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Listele();
            sayac();
        }
    }
}
