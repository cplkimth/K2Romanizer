#region
using System.Text;
#endregion

namespace K2Romanizer
{
    public class UserData
    {
        #region singleton
        private static readonly Lazy<UserData> _instance = new(() => new UserData());

        public static UserData Instance => _instance.Value;

        private UserData()
        {
        }
        #endregion

        public const string FileName = "UserData.txt";

        private readonly List<KeyValuePair<char, string>> _items = new();

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