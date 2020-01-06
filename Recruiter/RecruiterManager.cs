using System;
using System.Collections.Generic;
using System.Linq;

namespace Recruiter
{
	internal class RecruiterManager
	{
		private IRecruitPool recruitPool;

		public RecruiterManager(IRecruitPool recruitPool)
		{
			this.recruitPool = recruitPool ?? throw new ArgumentNullException("recruitPool");
		}

		internal Employee RecruitEmployee(Guid recruitId) => GetNewRecruits()
				.Where(r => r.Id == recruitId)
				.Select(r => new Employee {
					Id = r.Id,
					FirstName = r.FirstName,
					LastName = r.LastName
				}).SingleOrDefault();

		internal List<Recruit> GetNewRecruits() => recruitPool.GetNewRecruits() ?? new List<Recruit> { };
	}
}
