using UnityEngine;
using System.Collections;

public struct Functions
{
    public static int Factorial(int x)
    {
        return (x == 0) ? 1 : x * Factorial(x - 1);
    }

    public static Rational Binom(int n, int k)
    {
        if (n < k)
            return Rational.zero;

        return new Rational(Factorial(n), Factorial(k) * Factorial(n - k));
    }

    public static int NOD(int a, int b)
    {
        int na = a, nb = b;

        while (na != 0 && nb != 0)
        {
            if (na > nb)
                na %= nb;
            else
                nb %= na;
        }

        return na + nb;
    }

    public static string Times(int number)
    {
        if (number % 10 == 2 || number % 10 == 3 || number % 10 == 4)
            return "раза";

        return "раз";
    }
}
