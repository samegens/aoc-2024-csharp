namespace AoC;

public class Mul(int op1, int op2)
{
    public int Op1 => op1;
    public int Op2 => op2;
    public int Execute() => op1 * op2;

    // Implement Equals so unit tests can use Is.EquivalentTo.
    public override bool Equals(object? other)
    {
        if (other == null || GetType() != other.GetType())
        {
            return false;
        }

        Mul otherMul = (Mul)other;
        return otherMul.Op1 == op1 && otherMul.Op2 == op2;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(op1, op2);
    }

    public override string ToString()
    {
        return $"Mul({op1},{op2})";
    }
}
