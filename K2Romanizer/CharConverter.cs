namespace K2Romanizer
{
    public class CharConverter
    {
        #region singleton
        private static readonly Lazy<CharConverter> _instance = new(() => new CharConverter());

        public static CharConverter Instance => _instance.Value;

        private CharConverter()
        {
            _dictionary = ReadDataFile();
        }
        #endregion

        private readonly SortedDictionary<char, string> _dictionary;

        private SortedDictionary<char, string> ReadDataFile()
        {
            var dictionary = new SortedDictionary<char, string>();

            var text = ResourceHelper.Instance.GetSystemData();
            var lines = text.Replace(Environment.NewLine, "\n").Split('\n');
            foreach (var line in lines)
            {
                var tokens = line.Split('\t');

                if (tokens.Length != 2)
                    continue;

                if (dictionary.ContainsKey(tokens[0][0]))
                    continue;

                dictionary.Add(tokens[0][0], tokens[1]);
            }

            return dictionary;
        }

        public string this[char key]
        {
            get
            {
                int unicodeValue = Convert.ToInt32(key);
                if (unicodeValue is < 44032 or > 55203) //한글
                    return key.ToString();

                if (_dictionary.ContainsKey(key))
                    return _dictionary[key];

                return "?";
            }
        }
    }
}