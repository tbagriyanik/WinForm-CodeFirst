using System;
using System.Data.Entity;
using System.Linq;

namespace WinFormsEF_CoreFirst
{
    public class vtModel : DbContext
    {
        public vtModel()
            : base("name=vtModel")
        {
        }

        public virtual DbSet<birTablo> TabloVerileri { get; set; }
    }

    public class birTablo
    {
        public int Id { get; set; }
        public string Isim { get; set; }
        public float Ucret { get; set; }
        public DateTime DogumTarihi { get; set; }
        public bool MezunMu { get; set; }
    }
}