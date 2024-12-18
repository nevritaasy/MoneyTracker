using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace MoneyManagerLibrary
{
    public class MoneyLibrary
    {
        private readonly string _connectionString;

        public MoneyLibrary(string connectionString)
        {
            _connectionString = connectionString;
        }


        // Menambahkan Pemasukan User
        public void TambahPemasukan(decimal jumlahAwal, DateTime tanggal, string keterangan)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO pemasukan (jumlah_awal, tanggal, keterangan) VALUES (@jumlahAwal, @tanggal, @keterangan)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@jumlahAwal", jumlahAwal);
                    cmd.Parameters.AddWithValue("@tanggal", tanggal);
                    cmd.Parameters.AddWithValue("@keterangan", keterangan);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Menambahkan Pengeluaran User
        public void TambahPengeluaran(decimal jumlah, DateTime tanggal, string keterangan)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO pengeluaran (jumlah, tanggal, keterangan) VALUES (@jumlah, @tanggal, @keterangan)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@jumlah", jumlah);
                    cmd.Parameters.AddWithValue("@tanggal", tanggal);
                    cmd.Parameters.AddWithValue("@keterangan", keterangan);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Update Data yang Telah Dimasukkan Jika Ada Kesalahan

        public void UpdateData(string tableName, int id, decimal jumlah, DateTime tanggal, string keterangan)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string columnJumlah = tableName == "pemasukan" ? "jumlah_awal" : "jumlah";
                string query = $"UPDATE {tableName} SET {columnJumlah} = @jumlah, tanggal = @tanggal, keterangan = @keterangan WHERE id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@jumlah", jumlah);
                    cmd.Parameters.AddWithValue("@tanggal", tanggal);
                    cmd.Parameters.AddWithValue("@keterangan", keterangan);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Hapus Data 
        public void HapusData(string tableName, int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = $"DELETE FROM {tableName} WHERE id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Menampilkan Data yang Telah Dimasukkan Berdasarkan Bulannya

        public List<(string Table, int Id, decimal Jumlah, DateTime Tanggal, string Keterangan)> LihatData()
        {
            var records = new List<(string, int, decimal, DateTime, string)>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string queryPemasukan = "SELECT id, jumlah_awal AS jumlah, tanggal, keterangan FROM pemasukan ORDER BY MONTH(tanggal), id";
                string queryPengeluaran = "SELECT id, jumlah, tanggal, keterangan FROM pengeluaran ORDER BY MONTH(tanggal), id";

                // Fetch Pemasukan
                using (var cmd = new MySqlCommand(queryPemasukan, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        records.Add(("Pemasukan", reader.GetInt32("id"), reader.GetDecimal("jumlah"), reader.GetDateTime("tanggal"), reader.GetString("keterangan")));
                    }
                }

                // Fetch Pengeluaran
                using (var cmd = new MySqlCommand(queryPengeluaran, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        records.Add(("Pengeluaran", reader.GetInt32("id"), reader.GetDecimal("jumlah"), reader.GetDateTime("tanggal"), reader.GetString("keterangan")));
                    }
                }
            }

            return records;
        }


        // Menampilkan Total Keuangan yang Dimiliki Tiap Bulannya
        public Dictionary<string, (decimal Total, List<(string Table, decimal Jumlah, string Keterangan)>)> LihatDataDetail()
        {
            var monthlyDetails = new Dictionary<string, (decimal, List<(string, decimal, string)>)>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT DATE_FORMAT(tanggal, '%Y-%m') AS bulan, table_name, jumlah, keterangan, 
                        SUM(CASE WHEN table_name = 'pemasukan' THEN jumlah ELSE -jumlah END) OVER (PARTITION BY DATE_FORMAT(tanggal, '%Y-%m')) AS total_bulan
                    FROM (
                        SELECT 'pemasukan' AS table_name, jumlah_awal AS jumlah, tanggal, keterangan FROM pemasukan
                        UNION ALL
                        SELECT 'pengeluaran' AS table_name, jumlah, tanggal, keterangan FROM pengeluaran
                    ) AS combined
                    ORDER BY tanggal;";

                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string month = reader.GetString("bulan");
                        string table = reader.GetString("table_name");
                        decimal amount = reader.GetDecimal("jumlah");
                        string description = reader.GetString("keterangan");
                        decimal total = reader.GetDecimal("total_bulan");

                        if (!monthlyDetails.ContainsKey(month))
                        {
                            monthlyDetails[month] = (0, new List<(string, decimal, string)>());
                        }

                        monthlyDetails[month].Item2.Add((table, amount, description));
                        monthlyDetails[month] = (total, monthlyDetails[month].Item2);
                    }

                    return monthlyDetails;
                }
            }
        }
    }
}
