static void Display(int num = 40, char ch = '-')
{
    for(int i = 0; i < num; i++)
    {
        Console.Write(ch);
    }
    Console.WriteLine();
}
static void Main(string[] args)
{
    Display();
    Display(40, '+');
    Display(60, '$');
}