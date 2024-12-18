using System;
using MoneyManagerLibrary;

namespace MoneyManagerConsole.Controllers
{
    public class ControllerUpdate
    {
        private readonly MoneyLibrary _moneyLibrary;

        public ControllerUpdate(MoneyLibrary moneyLibrary)
        {
            _moneyLibrary = moneyLibrary;
        }

        public void UpdateData()
        {
            var records = _moneyLibrary.LihatData();
            Console.WriteLine("\n==== Menampilkan Semua Data yang Akan di Update ====");
            foreach (var record in records)
            {
                Console.WriteLine($"[{record.Table}] ID: {record.Id}, Jumlah (Rupiah): {record.Jumlah:C}, Tanggal: {record.Tanggal:yyyy-MM-dd}, Deskripsi: {record.Keterangan}");
            }

            Console.Write("Masukkan ID Data yang Akan di Update: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Pilih (pemasukan/pengeluaran): ");
            string table = Console.ReadLine();

            Console.Write("Masukkan Jumlah Baru: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Masukkan Tanggal Baru (yyyy-MM-dd): ");
            DateTime date = DateTime.Parse(Console.ReadLine());

            Console.Write("Masukkan Deskripsi Baru: ");
            string description = Console.ReadLine();

            _moneyLibrary.UpdateData(table, id, amount, date, description);
            Console.WriteLine("\nData Berhasil di Update.");
        }
    }
}
