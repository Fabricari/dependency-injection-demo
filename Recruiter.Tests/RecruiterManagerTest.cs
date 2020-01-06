using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Recruiter.Tests
{
	[TestClass]
	public class RecruiterManagerTest
	{
		[TestMethod]
		public void RecruiterManager_ShouldInstantiate()
		{
			//arrange
			var mockRecruitPool = new Mock<IRecruitPool>();

			//act
			var recruiterManager = new RecruiterManager(mockRecruitPool.Object);
			
			//assert
			Assert.IsNotNull(recruiterManager);
			Assert.IsInstanceOfType(recruiterManager, typeof(RecruiterManager));
		}

		[TestMethod]
		public void GetRecruits_ShouldRetrievePoolOfRecruits()
		{
			//arrange
			var mockRecruitPool = new Mock<IRecruitPool>();
			var recruiterManager = new RecruiterManager(mockRecruitPool.Object);

			//act
			List<Recruit> recruits = recruiterManager.GetNewRecruits();

			//assert
			Assert.IsNotNull(recruits);
		}

		[TestMethod]
		public void GetRecruits_ShouldRetrieveEquivilentPoolOfRecruits()
		{
			//arrange
			var expectedRecruits = new List<Recruit> { 
				new Recruit {
					Id = Guid.NewGuid(),
					LastName = "Harrison",
					FirstName = "Steven"
				},
				new Recruit {
					Id = Guid.NewGuid(),
					LastName = "Barker",
					FirstName = "Bob"
				}
			};

			var mockRecruitPool = new Mock<IRecruitPool>();
			mockRecruitPool.Setup(rp => rp.GetNewRecruits())
				.Returns(expectedRecruits);
			IRecruitPool recruitPool = mockRecruitPool.Object;

			var recruiterManager = new RecruiterManager(recruitPool);

			//act
			List<Recruit> recruits = recruiterManager.GetNewRecruits();

			//assert
			expectedRecruits.Should().BeEquivalentTo(recruits);
		}

		[TestMethod]
		public void RecruitEmployee_ShouldReturnExpectedEmployee_ByRecruitId()
		{
			//arrange
			var expectedRecruits = new List<Recruit> {
				new Recruit {
					Id = Guid.NewGuid(),
					LastName = "Harrison",
					FirstName = "Steven"
				}
			};
			var expectedRecruit = expectedRecruits.FirstOrDefault();
			var expectedEmployee = new Employee()
			{
				Id = expectedRecruit.Id,
				LastName = expectedRecruit.LastName,
				FirstName = expectedRecruit.FirstName
			};

			var mockRecruitPool = new Mock<IRecruitPool>();
			mockRecruitPool.Setup(rp => rp.GetNewRecruits())
				.Returns(expectedRecruits);

			var recruiterManager = new RecruiterManager(mockRecruitPool.Object);

			//act
			var employee = recruiterManager.RecruitEmployee(recruitId: expectedRecruit.Id);

			//assert
			expectedEmployee.Should().BeEquivalentTo(employee);
		}
	}
}
