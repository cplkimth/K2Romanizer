namespace K2Romanizer
{
    public class CharConverter
    {
        #region singleton
        private static readonly Lazy<CharConverter> _instance = new(() => new CharConverter());

        public static CharConverter Instance => _instance.Value;

        private CharConverter()
        {
        }
        #endregion

        private SortedDictionary<char, string> _dictionary;

        public void Initialize()
        {
            Initialize("SystemData.txt", "UserData.txt");
        }

        public void Initialize(string systemDataFilePath, string userDataFilePath)
        {
            _dictionary = ReadDataFile(systemDataFilePath);
            var userDictionary = ReadDataFile(userDataFilePath);

            var duplicatedItems = new List<KeyValuePair<char, string>>();
            foreach (var userItem in userDictionary)
                if (_dictionary.ContainsKey(userItem.Key))
                    duplicatedItems.Add(userItem);
                else
                    _dictionary.Add(userItem.Key, userItem.Value);

            if (duplicatedItems.Count > 0)
            {
                DuplicatedItemsFoundEventArgs arg = new DuplicatedItemsFoundEventArgs(duplicatedItems, false);
                OnDuplicatedItemsFound(arg);

                if (arg.UseSystemData == false)
                    foreach (var item in duplicatedItems)
                        _dictionary[item.Key] = item.Value;
            }
        }

        private SortedDictionary<char, string> ReadDataFile(string relativePath)
        {
            var dictionary = new SortedDictionary<char, string>();

            var lines = File.ReadAllLines(relativePath);
            foreach (var line in lines)
            {
                var tokens = line.Split('\t');

                if (tokens.Length != 2)
                    continue;

                if (dictionary.ContainsKey(tokens[0][0]))
                {
                    OnDuplicatedKeyFound(tokens[0][0]);
                    continue;
                }

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

                if (_dictionary.ContainsKey(key) == false)
                {
                    MissingItemFoundEventArgs arg = new MissingItemFoundEventArgs(key, null);
                    OnMissingItemFound(arg);

                    _dictionary.Add(arg.Key, arg.Value);
                }

                return _dictionary[key];
            }
        }

        #region MissingItemFound event things for C# 3.0
        public event EventHandler<MissingItemFoundEventArgs> MissingItemFound;

        protected virtual void OnMissingItemFound(MissingItemFoundEventArgs e)
        {
            if (MissingItemFound != null)
                MissingItemFound(this, e);
        }

        protected virtual void OnMissingItemFound(char key, string value)
        {
            if (MissingItemFound != null)
                MissingItemFound(this, new MissingItemFoundEventArgs(key, value));
        }

        public class MissingItemFoundEventArgs : EventArgs
        {
            public char Key { get; set; }
            public string Value { get; set; }

            public MissingItemFoundEventArgs()
            {
            }

            public MissingItemFoundEventArgs(char key, string value)
            {
                Key = key;
                Value = value;
            }
        }
        #endregion

        #region DuplicatedItemsFound event things for C# 3.0
        public event EventHandler<DuplicatedItemsFoundEventArgs> DuplicatedItemsFound;

        protected virtual void OnDuplicatedItemsFound(DuplicatedItemsFoundEventArgs e)
        {
            if (DuplicatedItemsFound != null)
                DuplicatedItemsFound(this, e);
        }

        protected virtual void OnDuplicatedItemsFound(List<KeyValuePair<char, string>> items, bool useSystemData)
        {
            if (DuplicatedItemsFound != null)
                DuplicatedItemsFound(this, new DuplicatedItemsFoundEventArgs(items, useSystemData));
        }

        public class DuplicatedItemsFoundEventArgs : EventArgs
        {
            public List<KeyValuePair<char, string>> Items { get; set; }
            public bool UseSystemData { get; set; }

            public DuplicatedItemsFoundEventArgs()
            {
            }

            public DuplicatedItemsFoundEventArgs(List<KeyValuePair<char, string>> items, bool useSystemData)
            {
                Items = items;
                UseSystemData = useSystemData;
            }
        }
        #endregion

        #region DuplicatedKeyFound event things for C# 3.0
        public event EventHandler<DuplicatedKeyFoundEventArgs> DuplicatedKeyFound;

        protected virtual void OnDuplicatedKeyFound(DuplicatedKeyFoundEventArgs e)
        {
            if (DuplicatedKeyFound != null)
                DuplicatedKeyFound(this, e);
        }

        protected virtual void OnDuplicatedKeyFound(char key)
        {
            if (DuplicatedKeyFound != null)
                DuplicatedKeyFound(this, new DuplicatedKeyFoundEventArgs(key));
        }

        public class DuplicatedKeyFoundEventArgs : EventArgs
        {
            public char Key { get; set; }

            public DuplicatedKeyFoundEventArgs()
            {
            }

            public DuplicatedKeyFoundEventArgs(char key)
            {
                Key = key;
            }
        }
        #endregion
    }
}