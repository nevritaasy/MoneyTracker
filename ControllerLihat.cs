using System;
using MoneyManagerLibrary;

namespace MoneyManagerConsole.Controllers
{
    public class ControllerLihat
    {
        private readonly MoneyLibrary _moneyLibrary;

        public ControllerLihat(MoneyLibrary moneyLibrary)
        {
            _moneyLibrary = moneyLibrary;
        }

        public void LihatData()
        {
            var records = _moneyLibrary.LihatData();
            Console.WriteLine("\n==== Menampilkan Semua Data ====");
            foreach (var record in records)
            {
                Console.WriteLine($"[{record.Table}] ID: {record.Id}, Jumlah: {record.Jumlah:C}, Tanggal: {record.Tanggal:yyyy-MM-dd}, Deskripsi: {record.Keterangan}");
            }
        }

        public void LihatDataDetail()
        {
            var monthlyDetails = _moneyLibrary.LihatDataDetail();
            Console.WriteLine("\n==== Detail Pemasukan dan Pengeluaran ====");
            foreach (var month in monthlyDetails)
            {
                Console.WriteLine($"Bulan: {month.Key}");
                Console.WriteLine($"Total Balance: {month.Value.Total:C}");
                foreach (var record in month.Value.Item2)
                {
                    Console.WriteLine($"  [{record.Table}] Jumlah: {record.Jumlah:C}, Deskripsi: {record.Keterangan}");
                }
                Console.WriteLine();
            }
        }
    }
}
