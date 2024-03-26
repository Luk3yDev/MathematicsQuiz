using System;
using System.Linq;

namespace MathematicsQuiz
{
    class Program
    {
        // Global variables
        static string operators = "+-*/";
        static int current_question = 0;

        static string questions = "";
        static int total_questions;
        static int correct_answers;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Maths Quiz! \n\nWhat is your name?");

            Console.ForegroundColor = ConsoleColor.Green;
            string name = Console.ReadLine();
            Console.ResetColor();
            name = CheckName(name);

            Console.WriteLine($"\nHi {name}, \nLet's begin!\n");

            while (true)
            {
                current_question++;
                Console.WriteLine($"Question {current_question}:");
                OneQuestion();

                Console.WriteLine($"Would you like another question? (Y/N)");
                if (Console.ReadLine().ToUpper() == "N")
                {
                    break;
                }
            }

            // Print question history
            Console.Clear();
            Console.WriteLine($"Question History: \n\n{questions}You got {correct_answers}/{total_questions} questions correct!");
        }

        static void OneQuestion()
        {
            float user_answer;
            float correct_answer = 0.0f;

            // Generate first number
            Random rnd = new Random();
            int first_num = rnd.Next(0, 10);

            // Generate second number
            int second_num = rnd.Next(0, 10);

            // Pick a random operator
            rnd = new Random();
            char current_operator = operators[rnd.Next(0, operators.Length)];

            // Calculate correct answer
            if (current_operator == '+')
            {
                correct_answer = first_num + second_num;
            }
            else if (current_operator == '-')
            {
                correct_answer = first_num - second_num;
            }
            else if (current_operator == '*')
            {
                correct_answer = first_num * second_num;
            }
            else if (current_operator == '/')
            {
                if (second_num == 0)
                    second_num++;

                correct_answer = first_num / second_num;
            }

            // Print the question
            Console.WriteLine($"{first_num} {current_operator} {second_num} = ?");

            // If the question is division, let the user know that the answer should be rounded to 1 decimal place
            if (current_operator == '/')
                Console.WriteLine("(Rounded to 1 decimal place)");

            user_answer = CheckFloat();

            // Compare correct answer to user answer
            if (user_answer == MathF.Round(correct_answer, 1))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You got it right! :D");
                questions = questions + ($"Question {current_question}: \n{first_num} {current_operator} {second_num} = {correct_answer} \nYour answer was {user_answer}, which was correct.\n\n");
                correct_answers++;
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Incorrect!");
                questions = questions + ($"Question {current_question}: \n{first_num} {current_operator} {second_num} = {correct_answer} \nYour answer was {user_answer}, which was incorrect.\n" +
                    $"The correct answer was {correct_answer}\n\n");
                Console.ResetColor();
            }
            total_questions++;
        }

        // Check integers
        static float CheckFloat()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                string input = Console.ReadLine();
                Console.ResetColor();

                float number;

                if (!float.TryParse(input, out number))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR! Please enter a number.");
                    Console.ResetColor();
                }
                else
                {
                    return number;
                }
            }
        }

        // Check names
        static string CheckName(string name)
        {
            while (name == "")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR! Please enter a name.");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                name = Console.ReadLine();
                Console.ResetColor();
            }

            name = char.ToUpper(name[0]) + name.Substring(1);
            return name;
        }
    }
}