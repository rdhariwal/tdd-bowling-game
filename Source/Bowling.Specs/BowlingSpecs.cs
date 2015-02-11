using Bowling;
using Bowling.Specs.Infrastructure;

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
		public void the_score_should_be_0()
		{
			_score.ShouldEqual(0);
		}

	}

	public class when_rolling_all_twos : concerns
	{
		private int _score;

		protected override void context()
		{
			var game = new BowlingGame();
			20.times(() => game.Roll(2));
			_score = game.Score;
		}

		[Specification]
		public void the_score_should_be_40()
		{
			_score.ShouldEqual(40);
		}

	}

	public class when_rolling_two_twos_and_rest_threes : concerns
	{
		private int _score;

		protected override void context()
		{
			var game = new BowlingGame();
			2.times(() => game.Roll(2));
			18.times(() => game.Roll(3));
			_score = game.Score;
		}

		[Specification]
		public void the_score_should_be_58()
		{
			_score.ShouldEqual(58);
		}

	}
}
