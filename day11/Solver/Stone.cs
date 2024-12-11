namespace AoC;

public record class Stone(long Number, int Generation = 0, bool IsPlaceHolder = false)
{
    public Stone(long Number) : this(Number, 0) { }

    public Stone AsPlaceholder() => IsPlaceHolder ? this : new(Number, Generation, true);

    public List<Stone> Blink()
    {
        if (IsPlaceHolder)
        {
            return [new(Number, Generation + 1, true)];
        }

        if (Number == 0)
        {
            return [new(1)];
        }

        int numberNrDigits = $"{Number}".Length;
        if (numberNrDigits % 2 == 0)
        {
            long firstNumber = long.Parse($"{Number}"[..(numberNrDigits / 2)]);
            long secondNumber = long.Parse($"{Number}"[(numberNrDigits / 2)..]);
            return [new(firstNumber), new(secondNumber)];
        }

        return [new(Number * 2024)];
    }
}