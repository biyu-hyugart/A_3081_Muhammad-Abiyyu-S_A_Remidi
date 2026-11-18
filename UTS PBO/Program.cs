using System;
using System.Collections.Generic;

namespace MBG
{
    // no 1 - Class, Object, Attribute, Method
    class Sekolah
    {
        public string idSekolah;
        public string namaSekolah;
        public int jumlahPaketDiterima;

        public Sekolah(string id, string nama, int jumlah)
        {
            idSekolah = id;
            namaSekolah = nama;
            jumlahPaketDiterima = jumlah;
        }

        public void TampilInfo()
        {
            Console.WriteLine("ID Sekolah      : " + idSekolah);
            Console.WriteLine("Nama Sekolah    : " + namaSekolah);
            Console.WriteLine("Paket Diterima  : " + jumlahPaketDiterima);
        }
    }

    // no 2 - Encapsulation
    // no 5 - Abstract class
    abstract class PaketMBG
    {
        private string idPaket;
        private string namaPaket;
        private int stokDapur;

        public PaketMBG(string id, string nama, int stok)
        {
            idPaket = id;
            namaPaket = nama;
            StokDapur = stok;
        }

        public string IdPaket
        {
            get { return idPaket; }
            set { idPaket = value; }
        }

        public string NamaPaket
        {
            get { return namaPaket; }
            set { namaPaket = value; }
        }

        public int StokDapur
        {
            get { return stokDapur; }
            private set
            {
                if (value >= 0)
                {
                    stokDapur = value;
                }
            }
        }


        public bool KurangiStok(int jumlah)
        {
            if (jumlah <= 0)
            {
                return false;
            }

            if (jumlah > stokDapur)
            {
                return false;
            }

            StokDapur = stokDapur - jumlah;
            return true;
        }
        public abstract int HitungBiaya(int jumlah);
    }


    // no 3 - Inheritance 
    class PaketReguler : PaketMBG
    {
        public string isiPaket = "Nasi, ayam, sayur, dan buah";

        public PaketReguler(string id, string nama, int stok) : base(id, nama, stok)
        {
        }

        public override int HitungBiaya(int jumlah)
        {
            return jumlah * 15000;
        }
    }

    class PaketKhusus : PaketMBG
    {
        // Atribut tambahan
        public string jenisDiet;

        public PaketKhusus(string id, string nama, int stok, string diet) : base(id, nama, stok)
        {
            jenisDiet = diet;
        }

        // no 4 - Polymorphism 
        public override int HitungBiaya(int jumlah)
        {
            return jumlah * 20000;
        }
    }
    // no 6 - Interface

    interface DapatDidistribusikan
    {
        void KirimPaket();
    }

    class DistribusiMBG : DapatDidistribusikan
    {
        private Sekolah sekolahTujuan;
        private PaketMBG paket;
        private int jumlah;

        public DistribusiMBG(Sekolah sekolah, PaketMBG paketMakanan, int jumlahPaket)
        {
            sekolahTujuan = sekolah;
            paket = paketMakanan;
            jumlah = jumlahPaket;
        }

        public void KirimPaket()
        {
            if (jumlah <= 0)
            {
                Console.WriteLine("Distribusi gagal! Jumlah paket harus lebih dari 0.");
                return;
            }

            if (jumlah > paket.StokDapur)
            {
                Console.WriteLine("Distribusi gagal! Jumlah paket (" + jumlah +
                    ") melebihi stok dapur yang tersedia (" + paket.StokDapur + ").");
                return;
            }

            paket.KurangiStok(jumlah);
            sekolahTujuan.jumlahPaketDiterima = sekolahTujuan.jumlahPaketDiterima + jumlah;

            Console.WriteLine("Distribusi MBG berhasil! Sebanyak " + jumlah +
                " paket makanan telah dikirim ke sekolah " + sekolahTujuan.namaSekolah);
        }
    }


    // no 7 pengelolaMBG

    class PengelolaMBG
    {
        private List<Sekolah> daftarSekolah = new List<Sekolah>();
        private List<PaketMBG> daftarPaket = new List<PaketMBG>();

        public void TambahSekolah(Sekolah sekolah)
        {
            daftarSekolah.Add(sekolah);
        }

        public void TambahPaket(PaketMBG paket)
        {
            daftarPaket.Add(paket);
        }

        public void TampilkanDataMBG()
        {
            Console.WriteLine("=== Data Sekolah Penerima ===");
            foreach (Sekolah s in daftarSekolah)
            {
                s.TampilInfo();
                Console.WriteLine("---");
            }

            Console.WriteLine("=== Data Paket Makanan ===");
            foreach (PaketMBG p in daftarPaket)
            {
                Console.WriteLine("ID Paket   : " + p.IdPaket);
                Console.WriteLine("Nama Paket : " + p.NamaPaket);
                Console.WriteLine("Stok Dapur : " + p.StokDapur);
                Console.WriteLine("---");
            }
        }
    }

    // Main Program
    class Program
    {
        static void Main(string[] args)
        {
            // no 1: buat 2 objek Sekolah
            Console.WriteLine("##### SOAL 1: Class dan Object #####");
            Sekolah sekolah1 = new Sekolah("S01", "SDN Jember 1", 0);
            Sekolah sekolah2 = new Sekolah("S02", "SMPN 2 Jember", 0);
            sekolah1.TampilInfo();
            Console.WriteLine("---");
            sekolah2.TampilInfo();

            // no 3: buat objek paket (turunan)
            PaketReguler paketReguler = new PaketReguler("P01", "Paket Reguler", 100);
            PaketKhusus paketKhusus = new PaketKhusus("P02", "Paket Khusus", 50, "Vegetarian");

            // no 2: Encapsulation 
            Console.WriteLine();
            Console.WriteLine("##### SOAL 2: Encapsulation #####");
            Console.WriteLine("Stok awal " + paketReguler.NamaPaket + ": " + paketReguler.StokDapur);
            bool hasil1 = paketReguler.KurangiStok(10);
            Console.WriteLine("KurangiStok(10) berhasil? " + hasil1 + ", stok sekarang: " + paketReguler.StokDapur);
            bool hasil2 = paketReguler.KurangiStok(500);
            Console.WriteLine("KurangiStok(500) berhasil? " + hasil2 + ", stok sekarang: " + paketReguler.StokDapur);

            // no 4: Polymorphism 

            Console.WriteLine();
            Console.WriteLine("##### SOAL 4: Polymorphism #####");
            PaketMBG paketA = paketReguler;
            PaketMBG paketB = paketKhusus;
            Console.WriteLine("Biaya 10 " + paketA.NamaPaket + " = Rp" + paketA.HitungBiaya(10));
            Console.WriteLine("Biaya 10 " + paketB.NamaPaket + " = Rp" + paketB.HitungBiaya(10));

            // no 5: Abstract Class
            Console.WriteLine();
            Console.WriteLine("##### SOAL 5: Abstract Class #####");
            Console.WriteLine("PaketMBG adalah abstract, hanya bisa dipakai lewat class turunannya.");

            // no 6: Interface (Distribusi)
            Console.WriteLine();
            Console.WriteLine("##### SOAL 6: Interface / Distribusi #####");

            Console.WriteLine("[Kirim sukses]");
            DapatDidistribusikan kirim1 = new DistribusiMBG(sekolah1, paketReguler, 30);
            kirim1.KirimPaket();

            Console.WriteLine("[Kirim gagal: jumlah 0]");
            DapatDidistribusikan kirim2 = new DistribusiMBG(sekolah1, paketReguler, 0);
            kirim2.KirimPaket();

            Console.WriteLine("[Kirim gagal: melebihi stok]");
            DapatDidistribusikan kirim3 = new DistribusiMBG(sekolah2, paketKhusus, 999);
            kirim3.KirimPaket();

            Console.WriteLine("[Kirim sukses ke sekolah 2]");
            DapatDidistribusikan kirim4 = new DistribusiMBG(sekolah2, paketKhusus, 20);
            kirim4.KirimPaket();

            //no 7: PengelolaMBG
            Console.WriteLine();
            Console.WriteLine("##### SOAL 7: PengelolaMBG #####");
            PengelolaMBG pengelola = new PengelolaMBG();
            pengelola.TambahSekolah(sekolah1);
            pengelola.TambahSekolah(sekolah2);
            pengelola.TambahPaket(paketReguler);
            pengelola.TambahPaket(paketKhusus);
            pengelola.TampilkanDataMBG();

            Console.ReadLine();
        }
    }
}