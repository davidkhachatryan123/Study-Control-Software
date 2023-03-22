﻿using System;

namespace StudyControlSoftware_API.Dto.Auth
{
	public class AuthResponseDto
	{
		public bool IsAuthSuccessful { get; set; }
		public string? Token { get; set; }
		public string? ErrorMessage { get; set; }

		public string? Email { get; set; }
		public string? Role { get; set; }
	}
}

