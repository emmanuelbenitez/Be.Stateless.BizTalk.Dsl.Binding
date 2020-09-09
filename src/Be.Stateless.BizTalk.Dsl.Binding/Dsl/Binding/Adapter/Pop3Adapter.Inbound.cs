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
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.BizTalk.Adapter.Sftp;

namespace Be.Stateless.BizTalk.Dsl.Binding.Adapter
{
	public partial class Pop3Adapter
	{
		#region Nested Type: Inbound

		/// <summary>
		/// You use the Post Office Protocol 3 (POP3) adapter to retrieve data from a server that houses POP3 mailboxes into a
		/// server running Microsoft BizTalk Server by means of the POP3 protocol.
		/// </summary>
		/// <remarks>
		/// The POP3 receive adapter retrieves e-mail from a specified mailbox on a specified POP3 server. By default, the POP3
		/// receive adapter applies MIME processing to the e-mail messages that it downloads and submits these messages to
		/// BizTalk Server as multi-part BizTalk messages. The POP3 receive adapter can receive and process e-mail in the
		/// following formats:
		/// <list type="bullet">
		/// <item>Plain text</item>
		/// <item>MIME encoded</item>
		/// <item>MIME encrypted</item>
		/// <item>MIME encoded and signed</item>
		/// <item>MIME encrypted and signed</item>
		/// </list>
		/// <para>
		/// The POP3 receive adapter does not support batching.
		/// </para>
		/// <para>
		/// The following authentication methods are supported for use with the POP3 adapter:
		/// <list type="bullet">
		/// <item>Basic. The POP3 server uses user provided credentials for authentication. These credentials are sent in clear text.</item>
		/// <item>Digest (APOP). The POP3 server uses a digest string for authentication.</item>
		/// <item>Secure Password Authentication (SPA). The POP3 server uses current process credentials for authentication.</item>
		/// </list>
		/// </para>
		/// </remarks>
		/// <seealso href="https://docs.microsoft.com/en-us/biztalk/core/what-is-the-pop3-adapter">What Is the POP3 Adapter?</seealso>
		/// <seealso href="https://docs.microsoft.com/en-us/biztalk/core/pop3-adapter">POP3 Adapter</seealso>
		/// <seealso href="https://docs.microsoft.com/en-us/biztalk/core/how-to-configure-a-pop3-receive-handler">How to Configure a POP3 Receive Handler</seealso>
		/// <seealso href="https://docs.microsoft.com/en-us/biztalk/core/how-to-configure-a-pop3-receive-location">How to Configure a POP3 Receive Location</seealso>
		/// <seealso href="https://docs.microsoft.com/en-us/biztalk/core/pop3-adapter-property-schema-and-properties">POP3 Adapter Property Schema and Properties</seealso>
		[XmlRoot("Config")]
		public class Inbound : Pop3Adapter, IInboundAdapter
		{
			public Inbound()
			{
				ApplyMimeDecoding = true;
				Port = 0;
				ErrorThreshold = 10;
				PollingInterval = TimeSpan.FromMinutes(5);
			}

			public Inbound(Action<Inbound> adapterConfigurator) : this()
			{
				if (adapterConfigurator == null) throw new ArgumentNullException(nameof(adapterConfigurator));
				adapterConfigurator(this);
			}

			#region Base Class Member Overrides

			protected override string GetAddress()
			{
				return $"POP3://{MailServer}#{UserName}";
			}

			#endregion

			#region MIME

			/// <summary>
			/// Specify whether to apply MIME decoding to messages received by the POP3 adapter. MIME decoding is used to parse
			/// the incoming message and any attachments into a multi-part BizTalk message.
			/// </summary>
			/// <remarks>
			/// <para>
			/// If this value is set to <c>False</c> then the POP3 adapter will submit the complete e-mail body including message
			/// headers to BizTalk Server.
			/// </para>
			/// <para>
			/// It defaults to <c>True</c>.
			/// </para>
			/// </remarks>
			[XmlElement("applyMIME")]
			public bool ApplyMimeDecoding { get; set; }

			/// <summary>
			/// Specify the body part content type of the incoming e-mail message to submit to BizTalk Server. This is an optional
			/// setting.
			/// </summary>
			/// <seealso href="https://docs.microsoft.com/en-us/biztalk/core/processing-multi-part-messages-with-the-pop3-adapter#body-part-selection-algorithm-used-by-the-pop3-adapter">Body Part Selection Algorithm Used by the POP3 Adapter</seealso>
			[XmlElement("bodyPartContentType")]
			public string BodyPartContentType { get; set; }

			/// <summary>
			/// Specify the body part of the incoming e-mail message to submit to BizTalk Server.
			/// </summary>
			/// <remarks>
			/// It defaults to <c>0</c>.
			/// </remarks>
			[XmlElement("bodyPartIndex")]
			public int BodyPartIndex { get; set; }

			#endregion

			#region POP3 Server

			/// <summary>
			/// Specify the POP3 mail server that houses the mailbox that will be polled by the POP3 adapter.
			/// </summary>
			[XmlElement("mailServer")]
			public string MailServer { get; set; }

			/// <summary>
			/// Specify the port for the POP3 mail server.
			/// </summary>
			/// <remarks>
			/// <para>
			/// Valid values ranges from <c>0</c> to <c>65535</c> inclusive.
			/// </para>
			/// <para>
			/// It defaults to <c>0</c>.
			/// </para>
			/// <para>
			/// A value of <c>0</c> indicates to use the default POP3 port of <c>110</c> if <see cref="UseSsl"/> is <c>False</c>
			/// or port <c>995</c> if <see cref="UseSsl"/> is <c>True</c>.
			/// </para>
			/// </remarks>
			[XmlElement("serverPort")]
			public int Port { get; set; }

			#endregion

			#region Security

			/// <summary>
			/// Specify the type of authentication to use with the destination server.
			/// </summary>
			/// <remarks>
			/// Valid options are:
			/// <list type="bullet">
			/// <item>
			/// <term><see cref="Pop3Adapter.AuthenticationScheme.Basic"/></term>
			/// <description>Password will be sent to POP3 server in clear text.</description>
			/// </item>
			/// <item>
			/// <term><see cref="Pop3Adapter.AuthenticationScheme.Digest"/></term>
			/// <description>Password hash will be sent to POP3 server.</description>
			/// </item>
			/// <item>
			/// <term><see cref="Pop3Adapter.AuthenticationScheme.SecurePasswordAuthentication"/></term>
			/// <description>
			/// NTLM will be used for authentication. The username must be specified with one of the following formats:
			/// <list type="bullet">
			/// <item>
			/// Domain accounts must be entered with the syntax: <c><![CDATA[<domain name>\<username>]]></c>.
			/// </item>
			/// <item>
			/// Local accounts must be entered with the syntax: <c><![CDATA[<machine name>\<username>]]></c>.
			/// </item>
			/// </list>
			/// </description>
			/// </item>
			/// </list>
			/// </remarks>
			[XmlElement("authenticationScheme")]
#pragma warning disable 108
			public AuthenticationScheme AuthenticationScheme { get; set; }
#pragma warning disable 108

			/// <summary>
			/// Specify the user name to use for authentication with the POP3 server. This property requires a value.
			/// </summary>
			/// <remarks>
			/// The account specified for the <see cref="UserName"/> property must have the ability to logon to the network. The
			/// POP3 adapter connects to the mailbox associated with the account specified for the <see cref="UserName"/>
			/// property. Therefore it is not possible to use the POP3 Adapter to connect to a mailbox other than the mailbox
			/// assigned to the specified account. For example, even if multiple accounts have Read permissions to the mailbox
			/// associated with a particular account, only the actual account name can be specified for <see cref="UserName"/>.
			/// </remarks>
			[XmlElement("userName")]
			public string UserName { get; set; }

			/// <summary>
			/// Specify the user password to use for authentication with the POP3 server.
			/// </summary>
			[XmlElement("password")]
			public string Password { get; set; }

			/// <summary>
			/// Specify whether to use Secure Sockets Layer (SSL) to communicate with the destination server.
			/// </summary>
			/// <remarks>
			/// It defaults to <c>False</c>.
			/// </remarks>
			[XmlElement("sslRequired")]
			public bool UseSsl { get; set; }

			#endregion

			#region Tuning

			/// <summary>
			/// Specify the maximum number of network or protocol errors to wait before shutting down the adapter. Specify a value
			/// of 0 to prevent the adapter from shutting down.
			/// </summary>
			/// <remarks>
			/// It defaults to <c>10</c>.
			/// </remarks>
			[XmlElement("errorThreshold")]
			public uint ErrorThreshold { get; set; }

			/// <summary>
			/// Specify the interval between attempts to retrieve messages from the POP3 server.
			/// </summary>
			/// <remarks>
			/// It defaults to <c>5</c> minutes.
			/// </remarks>
			[XmlIgnore]
			public TimeSpan PollingInterval
			{
				get => BuildTimeSpan(XmlAliasedPollingInterval, XmlAliasedPollingUnitOfMeasure);
				set
				{
					UnbuildTimeSpan(
						value,
						(q, u) => {
							XmlAliasedPollingInterval = q;
							XmlAliasedPollingUnitOfMeasure = u;
						});
				}
			}

			[EditorBrowsable(EditorBrowsableState.Never)]
			[XmlElement("pollingInterval")]
			public int XmlAliasedPollingInterval { get; set; }

			[EditorBrowsable(EditorBrowsableState.Never)]
			[XmlElement("pollingUnitOfMeasure")]
			public PollingIntervalUnit XmlAliasedPollingUnitOfMeasure { get; set; }

			#endregion
		}

		#endregion
	}
}
