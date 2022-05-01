using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsEF_CoreFirst
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        vtModel _vt = new vtModel(); //işlemlerde bu değişkenden veri alıp yollayacağız

        private void Form1_Load(object sender, EventArgs e)
        {
            //ilk açılışta veriler gelsin
            veriDoldur();
            toolStripStatusLabel1.Text = "Hoş geldiniz";
        }

        private void veriDoldur()
        {
            dataGridView1.DataSource = _vt.TabloVerileri.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            veriDoldur();
            //form değerleri de sıfırlansın
            textBox1.Text = "-1";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            dateTimePicker1.Value = new DateTime(2010, 5, 1);
            checkBox1.Checked = false;

            toolStripStatusLabel1.Text = "Yenilendi...";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //yeni ekle
            if (textBox2.Text != "")
            {
                birTablo _satir = new birTablo();
                _satir.Isim = textBox2.Text;
                _satir.Ucret = (float)Convert.ToDouble(textBox3.Text);
                _satir.DogumTarihi = dateTimePicker1.Value;
                _satir.MezunMu = checkBox1.Checked;

                _vt.TabloVerileri.Add(_satir);
                _vt.SaveChanges();

                toolStripStatusLabel1.Text = _satir.Isim + " eklendi...";
                veriDoldur(); //data grid yenilensin
            }
            else
            {
                MessageBox.Show("İsim boş geçilemez");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //silme için dataGrid'den kayıt seçili olmalıdır
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow _secili = dataGridView1.SelectedRows[0];
                int seciliID = (int)_secili.Cells[0].Value;

                var silinecekKayit = _vt.TabloVerileri.Where(x => x.Id == seciliID).FirstOrDefault();

                toolStripStatusLabel1.Text = _secili.Cells[1].Value + " silindi...";

                _vt.TabloVerileri.Remove(silinecekKayit);
                _vt.SaveChanges();

                veriDoldur(); //data grid yenilensin
            }
            else
            {
                MessageBox.Show("Kayıt seçmediniz");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //güncelleme için dataGrid'den kayıt seçilmelidir
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow _secili = dataGridView1.SelectedRows[0];
                textBox1.Text = _secili.Cells[0].Value.ToString();
                textBox2.Text = _secili.Cells[1].Value.ToString();
                textBox3.Text = _secili.Cells[2].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(_secili.Cells[3].Value);
                checkBox1.Checked = (_secili.Cells[4].Value.ToString() == "True") ? true : false;

                toolStripStatusLabel1.Text = _secili.Cells[1].Value + " seçildi...";
            }
            else
            {
                MessageBox.Show("Kayıt seçmediniz");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //eğer seçildi ise güncelleme yapabiliriz
            if (textBox1.Text != "-1")
            {
                if (textBox2.Text != "")
                {
                    int seciliID = Convert.ToInt32(textBox1.Text);

                    var guncellenecekKayit = _vt.TabloVerileri.Where(x => x.Id == seciliID).FirstOrDefault();

                    guncellenecekKayit.Isim = textBox2.Text;
                    guncellenecekKayit.Ucret = (float)Convert.ToDouble(textBox3.Text);
                    guncellenecekKayit.DogumTarihi = dateTimePicker1.Value;
                    guncellenecekKayit.MezunMu = checkBox1.Checked;

                    _vt.SaveChanges();
                    veriDoldur();

                    toolStripStatusLabel1.Text = textBox2.Text + " ismi olarak güncellendi...";
                }
                else MessageBox.Show("İsim girilmedi");
            }
            else MessageBox.Show("Güncelleme için kayıt seçmediniz");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                var aranan = _vt.TabloVerileri.Where(x => x.Isim.Contains(textBox4.Text)).ToList();
                dataGridView1.DataSource = aranan;
                toolStripStatusLabel1.Text = textBox4.Text + " arandı, " + aranan.Count + " adet bulundu...";
            }
            else MessageBox.Show("Arama metni girilmedi");
        }
    }
}
