﻿using System;

namespace Recruiter
{
	public class Employee
	{
		public Guid Id { get; set; }
		public string LastName { get; internal set; }
		public string FirstName { get; internal set; }
	}
}
