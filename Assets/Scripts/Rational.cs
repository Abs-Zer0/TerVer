using System;
using System.Collections;
using UnityEngine;

[Serializable]
public struct Rational
{
    public int Enumerator { get; set; }
    public int Denomenator { get; set; }

    public Rational(int enumerator, int denomenator)
    {
        this.Enumerator = enumerator;
        this.Denomenator = denomenator;

        Normalize();
    }

    public static Rational operator +(Rational self, Rational other)
    {
        return new Rational(self.Enumerator + other.Enumerator, self.Denomenator + other.Denomenator);
    }

    public static Rational operator +(Rational self, int other)
    {
        return new Rational(self.Enumerator + (int)self.Denomenator * other, self.Denomenator);
    }

    public static Rational operator *(Rational self, Rational other)
    {
        return new Rational(self.Enumerator * other.Enumerator, self.Denomenator * other.Denomenator);
    }

    public static Rational operator *(Rational self, int other)
    {
        return new Rational(self.Enumerator * other, self.Denomenator);
    }

    public static bool operator ==(Rational self, Rational other)
    {
        self.Normalize();
        other.Normalize();

        return self.Enumerator == other.Enumerator && self.Denomenator == other.Denomenator;
    }

    public static bool operator !=(Rational self, Rational other)
    {
        self.Normalize();
        other.Normalize();

        if (self.Enumerator != other.Enumerator)
            return true;

        if (self.Denomenator != other.Denomenator)
            return true;

        return false;
    }

    public Rational Pow(int n)
    {
        int nn = n;

        if (nn == 0)
            return one;

        Rational res = nn < 0 ? new Rational(this.Denomenator, this.Enumerator) : new Rational(this.Enumerator, this.Denomenator);

        while (nn > 1)
        {
            res.Enumerator *= res.Enumerator;
            res.Denomenator *= res.Denomenator;
            nn--;
        }

        return res;
    }

    public void Normalize()
    {
        int nod = Functions.NOD(this.Enumerator, this.Denomenator);

        this.Enumerator /= nod;
        this.Denomenator /= nod;
    }

    public override string ToString()
    {
        return "( " + this.Enumerator.ToString() + " / " + this.Denomenator.ToString() + " )";
    }

    public static Rational zero = new Rational(0, 1);
    public static Rational one = new Rational(1, 1);
    public static Rational half = new Rational(1, 2);
}
