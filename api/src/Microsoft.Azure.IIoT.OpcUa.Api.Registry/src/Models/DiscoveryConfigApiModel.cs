// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Api.Registry.Models {
    using System.Runtime.Serialization;
    using System.Collections.Generic;
    using System;

    /// <summary>
    /// Discovery configuration api model
    /// </summary>
    [DataContract]
    public class DiscoveryConfigApiModel {

        /// <summary>
        /// Address ranges to scan (null == all wired nics)
        /// </summary>
        [DataMember(Name = "addressRangesToScan",
            EmitDefaultValue = false)]
        public string AddressRangesToScan { get; set; }

        /// <summary>
        /// Network probe timeout
        /// </summary>
        [IgnoreDataMember]
        [Obsolete("Use NetworkProbeTimeout")]
        public int? NetworkProbeTimeoutMs {
            get => (int?)NetworkProbeTimeout?.TotalMilliseconds;
            set => NetworkProbeTimeout = value != null ?
                TimeSpan.FromMilliseconds(value.Value) : (TimeSpan?)null;
        }

        /// <summary>
        /// Network probe timeout
        /// </summary>
        [DataMember(Name = "networkProbeTimeout",
            EmitDefaultValue = false)]
        public TimeSpan? NetworkProbeTimeout { get; set; }

        /// <summary>
        /// Max network probes that should ever run.
        /// </summary>
        [DataMember(Name = "maxNetworkProbes",
            EmitDefaultValue = false)]
        public int? MaxNetworkProbes { get; set; }

        /// <summary>
        /// Port ranges to scan (null == all unassigned)
        /// </summary>
        [DataMember(Name = "portRangesToScan",
            EmitDefaultValue = false)]
        public string PortRangesToScan { get; set; }

        /// <summary>
        /// Port probe timeout
        /// </summary>
        [IgnoreDataMember]
        [Obsolete("Use PortProbeTimeout")]
        public int? PortProbeTimeoutMs {
            get => (int?)PortProbeTimeout?.TotalMilliseconds;
            set => PortProbeTimeout = value != null ?
                TimeSpan.FromMilliseconds(value.Value) : (TimeSpan?)null;
        }

        /// <summary>
        /// Port probe timeout
        /// </summary>
        [DataMember(Name = "portProbeTimeout",
            EmitDefaultValue = false)]
        public TimeSpan? PortProbeTimeout { get; set; }

        /// <summary>
        /// Max port probes that should ever run.
        /// </summary>
        [DataMember(Name = "maxPortProbes",
            EmitDefaultValue = false)]
        public int? MaxPortProbes { get; set; }

        /// <summary>
        /// Probes that must always be there as percent of max.
        /// </summary>
        [DataMember(Name = "minPortProbesPercent",
            EmitDefaultValue = false)]
        public int? MinPortProbesPercent { get; set; }

        /// <summary>
        /// Delay time between discovery sweeps in seconds
        /// </summary>
        [IgnoreDataMember]
        [Obsolete("Use IdleTimeBetweenScans")]
        public int? IdleTimeBetweenScansSec {
            get => (int?)IdleTimeBetweenScans?.TotalSeconds;
            set => IdleTimeBetweenScans = value != null ?
                TimeSpan.FromSeconds(value.Value) : (TimeSpan?)null;
        }

        /// <summary>
        /// Delay time between discovery sweeps
        /// </summary>
        [DataMember(Name = "idleTimeBetweenScans",
            EmitDefaultValue = false)]
        public TimeSpan? IdleTimeBetweenScans { get; set; }

        /// <summary>
        /// List of preset discovery urls to use
        /// </summary>
        [DataMember(Name = "discoveryUrls",
            EmitDefaultValue = false)]
        public List<string> DiscoveryUrls { get; set; }

        /// <summary>
        /// List of locales to filter with during discovery
        /// </summary>
        [DataMember(Name = "locales",
            EmitDefaultValue = false)]
        public List<string> Locales { get; set; }

        /// <summary>
        /// Activate all twins with this filter during onboarding.
        /// </summary>
        [DataMember(Name = "activationFilter",
            EmitDefaultValue = false)]
        public EndpointActivationFilterApiModel ActivationFilter { get; set; }
    }
}
