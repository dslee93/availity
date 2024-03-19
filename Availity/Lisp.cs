using System;
namespace Availity
{
	public static class Lisp
	{
		public static bool ValidateParentheses(this string lisp)
		{
			//Keep parenthesis count throughout the lisp
			int leftParCount = 0;
			int rightParCount = 0;

			//Loops through the lisp string and checks each char for parenthesis
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
			//Checks the counts of all the parenthesis and compares the count and returns true or false
			return leftParCount == rightParCount;
		}
	}
}

