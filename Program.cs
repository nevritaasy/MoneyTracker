using System;
using MoneyManagerConsole.Controllers;
using MoneyManagerLibrary;

class Program
{
    static string connectionString = "Server=127.0.01;port=3306;Database=manajemen_keuangan;Uid=root;Pwd=";
    static MoneyLibrary moneyLibrary = new MoneyLibrary(connectionString);

    static void Main(string[] args)
    {
        var pemasukanController = new ControllerPemasukan(moneyLibrary);
        var pengeluaranController = new ControllerPengeluaran(moneyLibrary);
        var updateController = new ControllerUpdate(moneyLibrary);
        var hapusController = new ControllerHapus(moneyLibrary);
        var lihatController = new ControllerLihat(moneyLibrary);

        while (true)
        {
            Console.WriteLine("\n---------------------------------------");
            Console.WriteLine("\n         APLIKASI MONEY TRACKER");
            Console.WriteLine("\n---------------------------------------");
            Console.WriteLine("\nSelamat Datang di Aplikasi Money Tracker!");
            Console.WriteLine("\nSilakan pilih kebutuhan Anda.");
            Console.WriteLine("1. Tambah Pemasukan Awal");
            Console.WriteLine("2. Tambah Pengeluaran");
            Console.WriteLine("3. Update Data");
            Console.WriteLine("4. Hapus Data");
            Console.WriteLine("5. Lihat Data (Disortir Berdasarkan Bulan)");
            Console.WriteLine("6. Lihat Total Uang per Bulan");
            Console.WriteLine("7. Keluar");
            Console.Write("Pilih menu: ");
            string pilihan = Console.ReadLine();

            switch (pilihan) 
            {
                case "1": pemasukanController.TambahPemasukan(); break;
                case "2": pengeluaranController.TambahPengeluaran(); break;
                case "3": updateController.UpdateData(); break;
                case "4": hapusController.HapusData(); break;
                case "5": lihatController.LihatData(); break;
                case "6": lihatController.LihatDataDetail(); break;
                case "7":
                    Console.WriteLine("\nTerima Kasih Sudah Menggunakan Aplikasi Money Tracker by Nevrita dan Gabriele ^^ ");
                    return;
                default: Console.WriteLine("\nPilihan tidak valid."); break;
            }
        }
    }
}