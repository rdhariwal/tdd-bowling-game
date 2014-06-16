using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Bowling
{
	
	public class BowlingGame
	{
		private List<int> _rolls = new List<int>();
		public void Roll(int pins)
		{
			_rolls.Add(pins);
		}

		public int GetScore()
		{
			var score = 0;
			for(var i = 0; i < _rolls.Count; i +=2 )
			{
				if(i + 2 < _rolls.Count)
				{
					score += GetFrameScore(_rolls[i], _rolls[i + 1], _rolls[i + 2]);
				}
				else
				{
					score += GetFrameScore(_rolls[i], _rolls[i + 1],null);
				}
			}
			return score;
		}

		protected int GetFrameScore(int roll1, int roll2, int? roll3)
		{
			var framescore = roll1 + roll2;
			if (IsSpare(framescore) && roll3.HasValue)
			{
				return framescore + roll3.Value;
			}
			return framescore;
		}

		private static bool IsSpare(int framescore)
		{
			return framescore == 10;
		}
	}
}