namespace WordGuess
{
    internal class WordGuess
    {
        public static string[] words = {
                "planet", "silver", "forest", "battle", "charge",
                "stream", "random", "memory", "vision", "thread",
                "dragon", "border", "friend", "mirror", "stable",
                "object", "screen", "method", "symbol", "player",
                "window", "future", "danger", "castle", "rocket",
                "island", "shadow", "hunter", "bridge", "camera"
        };

        public static void Validations(char input)
        {
            
        }

        public static void Display(char[] ans, int lives, HashSet<char> flag)
        {
            Console.WriteLine("Word :  " + string.Join(' ', ans));
            Console.WriteLine("Lives Left :  " + lives);
            Console.Write("Guessed :  ");
            foreach (var item in flag)
            {
                Console.Write((char)item + " ");
            }
            Console.WriteLine();

        }
        static void Main(string[] args)
        {
            Random rnd = new Random();
            
            //Word Selection
            int idx = rnd.Next(words.Length);
            string word = words[idx];
            word = word.ToUpper();

            //Flag
            HashSet<char> flag = new HashSet<char>();

            //User Inoputs
            char input=' ';
            char[] ans = new char[word.Length];
            Array.Fill(ans, '_');

            //Other Variables
            int lives = 6;
            int count = 0;

            Console.WriteLine(word);
            //Console.WriteLine(string.Join(" ", ans));
            while (lives > 0 && count!=word.Length)
            {
                Console.WriteLine();
                Display(ans, lives, flag);
                try
                {
                    Console.Write("Enter Word : ");
                    input = Console.ReadLine()[0];
                    input = Char.ToUpper(input);
                    if (input < 'A' || input > 'Z')
                    {
                        Console.WriteLine("Enter Value Again");
                        continue;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Enter Right Character");
                    continue;
                }

                if (flag.Contains(input))
                {
                    Console.WriteLine("You Already Used The word");
                    //Display(ans, lives, flag);
                    continue;    
                }

                else
                    flag.Add(input);

                bool check = false;
                for(int i =0; i<word.Length; i++)
                {
                    if (word[i] == input)
                    {
                        check = true;
                        ans[i] = word[i];
                        //Display(ans, lives, flag);
                        count++;
                    }
                }
                if (!check)
                {
                    lives--;
                    Console.WriteLine("Wrong Value");
                    //Display(ans, lives, flag);
                }
                
            }

            if(lives > 0 && count == word.Length )
                Console.WriteLine("You Won");
            else
                Console.WriteLine("You Lost");

        }
    }
}