using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Supabase;
using TuringTrainer.Services;

namespace TuringTrainer.LeaderBoard;

public class LeaderboardService
{
	private readonly Client client = Database.getClient();

	public async Task<LeaderboardOptions> GetOptionsAsync()
	{
		RequireSession();

		var result = await client.Rpc<OptionsResult>("coach_lb_options", new { });

		if (result == null)
		{
			throw new InvalidOperationException("No filter response was returned.");
		}

		return new LeaderboardOptions
		{
			Classes = result.classes
				.Select(ToChoice)
				.ToList(),

			Groups = result.groups
				.Select(ToChoice)
				.ToList(),

			Exercises = result.exercises
				.Select(ToChoice)
				.ToList()
		};
	}

	public async Task<List<LeaderboardRow>> GetRowsAsync(
		Guid classId,
		Guid groupId,
		Guid exerciseId)
	{
		RequireSession();

		var result = await client.Rpc<List<RowResult>>(
			"coach_lb_rows",
			new
			{
				p_class_id = classId,
				p_group_id = groupId,
				p_exercise_id = exerciseId
			});

		if (result == null)
		{
			throw new InvalidOperationException("No athlete response was returned.");
		}

		return result.Select(row => new LeaderboardRow
		{
			AthleteId = row.athlete_id,
			Name = row.athlete_name,
			CurrentWeightLb = row.current_weight_lb,
			WeightRecordedAt = row.weight_recorded_at,
			PrWeightLb = row.pr_weight_lb,
			PrBodyWeightLb = row.pr_body_weight_lb,
			Ratio = row.ratio
		}).ToList();
	}

	public async Task SaveAsync(
		Guid classId,
		Guid groupId,
		Guid athleteId,
		Guid exerciseId,
		string action,
		decimal value,
		decimal? prBodyWeight = null)
	{
		RequireSession();

		if (action != "current_weight" &&
			action != "new_pr" &&
			action != "pr_body_weight")
		{
			throw new ArgumentException("Unknown save action.", nameof(action));
		}

		decimal maximum = action == "new_pr"
			? 10000m
			: 1500m;

		if (value <= 0 || value > maximum)
		{
			throw new ArgumentOutOfRangeException(nameof(value),$"Weight must be greater than 0 and no more than {maximum} lb.");
		}

		if (action == "new_pr" &&
			(!prBodyWeight.HasValue ||
			 prBodyWeight.Value <= 0 ||
			 prBodyWeight.Value > 1500))
		{
			throw new ArgumentException("Enter body weight at the time of the PR.", nameof(prBodyWeight));
		}

		await client.Rpc(
			"coach_lb_save",
			new
			{
				p_class_id = classId,
				p_group_id = groupId,
				p_athlete_id = athleteId,
				p_exercise_id = exerciseId,
				p_action = action,
				p_value = value,
				p_pr_body_weight = prBodyWeight
			});
	}

	private void RequireSession()
	{
		if (client.Auth.CurrentSession == null)
		{
			throw new InvalidOperationException("Please sign in first.");
		}
	}

	private static LeaderboardChoice ToChoice(ChoiceResult item)
	{
		return new LeaderboardChoice
		{
			Id = item.id,
			Name = item.name,
			ClassId = item.class_id,
			GroupId = item.group_id
		};
	}

	public class OptionsResult
	{
		public List<ChoiceResult> classes { get; set; } = new();

		public List<ChoiceResult> groups { get; set; } = new();

		public List<ChoiceResult> exercises { get; set; } = new();
	}

	public class ChoiceResult
	{
		public Guid id { get; set; }

		public string name { get; set; } = "";

		public Guid? class_id { get; set; }

		public Guid? group_id { get; set; }
	}

	public class RowResult
	{
		public Guid athlete_id { get; set; }

		public string athlete_name { get; set; } = "";

		public decimal? current_weight_lb { get; set; }

		public DateTimeOffset? weight_recorded_at { get; set; }

		public double? pr_weight_lb { get; set; }

		public decimal? pr_body_weight_lb { get; set; }

		public double? ratio { get; set; }
	}
}