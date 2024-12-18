using System;
using MoneyManagerLibrary;

namespace MoneyManagerConsole.Controllers
{
    public class ControllerHapus
    {
        private readonly MoneyLibrary _moneyLibrary;

        public ControllerHapus(MoneyLibrary moneyLibrary)
        {
            _moneyLibrary = moneyLibrary;
        }

        public void HapusData()
        {
            var records = _moneyLibrary.LihatData();
            Console.WriteLine("\n==== Menampilkan Semua Data yang Akan di Hapus ====");
            foreach (var record in records)
            {
                Console.WriteLine($"[{record.Table}] ID: {record.Id}, Jumlah (Rupiah): {record.Jumlah:C}, Tanggal: {record.Tanggal:yyyy-MM-dd}, Deskripsi: {record.Keterangan}");
            }

            Console.Write("Masukkan Data ID yang Akan di Hapus: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Pilih (pemasukan/pengeluaran): ");
            string table = Console.ReadLine();

            _moneyLibrary.HapusData(table, id);
            Console.WriteLine("\nData Berhasil di Hapus.");
        }
    }
}
