using Bowling.Specs.Infrastructure;
using Bowling;

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
		private int _score;

		protected override void context()
		{
			var game = new BowlingGame();
			20.times(() => game.Roll(0));
			_score = game.Score;
		}

		[Specification]
		public void the_score_is_0()
		{
			_score.ShouldEqual(0);
		}
	}

	public class when_rolling_all_twos : concerns
	{
		private int _score;

		protected override void context()
		{
			var _game = new BowlingGame();
			20.times(() => _game.Roll(2));
			_score = _game.Score;
		}

		[Specification]
		public void the_score_is_40()
		{
			_score.ShouldEqual(40);
		}
	}

	public class when_the_first_2_rolls_are_2_and_the_rest_are_3 : concerns
	{
		private int _score;

		protected override void context()
		{
			var _game = new BowlingGame();
			2.times(() => _game.Roll(2));
			18.times(() => _game.Roll(3));
			_score = _game.Score;
		}

		[Specification]
		public void the_score_is_58()
		{
			_score.ShouldEqual(58);
		}
	}

	public class when_rolling_alternating_2s_and_5s : concerns
	{
		private int _score;

		protected override void context()
		{
			var _game = new BowlingGame();
			
			10.times(() =>
			{
				_game.Roll(2);
				_game.Roll(5);
			});

			_score = _game.Score;
		}

		[Specification]
		public void the_score_is_70()
		{
			_score.ShouldEqual(70);
		}
	}

	public class when_the_first_frame_is_a_spare_and_the_remaining_rolls_are_all_2 : concerns
	{
		private int _score;

		protected override void context()
		{
			var _game = new BowlingGame();

			2.times(() => _game.Roll(5));
			18.times(() => _game.Roll(2));

			_score = _game.Score;
		}

		[Specification]
		public void the_score_is_48()
		{
			_score.ShouldEqual(48);
		}
	}

	public class when_the_first_2_frames_are_spare_as_2_8_and_the_rest_score_2 : concerns
	{
		private int _score;

		protected override void context()
		{
			var _game = new BowlingGame();

			2.times(() =>
			{
				_game.Roll(2);
				_game.Roll(8);
			});
			16.times(() => _game.Roll(2));

			_score = _game.Score;
		}

		[Specification]
		public void the_score_is_56()
		{
			_score.ShouldEqual(56);
		}
	}

	public class when_10_frames_have_been_bowled_dont_allow_any_more_to_be_bowled : concerns
	{
		private bool _moreRollsAvailable;

		protected override void context()
		{
			var _game = new BowlingGame();
			20.times(() => _game.Roll(0));
			_moreRollsAvailable = _game.MoreRollsAvailable;
		}

		[Specification]
		public void additional_rolls_should_not_be_allowed()
		{
			_moreRollsAvailable.ShouldBeFalse();
		}
	}

	public class when_less_than_10_frames_have_been_bowled_allow_more_to_be_bowled : concerns
	{
		private bool _moreRollsAvailable;

		protected override void context()
		{
			var _game = new BowlingGame();
			_moreRollsAvailable = _game.MoreRollsAvailable;
		}

		[Specification]
		public void additional_rolls_should_be_allowed()
		{
			_moreRollsAvailable.ShouldBeTrue();
		}
	}

	public class when_the_first_frame_is_a_strike_and_the_rest_score_2 : concerns
	{
		private int _score;

		protected override void context()
		{
			var _game = new BowlingGame();

			_game.Roll(10);
			18.times(() => _game.Roll(2));

			_score = _game.Score;
		}

		[Specification]
		public void the_score_is_50()
		{
			_score.ShouldEqual(50);
		}
	}

	public class when_the_first_2_frames_are_strikes_and_the_rest_score_2 : concerns
	{
		private int _score;

		protected override void context()
		{
			var _game = new BowlingGame();

			2.times(() => _game.Roll(10));
			16.times(() => _game.Roll(2));

			_score = _game.Score;
		}

		[Specification]
		public void the_score_is_68()
		{
			_score.ShouldEqual(68);
		}
	}
}