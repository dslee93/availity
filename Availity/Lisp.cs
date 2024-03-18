using System;
namespace Availity
{
	public static class Lisp
	{
		public static bool ValidateParentheses(this string lisp)
		{
			int leftParCount = 0;
			int rightParCount = 0;

			foreach(var letter in lisp)
			{
				if (letter == '(')
				{
					leftParCount++;
				}
				if (letter == ')')
				{
					rightParCount++;
				}
			}
			return leftParCount == rightParCount;
		}
	}
}

