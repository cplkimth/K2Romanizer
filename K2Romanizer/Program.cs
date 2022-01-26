#region
using K2Romanizer;
using TextCopy;
#endregion

public static class Program
{
    public static void Main(string[] args)
    {
#if DEBUG
        args = new[] {"p", "가처분"};
#endif
        if (args.Length != 2)
        {
            PrintHelp();
            return;
        }

        bool parsed = ParseCasing(args[0], out Casing casing);
        if (parsed is false)
        {
            Console.WriteLine($"'{args[0]}'은(는) 지원되지 않는 케이싱입니다..");
            return;
        }

        var target = Romanizer.Romanize(args[1], casing);
        Console.WriteLine($"{args[1]} => {target}");

        Console.WriteLine($"변환된 '{target}'을(를) 클립보드로 복사할까요? (Y/n)");
        var copyable = Console.ReadLine().ToLower() != "n";

        if (copyable is false)
            return;

        Clipboard clipboard = new Clipboard();
        clipboard.SetText(target);
        Console.WriteLine("복사하였습니다.");
    }

    private static bool ParseCasing(string arg, out Casing casing)
    {
        try
        {
            casing = arg.ToLower() switch
            {
                "p" => Casing.Pascal,
                "c" => Casing.Camel,
                "s" => Casing.Snake,
                "u" => Casing.Upper,
                "l" => Casing.Lower,
                "g" => Casing.Noun,
                _ => throw new ArgumentException(),
            };

            return true;
        }
        catch
        {
            casing = Casing.Pascal;
            return false;
        }
    }

    private static void PrintHelp()
    {
        Console.WriteLine(ResourceHelper.Instance.GetHelp());
    }
}