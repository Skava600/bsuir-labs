
using System.Text;
using System.Text.RegularExpressions;


string GetRandomName(int length)
{
    Random r = new Random();
    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    char[] buffer = new char[length];

    for (int i = 0; i < length; i++)
    {
        buffer[i] = chars[r.Next(chars.Length)];
    }

    return new string(buffer);
}

void PrintToFile(string fileName, string text)
{
    File.WriteAllText(fileName, text);
}

void AddUnreachableCode(string input, string output)
{
    StringBuilder stringBuilder = new();
    using (StreamReader sr = new StreamReader(input))
    {
        while (sr.Peek() >= 0)
        {
            string line = sr.ReadLine();

            stringBuilder.Append(line);
            stringBuilder.Append("\n");

            if (line.Trim() == "break;" || line.Trim() == "return;")
            {
                stringBuilder.Append("MessageBox.Show($\"User has some important info that was sent to him!\", \"User Info!\");");
            }
        }
    }

    PrintToFile(output, stringBuilder.ToString());
}

void AddDeadCode(string input, string output)
{
    StringBuilder stringBuilder = new();
    using (StreamReader sr = new StreamReader(input))
    {
        bool shouldInsertCode = false;

        while (sr.Peek() >= 0)
        {
            string line = sr.ReadLine();

            stringBuilder.Append(line);
            stringBuilder.Append("\n");

            if (shouldInsertCode)
            {
                Random r = new Random();
                var randomValue = r.Next() % 10;
                if (randomValue < 3)
                {
                    stringBuilder.Append($"string {GetRandomName(randomValue + 7)} = \"Do not delete this one, it is bottom-line critical!\";");
                }
                else
                if (randomValue < 6)
                {
                    stringBuilder.Append($"int {GetRandomName(randomValue + 2)} = {r.Next() % 4123} * 2 / 3 * 123 - 1337;");
                }
                else
                {
                    stringBuilder.Append($"double {GetRandomName(5)} = 355 / 113;");
                }
                shouldInsertCode = false;
            }
            if (Regex.IsMatch(line.Trim(), @"^if(.+)$"))
            {
                shouldInsertCode = true;
            }
        }
    }

    PrintToFile(output, stringBuilder.ToString());
}

void TrimCode(string input, string output)
{
    StringBuilder stringBuilder = new();
    using (StreamReader sr = new StreamReader(input))
    {
        bool shouldInsertCode = false;

        while (sr.Peek() >= 0)
        {
            string line = sr.ReadLine().Trim();
            stringBuilder.Append(line);
        }
    }

    PrintToFile(output, stringBuilder.ToString());
}

AddUnreachableCode("input.txt", "output1.txt");
AddDeadCode("output1.txt", "output2.txt");
TrimCode("output2.txt", "output3.txt");