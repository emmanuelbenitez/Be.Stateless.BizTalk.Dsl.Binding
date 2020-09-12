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

namespace Be.Stateless.BizTalk.Dsl.Binding
{
	[SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Public DSL API.")]
	public class ReceivePort : ReceivePortBase<string>
	{
		protected ReceivePort() { }

		protected internal ReceivePort(Action<IReceivePort<string>> receivePortConfigurator) : base(receivePortConfigurator) { }

		protected IReceiveLocation<string> ReceiveLocation(Action<IReceiveLocation<string>> receiveLocationConfigurator)
		{
			var receiveLocation = new ReceiveLocation(receiveLocationConfigurator);
			return receiveLocation;
		}
	}
}