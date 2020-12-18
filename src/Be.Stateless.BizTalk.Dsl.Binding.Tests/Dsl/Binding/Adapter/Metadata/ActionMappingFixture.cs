﻿#region Copyright & License

// Copyright © 2012 - 2020 François Chabot
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;
using static Be.Stateless.Unit.DelegateFactory;

namespace Be.Stateless.BizTalk.Dsl.Binding.Adapter.Metadata
{
	public class ActionMappingFixture
	{
		[Fact]
		[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
		public void OperationCanBeMappedToNoAction()
		{
			Action(() => new ActionMapping { new ActionMappingOperation("UpdateTicket") }).Should().NotThrow();
		}

		[Fact]
		[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
		public void OperationCanBeMappedToSomeAction()
		{
			Action(() => new ActionMapping { new ActionMappingOperation("UpdateTicket", "http://Microsoft.LobServices.OracleDB/2007/03/SCOTT/Procedure/UPDATE_TICKET") })
				.Should().NotThrow();
		}

		[Fact]
		[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
		public void OperationHasToMapToNonEmptyAction()
		{
			Action(() => new ActionMapping { new ActionMappingOperation("UpdateTicket", null) })
				.Should().Throw<ArgumentNullException>()
				.Which.ParamName.Should().Be("action");
			Action(() => new ActionMapping { new ActionMappingOperation("UpdateTicket", "") })
				.Should().Throw<ArgumentNullException>()
				.Which.ParamName.Should().Be("action");
			Action(() => new ActionMapping { new ActionMappingOperation("UpdateTicket", "  ") })
				.Should().Throw<ArgumentNullException>()
				.Which.ParamName.Should().Be("action");
		}

		[Fact]
		[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
		public void OperationNameCannotBeEmpty()
		{
			Action(() => new ActionMapping { new ActionMappingOperation(null) })
				.Should().Throw<ArgumentNullException>()
				.Which.ParamName.Should().Be("name");
			Action(() => new ActionMapping { new ActionMappingOperation("") })
				.Should().Throw<ArgumentNullException>()
				.Which.ParamName.Should().Be("name");
			Action(() => new ActionMapping { new ActionMappingOperation("  ") })
				.Should().Throw<ArgumentNullException>()
				.Which.ParamName.Should().Be("name");
		}

		[Fact]
		public void SerializeToXmlString()
		{
			var actionMapping = new ActionMapping {
				new ActionMappingOperation("CreateTicket"),
				new ActionMappingOperation("UpdateTicket", "http://Microsoft.LobServices.OracleDB/2007/03/SCOTT/Procedure/UPDATE_TICKET")
			};

			((string) actionMapping).Should().Be(
				"<BtsActionMapping>"
				+ "<Operation Name=\"CreateTicket\" />"
				+ "<Operation Name=\"UpdateTicket\" Action=\"http://Microsoft.LobServices.OracleDB/2007/03/SCOTT/Procedure/UPDATE_TICKET\" />"
				+ "</BtsActionMapping>");
		}
	}
}
