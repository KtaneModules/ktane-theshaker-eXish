using System.Collections.Generic;
using System.Linq;

public static class Extensions
{
	public static int GetDigit(this int x, int digit)
	{
		return int.Parse(x.ToString().Reverse().ElementAt(digit).ToString());
	}

	public static int Product(this IEnumerable<int> list)
	{
		int num = 1;
		foreach (int num2 in list)
		{
			num *= num2 + 10;
		}
		return num;
	}
}
