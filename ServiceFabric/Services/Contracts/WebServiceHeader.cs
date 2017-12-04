using System;
using System.Web.Services.Protocols;

using NBK.Common.Foundations.Utilities;
using System.Runtime.Serialization;

namespace Common
{
	/// <summary>
	/// Header required by NBK Web Services, contains authentication information, and
	/// some useful IT data
	/// </summary>
    [DataContract]
	public class WebServiceHeader
	{
		#region Properties

		/// <summary>
		/// The store that holds the credentials provided for the system
		/// </summary>
        [DataMember]
		public string SystemCredentialStore;

		/// <summary>
		/// System id to validate
		/// </summary>
        [DataMember]
        public string SystemId;

		/// <summary>
		/// The store that holds the credentials provided for the user
		/// </summary>
        [DataMember]
        public string UserCredentialStore;

		/// <summary>
		/// User id to validate
		/// </summary>
        [DataMember]
        public string UserId;

		/// <summary>
		/// Security Context Token that denotes the current call
		/// </summary>
        [DataMember]
        public string Identifier;

		/// <summary>
		/// Different IT related data provided from the Caller
		/// </summary>
        //[DataMember]
        //public String2String ITData;

		#endregion Properties

	}
}
