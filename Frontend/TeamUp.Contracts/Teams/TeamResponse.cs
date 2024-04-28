namespace TeamUp.Contracts.Teams;

public sealed class TeamResponse
{
	public required string Name { get; set; }
	public required IReadOnlyList<TeamMemberResponse> Members { get; set; }
}
