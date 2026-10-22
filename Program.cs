
using System;
using System.Collections.Generic;

public class MealyBinaryValidator
{
    // States
    private enum State
    {
        q0,
        q1,
        q2,
        q3,
        qTrap
    }

    private static (State NextState, char Output) Transition(
        State state, char input)
    {
        switch (state)
        {
            case State.q0:
                if (input == '1')
                    return (State.q1, '0');

                return (State.qTrap, '0');

            case State.q1:
                if (input == '0')
                    return (State.q2, '0');

                return (State.qTrap, '0');

            case State.q2:
                if (input == '1')
                    return (State.q3, '1');

                return (State.qTrap, '0');

            case State.q3:
                // The string must be exactly 101.
                return (State.qTrap, '0');

            case State.qTrap:
                return (State.qTrap, '0');

            default:
                return (State.qTrap, '0');
        }
    }

    public static (string Outputs, bool IsAccepted) ProcessString(
        string input)
    {
        if (string.IsNullOrEmpty(input))
            return ("", false);

        State currentState = State.q0;
        List<char> outputs = new List<char>();

        foreach (char symbol in input)
        {

            if (symbol != '0' && symbol != '1')
                return ("Invalid input", false);

            var result = Transition(currentState, symbol);

            outputs.Add(result.Output);
            currentState = result.NextState;
        }

        string outputSequence = new string(outputs.ToArray());


        bool accepted =
            outputs.Count > 0 &&
            outputs[outputs.Count - 1] == '1';

        return (outputSequence, accepted);
    }

    public static void Main()
    {
        while (true)
        {
            Console.Write(
                "\nEnter binary string (or type 'exit' to quit): ");

            string input = Console.ReadLine() ?? "";

            if (input.Trim().Equals(
                "exit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            var result = ProcessString(input);

            Console.WriteLine($"Input:  {input}");
            Console.WriteLine($"Output: {result.Outputs}");
            Console.WriteLine(
                $"Result: {(result.IsAccepted ? "ACCEPTED" : "REJECTED")}");
        }
    }
}
