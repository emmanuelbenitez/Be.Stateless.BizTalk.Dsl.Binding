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

using System.Diagnostics.CodeAnalysis;
using Be.Stateless.BizTalk.Dsl.Binding.Adapter;

namespace Be.Stateless.BizTalk.Dsl.Binding
{
	public sealed class ReceiveLocationTransport : TransportBase<IInboundAdapter>
	{
		#region Nested Type: UnknownInboundAdapter

		private class UnknownInboundAdapter : UnknownAdapter, IInboundAdapter
		{
			public static readonly IInboundAdapter Instance = new UnknownInboundAdapter();
		}

		#endregion

		public ReceiveLocationTransport()
		{
			Adapter = UnknownInboundAdapter.Instance;
			Schedule = Schedule.None;
		}

		#region Base Class Member Overrides

		[SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
		protected override void ApplyEnvironmentOverrides(string environment)
		{
			(Schedule as ISupportEnvironmentOverride)?.ApplyEnvironmentOverrides(environment);
		}

		#endregion

		/// <summary>
		/// <see cref="Schedule"/> restricts receiving messages to specific dates.
		/// </summary>
		public Schedule Schedule { get; set; }
	}
}
