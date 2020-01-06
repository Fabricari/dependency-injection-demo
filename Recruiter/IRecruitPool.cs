using System.Collections.Generic;

namespace Recruiter
{
	public interface IRecruitPool
	{
		List<Recruit> GetNewRecruits();
	}
}
