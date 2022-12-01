﻿using ACadSharp.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace ACadSharp.Tests.IO
{
	public class UserSamplesReader : IOTestsBase
	{
		public static TheoryData<string> UserDwgFilePaths { get; }

		public static TheoryData<string> UserDxfFiles { get; }

		static UserSamplesReader()
		{
			string path = Path.Combine(_samplesFolder, "local", "user_files");
			UserDwgFilePaths = new TheoryData<string>();
			UserDxfFiles = new TheoryData<string>();

			if (!Directory.Exists(path))
			{
				UserDwgFilePaths.Add(string.Empty);
				UserDxfFiles.Add(string.Empty);
				return;
			}

			foreach (string file in Directory.GetFiles(path, $"*.dwg"))
			{
				UserDwgFilePaths.Add(file);
			}

			foreach (string file in Directory.GetFiles(path, $"*.dxf"))
			{
				UserDxfFiles.Add(file);
			}

			if (!UserDwgFilePaths.Any())
			{
				UserDwgFilePaths.Add(string.Empty);
			}

			if (!UserDxfFiles.Any())
			{
				UserDxfFiles.Add(string.Empty);
			}
		}

		public UserSamplesReader(ITestOutputHelper output) : base(output)
		{
		}

		[Theory]
		[MemberData(nameof(UserDwgFilePaths))]
		public void ReadUserDwg(string test)
		{
			if (string.IsNullOrEmpty(test))
				return;

			CadDocument doc = DwgReader.Read(test, this.onNotification);
		}

		[Theory]
		[MemberData(nameof(UserDxfFiles))]
		public void ReadUserDxf(string test)
		{
			if (string.IsNullOrEmpty(test))
				return;

			CadDocument doc = DxfReader.Read(test, this.onNotification);
		}
	}
}
