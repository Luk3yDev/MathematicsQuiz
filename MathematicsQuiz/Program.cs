using System;
using System.Linq;

namespace MathematicsQuiz
{
    class Program
    {
        // Global variables
        static string operators = "+-*/";
        static int current_question = 0;

        static List<string> question_history = new List<string>();
        static int total_questions;
        static int correct_answers;

        static void Main(string[] args)
        {
            // Print cool ASCII title
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\r\n___  ___      _   _       _____       _     \r\n" +
                "|  \\/  |     | | | |     |  _  |     (_)    \r\n" +
                "| .  . | __ _| |_| |__   | | | |_   _ _ ____\r\n" +
                "| |\\/| |/ _` | __| '_ \\  | | | | | | | |_  /\r\n" +
                "| |  | | (_| | |_| | | | \\ \\/' / |_| | |/ / \r\n" +
                "\\_|  |_/\\__,_|\\__|_| |_|  \\_/\\_\\\\__,_|_/___|\r\n" +
                "      \nBy Luke Madsen\n");
            Console.ResetColor();

            Console.WriteLine("Welcome to the Maths Quiz! \n\nWhat is your name?");

            Console.ForegroundColor = ConsoleColor.Green;
            string name = Console.ReadLine();
            Console.ResetColor();
            name = CheckName(name);

            Console.WriteLine($"\nHi {name}, \nLet's begin!\n");

            // Loop ask the user questions until they no longer want another
            while (true)
            {
                current_question++;
                Console.WriteLine($"Question {current_question}:");
                OneQuestion();

                Console.WriteLine($"Would you like another question? (Y/N)");
                Console.ForegroundColor = ConsoleColor.Green;

                string new_question = "";
                while (new_question != "Y" && new_question != "N")
                {
                    new_question = Console.ReadLine().ToUpper();
                    if (new_question != "Y" && new_question != "N")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR! Please enter either Y or N!");
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                }
                if (new_question == "N")
                {
                    Console.ResetColor();
                    break;
                }
                else if (new_question != "Y")
                {
                    Console.ResetColor();
                    return;
                }
            }

            // Print question history
            Console.Clear();
            Console.WriteLine($"Question History:\n\n");

            // Loop over every question in the question history list
            for (int i = 0; i < question_history.Count; i++)
            {
                Console.WriteLine($"{question_history[i]}");
            }

            // Question summary
            Console.WriteLine($"You got {correct_answers}/{total_questions} questions correct!");
        }

        // Randomly generates a question for the user, and compares the correct answer to their answer
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
                Console.ForegroundColor = ConsoleColor.Cyan; 
                correct_answer = first_num + second_num;
            }
            else if (current_operator == '-')
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                correct_answer = first_num - second_num;
            }
            else if (current_operator == '*')
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                correct_answer = first_num * second_num;
            }
            else if (current_operator == '/')
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                if (second_num == 0)
                    second_num++;

                correct_answer = MathF.Round((float)first_num / (float)second_num, 2);
            }

            // Print the question
            Console.WriteLine($"{first_num} {current_operator} {second_num} = ?");

            // If the question is division, let the user know that the answer should be rounded to 2 decimal places
            if (current_operator == '/')
                Console.WriteLine("(Rounded to 2 decimal places)");

            // Store the user's answer
            user_answer = CheckFloat();

            // Compare correct answer to user answer
            if (user_answer == correct_answer)
            {
                // Correct
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You got it right! :D");
                question_history.Add($"Question {current_question}: \n{first_num} {current_operator} {second_num} = {correct_answer} \nYour answer was {user_answer}, which was correct.\n\n");
                correct_answers++;
                Console.ResetColor();
            }
            else
            {
                // Incorrect
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Incorrect! The correct answer was {correct_answer}");
                question_history.Add($"Question {current_question}: \n{first_num} {current_operator} {second_num} = {correct_answer} \nYour answer was {user_answer}, which was incorrect.\n" +
                    $"The correct answer was {correct_answer}\n\n");
                Console.ResetColor();
            }
            total_questions++;
        }

        // Check floats
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

            // Capitalise first letter
            name = char.ToUpper(name[0]) + name.Substring(1);
            return name;
        }
    }
}