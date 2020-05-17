using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace _7lab
{
    class Rational  : IComparable<Rational>, IEquatable<Rational>, IComparer<Rational>
    {
        private long Numerator { get; }
        private long Denominator { get; }
        ~Rational() { }
        //--------------Конструкторы----------------------------
        public Rational()
        {
            Denominator = 1;
            Numerator = 1;
        }
        public Rational(long a)
        {
            Numerator = a;
            Denominator = 1;
        }
        public Rational(long Numerator, long Denominator)
        {
            if (Denominator == 0)
            {
                throw new DivideByZeroException();
            }
            if (Denominator < 0)
            {
                Denominator = -Denominator;
                Numerator = -Numerator;
            }
            this.Numerator = Numerator / GCD(Math.Abs(Numerator), Math.Abs(Denominator));
            this.Denominator = Denominator/ GCD(Math.Abs(Numerator), Math.Abs(Denominator));

        }
        //-----------------------------------------------------

        //******ПЕРЕОПРЕДЕЛЕННЫЕ ОПЕРАТОРЫ*********************
        public static Rational operator +(Rational r1, Rational r2)
        {
            Rational result;

            if (r1.Denominator == r2.Denominator)
                result = new Rational(r1.Numerator + r2.Numerator, r1.Denominator);
            else
                result = new Rational((r1.Numerator * r2.Denominator) + (r2.Numerator * r1.Denominator), r1.Denominator * r2.Denominator);
            return result;
        }
        public static Rational operator +(Rational r2,long a )
        {
            Rational result;
            result = new Rational((a * r2.Denominator) + (r2.Numerator), r2.Denominator);
            return result;
        }
        public static Rational operator -(Rational r1, Rational r2)
        {
            Rational result;

            if (r1.Denominator == r2.Denominator)
                result = new Rational(r1.Numerator - r2.Numerator, r1.Denominator);
            else
                result = new Rational((r1.Numerator * r2.Denominator) - (r2.Numerator * r1.Denominator), r1.Denominator * r2.Denominator);
            return result;
        }
        public static Rational operator -(long a, Rational r2)
        {
            Rational result;
            result = new Rational((a * r2.Denominator) - r2.Numerator, r2.Denominator);
            return result;
        }
        public static Rational operator *(Rational r1, Rational r2)
        {
            Rational result = new Rational(r1.Numerator * r2.Numerator, r1.Denominator * r2.Denominator);
            return result;
        }
        public static Rational operator *(long a, Rational r2)
        {
            Rational result = new Rational(a * r2.Numerator, r2.Denominator);
            return result;
        }
        public static Rational operator /(Rational r1, Rational r2)
        {
            Rational result = new Rational(r1.Numerator * r2.Denominator, r1.Denominator * r2.Numerator);
            return result;
        }
        public static Rational operator /(long a, Rational r2)
        {
            Rational result = new Rational(a * r2.Denominator, r2.Numerator);
            return result;
        }
        public static Rational operator ++(Rational rational)
        {
            Rational result = new Rational(rational.Numerator + rational.Denominator,rational.Denominator);
            return result;
        }
        public static Rational operator --(Rational r)
        {
            Rational result = new Rational(r.Numerator - r.Denominator, r.Denominator);
            return result;
        }
        public static bool operator <(Rational r1, Rational r2)
        {
            return r1.CompareTo(r2) == -1;
        }
        public static bool operator >(Rational r1, Rational r2)
        {
            return r1.CompareTo(r2) == 1;
        }
        
        public static bool operator >=(Rational r1, Rational r2)
        {
            return r1.CompareTo(r2) != -1;
        }
        public static bool operator ==(Rational r1, Rational r2)
        {
            return r1.Equals((object)r2);
        }
        public static bool operator !=(Rational r1, Rational r2)
        {
            return !(r1 == r2);
        }
        public static bool operator <=(Rational r1, Rational r2)
        {
            return r1.CompareTo(r2) != 1;
        }
        public static Rational Parse(string input)
        {
            Regex regex;
            MatchCollection match;
            regex = new Regex(@"-?\d+/-?\d+");                  // ЕСЛИ ФОРМАТ (-)d/(-)d
            match = regex.Matches(input);
            if (match.Count == 1)
            {
                long num, den;
                string str = match[0].ToString();
                string[] nums = str.Split(new char[] { '/' });
                num = long.Parse(nums[0]);
                den = long.Parse(nums[1]);
                return new Rational(num, den);
            }
            regex = new Regex(@"-?\d+\,\d+");                  //ФОРМАТ (-)d,(-)d
            match = regex.Matches(input);
            if (match.Count == 1)
            {
                long num, den;
                string str = match[0].ToString();
                string[] nums = str.Split(new char[] { ',' });
                num = long.Parse(nums[0]);
                den = 1;
                for (int i = 0; i < nums[1].Length; i++)
                {
                    checked
                    {
                        num *= 10;
                        den *= 10;
                    }
                    num += nums[1][i] - 48;
                }
                return new Rational(num, den);
            }
            regex = new Regex(@"-?\d+\.\d+");                  //ФОРМАТ d.d
            match = regex.Matches(input);
            if (match.Count == 1)
            {
                long num,den;
                string str = match[0].ToString();
                string[] nums = str.Split(new char[] { '.' });
                num = long.Parse(nums[0]);
                den = 1;
                for (int i = 0; i < nums[1].Length; i++)
                {
                    checked
                    {
                        num *= 10;
                        den *= 10;
                    }
                        num += nums[1][i] - 48;                   
                }
                return new Rational(num, den);
            }

            regex = new Regex(@"-?\d+");               //ФОРМАТ (-)d
            match = regex.Matches(input);
            if (match.Count == 1)
            {
                long num;
                num = long.Parse(match[0].Value);            
                return new Rational(num, 1);
            }
            return null;
        }
        //*****НОД ДВУХ ЧИСЕЛ***************
        private long GCD(long a, long b)
        {
            if (a == 0)
            {
                return b;
            }
            else
            {
                while (b != 0)
                {
                    if (a > b)
                    {
                        a -= b;
                    }
                    else
                    {
                        b -= a;
                    }
                }
                return a;
            }
        }
        //********************************************
        public int Compare(Rational r1, Rational r2)
        {
            if (r1 != null && r2 != null)
            {
                long a, b;
                checked
                {
                    a = r1.Numerator * r2.Denominator;
                    b = r1.Denominator * r2.Numerator;
                }
                return a.CompareTo(b);
            }
            else
            {
                throw new ArgumentException("Объект не рациональное число");
            }
        }
        public int CompareTo(Rational obj)
        {
            return this.Compare(this, obj);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Rational objAsRN = obj as Rational;
            if (objAsRN == null)
            {
                return false;
            }
            else
            {
                return Equals(objAsRN);
            }
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Numerator / Denominator);
        }
        public bool Equals(Rational obj)
        {
            if (obj == null)
            {
                return false;
            }
            return Numerator * obj.Denominator == obj.Numerator * Denominator;
        }
        public override string ToString()
        {
            return ToString("incorrect");
        }
        public string ToString(string type)
        {
            switch (type)
            {
                case "incorrect":                                         //неПравильная дробь
                    {
                        if (Denominator != 1 && Numerator != 0)
                        {
                            return Numerator + "/" + Denominator;
                        }
                        else
                        {
                            return Numerator.ToString();
                        }
                    }
                case "correct":                                           //правильная дробю
                    {
                        if (Denominator != 1 && Numerator != 0)
                        {
                            if (Math.Abs(Numerator / Denominator) >= 1)
                            {
                                return Numerator / Denominator + "+" + Numerator % Denominator + "/" + Denominator;
                            }
                            else
                            {
                                return Numerator + "/" + Denominator;
                            }
                        }
                        else
                        {
                            return Numerator.ToString();
                        }
                    }
                case "long":                                           // в long
                    {
                        if (Denominator != 1 && Numerator != 0)
                        {
                            return (Numerator / Denominator).ToString();
                        }
                        else
                        {
                            return Numerator.ToString();
                        }
                    }
                case "float":                                           //float
                    {
                        if (Denominator != 1 && Numerator != 0)
                        {
                            return (Numerator / (float)Denominator).ToString();
                        }
                        else
                        {
                            return Numerator.ToString();
                        }
                    }
                case "double":                                           //double
                    {
                        if (Denominator != 1 && Numerator != 0)
                        {
                            return (Numerator / (double)Denominator).ToString();
                        }
                        else
                        {
                            return Numerator.ToString();
                        }
                    }
                case "decimal":
                    {
                        if (Denominator != 1 && Numerator != 0)
                        {
                            return (Numerator / (decimal)Denominator).ToString();
                        }
                        else
                        {
                            return Numerator.ToString();
                        }
                    }
                default:
                    {
                        return this.ToString("correct");

                    }
            }
        }
        public static explicit operator long(Rational a)
        {
            checked
            {
                long ans = a.Numerator / a.Denominator;
                if ((Math.Abs(a.Numerator) % a.Denominator) * 2 >= a.Denominator)
                {
                    ans += a.Numerator / Math.Abs(a.Numerator);
                }
                return ans;
            }
        }
        public static explicit operator double( Rational a)
        {
            return ((double)a.Numerator) / a.Denominator;
        }
        public static explicit operator decimal(Rational a)
        {
            return ((decimal)a.Numerator) / a.Denominator;
        }
    }
}
