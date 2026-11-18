using System;
using System.Collections.Generic;
using System.Globalization;

namespace TuringTrainer.LeaderBoard;

public class LeaderboardChoice
{
	public Guid Id { get; set; }

	public string Name { get; set; } = "";

	public Guid? ClassId { get; set; }

	public Guid? GroupId { get; set; }
}

public class LeaderboardOptions
{
	public List<LeaderboardChoice> Classes { get; set; } = new();

	public List<LeaderboardChoice> Groups { get; set; } = new();

	public List<LeaderboardChoice> Exercises { get; set; } = new();
}

public class LeaderboardRow
{
	public Guid AthleteId { get; set; }

	public string Name { get; set; } = "";

	public decimal? CurrentWeightLb { get; set; }

	public DateTimeOffset? WeightRecordedAt { get; set; }

	public double? PrWeightLb { get; set; }

	public decimal? PrBodyWeightLb { get; set; }

	public double? Ratio { get; set; }

	public int? Rank { get; set; }

	public string RankText => Rank?.ToString() ?? "—";

	public string RatioText => Ratio.HasValue
		? Ratio.Value.ToString(
			"0.00",
			CultureInfo.CurrentCulture) + "×"
		: "—";

	public string PrText => PrWeightLb is > 0
		? $"PR: {PrWeightLb.Value:0.##} lb"
		: "No PR recorded";

	public string PrBodyWeightText => PrBodyWeightLb.HasValue
		? $"Body weight at PR: {PrBodyWeightLb.Value:0.###} lb"
		: "Body weight at PR: not recorded";

	public string CurrentWeightText => CurrentWeightLb.HasValue
		? $"Current body weight: {CurrentWeightLb.Value:0.###} lb"
		: "Current body weight: not recorded";

	public string RecordedText => WeightRecordedAt.HasValue
		? $"Weight recorded {WeightRecordedAt.Value.ToLocalTime():MMM d, yyyy}"
		: "";
}