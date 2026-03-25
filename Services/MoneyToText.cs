namespace InvocePDF.Services
{
    public static class MoneyToText
    {
        private static readonly string[] Units =
        {
        "", "один", "два", "три", "четыре", "пять",
        "шесть", "семь", "восемь", "девять", "десять",
        "одиннадцать", "двенадцать", "тринадцать",
        "четырнадцать", "пятнадцать", "шестнадцать",
        "семнадцать", "восемнадцать", "девятнадцать"
    };

        private static readonly string[] UnitsFemale =
        {
        "", "одна", "две", "три", "четыре", "пять",
        "шесть", "семь", "восемь", "девять", "десять",
        "одиннадцать", "двенадцать", "тринадцать",
        "четырнадцать", "пятнадцать", "шестнадцать",
        "семнадцать", "восемнадцать", "девятнадцать"
    };

        private static readonly string[] Tens =
        {
        "", "", "двадцать", "тридцать", "сорок",
        "пятьдесят", "шестьдесят", "семьдесят",
        "восемьдесят", "девяносто"
    };

        private static readonly string[] Hundreds =
        {
        "", "сто", "двести", "триста", "четыреста",
        "пятьсот", "шестьсот", "семьсот",
        "восемьсот", "девятьсот"
    };

        public static string Convert(decimal amount)
        {
            long rubles = (long)Math.Floor(amount);
            int kopeks = (int)Math.Round((amount - rubles) * 100);

            return $"{NumberToText(rubles)} {GetRubles(rubles)} {kopeks:00} {GetKopeks(kopeks)}";
        }

        private static string NumberToText(long number)
        {
            if (number == 0)
                return "ноль";

            string result = "";

            int thousands = (int)(number / 1000);
            int remainder = (int)(number % 1000);

            if (thousands > 0)
            {
                result += $"{ConvertHundreds(thousands, true)} {GetThousands(thousands)} ";
            }

            if (remainder > 0)
            {
                result += ConvertHundreds(remainder, false);
            }

            result.Trim();
            return char.ToUpper(result[0]) + result.Substring(1);
        }

        private static string ConvertHundreds(int number, bool feminine)
        {
            string result = Hundreds[number / 100];
            number %= 100;

            if (number < 20)
            {
                result += " " + GetUnits(number, feminine);
            }
            else
            {
                result += " " + Tens[number / 10];
                result += " " + GetUnits(number % 10, feminine);
            }

            return result.Trim();
        }

        private static string GetUnits(int number, bool feminine)
        {
            return feminine ? UnitsFemale[number] : Units[number];
        }

        private static string GetThousands(int number)
        {
            number %= 100;

            if (number >= 11 && number <= 19)
                return "тысяч";

            switch (number % 10)
            {
                case 1: return "тысяча";
                case 2:
                case 3:
                case 4: return "тысячи";
                default: return "тысяч";
            }
        }

        private static string GetRubles(long number)
        {
            number %= 100;

            if (number >= 11 && number <= 19)
                return "рублей";

            switch (number % 10)
            {
                case 1: return "рубль";
                case 2:
                case 3:
                case 4: return "рубля";
                default: return "рублей";
            }
        }

        private static string GetKopeks(int number)
        {
            if (number >= 11 && number <= 19)
                return "копеек";

            switch (number % 10)
            {
                case 1: return "копейка";
                case 2:
                case 3:
                case 4: return "копейки";
                default: return "копеек";
            }
        }
    }
}
