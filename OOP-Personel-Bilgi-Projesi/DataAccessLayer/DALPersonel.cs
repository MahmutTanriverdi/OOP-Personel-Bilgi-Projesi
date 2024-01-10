using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer1;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class DALPersonel
    {

        public static List<EntityPersonel> PersonelListesi()
        {
            List<EntityPersonel> degerler = new List<EntityPersonel>();
            SqlCommand komut1 = new SqlCommand("Select * from Tbl_Bilgi", Baglanti.bgl);
            if(komut1.Connection.State != ConnectionState.Open)
            {
                komut1.Connection.Open();
            }
            SqlDataReader dr = komut1.ExecuteReader();
            while(dr.Read())
            {
                EntityPersonel ent = new EntityPersonel();
                ent.Id = int.Parse(dr["id"].ToString());
                ent.Ad = dr["ad"].ToString();
                ent.Soyad = dr["soyad"].ToString() ;
                ent.Gorev = dr["gorev"].ToString();
                ent.Sehir = dr["sehir"].ToString();
                ent.Maas = short.Parse(dr["maas"].ToString());
                degerler.Add(ent);
            }
            Baglanti.bgl.Close();
            dr.Close();
            return degerler;

        }
        public static int PersonelEkle(EntityPersonel p)
        {
            SqlCommand komut2 = new SqlCommand("Insert into Tbl_Bilgi (ad, soyad, sehir, gorev, " +
                "maas) values(@p1,@p2,@p3,@p4,@p5)", Baglanti.bgl);
            if (komut2.Connection.State != ConnectionState.Open)
            {
                komut2.Connection.Open();
            }
            komut2.Parameters.AddWithValue("@p1", p.Ad);
            komut2.Parameters.AddWithValue("@p2", p.Soyad);
            komut2.Parameters.AddWithValue("@p3", p.Sehir);
            komut2.Parameters.AddWithValue("@p4", p.Gorev);
            komut2.Parameters.AddWithValue("@p5", p.Maas);
            return komut2.ExecuteNonQuery();

        }
        public static bool PersonelSil(int p)
        {
            SqlCommand komut3 = new SqlCommand("Delete from Tbl_Bilgi where id=@p1", Baglanti.bgl);
            if (komut3.Connection.State != ConnectionState.Open)
            {
                komut3.Connection.Open();
            }
            komut3.Parameters.AddWithValue("@p1", p);
            return komut3.ExecuteNonQuery() > 0;
        }
        public static bool PersonelGuncelle(EntityPersonel ent)
        {
            SqlCommand komut4 = new SqlCommand("Update Tbl_Bilgi Set ad=@p1, soyad=@p2, sehir=@p3, " +
                "gorev=@p4, maas=@p5 where id=@p6", Baglanti.bgl);
            if (komut4.Connection.State != ConnectionState.Open)
            {
                komut4.Connection.Open();
            }
            komut4.Parameters.AddWithValue("@p1", ent.Ad);
            komut4.Parameters.AddWithValue("@p2", ent.Soyad);
            komut4.Parameters.AddWithValue("@p3", ent.Sehir);
            komut4.Parameters.AddWithValue("@p4", ent.Gorev);
            komut4.Parameters.AddWithValue("@p5", ent.Maas);
            komut4.Parameters.AddWithValue("@p6", ent.Id);
            return komut4.ExecuteNonQuery() > 0;
        }
        
    }
}
