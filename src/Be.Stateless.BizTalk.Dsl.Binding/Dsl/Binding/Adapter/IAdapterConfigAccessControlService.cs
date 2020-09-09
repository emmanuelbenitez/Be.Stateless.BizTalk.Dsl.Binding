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
using Microsoft.ServiceBus;

namespace Be.Stateless.BizTalk.Dsl.Binding.Adapter
{
	[SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Public DSL API.")]
	[SuppressMessage("ReSharper", "UnusedMemberInSuper.Global", Justification = "Public DSL API.")]
	public interface IAdapterConfigAccessControlService
	{
		/// <summary>
		/// Specify the issuer name.
		/// </summary>
		/// <remarks>
		/// Typically this is set to owner.
		/// </remarks>
		string IssuerName { get; set; }

		/// <summary>
		/// Specify the issuer key.
		/// </summary>
		string IssuerSecret { get; set; }

		/// <summary>
		/// Specify the Service Access Control Service STS URI.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Set this to <c><![CDATA[https://<namespace>-sb.accesscontrol.windows.net/]]></c>, where
		/// <![CDATA[<namespace>]]> is your Service Bus namespace.
		/// </para>
		/// <para>
		/// Required if <see cref="RelayClientAuthenticationType.RelayAccessToken"/> is set to <see
		/// cref="RelayClientAuthenticationType"/>.
		/// </para>
		/// </remarks>
		Uri StsUri { get; set; }
	}
}
