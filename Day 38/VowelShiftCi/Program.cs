namespace VowelShiftCi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter string: ");
            string input = Console.ReadLine();
            Dictionary<char, char> map = new Dictionary<char, char>()
                        {
                            {'a','e'},
                            {'e','i'},
                            {'i','o'},
                            {'o','u'},
                            {'u','a'}
                        };

            string res = "";

            foreach (char c in input)
            {
                if (map.ContainsKey(c))   
                {
                    res += map[c];
                }
                else  
                {
                
                    char next = (char)((c - 'a' + 1) % 26 + 'a');

                
                    if (map.ContainsKey(next))
                    {
                        next = (char)((next - 'a' + 1) % 26 + 'a');
                    }

                    res += next;
                }
            }

            Console.WriteLine("Result: " + res);
        }
    }
}
