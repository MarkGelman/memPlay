using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemorryPlay
{
    class Program
    {
        static void Main(string[] args)

        {
            Random rnd = new Random();
            int row = 0;
            int column = 0;
            int[] balls = { 0, 0 } ;
 
            Console.WriteLine("Please enter size of table(number fo size must be):");
            int size = int.Parse(Console.ReadLine());
            Console.Clear();

            while (size%2 !=0 || size<=0)
            {
                Console.WriteLine($"Entered data '{size}' is not correct!!!");
                Console.WriteLine("Please enter size of table(number fo size must be):");
                size = int.Parse(Console.ReadLine());
                Console.Clear();
            }

            
             char[,,] field = new char[size, size, 2];

            int numberPlayCards = size*size / 2;
          
            for (int i = 0; i < numberPlayCards; i++)
            {
                char playCard = (char)rnd.Next(65, 90);
                for (int j = 0; j < 2; j++)
                {
                    do
                    {
                        row = rnd.Next(0, size );
                        column = rnd.Next(0, size);
                    }
                    while (field[row, column, 1] != '\0');

                    field[row, column, 1] = playCard;
                }
            }
            Play(size,field,numberPlayCards);
            Console.ReadKey();
        }

        static void Play (int size,char [,,] field,int numberPlayCards)
        {
            int[] balls = { 0, 0 };
            int [] coordinats = { size, size, size, size };
            bool starsField = true;
            bool firstCard = true;
            bool flag = false;

            while (balls[0] + balls[1] != numberPlayCards)
            {
                 for (int i = 1; i<=2;i++)
                 {
                    Demand(null, size, balls, field, coordinats, starsField, flag,"first", i);
                    coordinats = Coordinats(coordinats, firstCard, size, balls, field, starsField);
                    firstCard = false;
                    starsField = false;
                    Console.Clear();


                    Demand(null, size, balls, field, coordinats, starsField, flag,"second", i);
                    coordinats = Coordinats(coordinats,firstCard, size, balls, field, starsField);
                    firstCard = true;
                    Console.Clear();

                    if (field[coordinats[0],coordinats[1],1] == field[coordinats[2], coordinats[3],1])
                    {
                        balls[i - 1]++;
                        field[coordinats[0], coordinats[1], 1] = '0';
                        field[coordinats[2], coordinats[3], 1] = '0';
                        if (i == 1)
                        {
                            Console.WriteLine("Plus a point to the first player!!! Please press any key to transfer the game to the second player.!!!");
                            Appearance(size, balls, field, coordinats, starsField);
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else
                        {
                            Console.WriteLine("Plus a point to the second player!!! Please press any key to transfer the game to  first player!!!");
                            Appearance(size, balls, field, coordinats, starsField);
                            Console.ReadKey();
                            Console.Clear();
                        }
                        
                    }
                    else
                    {
                        if (i == 1)
                        {
                            Console.WriteLine("You need to take training to improve memory !!!!!! Please press any key to transfer the game to the second player.!!!");
                            Appearance(size, balls, field, coordinats, starsField);
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else
                        {
                            Console.WriteLine("You need to take training to improve memory !!!!!! Please press any key to transfer the game to the second player.!!!");
                            Appearance(size, balls, field, coordinats, starsField);
                            Console.ReadKey();
                            Console.Clear();
                        }

                        for (int j=0; j<4; j++)
                        {
                            coordinats[j] = size;
                        }
                        starsField = true;
                    }
                 }
            }
        }

        static int [] Coordinats (int [] coordinats,bool primaryCard,int size,int [] balls, char [,,] field,bool starsField)
        {
            bool flag = true;
            string [] str1;
            int num1;
               
            string str = Console.ReadLine();
            int indexOfChar = str.IndexOf(',');
            while (indexOfChar == -1)
            {
                Demand(str, size, balls, field, coordinats, starsField,flag,null,0);
                str = Console.ReadLine();
                indexOfChar = str.IndexOf(',');
            }

            str1 = str.Split(',');

            for (int i = 0; i<2; i++)
            {
                num1 = int.Parse(str1[i]);
                if (num1 > 7)
                {
                    Demand(str, size, balls, field, coordinats, starsField,flag,null,0);
                    Coordinats(coordinats,primaryCard,size,balls,field,starsField);
                }
                if (primaryCard)
                    coordinats[i] = num1;
                else
                {
                    coordinats[i + 2] = num1;
                }
            }

            return coordinats;
         

        }

        static void Demand (string str, int size, int[] balls, char[,,] field,int [] coordinats, bool starsField,bool flag,string numCard,int numPlayer)
        {
            if (flag)
            {
                Console.Clear();
                Console.WriteLine($"Entered data '{str}' is not correct!!!");
                Console.WriteLine($"Entered number row and column of card separated by commas:");
                Console.WriteLine();
                Appearance(size, balls, field, coordinats, starsField);
            }
            else
            {
                Console.WriteLine($"Player number: {numPlayer}");
                Console.WriteLine();
                Console.WriteLine($"     Entered coordinats row and column оf {numCard} play card  separated by commas:");
                Console.WriteLine();
                Appearance(size, balls, field, coordinats, starsField);
                Console.WriteLine();
            }
        }

        static void Appearance (int size, int [] balls,char [,,] field, int [] coordinats,bool starsField)
        {
            Border(size, true, balls);
            OpenField(size, field, coordinats,starsField);
            Border(size, false, balls);
        }

        static void Border(int size,bool header,int [] balls)
        {
            if (header)
            {
                Console.WriteLine($"Player1: { balls[0]}      Player2: { balls[1]}");
                Console.WriteLine();
                Console.Write("   ");
                for (int j = 0; j < size; j++)
                {
                    Console.Write(" " + j);
                }
                Console.WriteLine();
            }
            Console.Write("  ");
            for (int j = 0; j < size; j++)
            {
                Console.Write("--");
            }
            Console.WriteLine("---");
        }

        

        static void OpenField(int size, char[,,] field, int[] coordinats,bool starsField)
        {
            if (starsField)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                      if (field[i, j, 0] != '0')
                      {
                          field[i, j, 0] = '*';
                      }
                    }
                }
            }
            for (int i = 0; i < size; i++)
            {
                Console.Write(" " + i + "|");
                for (int j = 0; j < size; j++)
                {
                    if ((i == coordinats[0] && j == coordinats[1]|| i == coordinats[2] && j == coordinats[3]))
                    {
                        field[i, j, 0] = field[i, j, 1];
                    }
                    Console.Write(" " + field[i, j, 0]);
                }
                Console.WriteLine(" |");
            }
           
        }

    }
}
