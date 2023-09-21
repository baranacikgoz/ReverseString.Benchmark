using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

namespace ReverseString;

[MemoryDiagnoser, HideColumns("Error", "RatioSD", "StdDev", "Gen0")]
public class Benchmark
{
    private const string _input = "the quick brown fox jumps over the lazy dog";

    [Benchmark(Baseline = true)]
    public string ReverseUsingSpan()
    {
        return string.Create(_input.Length, _input, (span, input) =>
        {
            for (int i = 0; i < input.Length; i++)
            {
                span[i] = input[input.Length - i - 1];
            }
        });
    }

    [Benchmark]
    public string ReverseUsingCharBuffer()
    {
        var buffer = new char[_input.Length];
        for (int i = _input.Length - 1; i >= 0; i--)
        {
            buffer[_input.Length - i - 1] = _input[i];
        }
        return new string(buffer);
    }

    [Benchmark]
    public string ReverseUsingStringBuilder()
    {
        var sb = new StringBuilder(_input.Length);
        for (int i = _input.Length - 1; i >= 0; i--)
        {
            sb.Append(_input[i]);
        }
        return sb.ToString();
    }

    [Benchmark]
    public string ReverseUsingStack()
    {
        var stack = new Stack<char>(_input.Length);
        foreach (var c in _input)
        {
            stack.Push(c);
        }
        return new string(stack.ToArray());
    }

    [Benchmark]
    public string ReverseUsingLinq()
    {
        return new string(_input.Reverse().ToArray());
    }
}
