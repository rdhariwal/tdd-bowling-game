using System;
using System.Collections.Generic;
using Bowling;
using Bowling.Specs.Infrastructure;
using NUnit.Framework;

namespace specs_for_bowling
{
	public class when_everything_is_wired_up : concerns
	{
		private bool _itWorked;

		protected override void context()
		{
			_itWorked = true;
		}

		[Specification]
		public void it_works()
		{
			_itWorked.ShouldBeTrue();
		}
	}

	public class when_rolling_all_gutter_balls : concerns
	{
		private Game _game;
		protected override void context()
		{
			_game = new Game();
			for (int i = 0; i < 20; i++)
			{
				_game.Roll(0);
			}
		}

		[Specification]
		public void the_score_is_0()
		{
			_game.GetScore().ShouldEqual(0);
		}
	}

	public class when_rolling_all_twos : concerns
	{
		private Game _game;
		protected override void context()
		{
			_game = new Game();
			for (int i = 0; i < 20; i++)
			{
				_game.Roll(2);
			}
		}

		[Specification]
		public void the_score_is_40()
		{
			_game.GetScore().ShouldEqual(40);
		}
	}

	public class when_rolling_two_twos_followed_by_all_threes : concerns
	{
		private Game _game;
		protected override void context()
		{
			_game = new Game();
			_game.Roll(2);
			_game.Roll(2);
			18.times(() => _game.Roll(3));
		}

		[Specification]
		public void the_score_is_58()
		{
			_game.GetScore().ShouldEqual(58);
		}
	}

	public class when_rolling_alternating_twos_and_fives : concerns
	{
		private Game _game;
		protected override void context()
		{
			_game = new Game();
			10.times(() =>
			{
				_game.Roll(2);
				_game.Roll(5);
			});
		}

		[Specification]
		public void the_score_is_70()
		{
			_game.GetScore().ShouldEqual(70);
		}
	}

	public class when_rolling_a_spare_followed_by_all_twos : concerns
	{
		private Game _game;
		protected override void context()
		{
			_game = new Game();
			_game.Roll(1);
			_game.Roll(9);
			18.times(() => _game.Roll(2));
		}

		[Specification]
		public void the_score_is_48()
		{
			_game.GetScore().ShouldEqual(48);
		}
	}

	public class when_rolling_two_twoeight_spares_followd_by_all_twos : concerns
	{
		private Game _game;
		protected override void context()
		{
			_game = new Game();
			2.times(() =>
			{
				_game.Roll(2);
				_game.Roll(8);
			});
			16.times(() => _game.Roll(2));
		}

		[Specification]
		public void the_score_is_56()
		{
			_game.GetScore().ShouldEqual(56);
		}
	}

	public class rolling_more_than_ten_frames : concerns
	{
		private Game _game;
		protected override void context()
		{
			_game = new Game();
			20.times(() => _game.Roll(0));
		}

		[Specification]
		[ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "Game Over")]
		public void is_not_allowed()
		{
			_game.Roll(0);
		}
	}

	public class when_first_frame_is_strike_followed_by_all_twos : concerns
	{
		private Game _game;
		protected override void context()
		{
			_game = new Game();
			_game.Roll(10);
			18.times(() => _game.Roll(2));
		}

		[Specification]
		public void the_score_is_50()
		{
			_game.GetScore().ShouldEqual(50);
		}
	}

	public class when_first_two_frames_are_strikes_followed_by_all_twos : concerns
	{
		private Game _game;
		protected override void context()
		{
			_game = new Game();
			_game.Roll(10);
			_game.Roll(10);
			16.times(() => _game.Roll(2));
		}

		[Specification]
		public void the_score_is_68()
		{
			_game.GetScore().ShouldEqual(68);
		}
	}
	public class when_rolling_perfect_game : concerns
	{
		private Game _game;
		protected override void context()
		{
			_game = new Game();
			12.times(() => _game.Roll(10));
		}

		[Specification]
		public void the_score_is_300()
		{
			_game.GetScore().ShouldEqual(300);
		}
	}
}

namespace Bowling
{
	public class Game
	{
		private List<int> _rolls = new List<int>();

		public void Roll(int pins)
		{
			if (_rolls.Count >= 20)
			{
				throw new InvalidOperationException("Game Over");
			}

			_rolls.Add(pins);
		}

		public int GetScore()
		{
			var score = 0;
			int lastRoll = 0;
			var newFrame = true;
			var frameCount = 0;

			for (int i = 0; i < _rolls.Count; i++)
			{
				if (IsStrike(_rolls[i]))
				{
					if(frameCount < 10)
					{
						var frameScore = _rolls[i] + _rolls[i + 1] + _rolls[i + 2];
						score = score + frameScore;
						newFrame = true;
						frameCount++;
					}
				}
				else if (newFrame)
				{
					lastRoll = _rolls[i];
					newFrame = false;
				} else 
				{
					var frameScore = lastRoll + _rolls[i];
					if (IsSpare(lastRoll, _rolls[i]))
					{
						score = score + _rolls[i+1];
					}
					newFrame = true;
					frameCount++;
					score = score + frameScore;
				}
			}

			return score;
		}

		private bool IsStrike(int rolls)
		{
			return rolls == 10;
		}

		private bool IsSpare(int roll1, int roll2)
		{
			return roll1 + roll2 == 10;
		}
	}
}