using Microsoft.Data.Sqlite;


namespace InvocePDF.Services
{
    public class DocumentNumberService
    {
        private readonly string _connectionString = "Data Source=documentNumbers.db";
        private static readonly object _lock = new();

        public DocumentNumberService()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
        CREATE TABLE IF NOT EXISTS DocumentNumbers (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Date TEXT NOT NULL,
            Type TEXT NOT NULL,
            LastNumber INTEGER NOT NULL
        );
        ";

            command.ExecuteNonQuery();
        }

        public string GetNextActNumber()
        {
            return GetNextNumber("ACT");
        }

        public string GetNextInvoiceNumber()
        {
            return GetNextNumber("INV");
        }

        private string GetNextNumber(string type)
        {
            lock (_lock)
            {
                var today = DateTime.Now.ToString("yyyy-MM-dd");

                using var connection = new SqliteConnection(_connectionString);
                connection.Open();

                var select = connection.CreateCommand();
                select.CommandText =
                @"
            SELECT LastNumber 
            FROM DocumentNumbers 
            WHERE Date = $date AND Type = $type
            ";

                select.Parameters.AddWithValue("$date", today);
                select.Parameters.AddWithValue("$type", type);

                var result = select.ExecuteScalar();

                int number = 1;

                if (result != null)
                {
                    number = Convert.ToInt32(result) + 1;

                    var update = connection.CreateCommand();
                    update.CommandText =
                    @"
                UPDATE DocumentNumbers
                SET LastNumber = $number
                WHERE Date = $date AND Type = $type
                ";

                    update.Parameters.AddWithValue("$number", number);
                    update.Parameters.AddWithValue("$date", today);
                    update.Parameters.AddWithValue("$type", type);

                    update.ExecuteNonQuery();
                }
                else
                {
                    var insert = connection.CreateCommand();
                    insert.CommandText =
                    @"
                INSERT INTO DocumentNumbers (Date, Type, LastNumber)
                VALUES ($date, $type, 1)
                ";

                    insert.Parameters.AddWithValue("$date", today);
                    insert.Parameters.AddWithValue("$type", type);

                    insert.ExecuteNonQuery();
                }

                return $"{number:000}";
                return $"{DateTime.Now:yyyy}-{number:000}";
            }
        }
    }
}
