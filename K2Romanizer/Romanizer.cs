#region
using System.Text;
#endregion

namespace K2Romanizer
{
    public class Romanizer
    {
        #region singleton
        private static Romanizer _instance;

        public static Romanizer Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Romanizer();
                return _instance;
            }
        }

        private Romanizer()
        {
        }
        #endregion

        public string Romanize(string source, Casing casing)
        {
            var builder = new StringBuilder();

            for (int i = 0; i < source.Length; i++)
            {
                var value = CharConverter.Instance[source[i]];

                value = (casing, i) switch
                {
                    (Casing.Upper, _) => value.ToUpper(),
                    (Casing.Pascal, _) => value.Capitalize(),
                    (Casing.Noun, 0) => value.Capitalize(),
                    (Casing.Noun, _) => value,
                    (Casing.Camel, 0) => value,
                    (Casing.Camel, _) => value.Capitalize(),
                    (Casing.Snake, _) => value + "_",
                    _ => value
                };

                builder.Append(value);
            }

            var target = builder.ToString();

            if (target.EndsWith("_"))
                target = target[Range.EndAt(^1)];

            return target;
        }
    }
}