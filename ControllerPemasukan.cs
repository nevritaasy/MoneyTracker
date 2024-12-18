using System;
using MoneyManagerLibrary;

namespace MoneyManagerConsole.Controllers
{
    public class ControllerPemasukan
    {
        private readonly MoneyLibrary _moneyLibrary;

        public ControllerPemasukan(MoneyLibrary moneyLibrary)
        {
            _moneyLibrary = moneyLibrary;
        }

        public void TambahPemasukan()
        {
            Console.Write("Masukkan jumlah pemasukan: ");
            decimal jumlah = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Masukkan tanggal (yyyy-MM-dd): ");
            DateTime tanggal = DateTime.Parse(Console.ReadLine());
            Console.Write("Masukkan keterangan: ");
            string keterangan = Console.ReadLine();

            _moneyLibrary.TambahPemasukan(jumlah, tanggal, keterangan);
            Console.WriteLine("\nPemasukan berhasil ditambahkan.");
        }
    }
}
