using System;
using MoneyManagerLibrary;

namespace MoneyManagerConsole.Controllers
{
    public class ControllerPengeluaran
    {
        private readonly MoneyLibrary _moneyLibrary;

        public ControllerPengeluaran(MoneyLibrary moneyLibrary)
        {
            _moneyLibrary = moneyLibrary;
        }

        public void TambahPengeluaran()
        {
            Console.Write("Masukkan jumlah pengeluaran: ");
            decimal jumlah = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Masukkan tanggal (yyyy-MM-dd): ");
            DateTime tanggal = DateTime.Parse(Console.ReadLine());
            Console.Write("Masukkan keterangan: ");
            string keterangan = Console.ReadLine();

            _moneyLibrary.TambahPengeluaran(jumlah, tanggal, keterangan);
            Console.WriteLine("\nPengeluaran berhasil ditambahkan.");
        }
    }
}
