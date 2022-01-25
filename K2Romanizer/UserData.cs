using System.Collections.Generic;
using System.IO;
using System.Text;

namespace K2Romanizer
{
    public class UserData
    {
        #region singleton
        private static UserData _instance;

        public static UserData Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UserData();
                return _instance;
            }
        }

        private UserData()
        {
        }
        #endregion

        private const string FileName = "UserData.txt";

        private readonly List<KeyValuePair<char, string>> _items = new List<KeyValuePair<char, string>>();

        public void Add(char key, string value)
        {
            Add(new KeyValuePair<char, string>(key, value));
        }

        public void Add(KeyValuePair<char, string> item)
        {
            _items.Add(item);
        }

        public void SaveData()
        {
            var text = File.Exists(FileName) ? File.ReadAllText("UserData.txt") : string.Empty;

            StringBuilder builder = new StringBuilder(text);
            foreach (var item in _items)
            {
                builder.AppendLine();
                builder.AppendFormat("{0}\t{1}", item.Key, item.Value);
            }

            File.WriteAllText(FileName, builder.ToString());
        }
    }
}